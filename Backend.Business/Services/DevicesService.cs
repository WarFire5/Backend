using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Exceptions;
using Backend.DataLayer.Repositories;

namespace Backend.Business.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    public DevicesService(IDevicesRepository devicesRepository, IUsersRepository usersRepository)
    {
        _devicesRepository = devicesRepository;
        _usersRepository = usersRepository;
    }

    public DeviceDto GetDeviceById(Guid id) => _devicesRepository.GetDeviceById(id);
    public DeviceDto GetDeviceByOwnerId(Guid ownerId) => _devicesRepository.GetDeviceByOwnerId(ownerId);

    public Guid CreateDevice(string deviceName, string address, Guid ownerId)
    {
        var owner = _usersRepository.GetUserById(ownerId);

        if (owner == null)
        {
            throw new NotFoundException($"Пользователь с Id {ownerId} не найден");
        }

        DeviceDto device = new DeviceDto()
        {
            Id = Guid.NewGuid(),
            DeviceName = deviceName,
            DeviceType = DeviceType.PC,
            Address = address,
            Owner = owner
        };

        return _devicesRepository.CreateDevice(device);
    }

    public void DeleteDeviceById(Guid id)
    {
        var device = _devicesRepository.GetDeviceById(id);
        if (device is null)
        {
            throw new NotFoundException($"Девайс с Id {id} не найден");
        }

        _devicesRepository.DeleteDeviceById(id);
    }
}
