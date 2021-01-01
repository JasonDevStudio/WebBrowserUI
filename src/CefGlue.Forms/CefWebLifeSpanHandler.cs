namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="CefWebLifeSpanHandler" />.
    /// </summary>
    internal sealed class CefWebLifeSpanHandler : CefLifeSpanHandler
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly CefWebBrowser _core;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefWebLifeSpanHandler"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="CefWebBrowser"/>.</param>
        public CefWebLifeSpanHandler(CefWebBrowser core)
        {
            _core = core;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The DoClose.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool DoClose(CefBrowser browser)
        {
            // TODO: ... dispose core
            return false;
        }

        /// <summary>
        /// The OnAfterCreated.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        protected override void OnAfterCreated(CefBrowser browser)
        {
            base.OnAfterCreated(browser);

            _core.InvokeIfRequired(() => _core.OnBrowserAfterCreated(browser));
        }

        /// <summary>
        /// The OnBeforeClose.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        protected override void OnBeforeClose(CefBrowser browser)
        {
            if (_core.InvokeRequired)
                _core.BeginInvoke((Action)_core.OnBeforeClose);
            else
                _core.OnBeforeClose();
        }

        /// <summary>
        /// The OnBeforePopup.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="targetUrl">The targetUrl<see cref="string"/>.</param>
        /// <param name="targetFrameName">The targetFrameName<see cref="string"/>.</param>
        /// <param name="targetDisposition">The targetDisposition<see cref="CefWindowOpenDisposition"/>.</param>
        /// <param name="userGesture">The userGesture<see cref="bool"/>.</param>
        /// <param name="popupFeatures">The popupFeatures<see cref="CefPopupFeatures"/>.</param>
        /// <param name="windowInfo">The windowInfo<see cref="CefWindowInfo"/>.</param>
        /// <param name="client">The client<see cref="CefClient"/>.</param>
        /// <param name="settings">The settings<see cref="CefBrowserSettings"/>.</param>
        /// <param name="extraInfo">The extraInfo<see cref="CefDictionaryValue"/>.</param>
        /// <param name="noJavascriptAccess">The noJavascriptAccess<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool OnBeforePopup(CefBrowser browser, CefFrame frame, string targetUrl, string targetFrameName, CefWindowOpenDisposition targetDisposition, bool userGesture, CefPopupFeatures popupFeatures, CefWindowInfo windowInfo, ref CefClient client, CefBrowserSettings settings, ref CefDictionaryValue extraInfo, ref bool noJavascriptAccess)
        {
            var e = new BeforePopupEventArgs(frame, targetUrl, targetFrameName, popupFeatures, windowInfo, client, settings,
                                 noJavascriptAccess);

            _core.InvokeIfRequired(() => _core.OnBeforePopup(e));

            client = e.Client;
            noJavascriptAccess = e.NoJavascriptAccess;

            return e.Handled;
        }

        #endregion
    }
}
