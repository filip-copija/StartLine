using System.ComponentModel.DataAnnotations;

namespace StartLine_social_network.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }       
        public string Province { get; set; }
    }
}
