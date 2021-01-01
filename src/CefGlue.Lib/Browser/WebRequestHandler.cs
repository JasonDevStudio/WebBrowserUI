namespace CefGlue.Lib.Browser
{
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="WebRequestHandler" />.
    /// </summary>
    public class WebRequestHandler : CefRequestHandler
    {
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
        /// The OnBeforeBrowse.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="request">The request<see cref="CefRequest"/>.</param>
        /// <param name="userGesture">The userGesture<see cref="bool"/>.</param>
        /// <param name="isRedirect">The isRedirect<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool OnBeforeBrowse(CefBrowser browser, CefFrame frame, CefRequest request, bool userGesture, bool isRedirect)
        {
            //DemoApp.BrowserMessageRouter.OnBeforeBrowse(browser, frame);
            return base.OnBeforeBrowse(browser, frame, request, userGesture, isRedirect);
        }

        /// <summary>
        /// The OnRenderProcessTerminated.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="status">The status<see cref="CefTerminationStatus"/>.</param>
        protected override void OnRenderProcessTerminated(CefBrowser browser, CefTerminationStatus status)
        {
        }

        #endregion
    }
}
