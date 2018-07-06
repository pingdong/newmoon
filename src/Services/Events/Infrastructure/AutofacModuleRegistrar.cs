using Autofac;
using PingDong.Application.Dependency;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Infrastructure.Repositories;

namespace PingDong.Newmoon.Events.Infrastructure
{
    public class AutofacModuleRegistrar : AutofacDependencyRegistrar
    {
        public AutofacModuleRegistrar() : base(DependecyType.Infrastructure)
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventRepository>()
                .As<IEventRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PlaceRepository>()
                .As<IPlaceRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
