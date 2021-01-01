namespace Xilium.CefGlue.WindowsForms
{
    /// <summary>
    /// Defines the <see cref="CefWebLoadHandler" />.
    /// </summary>
    public sealed class CefWebLoadHandler : CefLoadHandler
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly CefWebBrowser _core;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefWebLoadHandler"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="CefWebBrowser"/>.</param>
        public CefWebLoadHandler(CefWebBrowser core)
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
            _core.InvokeIfRequired(() => _core.OnLoadEnd(new LoadEndEventArgs(frame, httpStatusCode)));
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
            _core.InvokeIfRequired(() => _core.OnLoadError(new LoadErrorEventArgs(frame, errorCode, errorText, failedUrl)));
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
            _core.InvokeIfRequired(() => _core.OnLoadingStateChange(new LoadingStateChangeEventArgs(isLoading, canGoBack, canGoForward)));
        }

        /// <summary>
        /// The OnLoadStart.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="transitionType">The transitionType<see cref="CefTransitionType"/>.</param>
        protected override void OnLoadStart(CefBrowser browser, CefFrame frame, CefTransitionType transitionType)
        {
            _core.InvokeIfRequired(() => _core.OnLoadStart(new LoadStartEventArgs(frame)));
        }

        #endregion
    }
}
