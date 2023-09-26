using System.ComponentModel.DataAnnotations;

namespace BEApi.Dtos
{
    public class CreateDiaryDto
    {
        public int Id { get; set; }
        [Required]
        public string Journey { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime DayStart { get; set; }
    }
}