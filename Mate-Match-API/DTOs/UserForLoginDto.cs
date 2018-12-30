using System.ComponentModel.DataAnnotations;

namespace Mate_Match_API.DTOs
{
    public class UserForLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}