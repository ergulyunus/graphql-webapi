using System;

namespace GraphQL.WebApi.Interfaces
{
    public interface IEntity
    {
        int id { get; set; }
        DateTime? creationDate { get; set; }
    }
}
