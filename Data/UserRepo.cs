using System.Security.Cryptography;
using System.Text;
using BEApi.Dtos;
using BEApi.Model;
using BEApi.Servies;
using Microsoft.EntityFrameworkCore;

namespace BEApi.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;

        public UserRepo(AppDbContext context,
                        ITokenService tokenService,
                        IMailService mailService)
        {
            _context = context;
            _tokenService = tokenService;
            _mailService = mailService;
        }

        public async Task<User> ExistUserAsync(string Username)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Username == Username.ToLower());
        }

        public async Task<ApiResponse> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == loginDto.Username);
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                    return new ApiResponse(false, "Invalid username/password", "");
            }

            var token = await _tokenService.GenerateToken(user);

            return new ApiResponse(true, "Authenticate success", token);
        }

        public async Task<ApiResponse> RegisterAsync(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();

            User user = new User()
            {
                Id = Guid.NewGuid().ToString("N"),
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                IsAdmin = registerDto.IsAdmin,
                Email = registerDto.Email,
                DiaryOwner = registerDto.DiaryOwner
            };

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = await _tokenService.GenerateToken(user);

            return new ApiResponse(true, "Authenticate success", token);
        }

        public async Task<ApiResponse> UpdateUserAsync(string username, UpdateUserDto updateUserDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == username);

            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateUserDto.NewPassword));
            user.PasswordSalt = hmac.Key;
            user.Email = updateUserDto.Email;
            user.DiaryOwner = updateUserDto.DiaryOwner;

            await _context.SaveChangesAsync();

            return new ApiResponse(true, "Update success", "");
        }

        public async Task<ApiResponse> ForgotPasswordAsync(User user)
        {
            var mailData = new MailData()
            {
                EmailToId = user.Email,
                EmailToName = user.Email,
                EmailSubject = "Forgot Password",
                EmailBody = Guid.NewGuid().ToString()
            };

            var isSuccess = _mailService.SendMail(mailData);

            return new ApiResponse(isSuccess, isSuccess ? "Update success" : "Error updating password", "");
        }

        public async Task<bool> CorrectPassword(string Username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == Username);

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                    return false;
            }

            return true;
        }
    }
}