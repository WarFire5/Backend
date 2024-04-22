using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public interface IDevicesRepository
{
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByOwnerId(Guid ownerId);
    Guid CreateDevice(DeviceDto device);
    void DeleteDeviceById(Guid id);
}