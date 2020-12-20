using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQL.WebApi.Models
{
    [Table("personel", Schema = "business")]
    public partial class Personel : BaseEntity
    {
        public string firtstName { get; set; }

        public string lastName { get; set; }

        public DateTime? birthday { get; set; }

        public string eMail { get; set; }

        public int companyId { get; set; }

        [ForeignKey("companyId")]
        public Company Company { get; set; }

    }
}
