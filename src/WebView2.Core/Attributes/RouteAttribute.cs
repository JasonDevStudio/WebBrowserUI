using System;

namespace Microsoft.Web.WebView2.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class RouteAttribute : Attribute
    {
        /// <summary>
        /// 路由地址
        /// </summary>
        public string RoutePath { get; private set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public RouteAttribute(string routePath = null) => RoutePath = routePath;
    }
}