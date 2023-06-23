using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace robotCompanions.Models
{
    [Table("RobotSize")]
    public class robotSize
    {
        public int Id { get; set; }
        public string size { get; set; }
    }
}
