using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.DataLayer.Repositories;

public interface ICoinsRepository
{
    List<OperationWithCoinsDto> GetOperationsWithCoins();
    List<OperationWithCoinsDto> GetOperationWithCoinsByDeviceId(Guid deviceId);
    List<OperationWithCoinsDto> GetOperationWithCoinsByDeviceIdFromCoinType(Guid deviceId, CoinType type);
}