using System.Reflection;
using Autofac;
using GraphQL;
using GraphQL.Types;
using PingDong.Reflection;
using PingDong.Application.Dependency;
using PingDong.DomainDriven.Service.GraphQL;

namespace PingDong.Newmoon.Events.Service
{
    public class GraphQLModuleRegistrar : AutofacDependencyRegistrar
    {
        public GraphQLModuleRegistrar() : base(DependecyType.Service)
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            var types = Assembly.GetExecutingAssembly().FindAttribute<GraphTypeAttribute>(typeof(GraphTypeAttribute));
            types.ForEach(t => builder.RegisterType(t).SingleInstance());
            
            builder.RegisterType<GraphQL.EventsSchema>().As<ISchema>().SingleInstance();

            builder.Register<IDependencyResolver>(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    return new FuncDependencyResolver(type => context.Resolve(type));
                });
        }
    }
}
