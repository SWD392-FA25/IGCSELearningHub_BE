using Application.Exceptions;
using Application.Services.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Accounts;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAllAccountsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaginated([FromQuery] QueryParameters query)
        {
            var result = await _accountService.GetAllAccountsPaginatedAsync(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("check-exists")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckUsernameOrEmailExists([FromQuery] string username, [FromQuery] string email)
        {
            var result = await _accountService.CheckUsernameOrEmailExistsAsync(email, username);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            var result = await _accountService.UpdateAccountAsync(id, updateDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            var result = await _accountService.ResetPasswordAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("ban/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> BanAccount(int id)
        {
            var result = await _accountService.BanAccountAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("unban/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UnbanAccount(int id)
        {
            var result = await _accountService.UnbanAccountAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
