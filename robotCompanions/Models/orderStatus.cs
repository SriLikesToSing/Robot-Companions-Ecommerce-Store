using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace robotCompanions.Models
{
    [Table("orderStatus")]
    public class orderStatus
    {
        public int Id { get; set; }
        [Required]
        public int statusId { get; set; }
        [Required, MaxLength(40)]

        public string ?statusName { get; set; }

    }
}
