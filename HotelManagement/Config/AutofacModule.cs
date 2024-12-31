using Autofac;
using HotelManagement.Application.Features.RoomManagement.Rooms.Query;
using HotelManagement.Core.Interfaces;
using HotelManagement.Repository;
using HotelManagement.Service;
using MediatR;

namespace HotelManagement.Config;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(GetAllRoomsQueryHandler).Assembly)
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(AuthenticationService).Assembly)
            .Where(a => a.Name.EndsWith("Service"))
            .AsImplementedInterfaces().InstancePerLifetimeScope();


    }
}