using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace robotCompanions.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string userId { get; set; }
        public DateTime createDate { get; set; } = DateTime.UtcNow;
        public int orderStatusId { get; set; }
        public bool isDeleted { get; set; } = false;
        public orderStatus orderStatus { get; set; }

        public List<orderDetails> orderDetails { get; set; }

        }

}
