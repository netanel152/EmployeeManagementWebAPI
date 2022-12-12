using System.ComponentModel.DataAnnotations;

namespace LoginAdminWebAPI.Models.Auth
{
    public class AuthAdmin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
