using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;

namespace Yin.Infrastructure
{
    public static class ConsulExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, ConsulOption consulOption)
        {
            var consulClient = new ConsulClient(x =>
            {
                x.Address = new Uri(consulOption.CousulAddress);
                x.Datacenter = "dc1";
            });

            var registration = new AgentServiceRegistration()
            {
                ID = consulOption.ServiceName + "_" + Guid.NewGuid().ToString(),
                Name = consulOption.ServiceName,// 服务名
                Address = consulOption.ServiceAddress, // 服务绑定IP
                Port = consulOption.ServicePort, // 服务绑定端口
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(2),//服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                    HTTP = $"http://{consulOption.ServiceAddress}:{consulOption.ServicePort}/health",//健康检查地址
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };

            // 服务注册
            consulClient.Agent.ServiceRegister(registration);

            // 应用程序终止时，服务取消注册
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });
            return app;
        }
    }

    public class ConsulOption
    {
        public string CousulAddress { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public int ServicePort { get; set; }
    }

}
