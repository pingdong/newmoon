using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace PingDong.Newmoon.IdentityServer.Identity.Migrations
{
    public class DesignTimeFactory
    {
        public class ConfigurationContextDesignTimeFactory : DesignTimeDbContextFactoryBase<ConfigurationDbContext>
        {
            public ConfigurationContextDesignTimeFactory()
                : base("DefaultDbConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
            {
            }

            protected override ConfigurationDbContext CreateNewInstance(DbContextOptions<ConfigurationDbContext> options)
            {
                var configOption = new ConfigurationStoreOptions { DefaultSchema = IdentityDbContextConfig.DefaultSchema };

                return new ConfigurationDbContext(options, configOption);
            }
        }

        public class PersistedGrantContextDesignTimeFactory : DesignTimeDbContextFactoryBase<PersistedGrantDbContext>
        {
            public PersistedGrantContextDesignTimeFactory()
                : base("DefaultDbConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
            {
            }

            protected override PersistedGrantDbContext CreateNewInstance(DbContextOptions<PersistedGrantDbContext> options)
            {
                var operationOption = new OperationalStoreOptions { DefaultSchema = IdentityDbContextConfig.DefaultSchema };

                return new PersistedGrantDbContext(options, operationOption);
            }
        }
    }
}
