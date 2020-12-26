using System;
using System.Net.Http;

namespace Microsoft.Web.WebView2.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class RouteMethodAttribute : Attribute
    {
        /// <summary>
        /// 请求方式 GET POST PUT DELETE 
        /// </summary>
        public HttpMethod RequestMethod { get; private set; }

        /// <summary>
        /// 路由路径
        /// </summary>
        public string RoutePath { get; private set; }

        /// <summary>
        /// RouteMethodAttribute
        /// </summary>
        /// <param name="requestMethod">请求方式 GET POST PUT DELETE </param>
        /// <param name="routePath">路由路径</param>
        internal RouteMethodAttribute(HttpMethod requestMethod, string routePath = null)
        {
            RequestMethod = requestMethod;
            RoutePath = routePath;
        }
    }

    /// <summary>
    /// DeleteAttribute Route Method
    /// </summary>
    public sealed class DeleteAttribute : RouteMethodAttribute
    {
        /// <summary>
        /// DeleteAttribute
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public DeleteAttribute(string routePath = null) : base(HttpMethod.Delete, routePath)
        {
        }
    }
    
    /// <summary>
    /// PostAttribute Route Method
    /// </summary>
    public sealed class PostAttribute : RouteMethodAttribute
    {
        /// <summary>
        /// PostAttribute
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public PostAttribute(string routePath = null) : base(HttpMethod.Post, routePath)
        {
        }
    }
    
    /// <summary>
    /// PutAttribute Route Method
    /// </summary>
    public sealed class PutAttribute : RouteMethodAttribute
    {
        /// <summary>
        /// PutAttribute
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public PutAttribute(string routePath = null) : base(HttpMethod.Put, routePath)
        {
        }
    }
    
    /// <summary>
    /// PutAttribute Route Method
    /// </summary>
    public sealed class GetAttribute : RouteMethodAttribute
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public GetAttribute(string routePath = null) : base(HttpMethod.Get, routePath)
        {
        }
    }
    
    /// <summary>
    /// TraceAttribute Route Method
    /// </summary>
    public sealed class TraceAttribute : RouteMethodAttribute
    {
        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public TraceAttribute(string routePath = null) : base(HttpMethod.Trace, routePath)
        {
        }
    }
    
    /// <summary>
    /// HeadAttribute Route Method
    /// </summary>
    public sealed class HeadAttribute : RouteMethodAttribute
    {
        /// <summary>
        /// Head
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public HeadAttribute(string routePath = null) : base(HttpMethod.Head, routePath)
        {
        }
    }
    
    /// <summary>
    /// HeadAttribute Route Method
    /// </summary>
    public sealed class OptionsAttribute : RouteMethodAttribute
    {
        /// <summary>
        /// Options
        /// </summary>
        /// <param name="routePath">路由地址</param>
        public OptionsAttribute(string routePath = null) : base(HttpMethod.Options, routePath)
        {
        }
    }
}