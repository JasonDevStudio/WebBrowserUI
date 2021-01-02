namespace CefGlue.Lib
{
    using System;

    /// <summary>
    /// Defines the <see cref="RouteAttribute" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class RouteAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteAttribute"/> class.
        /// </summary>
        /// <param name="routePath">路由地址.</param>
        public RouteAttribute(string routePath = null) => RoutePath = routePath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the RoutePath
        /// 路由地址.
        /// </summary>
        public string RoutePath { get; private set; }

        #endregion
    }
}
