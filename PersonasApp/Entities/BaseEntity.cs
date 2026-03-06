using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonsApp.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public String Id { get; set; }

        // Audit fields
        [Column("created_by_id")]
        public string CreatedById { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("update_by_id")]
        public string UpdateById { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }
    }
}