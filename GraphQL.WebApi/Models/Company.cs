using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.WebApi.Models
{
    [Table("company", Schema = "business")]
    public partial class Company : BaseEntity
    {
        public string name { get; set; }

    }
}
