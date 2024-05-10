using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Backend.Core.Models.Devices.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Business.Services;

public interface IDevicesService
{
    Guid AddDevice(Guid ownerId, DeviceRequest request);
    DeviceDto GetDeviceById(Guid id);
    void DeleteDeviceById(Guid id);
    List<DeviceDto> GetDevices();
    List<DeviceDto> GetDevicesByOwnerId(Guid ownerId);
    IdOperationWithCoinsResponse GenerateCoinsWithDevice([FromRoute] Guid deviceId, CoinsWithDeviceRequest request);
}