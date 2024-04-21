using Backend.Core.DTOs;
using Backend.DataLayer.Repositories;

namespace Backend.Business.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;
    public DevicesService(IDevicesRepository devicesRepository)
    {
        _devicesRepository = devicesRepository;
    }
    public DeviceDto GetDeviceById(Guid id) => _devicesRepository.GetDeviceById(id);
    public DeviceDto GetDeviceByOwnerId(Guid ownerId) => _devicesRepository.GetDeviceByOwnerId(ownerId);
}
