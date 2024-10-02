using System.Reflection;
using AutoSpareParts.Application.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AutoSpareParts.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection  serviceCollection,IConfiguration configuration)
    {
        //auto mapper
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        //mediatR
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        //fluent validation
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddFluentValidationClientsideAdapters();
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        

        //HttpContext and UrlHelper
        serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddScoped(x =>
                x.GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext!));
        
        
        //for Authorize Filters
        //serviceCollection.AddControllers(options => options.Filters.Add<AuthorizeEndpointsFilter>());
    }
}