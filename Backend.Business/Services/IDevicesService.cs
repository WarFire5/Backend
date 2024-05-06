using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Devices.Requests;

namespace Backend.Business.Services;

public interface IDevicesService
{
    //метод для отображения выпадающего списка в свагере
    Guid AddDevice(Guid ownerId, DeviceType deviceType, string deviceName);
    Guid AddDevice(Guid ownerId, AddDeviceRequest request);
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByOwnerId(Guid ownerId);
    void DeleteDeviceById(Guid id);
}