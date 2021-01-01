namespace CefGlue.Lib.Browser
{
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="WebLoadHandler" />.
    /// </summary>
    internal sealed class WebLoadHandler : CefLoadHandler
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly WebBrowser _core;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebLoadHandler"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="WebBrowser"/>.</param>
        public WebLoadHandler(WebBrowser core)
        {
            _core = core;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OnLoadEnd.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="httpStatusCode">The httpStatusCode<see cref="int"/>.</param>
        protected override void OnLoadEnd(CefBrowser browser, CefFrame frame, int httpStatusCode)
        {
            _core.OnLoadEnd(new LoadEndEventArgs(frame, httpStatusCode));
        }

        /// <summary>
        /// The OnLoadError.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="CefErrorCode"/>.</param>
        /// <param name="errorText">The errorText<see cref="string"/>.</param>
        /// <param name="failedUrl">The failedUrl<see cref="string"/>.</param>
        protected override void OnLoadError(CefBrowser browser, CefFrame frame, CefErrorCode errorCode, string errorText, string failedUrl)
        {
            _core.OnLoadError(new LoadErrorEventArgs(frame, errorCode, errorText, failedUrl));
        }

        /// <summary>
        /// The OnLoadingStateChange.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="isLoading">The isLoading<see cref="bool"/>.</param>
        /// <param name="canGoBack">The canGoBack<see cref="bool"/>.</param>
        /// <param name="canGoForward">The canGoForward<see cref="bool"/>.</param>
        protected override void OnLoadingStateChange(CefBrowser browser, bool isLoading, bool canGoBack, bool canGoForward)
        {
            _core.OnLoadingStateChanged(isLoading, canGoBack, canGoForward);
        }

        /// <summary>
        /// The OnLoadStart.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="transitionType">The transitionType<see cref="CefTransitionType"/>.</param>
        protected override void OnLoadStart(CefBrowser browser, CefFrame frame, CefTransitionType transitionType)
        {
            _core.OnLoadStart(new LoadStartEventArgs(frame));
        }

        #endregion
    }
}
