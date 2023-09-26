using BEApi.Dtos;
using BEApi.Model;

namespace BEApi.Data
{
    public interface IUserRepo
    {
        Task<User> ExistUserAsync(string Username);
        Task<ApiResponse> LoginAsync(LoginDto Login);
        Task<ApiResponse> RegisterAsync(RegisterDto registerDto);
        Task<ApiResponse> UpdateUserAsync(User user, UpdateUserDto registerDto);
        Task<ApiResponse> ForgotPasswordAsync(User user);
    }
}