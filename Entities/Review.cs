using System;
using System.Collections.Generic;

namespace style.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? ClientId { get; set; }

    public int? ServiceId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateOnly? ReviewDate { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Service? Service { get; set; }
}
