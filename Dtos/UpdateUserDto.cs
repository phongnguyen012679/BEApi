using System.ComponentModel.DataAnnotations;

namespace BEApi.Dtos
{
    public class UpdateUserDto
    {

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string DiaryOwner { get; set; }
    }
}