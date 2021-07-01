using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Yin.Infrastructure;

namespace Yin.ProductService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Uri uri = new Uri(Configuration["urls"]);
            app.RegisterConsul(lifetime, new ConsulOption
            {
                CousulAddress = Configuration["ConsulAddress"],
                ServiceName = "Product",
                ServiceAddress = uri.Host,
                ServicePort = uri.Port
            });

            app.UseRouting();

            app.Map("/health", app =>
            {
                app.Run(async context => await context.Response.WriteAsync("ok"));
            });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
