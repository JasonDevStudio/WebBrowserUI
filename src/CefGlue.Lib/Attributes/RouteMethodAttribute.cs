namespace CefGlue.Lib
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// DeleteAttribute Route Method.
    /// </summary>
    public sealed class DeleteAttribute : RouteMethodAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public DeleteAttribute(string routePath = null) : base(HttpMethod.Delete, routePath)
        {
        }

        #endregion
    }

    /// <summary>
    /// PutAttribute Route Method.
    /// </summary>
    public sealed class GetAttribute : RouteMethodAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public GetAttribute(string routePath = null) : base(HttpMethod.Get, routePath)
        {
        }

        #endregion
    }

    /// <summary>
    /// HeadAttribute Route Method.
    /// </summary>
    public sealed class HeadAttribute : RouteMethodAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public HeadAttribute(string routePath = null) : base(HttpMethod.Head, routePath)
        {
        }

        #endregion
    }

    /// <summary>
    /// HeadAttribute Route Method.
    /// </summary>
    public sealed class OptionsAttribute : RouteMethodAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public OptionsAttribute(string routePath = null) : base(HttpMethod.Options, routePath)
        {
        }

        #endregion
    }

    /// <summary>
    /// PostAttribute Route Method.
    /// </summary>
    public sealed class PostAttribute : RouteMethodAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public PostAttribute(string routePath = null) : base(HttpMethod.Post, routePath)
        {
        }

        #endregion
    }

    /// <summary>
    /// PutAttribute Route Method.
    /// </summary>
    public sealed class PutAttribute : RouteMethodAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PutAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public PutAttribute(string routePath = null) : base(HttpMethod.Put, routePath)
        {
        }

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="RouteMethodAttribute" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class RouteMethodAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteMethodAttribute"/> class.
        /// </summary>
        /// <param name="requestMethod">请求方式 GET POST PUT DELETE .</param>
        /// <param name="routePath">路由路径.</param>
        internal RouteMethodAttribute(HttpMethod requestMethod, string routePath = null)
        {
            RequestMethod = requestMethod;
            RoutePath = routePath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the RequestMethod
        /// 请求方式 GET POST PUT DELETE.
        /// </summary>
        public HttpMethod RequestMethod { get; private set; }

        /// <summary>
        /// Gets the RoutePath
        /// 路由路径.
        /// </summary>
        public string RoutePath { get; private set; }

        #endregion
    }

    /// <summary>
    /// TraceAttribute Route Method.
    /// </summary>
    public sealed class TraceAttribute : RouteMethodAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public TraceAttribute(string routePath = null) : base(HttpMethod.Trace, routePath)
        {
        }

        #endregion
    }
}
