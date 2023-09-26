using BEApi.Model;
using BEApi.Dtos;

namespace BEApi.Data
{
    public interface IDiaryRepo
    {
        Task<IEnumerable<DiaryDto>> GetAllDiary();

        Task<object> GetDiaryDetailsByDiaryId(int DiaryId);

        Task<IEnumerable<DiaryDetailDto>> CreateDiary(string userId, string chatbotResponse, CreateDiaryDto diaryDto);
    }
}