using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentCounselling.Entities
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AddressId { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MaxLength(250)]
        public string Region { get; set; }
        [Required]
        [MaxLength(250)]
        public string City { get; set; }
        [MaxLength(250)]
        public string SubCity { get; set; }
        [Required]
        [MaxLength(250)]
        public string Kebele { get; set; }
        [MaxLength(250)]
        public string Wereda { get; set; }
        [MaxLength(250)]
        public string HouseNumber { get; set; }
        public User User { get; set; }
    }
}
