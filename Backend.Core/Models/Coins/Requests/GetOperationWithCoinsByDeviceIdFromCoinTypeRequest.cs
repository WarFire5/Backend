﻿using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class GetOperationWithCoinsByDeviceIdFromCoinTypeRequest
{
    public Guid DeviceId { get; set; }
    public CoinType CoinType { get; set; }
}