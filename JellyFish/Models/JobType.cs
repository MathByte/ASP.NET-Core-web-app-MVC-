using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class JobType
{
    public int JobTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Job> Jobs { get; } = new List<Job>();
}
