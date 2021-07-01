using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace Yin.ApiGetaway
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
            //1.ע��Ocelot��Consul
            services.AddOcelot()
                .AddConsul()
                .AddPolly();

            //2.ע��ID4У��
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication("UserServiceKey", option =>      //����UserServiceKey��Ocelot�����ļ��е�AuthenticationProviderKey��Ӧ,�Ӷ����а���֤
                    {
                        option.Authority = "http://127.0.0.1:7051";  //����������127.0.0.1����ôͨ��ID4��������ȡtoken��ʱ�򣬾ͱ���д127.0.0.1������дlocalhost.   
                        option.ApiName = "UserService";             //�����ӦID4��������GetApiResources���õ�apiName,�˴��������д����
                        option.RequireHttpsMetadata = false;
                    })
                    .AddIdentityServerAuthentication("ProductServiceKey", option =>
                    {
                        option.Authority = "http://127.0.0.1:7051";
                        option.ApiName = "ProductService";
                        option.RequireHttpsMetadata = false;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseOcelot();
            app.UseAuthorization();
        }
    }
}
