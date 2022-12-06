﻿namespace ArmoryManagerApi.DataTransferObjects.BulletPurchaseDtos;

public class BulletPurchaseDto
{
    public long Id { get; set; }

    public long? Make { get; set; }

    public string Calibre { get; set; } = null!;

    public string? Model { get; set; }

    public long Grain { get; set; }

    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }

    public long Remaining { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;
}
