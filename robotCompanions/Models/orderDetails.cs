using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace robotCompanions.Models
{
    [Table("orderDetails")]
    public class orderDetails
    {
        public int Id { get; set; }
        [Required]
        public int orderId { get; set; }
        [Required]
        public int robotId { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public double unitPrice { get; set; }

        public Order order { get; set; }
        public Robots robot { get; set; }


    }
}
