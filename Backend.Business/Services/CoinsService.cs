using AutoMapper;
using Backend.Core.Enums;
using Backend.Core.Models.Coins.Responses;
using Backend.DataLayer.Repositories;

namespace Backend.Business.Services;

public class CoinsService : ICoinsService
{
    private readonly ICoinsRepository _coinsRepository;
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public CoinsService(ICoinsRepository coinsRepository, IDevicesRepository devicesRepository, IUsersRepository usersRepository, IMapper mapper)
    {
        _coinsRepository = coinsRepository;
        _devicesRepository = devicesRepository;
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public List<CoinTypeResponse> GetCoinTypesByDeviceType(DeviceType deviceType)
    {
        var listOperationWithCoins = _coinsRepository.GetCoinTypesByDeviceType(deviceType);
        var result = _mapper.Map<List<CoinTypeResponse>>(listOperationWithCoins);

        return result;
    }

    public List<OperationWithCoinsResponse> GetOperationsWithCoins()
    {
        var operations = _coinsRepository.GetOperationsWithCoins();
        var result = _mapper.Map<List<OperationWithCoinsResponse>>(operations);

        return result;
    }

    public List<OperationWithCoinsResponse> GetOperationsWithCoinsByDeviceId(Guid deviceId)
    {
        var OperationWithCoinsDto = _coinsRepository.GetOperationsWithCoinsByDeviceId(deviceId);
        var result = _mapper.Map<List<OperationWithCoinsResponse>>(OperationWithCoinsDto);

        return result;
    }

    public List<OperationWithCoinsResponse> GetOperationsWithCoinsForCoinTypeByDeviceId(Guid deviceId, CoinType coinType)
    {
        var OperationWithCoinsDto = _coinsRepository.GetOperationsWithCoinsForCoinTypeByDeviceId(deviceId, coinType);
        var result = _mapper.Map<List<OperationWithCoinsResponse>>(OperationWithCoinsDto);

        return result;
    }

    public CoinTypeAndQuantityResponse GetCoinQuantityFromCurrentTypeForCurrentDeviceId(Guid deviceId, CoinType coinType)
    {
        var operationList = GetOperationsWithCoinsForCoinTypeByDeviceId(deviceId, coinType);
        int quantity = 0;

        foreach (var operation in operationList)
        {
            quantity += operation.Quantity;
        }

        var response = new CoinTypeAndQuantityResponse()
        {
            Quantity = quantity,
            CoinType = coinType
        };

        return response;
    }

    public List<CoinTypeAndQuantityResponse> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId)
    {
        var listOfCoins = _coinsRepository.GetListCoinTypesWithQuantityByDeviceId(deviceId);
        var result = _mapper.Map<List<CoinTypeAndQuantityResponse>>(listOfCoins);

        return result;
    }

    public List<CoinTypeAndQuantityResponse> GetListCoinTypesWithQuantityByOwnerId(Guid ownerId)
    {
        var listOfCoins = _coinsRepository.GetListCoinTypesWithQuantityByOwnerId(ownerId);
        var result = _mapper.Map<List<CoinTypeAndQuantityResponse>>(listOfCoins);

        return result;
    }
}
