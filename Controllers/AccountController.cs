using BEApi.Data;
using BEApi.Dtos;
using BEApi.Servies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BEApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepo _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IIdentityService _identity;

        public AccountController(IUserRepo userRepo,
                                 ITokenService tokenService,
                                 IIdentityService identity)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _identity = identity;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<ApiResponse>> Login(LoginDto loginDto)
        {
            var existUser = await _userRepo.ExistUserAsync(loginDto.Username);
            if (loginDto == null || existUser == null)
                return new ApiResponse(false, "User not found", "");

            var result = await _userRepo.LoginAsync(loginDto);

            return await Task.FromResult(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<ActionResult<ApiResponse>> Register(RegisterDto registerDto)
        {
            var existUser = await _userRepo.ExistUserAsync(registerDto.Username);
            if (existUser != null)
                return new ApiResponse(false, "User already registered", "");

            var result = await _userRepo.RegisterAsync(registerDto);

            return await Task.FromResult(result);
        }

        [Authorize]
        [HttpPost(nameof(UpdateInfo))]
        public async Task<ActionResult<ApiResponse>> UpdateInfo(UpdateUserDto updateUserDto)
        {
            var flagcheck = 0;
            if (updateUserDto.NewPassword.ToLower() != updateUserDto.ConfirmPassword.ToLower())
                flagcheck = -1;

            var username = _identity.GetUserNameIdentity();
            var correctPassword = await _userRepo.CorrectPassword(username, updateUserDto.NewPassword);
            if (!correctPassword)
                flagcheck = -2;

            if (flagcheck == -1 || flagcheck == -2)
                return new ApiResponse(false, flagcheck == -2 ? "Password not correct" : "NewPassword and ConfirmPassword must be the same", "");

            var result = await _userRepo.UpdateUserAsync(username, updateUserDto);

            return await Task.FromResult(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(ForgotPassword))]
        public async Task<ActionResult<ApiResponse>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userRepo.ExistEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return new ApiResponse(false, "User not found", "");

            var result = _userRepo.ForgotPasswordAsync(user);

            return await Task.FromResult(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(RefreshToken))]
        public async Task<ActionResult<ApiResponse>> RefreshToken(TokenModel tokenModel)
        {
            var result = await _tokenService.RenewToken(tokenModel);

            return await Task.FromResult(result);
        }
    }
}