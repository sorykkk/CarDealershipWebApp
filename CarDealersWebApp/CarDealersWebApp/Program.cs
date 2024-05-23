//using CarDealersWebApp.Data.Context;
//using CarDealersWebApp.Data.Contracts;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Repositories;
using CarDealersWebApp.Models.Validation;
using CarDealersWebApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<DapperContext>();
//builder.Services.AddScoped<IDealerRepository, DealerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IImageDriveService, ImageDriveService>();
builder.Services.AddControllers();
//
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//

//
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DealerOnly", policy => policy.RequireRole("Dealer"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
});
//

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

//
app.UseSession();
//

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
