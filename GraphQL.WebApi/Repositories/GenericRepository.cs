using GraphQL.WebApi.Interfaces;
using GraphQL.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.WebApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public GenericRepository(DatabaseContext context)
        {
            this.context = context;
            entities = context.Set<T>();

        }
        public void Delete(int id)
        {
            T entity = entities.SingleOrDefault(s => s.id == id);
            entities.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(int id)
        {
            return entities.SingleOrDefault(s => s.id == id);
        }

        public T Insert(T entity)
        {
            if (entity == null) throw new ArgumentException("entity null");

            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null) throw new ArgumentException("entity null");

            context.SaveChanges();
            return entity;
        }
    }
}
