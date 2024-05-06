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

    public DeviceDto GetDeviceByOwnerId(Guid ownerId) => _ctx.Devices.FirstOrDefault(d => d.Owner.Id == ownerId);

    public void DeleteDeviceById(Guid id)
    {
        var device = GetDeviceById(id);

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