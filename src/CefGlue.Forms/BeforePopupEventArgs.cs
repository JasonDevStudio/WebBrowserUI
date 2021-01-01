namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="BeforePopupEventArgs" />.
    /// </summary>
    public class BeforePopupEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BeforePopupEventArgs"/> class.
        /// </summary>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="targetUrl">The targetUrl<see cref="string"/>.</param>
        /// <param name="targetFrameName">The targetFrameName<see cref="string"/>.</param>
        /// <param name="popupFeatures">The popupFeatures<see cref="CefPopupFeatures"/>.</param>
        /// <param name="windowInfo">The windowInfo<see cref="CefWindowInfo"/>.</param>
        /// <param name="client">The client<see cref="CefClient"/>.</param>
        /// <param name="settings">The settings<see cref="CefBrowserSettings"/>.</param>
        /// <param name="noJavascriptAccess">The noJavascriptAccess<see cref="bool"/>.</param>
        public BeforePopupEventArgs(
            CefFrame frame,
            string targetUrl,
            string targetFrameName,
            CefPopupFeatures popupFeatures,
            CefWindowInfo windowInfo,
            CefClient client,
            CefBrowserSettings settings,
            bool noJavascriptAccess)
        {
            Frame = frame;
            TargetUrl = targetUrl;
            TargetFrameName = targetFrameName;
            PopupFeatures = popupFeatures;
            WindowInfo = windowInfo;
            Client = client;
            Settings = settings;
            NoJavascriptAccess = noJavascriptAccess;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Client.
        /// </summary>
        public CefClient Client { get; set; }

        /// <summary>
        /// Gets the Frame.
        /// </summary>
        public CefFrame Frame { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Handled.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether NoJavascriptAccess.
        /// </summary>
        public bool NoJavascriptAccess { get; set; }

        /// <summary>
        /// Gets the PopupFeatures.
        /// </summary>
        public CefPopupFeatures PopupFeatures { get; private set; }

        /// <summary>
        /// Gets the Settings.
        /// </summary>
        public CefBrowserSettings Settings { get; private set; }

        /// <summary>
        /// Gets the TargetFrameName.
        /// </summary>
        public string TargetFrameName { get; private set; }

        /// <summary>
        /// Gets the TargetUrl.
        /// </summary>
        public string TargetUrl { get; private set; }

        /// <summary>
        /// Gets the WindowInfo.
        /// </summary>
        public CefWindowInfo WindowInfo { get; private set; }

        #endregion
    }
}
