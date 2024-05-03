using Backend.Core.DTOs;

namespace Backend.Business.Services;

public interface IDevicesService
{
    Guid AddDevice(string deviceName, string address, Guid ownerId);
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByOwnerId(Guid ownerId);
    void DeleteDeviceById(Guid id);
}