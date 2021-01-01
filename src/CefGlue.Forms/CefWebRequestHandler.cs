namespace Xilium.CefGlue.WindowsForms
{
    /// <summary>
    /// Defines the <see cref="CefWebRequestHandler" />.
    /// </summary>
    public sealed class CefWebRequestHandler : CefRequestHandler
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly CefWebBrowser _core;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefWebRequestHandler"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="CefWebBrowser"/>.</param>
        public CefWebRequestHandler(CefWebBrowser core)
        {
            _core = core;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The GetResourceRequestHandler.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="request">The request<see cref="CefRequest"/>.</param>
        /// <param name="isNavigation">The isNavigation<see cref="bool"/>.</param>
        /// <param name="isDownload">The isDownload<see cref="bool"/>.</param>
        /// <param name="requestInitiator">The requestInitiator<see cref="string"/>.</param>
        /// <param name="disableDefaultHandling">The disableDefaultHandling<see cref="bool"/>.</param>
        /// <returns>The <see cref="CefResourceRequestHandler"/>.</returns>
        protected override CefResourceRequestHandler GetResourceRequestHandler(CefBrowser browser, CefFrame frame, CefRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return null;
        }

        /// <summary>
        /// The OnPluginCrashed.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="pluginPath">The pluginPath<see cref="string"/>.</param>
        protected override void OnPluginCrashed(CefBrowser browser, string pluginPath)
        {
            _core.InvokeIfRequired(() => _core.OnPluginCrashed(new PluginCrashedEventArgs(pluginPath)));
        }

        /// <summary>
        /// The OnRenderProcessTerminated.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="status">The status<see cref="CefTerminationStatus"/>.</param>
        protected override void OnRenderProcessTerminated(CefBrowser browser, CefTerminationStatus status)
        {
            _core.InvokeIfRequired(() => _core.OnRenderProcessTerminated(new RenderProcessTerminatedEventArgs(status)));
        }

        #endregion
    }
}
