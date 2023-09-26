using BEApi.Dtos;
using BEApi.Model;

namespace BEApi.Servies
{
    public interface ITokenService
    {
        Task<TokenModel> GenerateToken(User user);
        Task<ApiResponse> RenewToken(TokenModel model);
    }
}