# GraphQL Web API (.NET Core) — Geliştirilmiş README

Bu depo; **ASP.NET Core** ve **GraphQL.NET** ile geliştirilmiş bir Web API örneği ve **PostgreSQL** veritabanı ile çalışacak şekilde hazırlanmış **Docker Compose** dosyası içerir. Ayrıca örnek bir **React (Apollo Client)** entegrasyonu için hızlı başlangıç adımları sunar.

> Bu README, mevcut depodaki notları düzenleyip genişleterek hazırlanmıştır ve eksik/dağınık kısımları toparlar. Orijinal notlarda yer alan compose ve EF CLI yönergeleri kaynak alınmıştır.  

---

## İçindekiler
- [Mimari ve Teknolojiler](#mimari-ve-teknolojiler)
- [Ön Koşullar](#ön-koşullar)
- [Hızlı Kurulum (Docker ile)](#hızlı-kurulum-docker-ile)
- [Yerel Geliştirme (Docker olmadan)](#yerel-geliştirme-docker-olmadan)
- [EF Core Migration Akışı](#ef-core-migration-akışı)
- [GraphQL Uç Noktası ve Örnek İstekler](#graphql-uç-noktası-ve-örnek-istekler)
- [React İstemci (Apollo) Hızlı Başlangıç](#react-istemci-apollo-hızlı-başlangıç)
- [Sık Karşılaşılan Komutlar](#sık-karşılaşılan-komutlar)
- [Lisans](#lisans)

---

## Mimari ve Teknolojiler

- **Backend:** ASP.NET Core Web API (C#)
- **GraphQL Sunucu:** GraphQL for .NET + ASP.NET Core middleware
- **Veritabanı:** PostgreSQL (Docker üzerinden hızlı başlatma)
- **ORM:** Entity Framework Core (Code First)
- **İstemci (opsiyonel):** React + Apollo Client

> ASP.NET Core için GraphQL middleware hakkında bilgi için: GraphQL .NET Server paketi (docs) kullanılabilir.  

---

## Ön Koşullar

- **.NET SDK**: 3.1+ (proje 3.1 EF CLI referansı içeriyor)
- **Docker** ve **Docker Compose**: (PostgreSQL’i hızlı başlatmak için)
- (İstemci için) **Node.js** 18+ ve **npm** / **pnpm**

---

## Hızlı Kurulum (Docker ile)

Depoda bir `docker-compose.yaml` yer alır ve PostgreSQL’i şu şekilde ayağa kaldırır:

```yaml
version: "3.3"

networks:
  graph-starter:

services:
  postgresql:
    restart: always
    image: "postgres"
    ports:
      - "5432:5432"
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
      - POSTGRES_DB=graphdb
    volumes:
      - /var/lib/postgresql/data
    networks:
      - graph-starter
```

### Çalıştırma
```bash
# compose dosyasının bulunduğu dizinden:
docker-compose up -d     # PostgreSQL’i başlatır
docker ps                # konteyner durumunu gösterir
```

### Durdurma/Temizleme
```bash
docker-compose down      # konteyneri ve ağı durdurur/siler
# Tek bir konteyneri zorla silmek için (örnek ID: 825)
docker rm 825 -f
```

> Varsayılan bağlantı bilgileri: `Host=localhost; Port=5432; Database=graphdb; Username=postgres; Password=postgres`

---

## Yerel Geliştirme (Docker olmadan)

1. PostgreSQL’i sisteminize kurun ve bir `graphdb` veritabanı oluşturun (kullanıcı/şifre: `postgres/postgres` veya kendi değerleriniz).
2. API projesindeki `appsettings*.json` içinde **ConnectionString** değerini güncelleyin.
3. EF Core migration’ları uygulayın (aşağıya bakın) ve API’yi çalıştırın.

---

## EF Core Migration Akışı

İlk kez EF CLI kurulumu (mevcut README’deki isim hatasını düzeltilmiş haliyle):
```bash
dotnet tool install --global dotnet-ef --version 3.1.6
```

İlk migration oluşturma ve veritabanına uygulama (Web API projesi dizininde):
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Yanlış migration’ı geri almak için:
```bash
dotnet ef migrations remove   # yalnızca henüz 'update' edilmemiş en son migration'ı siler
```

> Not: Projede kullanılan EF Core sürümüne göre komutlardaki sürüm numarasını güncelleyebilirsiniz.

---

## GraphQL Uç Noktası ve Örnek İstekler

Uygulamayı çalıştırdıktan sonra GraphQL uç noktası genellikle `/graphql` olur. (Projede farklı bir yol tanımlı ise konsoldaki loglardan görebilirsiniz.)

### Örnek Query
```graphql
{
  user(id: 1) {
    id
    name
    email
  }
}
```

### Örnek Mutation
```graphql
mutation {
  addUser(name: "Yunus Ergul", email: "ergulyunus@gmail.com") {
    id
    name
    email
  }
}
```

> İsteği `POST http://localhost:5000/graphql` (veya uygulamanın portuna) JSON gövdesi ile gönderebilirsiniz:
```json
{
  "query": "{ user(id: 1) { id name email } }"
}
```

---

## React İstemci (Apollo) Hızlı Başlangıç

> Orijinal notlarda `apollo-boost` ve `react-apollo` kullanılmış (artık eski). Aşağıda **güncel Apollo Client** ile kısa kurulum yer alıyor.

```bash
# React uygulaması oluşturma (örnek isim: graphql-client)
npm create vite@latest graphql-client -- --template react
cd graphql-client
npm install @apollo/client graphql
```

`src/apollo.ts`:
```ts
import { ApolloClient, InMemoryCache } from "@apollo/client";

export const client = new ApolloClient({
  uri: "http://localhost:5000/graphql",
  cache: new InMemoryCache(),
});
```

`src/main.tsx`:
```tsx
import React from "react";
import ReactDOM from "react-dom/client";
import { ApolloProvider } from "@apollo/client";
import { client } from "./apollo";
import App from "./App";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <ApolloProvider client={client}>
    <App />
  </ApolloProvider>
);
```

Örnek kullanım (`src/App.tsx`):
```tsx
import { gql, useQuery } from "@apollo/client";

const GET_USER = gql`
  query GetUser($id: ID!) {
    user(id: $id) { id name email }
  }
`;

export default function App() {
  const { data, loading, error } = useQuery(GET_USER, { variables: { id: 1 } });

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return <pre>{JSON.stringify(data, null, 2)}</pre>;
}
```

> Eğer mevcut CRA notlarını takip etmek isterseniz, eski kurulum:
> ```bash
> npx create-react-app graphql-client
> npm i apollo-boost graphql react-apollo
> npm i cors
> ```
> (Güncel projelerde `@apollo/client` kullanılması önerilir.)

---

## Sık Karşılaşılan Komutlar

```bash
# API’yi çalıştırma (proje dizininde)
dotnet run

# EF CLI sürümünü güncelleme
dotnet tool update --global dotnet-ef --version 3.1.6

# Docker
docker-compose up -d
docker-compose down
docker ps
docker rm <CONTAINER_ID> -f
```

---

## Lisans

Bu depo için lisans belirtilmemiştir. Açık kaynak lisansı eklemek isterseniz `LICENSE` dosyası oluşturabilirsiniz (MIT vb.).
