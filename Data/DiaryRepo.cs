using AutoMapper;
using BEApi.Dtos;
using BEApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BEApi.Data
{
    public class DiaryRepo : IDiaryRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DiaryRepo(AppDbContext context,
                         IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DiaryDetailDto>> CreateDiary(string userId, string chatbotResponse, CreateDiaryDto createDiaryDto)
        {
            var diaryId = createDiaryDto.Id;
            if (diaryId == 0)
            {
                var diary = new Diary()
                {
                    Journey = createDiaryDto.Journey,
                    DayStart = createDiaryDto.DayStart,
                    UserId = userId,
                };

                await _context.Diary.AddAsync(diary);
                await _context.SaveChangesAsync();

                diaryId = diary.Id;
            }

            List<DiaryDetails> diaryDetail = new()
            {
                new DiaryDetails() {
                    DiaryId = diaryId,
                    Content = createDiaryDto.Content,
                    IsUser = true,
                },
                new DiaryDetails() {
                    DiaryId = diaryId,
                    Content = chatbotResponse,
                    IsUser = false,
                }
            };

            await _context.DiaryDetails.AddRangeAsync(diaryDetail);
            await _context.SaveChangesAsync();

            return _mapper.Map<IEnumerable<DiaryDetailDto>>(diaryDetail);
        }

        public async Task<IEnumerable<DiaryDto>> GetAllDiary()
        {
            var diaries = await _context.Diary.ToListAsync();
            return _mapper.Map<IEnumerable<DiaryDto>>(diaries);
        }

        public async Task<object> GetDiaryDetailsByDiaryId(int DiaryId)
        {
            var diaryDetails = await _context.DiaryDetails.Include(x => x.Diary).Where(x => x.DiaryId == DiaryId).ToListAsync();

            return new
            {
                Journey = diaryDetails[0].Diary.Journey,
                DayStart = diaryDetails[0].Diary.DayStart,
                DiaryDetails = _mapper.Map<IEnumerable<DiaryDetailDto>>(diaryDetails)
            };
        }
    }
}