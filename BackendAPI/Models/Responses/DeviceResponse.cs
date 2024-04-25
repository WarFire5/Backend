using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.API.Models.Responses;

public class DeviceResponse
{
    public Guid Id { get; set; }
    public string DeviceName { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Address { get; set; }
}
