using BEApi.Dtos;
using BEApi.Model;
using BEApi.Data;
using BEApi.Servies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;

namespace BEApi.Controllers
{
    [Authorize]
    public class DiaryController : BaseApiController
    {
        private readonly OpenAIDto _OpenAI;
        private readonly IServiceProvider _service;
        private readonly IIdentityService _identity;
        private readonly IDiaryRepo _diaryRepo;

        public DiaryController(IOptionsMonitor<OpenAIDto> optionsMonitor,
                              IServiceProvider service,
                              IIdentityService identity,
                              IDiaryRepo diaryRepo)
        {
            _OpenAI = optionsMonitor.CurrentValue;
            _service = service;
            _identity = identity;
            _diaryRepo = diaryRepo;
        }

        [HttpPost(nameof(PostDiary))]
        public async Task<ActionResult<IEnumerable<DiaryDetailDto>>> PostDiary(CreateDiaryDto createDiaryDto)
        {
            var UserId = _identity.GetUserIdIdentity();

            // var openAiService = _service.GetRequiredService<IOpenAIService>();
            // openAiService.SetDefaultModelId(Models.Davinci);
            // var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            // {
            //     Messages = new List<ChatMessage>
            //     {
            //         ChatMessage.FromUser(diaryDto.Content)
            //     },
            //     Model = Models.Gpt_3_5_Turbo_16k_0613,
            //     MaxTokens = 600
            // });

            // if (completionResult.Successful)
            // {
            var chatbotResponse = "Đừng quên rằng thất bại là một phần của cuộc sống và học hỏi từ nó có thể giúp bạn trở nên mạnh mẽ hơn và thành công hơn trong tương lai. Hãy tập trung vào mục tiêu của mình và tiếp tục nỗ lực. Chúc bạn thành công trong những nhiệm vụ và thử thách tiếp theo của mình.";//completionResult.Choices.First().Message.Content;
            var result = await _diaryRepo.CreateDiary(UserId, chatbotResponse, createDiaryDto);

            return Ok(result);
            //}

            //return BadRequest();
        }

        [HttpGet(nameof(GetAllDiary))]
        public async Task<ActionResult<IEnumerable<DiaryDto>>> GetAllDiary()
        {
            return Ok(await _diaryRepo.GetAllDiary());
        }

        [HttpGet(nameof(GetDiaryDetailsByDiaryId))]
        public async Task<ActionResult<object>> GetDiaryDetailsByDiaryId(int diaryId)
        {
            if (diaryId == 0) return NotFound();
            return Ok(await _diaryRepo.GetDiaryDetailsByDiaryId(diaryId));
        }
    }
}