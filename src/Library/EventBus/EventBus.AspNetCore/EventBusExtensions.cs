using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PingDong.EventBus.Abstractions;
using PingDong.Reflection;

namespace PingDong.EventBus
{
    /// <summary>
    /// Extension to subscribe integration events
    /// </summary>
    public static class EventBusExtensions
    {
        /// <summary>
        /// Subscribe to integration events
        /// </summary>
        /// <param name="assemblies">Assemblies used to search event registrar</param>
        public static void SubscribeIntegrationEvents(this IApplicationBuilder app, IEnumerable<Assembly> assemblies)
        {
            var eventBus = app.ApplicationServices.GetService<IEventBus>();
            if (eventBus == null)
                return;

            if (assemblies == null || !assemblies.Any())
                return;
            var eventBusRegistrars = assemblies.FindInterfaces<IEventBusSubscription>();

            eventBusRegistrars.ForEach(reg => reg.Subscribe(eventBus));
        }
    }
}
