using Application.DTOs.Devices;
using Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<Result<DeviceDTO>> SyncAsync(int accountId, DeviceSyncRequest request, CancellationToken ct = default);
        Task<Result<bool>> UnregisterAsync(int accountId, DeviceUnregisterRequest request, CancellationToken ct = default);
        Task<Result<IEnumerable<DeviceDTO>>> GetMyDevicesAsync(int accountId);
        Task<IReadOnlyList<string>> GetActiveDeviceTokensAsync(int accountId, CancellationToken ct = default);
    }
}
