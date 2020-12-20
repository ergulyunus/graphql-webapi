using GraphQL.Types;
using GraphQL.WebApi.Interfaces;
using GraphQL.WebApi.Models;
using System;

namespace GraphQL.WebApi.Graph.Type
{
    public class CompanyGType : ObjectGraphType<Company>
    {
        public IServiceProvider Provider { get; set; }
        public CompanyGType(IServiceProvider provider)
        {
            Field(x => x.id, type: typeof(IntGraphType));
            Field(x => x.name, type: typeof(StringGraphType));
        }
    }
}
