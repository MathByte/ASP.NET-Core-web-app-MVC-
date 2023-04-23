using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Job
{
    public int JobId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Location { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsOpen { get; set; }

    public int CategoryId { get; set; }

    public int JobTypeId { get; set; }

    public int LevelId { get; set; }

    public bool? IsRemote { get; set; }

    public string EmployerId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; } = new List<Applicant>();
    
    [ValidateNever]
    public virtual Category Category { get; set; } = null!;
    
    [ValidateNever]
    public virtual Employer Employer { get; set; } = null!;
    
    [ValidateNever]
    public virtual JobType JobType { get; set; } = null!;
    
    [ValidateNever]
    public virtual Level Level { get; set; } = null!;
}
