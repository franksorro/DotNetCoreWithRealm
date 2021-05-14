using DotNetCoreWithRealm.Interfaces;
using DotNetCoreWithRealm.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Realms;

namespace DotNetCoreWithRealm
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly RealmConfiguration realmConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
            ;

            configuration = builder.Build();

            realmConfig = new RealmConfiguration($"{configuration.GetValue<string>("Realm:Path")}/{configuration.GetValue<string>("Realm:DatabaseName")}")
            {
                SchemaVersion = configuration.GetValue<ulong>("Realm:Version"),
                ShouldDeleteIfMigrationNeeded = configuration.GetValue<bool>("Realm:DeleteOnMigrate")
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opts.SerializerSettings.Formatting = Formatting.None;
                    opts.SerializerSettings.MaxDepth = 10;
                    opts.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddSingleton<IRealmService>(new RealmService(realmConfig));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapControllerRoute(
                            name: "api",
                            pattern: "api/{controller}/{id?}");
                    endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                })
            ;
        }
    }
}
