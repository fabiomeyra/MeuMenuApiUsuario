using MeuMenu.Application.AppServices;
using MeuMenu.Application.Interfaces;
using MeuMenu.CrossCutting;
using MeuMenu.Domain.Interfaces.Context;
using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Interfaces.Utilitarios;
using MeuMenu.Domain.Notificador;
using MeuMenu.Domain.Services;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.Services.Utils;
using MeuMenu.Domain.UoW;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repositories;
using MeuMenu.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MeuMenu.Infra.IoC;

public static class BootStrapper
{
    public static IServiceCollection ResolveDependencias(this IServiceCollection services)
    {
        services.AddScoped<INotificador, Notificador>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<NegocioService>();
        services.AddSingleton<IServicoDeCriptografia, ServicoDeCriptografia>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Serviço com informações do usuário logado
        services.AddScoped<IUsuarioLogadoService, UsuarioLogadoService>();

        // Automapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        services.AddScoped(provider =>
        {
            // pega a mesma instância configurada para a injeção da interface do contexto,
            // assim é possível injetar direto via classe para usar na camada de dados e via interface nas demais camadas
            var service = provider.GetService<IMeuMenuUsuarioContext>() as MeuMenuUsuarioContext;
            return service!;
        });
        services.AddScoped<IMeuMenuUsuarioContext, MeuMenuUsuarioContext>();

        // appServices
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        services.AddScoped<IPerfilAppService, PerfilAppService>();
        
        // services
        services.AddScoped<IUsuarioService, UsuarioService>();

        // repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IPerfilRepository, PerfilRepository>();

        return services;
    }
}