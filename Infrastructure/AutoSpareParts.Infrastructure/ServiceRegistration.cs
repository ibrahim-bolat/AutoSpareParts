using AutoSpareParts.Application.Models;
using AutoSpareParts.Application.Services;
using AutoSpareParts.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoSpareParts.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        //configure
        serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        //services
        serviceCollection.AddTransient<IEmailService, EmailService>();
    }
}