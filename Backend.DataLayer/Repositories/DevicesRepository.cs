using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Responses;
using Serilog;

namespace Backend.DataLayer.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    private readonly ILogger _logger = Log.ForContext<DevicesRepository>();

    public DevicesRepository(MainerWomanContext context) : base(context)
    {
    }

    public Guid AddDevice(DeviceDto device)
    {
        _ctx.Devices.Add(device);
        _ctx.SaveChanges();

        return device.Id;
    }

    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.FirstOrDefault(d => d.Id == id);

    public List<DeviceDto> GetDevicesByOwnerId(Guid ownerId)
    {
        _logger.Information($"Ищем девайсы по айди пользователя {ownerId}.");

        var devices = _ctx.Devices.Where(d => d.Owner.Id == ownerId).ToList();
        if (devices == null)
        {
            devices = new List<DeviceDto>();
            _logger.Information($"У пользователя {ownerId} нет девайсов. Создаём пустой список.");
        }

        _logger.Information($"Отправляем список девайсов для пользователя {ownerId}.");
        return devices;
    }

    public void DeleteDeviceById(DeviceDto device)
    {
        _ctx.Devices.Remove(device);
        _ctx.SaveChanges();
    }

    public CoinIdResponse GenerateCoinWithDevice(DeviceDto device)
    {
        _ctx.Devices.Update(device);
        _ctx.SaveChanges();
        CoinIdResponse response = new CoinIdResponse() { Id = device.Id };

        return response;
    }
}