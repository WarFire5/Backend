using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    public DevicesRepository(MainerWomanContext context) : base(context)
    {
    }

    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.FirstOrDefault(d => d.Id == id);
    public DeviceDto GetDeviceByOwnerId(Guid ownerId) => _ctx.Devices.FirstOrDefault(d => d.Owner.Id == ownerId);

    public Guid CreateDevice(DeviceDto device)
    {
        _ctx.Devices.Add(device);
        _ctx.SaveChanges();

        return device.Id;
    }

    public void DeleteDeviceById(Guid id)
    {
        var device = GetDeviceById(id);

        _ctx.Devices.Remove(device);
        _ctx.SaveChanges();
    }
}
