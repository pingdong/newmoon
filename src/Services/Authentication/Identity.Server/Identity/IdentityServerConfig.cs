using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using PingDong.Newmoon.IdentityServer.Configuration;

namespace PingDong.Newmoon.IdentityServer.Identity
{
    public class IdentityServerConfig
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            // http://docs.identityserver.io/en/release/reference/identity_resource.html
            return new List<IdentityResource>
                {
                    new IdentityResources.Email(),
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            // http://docs.identityserver.io/en/release/reference/api_resource.html
            return new List<ApiResource>
                {
                    new ApiResource (name: "events.api", displayName: "Events API")
                };
        }

        // http://docs.identityserver.io/en/release/reference/client.html
        public static IEnumerable<Client> GetClients(AppSettings settings)
        {
            // client credentials client
            return new List<Client>
                {
                    new Client
                    {
                        ClientId = "postman",
                        ClientName = "Postman",
                        ClientSecrets = { new Secret("postman".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        // TODO: Enable consent
                        RequireConsent = false,
                        RedirectUris = { $"{settings.ExternalServices.EventsService}" },
                        PostLogoutRedirectUris = { $"{settings.ExternalServices.EventsService}" },
                        AllowedScopes =
                            {
                                "events.api"
                            }
                    },
                    new Client
                    {
                        ClientId = "swagger",
                        ClientName = "Swagger Client",
                        ClientSecrets = { new Secret("swagger".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        // TODO: Enable consent
                        RequireConsent = false,
                        EnableLocalLogin = true,
                        RedirectUris = { $"{settings.ExternalServices.EventsService}/swagger/oauth2-redirect.html" },
                        PostLogoutRedirectUris = { $"{settings.ExternalServices.EventsService}/swagger" },
                        AllowedCorsOrigins = { $"{settings.ExternalServices.EventsService}" },
                        AllowedScopes =
                            {
                                "events.api",
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Email,
                                IdentityServerConstants.StandardScopes.Profile
                            }
                    },
                    new Client
                    {
                        ClientId = "events_api-client",
                        ClientName = "Events Client",
                        ClientSecrets = { new Secret("events_api-seCrEt".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        // TODO: Enable consent
                        RequireConsent = false,
                        RedirectUris = { $"{settings.ExternalServices.EventsService}/signin-oidc" },
                        PostLogoutRedirectUris = { $"{settings.ExternalServices.EventsService}/signout-callback-oidc" },
                        AllowedScopes =
                            {
                                "events.api",
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Email,
                                IdentityServerConstants.StandardScopes.Profile
                            }
                    }
                };
        }
    }
}
