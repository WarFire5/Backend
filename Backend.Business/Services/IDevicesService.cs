using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Backend.Core.Models.Devices.Requests;
using Backend.Core.Models.Devices.Responses;

namespace Backend.Business.Services;

public interface IDevicesService
{
    Guid AddDevice(Guid ownerId, DeviceRequest request);
    DeviceDto GetDeviceById(Guid id);
    void DeleteDeviceById(Guid id);
    List<DeviceResponse> GetDevices();
    //List<DeviceDto> GetDevicesByOwnerId(Guid ownerId);
    List<DeviceResponse> GetDevicesByOwnerId(Guid ownerId);
    IdOperationWithCoinsResponse GenerateCoinsWithDevice(Guid deviceId, CoinTypeAndQuantityRequest request);
}