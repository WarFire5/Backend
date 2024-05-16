using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.DataLayer.Repositories;

public interface ICoinsRepository
{
    List<OperationWithCoinsDto> GetCoinTypesByDeviceType(DeviceType deviceType);
    List<OperationWithCoinsDto> GetOperationsWithCoins();
    List<OperationWithCoinsDto> GetOperationsWithCoinsByDeviceId(Guid deviceId);
    List<OperationWithCoinsDto> GetOperationsWithCoinsForCoinTypeByDeviceId(Guid deviceId, CoinType type);
    List<OperationWithCoinsDto> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId);
    List<OperationWithCoinsDto> GetListCoinTypesWithQuantityByOwnerId(Guid ownerId);
}