using Application.DTOs.Devices;
using Application.Services.Interfaces;
using Application.Wrappers;
using Application.IRepository;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeviceService> logger)
        {
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<DeviceDTO>> SyncAsync(int accountId, DeviceSyncRequest request, CancellationToken ct = default)
        {
            var existing = await _deviceRepository.FindOneAsync(d => d.AccountId == accountId && d.DeviceToken == request.DeviceToken);
            if (existing != null)
            {
                existing.IsActive = true;
                existing.DeviceType = request.DeviceType;
                existing.DeviceName = request.DeviceName;
                existing.OsVersion = request.OsVersion;
                existing.AppVersion = request.AppVersion;
                existing.LastSeenAt = DateTime.UtcNow;
                _deviceRepository.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Updated device token for account {AccountId}", accountId);
                return Result<DeviceDTO>.Success(_mapper.Map<DeviceDTO>(existing), "Device updated.");
            }

            var device = new Device
            {
                AccountId = accountId,
                DeviceToken = request.DeviceToken,
                DeviceType = request.DeviceType,
                DeviceName = request.DeviceName,
                OsVersion = request.OsVersion,
                AppVersion = request.AppVersion,
                IsActive = true,
                LastSeenAt = DateTime.UtcNow
            };

            await _deviceRepository.AddAsync(device);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Registered new device for account {AccountId}", accountId);
            return Result<DeviceDTO>.Success(_mapper.Map<DeviceDTO>(device), "Device registered.");
        }

        public async Task<Result<bool>> UnregisterAsync(int accountId, DeviceUnregisterRequest request, CancellationToken ct = default)
        {
            var existing = await _deviceRepository.FindOneAsync(d => d.AccountId == accountId && d.DeviceToken == request.DeviceToken);
            if (existing == null)
            {
                return Result<bool>.Fail("Device not found.", 404);
            }

            existing.IsActive = false;
            existing.LastSeenAt = DateTime.UtcNow;
            _deviceRepository.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true, "Device unregistered.");
        }

        public async Task<Result<IEnumerable<DeviceDTO>>> GetMyDevicesAsync(int accountId)
        {
            var devices = await _deviceRepository.GetAllQueryable()
                .Where(d => d.AccountId == accountId)
                .OrderByDescending(d => d.LastSeenAt ?? d.CreatedAt)
                .ToListAsync();
            var mapped = devices.Select(d => _mapper.Map<DeviceDTO>(d));
            return Result<IEnumerable<DeviceDTO>>.Success(mapped, "Devices retrieved.");
        }

        public async Task<IReadOnlyList<string>> GetActiveDeviceTokensAsync(int accountId, CancellationToken ct = default)
        {
            return await _deviceRepository.GetAllQueryable()
                .Where(d => d.AccountId == accountId && d.IsActive && !d.IsDeleted && !string.IsNullOrWhiteSpace(d.DeviceToken))
                .Select(d => d.DeviceToken)
                .ToListAsync(ct);
        }
    }
}
