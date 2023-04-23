using System;
using System.Collections.Generic;

namespace JellyFish.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
