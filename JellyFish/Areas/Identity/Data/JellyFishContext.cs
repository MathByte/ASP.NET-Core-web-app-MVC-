using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JellyFish.Areas.Identity.Data;

public class JellyFishContext : IdentityDbContext<JellyFishUser>
{
    public JellyFishContext(DbContextOptions<JellyFishContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "919d93a8-3810-4ee4-8b51-a6b0a1605508",
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR"
        });
      builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "919d93a8-3810-4ee4-8b51-a6b0a1605509",
            Name = "JobSeeker",
            NormalizedName = "JOBSEEKER"
        });
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "919d93a8-3810-4ee4-8b51-a6b0a1605510",
            Name = "Employer",
            NormalizedName = "EMPLOYER"
        });
    }
}
