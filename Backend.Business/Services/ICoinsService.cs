using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Business.Services;

public interface ICoinsService
{
    //DeviceDto GetCoinTypeByDeviceType(GetCoinTypeByDeviceTypeRequest request);
    List<OperationWithCoinsDto> GetOperationsWithCoins();
    List<OperationWithCoinsResponse> GetOperationWithCoinsByDeviceId(Guid deviceId);
    List<OperationWithCoinsResponse> GetOperationWithCoinsByDeviceIdFromCoinType(Guid deviceId, CoinType coinType);
    CoinTypeAndQuantityResponse GetCoinQuantityFromCurrentTypeForCurrentDeviceId(Guid deviceId, CoinType coinType);
    List<CoinTypeAndQuantityResponse> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId);
    ListCoinTypeAndQuantityResponse GetListCoinTypesWithQuantityByOwnerId(Guid ownerId, Guid deviceId, CoinType coinType);
}