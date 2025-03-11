﻿using System;
using System.Collections.Generic;

namespace style.Entities;

public partial class Servicecategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
