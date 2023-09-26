using System.ComponentModel.DataAnnotations;

namespace BEApi.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string DiaryOwner { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IsAdmin { get; set; }
    }
}