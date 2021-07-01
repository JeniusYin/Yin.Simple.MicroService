using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yin.IdentityServer.Config;

namespace Yin.IdentityServer
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
            //1. 客户端模式
            services.AddIdentityServer()
                  .AddDeveloperSigningCredential()    //生成Token签名需要的公钥和私钥,存储在bin下tempkey.rsa(生产场景要用真实证书，此处改为AddSigningCredential)
                  .AddInMemoryApiResources(ClientCredentialsConfig.GetApiResources())  //存储需要保护api资源
                  .AddInMemoryApiScopes(ClientCredentialsConfig.GetApiScopes())        //配置api范围 4.x版本必须配置的
                  .AddInMemoryClients(ClientCredentialsConfig.GetClients()); //存储客户端模式(即哪些客户端可以用)


            //2. 用户名密码模式
            //services.AddIdentityServer()
            //      .AddDeveloperSigningCredential()    //生成Token签名需要的公钥和私钥,存储在bin下tempkey.rsa(生产场景要用真实证书，此处改为AddSigningCredential)
            //      .AddInMemoryApiResources(ResourceOwnerPasswordConfig.GetApiResources())  //存储需要保护api资源
            //      .AddInMemoryApiScopes(ResourceOwnerPasswordConfig.GetApiScopes())        //配置api范围 4.x版本必须配置的
            //      .AddTestUsers(ResourceOwnerPasswordConfig.GetUsers().ToList());  //存储哪些用户、密码可以访问 (用户名密码模式)
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //启用IdentityServe4
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
