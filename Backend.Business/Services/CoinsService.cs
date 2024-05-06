using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Backend.DataLayer.Repositories;

namespace Backend.Business.Services;

public class CoinsService : ICoinsService
{
    private readonly ICoinsRepository _coinsRepository;
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    public CoinsService(ICoinsRepository coinsRepository, IDevicesRepository devicesRepository, IUsersRepository usersRepository)
    {
        _coinsRepository = coinsRepository;
        _devicesRepository = devicesRepository;
        _usersRepository = usersRepository;
    }

    //public DeviceDto GetCoinTypeByDeviceType(GetCoinTypeByDeviceTypeRequest request)
    //{
    //    throw new NotImplementedException();
    //}

    //public CoinIdResponse GenerateCoinWithDevice(GenerateCoinWithDeviceRequest request)
    //{
    //    throw new NotFoundException("тип девайса неизвестен");
    //}
}
