using Application;
using Autofac;
using Domain;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.ioC
{
    public class IocModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //仓储注入
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                .As(typeof(IRepository<,>))
                .InstancePerDependency();
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            //服务注入
            builder.RegisterAssemblyTypes(typeof(IService<,>).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IService<>).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

    }
}
