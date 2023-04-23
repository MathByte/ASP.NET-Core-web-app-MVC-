using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JellyFish.Areas.Identity.Data;

// Add profile data for application users by adding properties to the JellyFishUser class
public class JellyFishUser : IdentityUser
{
    public DateTime? DateOfBirth { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
}

