using Autofac;
using MediatR;
using PingDong.Application.Dependency;
using PingDong.Newmoon.Events.Service.Commands.Mediator;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Service
{
    public class ModuleRegistrar : AutofacDependencyRegistrar
    {
        public ModuleRegistrar() : base(DependecyType.Service)
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = base.Configuration["SqlServer_ConnectionString"];

            // Event
            builder.Register(c => new Queries.EventQuery(connectionString))
                    .As<IEventQuery>()
                    .InstancePerLifetimeScope();
            // Place
            builder.Register(c => new PlaceQuery(connectionString))
                .As<IPlaceQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            // For demostration purpose only, request is validated in the ASP.Net pipeline before hitting controller.
            // MediatR behavior happens after call send in controller
            //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
