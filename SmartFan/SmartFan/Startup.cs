using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SmartFan.Device;
using SmartFan.Hubs;

namespace SmartFan
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<DeviceManager>();
            services.Configure<ServerOptions>(Configuration.GetSection(nameof(ServerOptions)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptionsSnapshot<ServerOptions> options)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapFallbackToFile(options.Value.FileName);
                endpoints.MapHub<DataHub>("/fan");
            });
        }
    }
}
