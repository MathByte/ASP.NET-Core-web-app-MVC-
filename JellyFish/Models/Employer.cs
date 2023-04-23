using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Employer
{
    public string EmployerId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual AspNetUser EmployerNavigation { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; } = new List<Job>();
}
