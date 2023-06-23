using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace robotCompanions.Models
{
    [Table("cartDetails")]
    public class cartDetails
    {
        public int Id { get; set; }
        [Required]
        public int shoppingCartId { get; set; }
        [Required]
        public int robotId { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public int RobotsId { get; set; }

        [Required]
        public double unitPrice { get; set; }
        public Robots Robots { get; set; }
        public shoppingCart shoppingCart { get; set; }


    }
}