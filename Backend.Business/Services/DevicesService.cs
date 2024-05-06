using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Exceptions;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
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

    ////метод для отображения выпадающего списка в свагере
    //public Guid AddDevice(Guid ownerId, DeviceType deviceType, string deviceName)
    //{
    //    //var validationResult = _addDeviceValidator.Validate(request);
    //    //if (validationResult.IsValid)
    //    //{
    //    var owner = _usersRepository.GetUserById(ownerId);

    //    if (owner == null)
    //    {
    //        throw new NotFoundException($"Пользователь с Id {ownerId} не найден");
    //    }

    //    DeviceDto device = new DeviceDto()
    //    {
    //        DeviceName = deviceName,
    //        DeviceType = deviceType,
    //        Owner = owner
    //    };

    //    return _devicesRepository.AddDevice(device);
    //    //}

    //    //string exceptions = string.Join(Environment.NewLine, validationResult.Errors);
    //    //throw new ValidationException(exceptions);
    //}

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

        if (device is null) throw new NotFoundException($"Девайс с Id {id} не найден");

        _devicesRepository.DeleteDeviceById(id);
    }

    public CoinIdResponse GenerateCoinWithDevice(GenerateCoinWithDeviceRequest request)
    {
        var device = GetDeviceById(request.DeviceId);

        if (device is null) throw new NotFoundException($"Девайс с Id {request.DeviceId} не найден/ Could not find the device with Id {request.DeviceId}");

        if (device.Coins == null) device.Coins = new List<OperationWithCoinsDto>();

        if (device.DeviceType == DeviceType.PC)
        {
            if (request.CoinType == CoinType.Bitcoin
                || request.CoinType == CoinType.Ethereum
                || request.CoinType == CoinType.Litecoin
                || request.CoinType == CoinType.BitcoinCash
                || request.CoinType == CoinType.Monero)
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.CoinType,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }
        if (device.DeviceType == DeviceType.Laptop)
        {
            if (request.CoinType == CoinType.Dash
                || request.CoinType == CoinType.Zcash
                || request.CoinType == CoinType.VertCoin
                || request.CoinType == CoinType.BitShares
                || request.CoinType == CoinType.Factom)
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.CoinType,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }
        if (device.DeviceType == DeviceType.VideoCard)
        {
            if (request.CoinType == CoinType.NEM
                || request.CoinType == CoinType.Dogecoin
                || request.CoinType == CoinType.MaidSafeCoin
                || request.CoinType == CoinType.DigiByte
                || request.CoinType == CoinType.Nautiluscoin)
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.CoinType,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }
        if (device.DeviceType == DeviceType.ASIC)
        {
            if (request.CoinType == CoinType.Clams
                || request.CoinType == CoinType.Siacoin
                || request.CoinType == CoinType.Decred
                || request.CoinType == CoinType.VeriCoin
                || request.CoinType == CoinType.Einsteinium)
            {
                device.Coins.Add(new OperationWithCoinsDto()
                {
                    CoinType = request.CoinType,
                    Quantity = request.Quantity,
                });

                return _devicesRepository.GenerateCoinWithDevice(device);
            }

            throw new ValidationException("Тип валюты не соответсвует выбранному устройству");
        }

        throw new NotFoundException("Неизвестный тип девайса");
    }
}
