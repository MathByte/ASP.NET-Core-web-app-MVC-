using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public string? Logo { get; set; }

    public virtual ICollection<Employer> Employers { get; } = new List<Employer>();
}
