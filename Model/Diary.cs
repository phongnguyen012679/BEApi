using System.ComponentModel.DataAnnotations;

namespace BEApi.Model
{
    public class Diary
    {
        [Key]
        public int Id { get; set; }
        public string Journey { get; set; }
        public DateTime DayStart { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public List<DiaryDetails> DiariesDetail { get; set; }
    }
}