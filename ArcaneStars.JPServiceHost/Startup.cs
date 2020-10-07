using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcaneStars.Infrastructure;
using ArcaneStars.Infrastructure.Events;
using ArcaneStars.JPService.Configurations;
using ArcaneStars.Service.Configurations;
using ArcaneStars.Service.Domain.Events;
using ArcaneStars.Service.Interceptors;
using ArcaneStars.Service.Repositories;
using ArcaneStars.ServiceHost.Configurations;
using ArcaneStars.ServiceHost.Filters;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ArcaneStars.JPServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
                options.Filters.Add<LogFilter>();
                options.Filters.Add<ApiKeyFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);//.AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddOptions();

            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<IServiceConfigurationAgent, ServiceConfigurationAgent>();
            services.AddScoped<IDbUnitOfWork, DbUnitOfWork>();
            services.AddScoped<IDbContextProvider, DbContextProvider>();

            services.AddDbContext<ServiceDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("ServiceDb"));
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                    {
                        o.LoginPath = new PathString("/User/Login");
                        o.AccessDeniedPath = new PathString("/Error/Forbidden");
                    });
            services.AddMediatR(typeof(Startup).Assembly, typeof(IEventBus).Assembly, typeof(IDomainEvent).Assembly);
            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMapperConfig));

            //services.AddTransient<IUserAppService, UserAppService>();
            //services.AddTransient<IUserRepository, UserRepository>();

            //services.AddTransient<IDomainEventHandler<VerificationCreatedEvent>, VerificationCreatedEventHandler>();

            services.ConfigureDynamicProxy(config =>
            {
                config.Interceptors.AddTyped<ExceptionInterceptor>(m => m.DeclaringType.Name.EndsWith("AppService"));
                config.Interceptors.AddTyped<TransactionInterceptor>(m => m.DeclaringType.Name.EndsWith("AppService"));
                config.Interceptors.AddTyped<ProxyInterceptor>(m => m.DeclaringType.Name.EndsWith("Proxy"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeDatabase(app);

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ServiceDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
