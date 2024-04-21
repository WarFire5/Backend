using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    public DevicesRepository(MainerWomanContext context) : base(context)
    {
    }

    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.FirstOrDefault(d => d.Id == id);
    public DeviceDto GetDeviceByOwnerId(Guid ownerId) => _ctx.Devices.FirstOrDefault(d => d.Owner.Id == ownerId);
}
