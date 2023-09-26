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

        public AccountController(IUserRepo userRepo,
                                 ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<ApiResponse>> Login(LoginDto loginDto)
        {
            var existUser = await _userRepo.ExistUserAsync(loginDto.Username);
            if (loginDto == null || existUser == null) return NotFound();

            var result = await _userRepo.LoginAsync(loginDto);

            return await Task.FromResult(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<ActionResult<ApiResponse>> Register(RegisterDto registerDto)
        {
            var existUser = await _userRepo.ExistUserAsync(registerDto.Username);
            if (existUser != null) return BadRequest("User already registered");

            var result = await _userRepo.RegisterAsync(registerDto);

            return await Task.FromResult(result);
        }

        [Authorize]
        [HttpPost(nameof(UpdateInfo))]
        public async Task<ActionResult<ApiResponse>> UpdateInfo(UpdateUserDto updateUserDto)
        {
            var existUser = await _userRepo.ExistUserAsync(updateUserDto.Username);
            if (existUser == null) return BadRequest("User not found");

            var result = await _userRepo.UpdateUserAsync(existUser, updateUserDto);

            return await Task.FromResult(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(ForgotPassword))]
        public async Task<ActionResult<ApiResponse>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var existUser = await _userRepo.ExistUserAsync(forgotPasswordDto.Username);
            if (existUser == null) return BadRequest("User not found");

            var result = await _userRepo.ForgotPasswordAsync(existUser);

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