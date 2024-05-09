using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Responses;

namespace Backend.DataLayer.Repositories;

public interface IDevicesRepository
{
    Guid AddDevice(DeviceDto device);
    DeviceDto GetDeviceById(Guid id);
    void DeleteDeviceById(DeviceDto device);
    List<DeviceDto> GetDevices();
    List<DeviceDto> GetDevicesByOwnerId(Guid ownerId);
    IdOperationWithCoinsResponse GenerateCoinWithDevice(DeviceDto device);
}