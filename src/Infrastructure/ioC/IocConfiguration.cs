using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.ioC
{
    public class IocConfiguration
    {
        public static IServiceProvider UseIoc(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<IocModule>();

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
