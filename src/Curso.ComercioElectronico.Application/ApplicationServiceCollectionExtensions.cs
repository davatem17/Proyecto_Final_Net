using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ComercioElectronico.Application;

public static class ApplicationServiceCollectionExtensions
{

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {

        services.AddTransient<IMarcaAppService, MarcaAppService>();

   
        //Configurar todas las validaciones
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;

    }
}