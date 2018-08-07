using Autofac;
using MediatR;
using Microsoft.Extensions.Hosting;
using PingDong.Application.Dependency;
using PingDong.Newmoon.Events.Service.Queries;
using PingDong.Newmoon.Events.Service.Tasks;

namespace PingDong.Newmoon.Events.Service
{
    public class AutofacModuleRegistrar : AutofacDependencyRegistrar
    {
        public AutofacModuleRegistrar() : base(DependecyType.Service)
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = base.Configuration["SqlServer:ConnectionString"];

            // Event
            builder.Register(c => new EventQuery(connectionString))
                    .As<IEventQuery>()
                    .InstancePerLifetimeScope();
            // Place
            builder.Register(c => new PlaceQuery(connectionString))
                .As<IPlaceQuery>()
                .InstancePerLifetimeScope();

            // Background Tasks
            builder.RegisterType<PastEventCleanUp>().As<IHostedService>().SingleInstance();

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            // For demostration purpose only, request is validated in the ASP.Net pipeline before hitting controller.
            // MediatR behavior happens after call send in controller
            //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
