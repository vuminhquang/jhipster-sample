using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace JhipsterSample.Configuration;

public static class AutoMapperStartup
{
    public static IServiceCollection AddAutoMapperModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup));
        return services;
    }
}
