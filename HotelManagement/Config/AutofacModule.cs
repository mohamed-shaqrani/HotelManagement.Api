using Autofac;
using HotelManagement.App.Core.Interfaces;
using HotelManagement.App.Repository;
using HotelManagement.App.Service;

namespace Food.App.API.Config;

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