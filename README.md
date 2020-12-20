# graphql-webapi

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


docker-compose.yaml dosyası oluşturulduktan sonra aşağıdaki komutlar çalıştırılır. .yaml olduğu dizine gidilerek 

docker ps -a  --> image göstermek için
docker rm 825 -f  -> image silmek için
docker-compose up -d --> imaj oluşturmak için (boş database)
docker-compose down --> oluşturulan database imajını siler.



ilk defa dotnet ef core kullanılacaksa makinada ;

dotnet tool install --global dotnet -ef --version 3.1.6


webApi içinde ilk migrasyon için
dotnet ef migrations add firstMigration
dotnet ef migrations remove --> son yapılan migrations siler. Update edilmeden önce yapılması gerekir.
dotnet ef database update --> migration ları db'ye uygular.



yazılan projeye react client ile bağlanmak

npm init react-app grapgql-client --> react projesi oluştur. ilgili klasöt altında. 

npm i apollo-boost graphql react-apollo -S

npm i --save cors
