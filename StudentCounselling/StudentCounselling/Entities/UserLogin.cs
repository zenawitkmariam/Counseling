using System.ComponentModel.DataAnnotations;

namespace StudentCounselling.Entities
{
    public class UserLogin
    {
        [Key]
        public string UserLoginId { get; set; }
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
