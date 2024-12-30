using Autofac;
using HotelManagement.Core.Interfaces;
using HotelManagement.Repository;
using HotelManagement.Service;

namespace HotelManagement.Config;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(AuthenticationService).Assembly)
            .Where(a => a.Name.EndsWith("Service"))
            .AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}