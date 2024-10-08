using System.Net;
using AutoSpareParts.Application;
using AutoSpareParts.Application.Extensions;
using AutoSpareParts.Infrastructure;
using AutoSpareParts.MVC;
using AutoSpareParts.MVC.Configurations.RateLimit;
using AutoSpareParts.MVC.Extensions;
using AutoSpareParts.Persistence;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration, builder.Host);


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler($"/Error/Index?statusCode={(int)HttpStatusCode.InternalServerError}");
    //alttakinide kullanabilirsin
    //app.CustomExceptionHandler(); 
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseSerilogRequestLogging();
//app.UseHttpLogging();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Auto update migration
await app.MigrateDatabaseAsync();

// Auto seed authorize endpoints data
await app.AuthorizeEndpointsMigrateAsync(typeof(Program));

// Add user_name info to serilog LogContext
//app.AddUserIdtoSeriLogContext();

// Use Rate Limiting
app.UseRateLimiter();

// Configure Localization
app.ConfigureLocalization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .RequireRateLimiting("CustomRateLimitPolicy");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireRateLimiting("CustomRateLimitPolicy");
app.Run();