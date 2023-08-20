using Identity.WebApp.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Config Identity Core

builder.Services.AddIdentityCore<MyUser>(options => { })
	.AddUserManager<UserManagerBase<MyUser>>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<IUserStore<MyUser>, MyUserStore>();


builder.Services.AddAuthentication("cookies")
	.AddCookie("cookies", options => 
	options.LoginPath = "/Home/Login"
	);

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
