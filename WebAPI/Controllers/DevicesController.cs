using Application.DTOs.Devices;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/devices")]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync([FromBody] DeviceSyncRequest request, CancellationToken ct)
        {
            var result = await _deviceService.SyncAsync(CurrentUserId, request, ct);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("unregister")]
        public async Task<IActionResult> Unregister([FromBody] DeviceUnregisterRequest request, CancellationToken ct)
        {
            var result = await _deviceService.UnregisterAsync(CurrentUserId, request, ct);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyDevices()
        {
            var result = await _deviceService.GetMyDevicesAsync(CurrentUserId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
