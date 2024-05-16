using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Coins.Responses;

namespace Backend.Business.Services;

public interface ICoinsService
{
    List<CoinTypeResponse> GetCoinTypesByDeviceType(DeviceType deviceType);
    List<OperationWithCoinsResponse> GetOperationsWithCoins();
    List<OperationWithCoinsResponse> GetOperationsWithCoinsByDeviceId(Guid deviceId);
    List<OperationWithCoinsResponse> GetOperationsWithCoinsForCoinTypeByDeviceId(Guid deviceId, CoinType coinType);
    CoinTypeAndQuantityResponse GetCoinQuantityFromCurrentTypeForCurrentDeviceId(Guid deviceId, CoinType coinType);
    List<CoinTypeAndQuantityResponse> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId);
    List<CoinTypeAndQuantityResponse> GetListCoinTypesWithQuantityByOwnerId(Guid ownerId);
}