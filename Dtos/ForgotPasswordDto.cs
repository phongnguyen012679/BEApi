using System.ComponentModel.DataAnnotations;

namespace BEApi.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}