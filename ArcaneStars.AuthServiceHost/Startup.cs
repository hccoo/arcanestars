using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ArcaneStars.AuthServiceHost.Configurations;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.DbContexts;
using System;
using IdentityServer4.EntityFramework.Mappers;

namespace ArcaneStars.AuthServiceHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ServiceDb");
            var identityServerMigrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                    .AddSigningCredential(new X509Certificate2(@"./certificates/gooios.pfx", "!QAZ2wsx098", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable))
                    .AddTestUsers(DataConfiguration.Users.ToList())
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseMySql(connectionString, sql => sql.MigrationsAssembly(identityServerMigrationsAssembly));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseMySql(connectionString, sql => sql.MigrationsAssembly(identityServerMigrationsAssembly));
                        options.EnableTokenCleanup = true;
                        options.TokenCleanupInterval = 7200;
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.UseIdentityServer();
        }


        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {

                Console.WriteLine("开始初始化 PersistedGrantDbContext ...");
                #region PersistedGrantDbContext

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                #endregion

                Console.WriteLine("开始初始化 ConfigurationDbContext ...");
                #region ConfigurationDbContext 

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();



                Console.WriteLine("开始初始化 IdentityResources ...");
                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in DataConfiguration.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                Console.WriteLine("开始初始化 ApiResources ...");
                if (!context.ApiResources.Any())
                {
                    foreach (var resource in DataConfiguration.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                Console.WriteLine("开始初始化 ApiScopes ...");
                if (!context.ApiScopes.Any())
                {
                    foreach (var scope in DataConfiguration.ApiScopes)
                    {
                        context.ApiScopes.Add(scope.ToEntity());
                    }
                    context.SaveChanges();
                }

                Console.WriteLine("开始初始化 Clients ...");
                if (!context.Clients.Any())
                {
                    foreach (var client in DataConfiguration.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                #endregion


            }
        }
    }
}
