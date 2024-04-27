using Backend.Core.DTOs;

namespace Backend.Business.Services;

public interface IDevicesService
{
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByOwnerId(Guid ownerId);
    Guid CreateDevice(string deviceName, string address, Guid ownerId);
    void DeleteDeviceById(Guid id);
}