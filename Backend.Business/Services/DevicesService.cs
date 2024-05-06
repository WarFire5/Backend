using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Exceptions;
using Backend.Core.Models.Devices.Requests;
using Backend.DataLayer.Repositories;
using FluentValidation;
using Serilog;
using ValidationException = Backend.Core.Exceptions.ValidationException;

namespace Backend.Business.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;
    private readonly IValidator<AddDeviceRequest> _addDeviceValidator;

    public DevicesService(IDevicesRepository devicesRepository, IUsersRepository usersRepository, IMapper mapper, IValidator<AddDeviceRequest> addDeviceValidator)
    {
        _devicesRepository = devicesRepository;
        _usersRepository = usersRepository;
        _mapper = mapper;
        _addDeviceValidator = addDeviceValidator;
    }

    //метод для отображения выпадающего списка в свагере
    public Guid AddDevice(Guid ownerId, DeviceType deviceType, string deviceName)
    {
        //var validationResult = _addDeviceValidator.Validate(request);
        //if (validationResult.IsValid)
        //{
            var owner = _usersRepository.GetUserById(ownerId);

            if (owner == null)
            {
                throw new NotFoundException($"Пользователь с Id {ownerId} не найден");
            }

            DeviceDto device = new DeviceDto()
            {
                DeviceName = deviceName,
                DeviceType = deviceType,
                Owner = owner
            };

            return _devicesRepository.AddDevice(device);
        //}

        //string exceptions = string.Join(Environment.NewLine, validationResult.Errors);
        //throw new ValidationException(exceptions);
    }

    public Guid AddDevice(Guid ownerId, AddDeviceRequest request)
    {
        var validationResult = _addDeviceValidator.Validate(request);
        if (validationResult.IsValid)
        {
            var owner = _usersRepository.GetUserById(ownerId);

            if (owner == null)
            {
                throw new NotFoundException($"Пользователь с Id {ownerId} не найден");
            }

            DeviceDto device = new DeviceDto()
            {
                DeviceName = request.DeviceName,
                DeviceType = request.DeviceType,
                Owner = owner
            };

            return _devicesRepository.AddDevice(device);
        }

        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);
        throw new ValidationException(exceptions);
    }

    public DeviceDto GetDeviceById(Guid id) => _devicesRepository.GetDeviceById(id);

    public DeviceDto GetDeviceByOwnerId(Guid ownerId) => _devicesRepository.GetDeviceByOwnerId(ownerId);

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
