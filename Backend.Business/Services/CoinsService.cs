using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Exceptions;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Backend.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    //public DeviceDto GetCoinTypeByDeviceType(GetCoinTypeByDeviceTypeRequest request)
    //{
    //    throw new NotImplementedException();
    //}

    public List<OperationWithCoinsDto> GetOperationsWithCoins()
    {
        // здесь есть бизнес логика
        return _coinsRepository.GetOperationsWithCoins();
    }

    public List<OperationWithCoinsResponse> GetOperationWithCoinsByDeviceId(Guid deviceId)
    {
        var OperationWithCoinsDto = _coinsRepository.GetOperationWithCoinsByDeviceId(deviceId);
        var result = _mapper.Map<List<OperationWithCoinsResponse>>(OperationWithCoinsDto);

        return result;
    } 
    
    public List<OperationWithCoinsResponse> GetOperationWithCoinsByDeviceIdFromCoinType(Guid deviceId, CoinType coinType)
    {
        var OperationWithCoinsDto = _coinsRepository.GetOperationWithCoinsByDeviceIdFromCoinType(deviceId, coinType);
        var result = _mapper.Map<List<OperationWithCoinsResponse>>(OperationWithCoinsDto);

        return result;
    }

    public CoinQuantityForCoinTypeResponse GetCoinQuantityFromCurrentTypeForCurrentDeviceId(Guid deviceId, CoinType coinType)
    {
        var operationList = GetOperationWithCoinsByDeviceIdFromCoinType(deviceId, coinType);
        int quantity = 0;

        foreach (var operation in operationList)
        {
            quantity += operation.Quantity;
        }

        var response = new CoinQuantityForCoinTypeResponse()
        {
            Quantity = quantity,
            CoinType = coinType
        };

        return response;
    }

    ////получить лист типов коинов с количеством коинов с одного девайса
    //public ListCoinTypesWithQuantityResponse GetListCoinTypesWithQuantityByDeviceId(Guid deviceId, CoinType coinType)
    //{
    //    var typeQuantityList = GetCoinQuantityFromCurrentTypeForCurrentDeviceId(deviceId, coinType);

    //    return new ListCoinTypesWithQuantityResponse();
    //}  
    
    //получить лист типов коинов с количеством коинов с одного девайса
    public List<CoinTypesWithQuantityResponse> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId)
    {
        var listOfCoins = _coinsRepository.GetListCoinTypesWithQuantityByDeviceId(deviceId);

        var result = _mapper.Map<List<CoinTypesWithQuantityResponse>>(listOfCoins);

        return result;
    }

     
    //получить лист типов коинов с количеством коинов со всех девайсов пользователя
    public ListCoinTypesWithQuantityResponse GetListCoinTypesWithQuantityByOwnerId(Guid ownerId, Guid deviceId, CoinType coinType)
    {
        var typeQuantityList = GetCoinQuantityFromCurrentTypeForCurrentDeviceId(deviceId, coinType);

        return new ListCoinTypesWithQuantityResponse();
    }
}
