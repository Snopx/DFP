using Application;
using Autofac;
using Domain;
using Domain.InterFace;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.UnitOfWorkFolder;

namespace Infrastructure.ioC
{
    public class IocModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //UnitOfWork
            builder.RegisterType<DFDbContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
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
