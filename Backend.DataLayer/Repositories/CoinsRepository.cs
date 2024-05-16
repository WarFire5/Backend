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

    public List<OperationWithCoinsDto> GetCoinTypesByDeviceType(DeviceType deviceType)
    {
        _logger.Information($"Ищем типы коинов по типу девайса {deviceType}.");
        var coinTypes = _ctx.OperationsWithCoins.Include(o => o.Device.Owner).Where(o => o.Device.DeviceType == deviceType).ToList();
        if (coinTypes == null)
        {
            coinTypes = new List<OperationWithCoinsDto>();
            _logger.Information($"У девайса типа {deviceType} нет привязанных типов. Создаём пустой список.");
        }

        _logger.Information($"Отправляем список типов коинов для девайса типа {deviceType}.");
        return coinTypes;
    }

    public List<OperationWithCoinsDto> GetOperationsWithCoins()
    {
        return _ctx.OperationsWithCoins.ToList();
    }

    public List<OperationWithCoinsDto> GetOperationsWithCoinsByDeviceId(Guid deviceId)
    {
        _logger.Information($"Ищем операции с коинами по айди девайса {deviceId}.");
        var coins = _ctx.OperationsWithCoins.Include(o => o.Device.Owner).Where(o => o.Device.Id == deviceId).ToList();
        if (coins == null)
        {
            coins = new List<OperationWithCoinsDto>();
            _logger.Information($"У девайса с айди {deviceId} нет операций. Создаём пустой список.");
        }

        _logger.Information($"Отправляем список операций для девайса с айди {deviceId}.");
        return coins;
    }

    public List<OperationWithCoinsDto> GetOperationsWithCoinsForCoinTypeByDeviceId(Guid deviceId, CoinType type)
    {
        _logger.Information($"Ищем операции с коинами по айди девайса {deviceId}.");
        var coins = _ctx.OperationsWithCoins.Include(o => o.Device.Owner).Where(o => o.Device.Id == deviceId && o.CoinType == type).ToList();
        if (coins == null)
        {
            coins = new List<OperationWithCoinsDto>();
            _logger.Information($"У девайса с айди {deviceId} нет операций. Создаём пустой список.");
        }

        _logger.Information($"Отправляем список операций для девайса с айди {deviceId}.");
        return coins;
    }

    public List<OperationWithCoinsDto> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId)
    {
        _logger.Information($"Ищем операции с коинами по айди девайса {deviceId}.");
        var listCoinDtos = _ctx.OperationsWithCoins
                               .Where(o => o.Device.Id == deviceId)
                               .GroupBy(o => o.CoinType)
                               .Select(g => new OperationWithCoinsDto
                               {
                                   CoinType = g.Key,
                                   Quantity = g.Sum(o => o.Quantity)
                               })
                               .ToList();
        //if (coins == null)
        //{
        //    coins = new List<OperationWithCoinsDto>();
        //    _logger.Information($"У девайса с айди {deviceId} нет операций. Создаём пустой список.");
        //}

        _logger.Information($"Отправляем список операций для девайса с айди {deviceId}.");
        return listCoinDtos;
    }

    public List<OperationWithCoinsDto> GetListCoinTypesWithQuantityByOwnerId(Guid ownerId)
    {
        var operationsByOwner = _ctx.OperationsWithCoins
            .Where(o => o.Device.Owner.Id == ownerId)
            .GroupBy(o => o.CoinType)
            .Select(g => new OperationWithCoinsDto
            {
                CoinType = g.Key,
                Quantity = g.Sum(o => o.Quantity)
            })
            .ToList();

        return operationsByOwner;
    }
}