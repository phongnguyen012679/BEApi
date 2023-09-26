using System.ComponentModel.DataAnnotations;

namespace BEApi.Model
{
    public class DiaryDetails
    {
        [Key]
        public int Id { get; set; }

        public int DiaryId { get; set; }
        public Diary Diary { get; set; }

        public string Content { get; set; }
        public bool IsUser { get; set; }
    }
}