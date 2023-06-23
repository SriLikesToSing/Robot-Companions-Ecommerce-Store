using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace robotCompanions.Models
   {
    [Table("shoppingCart")]
    public class shoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string userId { get; set; }
        public bool isDeleted { get; set; } = false;

        public ICollection<cartDetails> cartDetails { get; set; }
    }
}
