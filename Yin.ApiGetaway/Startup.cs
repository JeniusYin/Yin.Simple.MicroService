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
            //1.注册Ocelot、Consul
            services.AddOcelot()
                .AddConsul()
                .AddPolly();

            //2.注册ID4校验
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication("UserServiceKey", option =>      //这里UserServiceKey与Ocelot配置文件中的AuthenticationProviderKey对应,从而进行绑定验证
                    {
                        option.Authority = "http://127.0.0.1:7051";  //这里配置是127.0.0.1，那么通过ID4服务器获取token的时候，就必须写127.0.0.1，不能写localhost.   
                        option.ApiName = "UserService";             //必须对应ID4服务器中GetApiResources配置的apiName,此处不能随便写！！
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
