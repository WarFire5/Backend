using AutoMapper;
using Backend.Core.Data;
using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Exceptions;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Backend.Core.Models.Devices.Requests;
using Backend.DataLayer.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ValidationException = Backend.Core.Exceptions.ValidationException;

namespace Backend.Business.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<DevicesService>();
    private readonly IMapper _mapper;
    private readonly IValidator<DeviceRequest> _addDeviceValidator;

    public DevicesService(IDevicesRepository devicesRepository, IUsersRepository usersRepository, IMapper mapper, IValidator<DeviceRequest> addDeviceValidator)
    {
        _devicesRepository = devicesRepository;
        _usersRepository = usersRepository;
        _mapper = mapper;
        _addDeviceValidator = addDeviceValidator;
    }

    public Guid AddDevice(Guid ownerId, DeviceRequest request)
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
                DeviceName = request.Name,
                DeviceType = request.Type,
                Owner = owner
            };

            return _devicesRepository.AddDevice(device);
        }

        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);
        throw new ValidationException(exceptions);
    }

    public DeviceDto GetDeviceById(Guid id) => _devicesRepository.GetDeviceById(id);

    public void DeleteDeviceById(Guid id)
    {
        var device = _devicesRepository.GetDeviceById(id);

        if (device is null) throw new NotFoundException($"Девайс с Id {id} не найден");

        _devicesRepository.DeleteDeviceById(device);
    }

    public List<DeviceDto> GetDevices()
    {
        // здесь есть бизнес логика
        return _devicesRepository.GetDevices();
    }

    public List<DeviceDto> GetDevicesByOwnerId(Guid ownerId) => _devicesRepository.GetDevicesByOwnerId(ownerId);

    public IdOperationWithCoinsResponse GenerateCoinsWithDevice([FromRoute] Guid deviceId, CoinTypeAndQuantityRequest request)
    {
        var device = GetDeviceById(deviceId);

        if (device is null) throw new NotFoundException($"Девайс с Id {deviceId} не найден/ Could not find the device with Id {deviceId}");

        if (device.Coins == null) device.Coins = new List<OperationWithCoinsDto>();

        if (device.DeviceType == DeviceType.PC)
        {
            if (EnumProvider.GetCoinTypesForPc().Contains(request.Type))
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.Type,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }
        if (device.DeviceType == DeviceType.Laptop)
        {
            if (EnumProvider.GetCoinTypesForLaptop().Contains(request.Type))
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.Type,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }
        if (device.DeviceType == DeviceType.VideoCard)
        {
            if (EnumProvider.GetCoinTypesForVideoCard().Contains(request.Type))
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.Type,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }
        if (device.DeviceType == DeviceType.ASIC)
        {
            if (EnumProvider.GetCoinTypesForAsic().Contains(request.Type))
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.Type,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }

        throw new NotFoundException("Неизвестный тип девайса");
    }
}