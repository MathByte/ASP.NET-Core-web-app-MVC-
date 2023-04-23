using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; } = new List<Job>();
}
