using System.Reflection;
using Autofac;
using GraphQL;
using GraphQL.Types;
using MediatR;
using PingDong.Application.Dependency;
using PingDong.Newmoon.Events.Service.Commands.Mediator;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Service
{
    public class AutofacModuleRegistrar : AutofacDependencyRegistrar
    {
        public AutofacModuleRegistrar() : base(DependecyType.Service)
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

            // GraphQL
            var asm = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(asm)
                    .PublicOnly()
                    .Where(t => t.Name.EndsWith("GraphType"))
                    .AsSelf();
            
            builder.RegisterType<GraphQL.EventsQuery>().AsSelf();
            builder.RegisterType<GraphQL.EventsSchema>().As<ISchema>();
            
            builder.Register<IDependencyResolver>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return new FuncDependencyResolver(type => context.Resolve(type));
            });
        }
    }
}
