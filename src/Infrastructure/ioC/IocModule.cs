using Application;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Domain.Interface;
using Infrastructure.aop;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.UnitOfWorkFolder;

namespace Infrastructure.ioC
{
    public class IocModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //aop
            builder.RegisterType<BaseAop>();
            //UnitOfWork
            builder.RegisterType<DFDbContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            //仓储注入
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                .As(typeof(IRepository<,>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            //服务注入
            builder.RegisterAssemblyTypes(typeof(IService<,>).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(BaseAop));
            builder.RegisterAssemblyTypes(typeof(IService<>).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(BaseAop));

        }

    }
}
