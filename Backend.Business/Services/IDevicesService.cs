using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Backend.Core.Models.Devices.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Business.Services;

public interface IDevicesService
{
    ////метод для отображения выпадающего списка в свагере
    //Guid AddDevice(Guid ownerId, DeviceType deviceType, string deviceName);
    Guid AddDevice(Guid ownerId, AddDeviceRequest request);
    DeviceDto GetDeviceById(Guid id);
    void DeleteDeviceById(Guid id);
    List<DeviceDto> GetDevices();
    List<DeviceDto> GetDevicesByOwnerId(Guid ownerId);
    IdOperationWithCoinsResponse GenerateCoinsWithDevice([FromRoute] Guid deviceId, GenerateCoinsWithDeviceRequest request);
}