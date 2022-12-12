using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginAdminWebAPI.Models.Admin
{
    public class Admin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
