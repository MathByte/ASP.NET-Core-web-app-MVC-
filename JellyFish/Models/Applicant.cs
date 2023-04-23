using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Applicant
{
    public int ApplicantId { get; set; }

    public int JobId { get; set; }

    public string UserId { get; set; } = null!;

    public int IsAccepted { get; set; }

    public bool? IsApplied { get; set; }

    public bool? IsSaved { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
