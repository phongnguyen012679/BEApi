using BEApi.Dtos;
using BEApi.Model;

namespace BEApi.Data
{
    public interface IUserRepo
    {
        Task<User> ExistUserAsync(string Username);
        Task<User> ExistEmailAsync(string Username);
        Task<ApiResponse> LoginAsync(LoginDto Login);
        Task<ApiResponse> RegisterAsync(RegisterDto registerDto);
        Task<ApiResponse> UpdateUserAsync(string username, UpdateUserDto updateUserDto);
        ApiResponse ForgotPasswordAsync(User user);
        Task<bool> CorrectPassword(string Username, string password);
    }
}