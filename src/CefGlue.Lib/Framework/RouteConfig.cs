namespace CefGlue.Lib.Framework
{
    using CefGlue.Lib.Browser;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// 路由设置.
    /// </summary>
    public class RouteConfig
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteConfig"/> class.
        /// </summary>
        /// <param name="routeMethod">请求方式 GET POST DELETE PUT.</param>
        /// <param name="routePath">路由地址.</param>
        /// <param name="handler">路由委托请求事件.</param>
        public RouteConfig(HttpMethod routeMethod, string routePath, Func<WebRequest, WebResponse> handler)
        {
            RouteMethod = routeMethod;
            RoutePath = routePath;
            Handler = handler;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Handler
        /// 路由委托请求事件.
        /// </summary>
        public Func<WebRequest, WebResponse> Handler { get; private set; }

        /// <summary>
        /// Gets the RouteMethod
        /// 请求方式 GET POST DELETE PUT.
        /// </summary>
        public HttpMethod RouteMethod { get; private set; }

        /// <summary>
        /// Gets the RoutePath
        /// 路由地址.
        /// </summary>
        public string RoutePath { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// 同步函数 执行请求委托.
        /// </summary>
        /// <param name="request">.</param>
        /// <returns>.</returns>
        public WebResponse ExecuteCore(WebRequest request) => Handler?.Invoke(request);

        /// <summary>
        /// 异步函数 执行请求委托.
        /// </summary>
        /// <param name="request">.</param>
        /// <returns>.</returns>
        public Task<WebResponse> ExecuteCoreAsync(WebRequest request) => Task.Run(() => Handler?.Invoke(request));

        #endregion
    }
}
