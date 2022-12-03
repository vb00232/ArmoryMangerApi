﻿using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class BulletPurchase
{
    public long Id { get; set; }

    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }

    public long Remaining { get; set; }

    public long UserId { get; set; }

    public long BulletTemplateId { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;

    public virtual BulletTemplate BulletTemplate { get; set; } = null!;

    public virtual ICollection<Reload> Reloads { get; } = new List<Reload>();

    public virtual User User { get; set; } = null!;
}