using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Address
{
    public string AddressId { get; set; } = null!;

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? Province { get; set; }

    public virtual AspNetUser AddressNavigation { get; set; } = null!;
}
