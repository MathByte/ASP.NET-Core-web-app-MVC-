using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Notification
{
    public int NotiId { get; set; }

    public string? FromUserId { get; set; }

    public string? ToUserId { get; set; }

    public string? NotiHeader { get; set; }

    public string? NotiBody { get; set; }

    public bool? IsRead { get; set; }

    public string? Url { get; set; }

    public DateTime? CreatedDate { get; set; }
}
