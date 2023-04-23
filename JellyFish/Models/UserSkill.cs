using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class UserSkill
{
    public int UserSkillId { get; set; }

    public int SkillId { get; set; }

    public string UserId { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
