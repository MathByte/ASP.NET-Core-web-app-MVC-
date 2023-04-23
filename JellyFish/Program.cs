using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JellyFish.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using JellyFish.Repository.IRepository;
using JellyFish.Repository;
using JellyFish.IService;
using JellyFish.Common;
using JellyFish.Service;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.
var connectionString = builder.Configuration.GetConnectionString("JellyFishContextConnection") ?? throw new InvalidOperationException("Connection string 'JellyFishContextConnection' not found."); 


builder.Services.AddDbContext<JellyFish.Models.JellyFishDbContext>(options =>
 options.UseSqlServer(connectionString));

builder.Services.AddDbContext<JellyFishContext>(options =>
 options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<INotiService, NotiService>();

builder.Services.AddDefaultIdentity<JellyFishUser>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<JellyFishContext>();// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
///builder.Services.AddTransient<IEmailSender, EmailSender>();
var app = builder.Build();// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
 app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles(); app.UseRouting(); app.UseAuthentication();
app.UseAuthorization(); app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();