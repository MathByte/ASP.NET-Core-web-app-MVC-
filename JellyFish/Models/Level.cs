using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Level
{
    public int Id { get; set; }

    public string? LevelName { get; set; }

    public virtual ICollection<Job> Jobs { get; } = new List<Job>();
}
