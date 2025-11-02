using Application.Authentication.Interfaces;
using Application.DTOs.Authentication;
using Application.Exceptions;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authService, ITokenService tokenService, ILogger<AuthenticationController> logger)
        {
            _authenticationService = authService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountRegistrationDTO registrationDto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");

                throw new ValidationException(errors);
            }

            var result = await _authenticationService.RegisterAsync(registrationDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountLoginDTO loginDto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");

                throw new ValidationException(errors);
            }

            var result = await _authenticationService.LoginAsync(loginDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");

                throw new ValidationException(errors);
            }

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _authenticationService.RefreshAsync(dto, ip);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("revoke")]
        [AllowAnonymous]
        public async Task<IActionResult> Revoke([FromBody] RevokeTokenRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RefreshToken))
            {
                return StatusCode(400, Application.Wrappers.Result<object>.Fail("Refresh token is required.", 400));
            }
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var ok = await _tokenService.RevokeByTokenAsync(dto.RefreshToken, dto.Reason, ip);
            return StatusCode(ok ? 200 : 404, Application.Wrappers.Result<object>.Success(new { revoked = ok }, ok ? "Revoked" : "Not found", ok ? 200 : 404));
        }

        [HttpPost("revoke-all")]
        [Authorize]
        public async Task<IActionResult> RevokeAll([FromBody] RevokeAllRequestDTO dto)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return StatusCode(401, Application.Wrappers.Result<object>.Fail("Unauthorized", 401));
            var count = await _tokenService.RevokeAllForAccountAsync(userId, dto?.Reason, ip);
            return StatusCode(200, Application.Wrappers.Result<object>.Success(new { revoked = count }, "All sessions revoked", 200));
        }
    }
}
