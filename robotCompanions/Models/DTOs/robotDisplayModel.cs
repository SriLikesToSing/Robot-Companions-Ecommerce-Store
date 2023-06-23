using Microsoft.Identity.Client;

namespace robotCompanions.Models.DTOs
{
    public class robotDisplayModel
    {
        public IEnumerable<Robots> Robots { get; set; }

        public IEnumerable<robotSize> robotSizes { get; set; }
        public string sTerm { get; set; } = "";

        public int robotSizeId { get; set; } = 0;
    }
}
