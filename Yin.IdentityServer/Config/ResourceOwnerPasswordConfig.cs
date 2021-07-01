using IdentityServer4.Models;
using System.Collections.Generic;

namespace Yin.IdentityServer.Config
{
    /// <summary>
    /// 用户名密码模式
    /// </summary>
    public class ResourceOwnerPasswordConfig
    {
        /// <summary>
        /// 配置Api范围集合
        /// 4.x版本新增的配置
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("UserService"),
                new ApiScope("ProductService")
            };
        }


        /// <summary>
        /// 需要保护的Api资源
        /// 4.x版本新增后续Scopes的配置
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> resources = new List<ApiResource>();
            //ApiResource第一个参数是ServiceName，第二个参数是描述
            resources.Add(new ApiResource("UserService", "UserService服务需要保护哦") { Scopes = { "UserService" } });
            resources.Add(new ApiResource("ProductService", "ProductService服务需要保护哦") { Scopes = { "ProductService" } });
            return resources;
        }

        /// <summary>
        /// 定义可以使用ID4的用户资源
        /// </summary>
        /// <returns></returns>
        //public static IEnumerable<TestUser> GetUsers()
        //{
        //    return new List<TestUser>()
        //    {
        //        new TestUser
        //        {
        //            SubjectId = "10001",
        //            Username = "yqj1",     //账号
        //            Password = "yqj001"    //密码
        //        },
        //        new TestUser
        //        {
        //            SubjectId = "10002",
        //            Username = "yqj2",
        //            Password = "yqj002"
        //        },
        //        new TestUser
        //        {
        //            SubjectId = "10003",
        //            Username = "yqj3",
        //            Password = "yqj003"
        //        }
        //    };
        //}
    }
}
