using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using PingDong.Newmoon.IdentityServer.Infrastructure.Configuration;

namespace PingDong.Newmoon.IdentityServer.Identity
{
    public class IdentityConfigurationSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context, AppSettings settings)
        {
            if (!context.ApiResources.Any())
            {
                foreach (var resource in IdentityServerConfig.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfig.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.Clients.Any())
            {
                foreach (var client in IdentityServerConfig.GetClients(settings))
                {
                    context.Clients.Add(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
