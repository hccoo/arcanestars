using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace ArcaneStars.AuthServiceHost.Configurations
{
    public class DataConfiguration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
       new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
       };

        public static IEnumerable<ApiResource> ApiResources
            => new List<ApiResource> { new ApiResource("jp_api", "JP-API") };

        public static IEnumerable<ApiScope> ApiScopes
            => new List<ApiScope> { new ApiScope("jp_api_scope", "JP-API-SCOPE") };

        public static IEnumerable<Client> Clients =>
            new List<Client> {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "jp_api_scope" }
                },
                new Client
                {
                    ClientId = "jp_wap",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = new List<string>{ GrantTypes.ResourceOwnerPassword.FirstOrDefault() },//GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "jp_api_scope", StandardScopes.OfflineAccess },
                    AccessTokenLifetime = 3600 * 24 * 7,
                    AllowOfflineAccess = true,                         //如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    AbsoluteRefreshTokenLifetime = 2592000,           //RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding, //刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600 * 24 * 14
                }
            };


        public static IEnumerable<TestUser> Users
            => new List<TestUser> { new TestUser { SubjectId = "1", Username = "caohui", Password = "111111" } };
    }
}
