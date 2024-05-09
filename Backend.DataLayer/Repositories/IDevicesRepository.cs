using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Responses;

namespace Backend.DataLayer.Repositories;

public interface IDevicesRepository
{
    Guid AddDevice(DeviceDto device);
    DeviceDto GetDeviceById(Guid id);
    List<DeviceDto> GetDevicesByOwnerId(Guid ownerId);
    void DeleteDeviceById(DeviceDto device);
    CoinIdResponse GenerateCoinWithDevice(DeviceDto device);
}