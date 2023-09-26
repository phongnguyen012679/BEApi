namespace BEApi.Dtos
{
    public class DiaryDetailDto
    {
        public int Id { get; set; }
        public int DiaryId { get; set; }

        public string Content { get; set; }
        public bool IsUser { get; set; }
    }
}