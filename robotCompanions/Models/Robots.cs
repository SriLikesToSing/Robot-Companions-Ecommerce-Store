using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace robotCompanions.Models

{
    [Table("Robot")]
    public class Robots
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string robotName { get; set; }
        [Required]
        public double price { get; set; }
        public string? image { get; set; }

        [Required]
        [MaxLength(2000)]
        public string description { get; set; }

        [Required]
        public robotSize robotSize { get; set; }

        public List<orderDetails> orderDetails { get; set; }
        public List<cartDetails> cartDetails { get; set; }

        [NotMapped]
        public string RobotSize { get; set; }


    }
}
