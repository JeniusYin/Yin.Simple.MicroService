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
            //1. �ͻ���ģʽ
            services.AddIdentityServer()
                  .AddDeveloperSigningCredential()    //����Tokenǩ����Ҫ�Ĺ�Կ��˽Կ,�洢��bin��tempkey.rsa(��������Ҫ����ʵ֤�飬�˴���ΪAddSigningCredential)
                  .AddInMemoryApiResources(ClientCredentialsConfig.GetApiResources())  //�洢��Ҫ����api��Դ
                  .AddInMemoryApiScopes(ClientCredentialsConfig.GetApiScopes())        //����api��Χ 4.x�汾�������õ�
                  .AddInMemoryClients(ClientCredentialsConfig.GetClients()); //�洢�ͻ���ģʽ(����Щ�ͻ��˿�����)


            //2. �û�������ģʽ
            //services.AddIdentityServer()
            //      .AddDeveloperSigningCredential()    //����Tokenǩ����Ҫ�Ĺ�Կ��˽Կ,�洢��bin��tempkey.rsa(��������Ҫ����ʵ֤�飬�˴���ΪAddSigningCredential)
            //      .AddInMemoryApiResources(ResourceOwnerPasswordConfig.GetApiResources())  //�洢��Ҫ����api��Դ
            //      .AddInMemoryApiScopes(ResourceOwnerPasswordConfig.GetApiScopes())        //����api��Χ 4.x�汾�������õ�
            //      .AddTestUsers(ResourceOwnerPasswordConfig.GetUsers().ToList());  //�洢��Щ�û���������Է��� (�û�������ģʽ)
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
            //����IdentityServe4
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
