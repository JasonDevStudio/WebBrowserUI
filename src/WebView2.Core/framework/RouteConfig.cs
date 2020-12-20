using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;

namespace Microsoft.Web.WebView2.Core
{
    /// <summary>
    /// 路由设置
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// 请求方式 GET POST DELETE PUT
        /// </summary>
        public HttpMethod RouteMethod { get; private set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string RoutePath { get; private set; }
        
        /// <summary>
        /// 路由委托请求事件
        /// </summary>
        public Func<CoreWebView2WebResourceRequest, CoreWebView2WebResourceResponse> Handler { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="routeMethod">请求方式 GET POST DELETE PUT</param>
        /// <param name="routePath">路由地址</param>
        /// <param name="handler">路由委托请求事件</param>
        public RouteConfig(HttpMethod routeMethod, string routePath, Func<CoreWebView2WebResourceRequest, CoreWebView2WebResourceResponse> handler)
        {
            RouteMethod = routeMethod;
            RoutePath = routePath;
            Handler = handler;
        }

        /// <summary>
        /// 同步函数 执行请求委托
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CoreWebView2WebResourceResponse ExecuteCore(CoreWebView2WebResourceRequest request) => Handler?.Invoke(request); 

        /// <summary>
        /// 异步函数 执行请求委托
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<CoreWebView2WebResourceResponse> ExecuteCoreAsync(CoreWebView2WebResourceRequest request) => Task.Run(() => Handler?.Invoke(request)); 
    }
}