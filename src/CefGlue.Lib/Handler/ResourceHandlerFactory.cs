using System.Reflection;
using Xilium.CefGlue;

namespace CefGlue.Lib.Hanlers
{
    /// <summary>
    /// Defines the <see cref="ResourceHandlerFactory" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ResourceHandlerFactory<T> : CefSchemeHandlerFactory
        where T : ResourceHandlerBase, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceHandlerFactory{T}"/> class.
        /// </summary>
        public ResourceHandlerFactory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceHandlerFactory{T}"/> class.
        /// </summary>
        /// <param name="scheme">The scheme<see cref="string"/>.</param>
        /// <param name="domain">The domain<see cref="string"/>.</param>
        /// <param name="dir">The dir<see cref="string"/>.</param>
        /// <param name="resourceAssembly">The resourceAssembly<see cref="Assembly"/>.</param>
        public ResourceHandlerFactory(string scheme, string domain, string dir, Assembly resourceAssembly)
        {
            this.Scheme = scheme;
            this.Domain = domain;
            this.Dir = dir;
            this.ResourceAssembly = resourceAssembly;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Dir.
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// Gets or sets the Domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the ResourceAssembly.
        /// </summary>
        public Assembly ResourceAssembly { get; set; }

        /// <summary>
        /// Gets or sets the Scheme.
        /// </summary>
        public string Scheme { get; set; } = "http";

        #endregion

        #region Methods

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="schemeName">The schemeName<see cref="string"/>.</param>
        /// <param name="request">The request<see cref="CefRequest"/>.</param>
        /// <returns>The <see cref="CefResourceHandler"/>.</returns>
        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName,
            CefRequest request) => new T { Scheme = this.Scheme, Domain = this.Domain, Dir = this.Dir, ResourceAssembly = this.ResourceAssembly };

        #endregion
    }
}
