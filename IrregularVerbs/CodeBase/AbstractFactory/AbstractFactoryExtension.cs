using System;
using Microsoft.Extensions.DependencyInjection;

namespace IrregularVerbs.CodeBase.AbstractFactory;

public static class AbstractFactoryExtension
{
    public static IServiceCollection AddAbstractFactory<T>(this IServiceCollection services) where T : class
    {
        services.AddTransient<T>();
        services.AddTransient<Func<T>>(serviceProvider => serviceProvider.GetRequiredService<T>);
        services.AddTransient<IAbstractFactory<T>, AbstractFactory<T>>();

        return services;
    }
}