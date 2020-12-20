using GraphQL.WebApi.Interfaces;
using System;

namespace GraphQL.WebApi.Models
{
    public abstract class BaseEntity : IEntity
    {
        public int id { get; set; }
        public DateTime? creationDate { get; set; }
    }
}
