using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public interface IDevicesRepository
{
    Guid AddDevice(DeviceDto device);
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByOwnerId(Guid ownerId);
    void DeleteDeviceById(Guid id);
}