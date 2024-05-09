using Backend.Core.DTOs;
using Backend.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.DataLayer.Repositories;

public class CoinsRepository : BaseRepository, ICoinsRepository
{
    private readonly ILogger _logger = Log.ForContext<DevicesRepository>();

    public CoinsRepository(MainerWomanContext context) : base(context)
    {
    }

    public List<OperationWithCoinsDto> GetOperationsWithCoins()
    {
        return _ctx.OperationsWithCoins.ToList();
    }

    public List<OperationWithCoinsDto> GetOperationWithCoinsByDeviceId(Guid deviceId)
    {
        _logger.Information($"Ищем операции с коинами по айди девайса {deviceId}.");

        var coins = _ctx.OperationsWithCoins.Include(o=>o.Device.Owner).Where(o => o.Device.Id==deviceId).ToList();
        if (coins == null)
        {
            coins = new List<OperationWithCoinsDto>();
            _logger.Information($"У девайса с айди {deviceId} нет операций. Создаём пустой список.");
        }

        _logger.Information($"Отправляем список операций для девайса с айди {deviceId}.");
        return coins;
    }   
    
    public List<OperationWithCoinsDto> GetOperationWithCoinsByDeviceIdFromCoinType(Guid deviceId, CoinType type)
    {
        _logger.Information($"Ищем операции с коинами по айди девайса {deviceId}.");

        var coins = _ctx.OperationsWithCoins.Include(o=>o.Device.Owner).Where(o => o.Device.Id==deviceId && o.CoinType == type).ToList();
        if (coins == null)
        {
            coins = new List<OperationWithCoinsDto>();
            _logger.Information($"У девайса с айди {deviceId} нет операций. Создаём пустой список.");
        }

        _logger.Information($"Отправляем список операций для девайса с айди {deviceId}.");
        return coins;
    }
}