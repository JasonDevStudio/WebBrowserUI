namespace CefGlue.Lib.Browser
{
    using System;
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="WebDisplayHandler" />.
    /// </summary>
    internal sealed class WebDisplayHandler : CefDisplayHandler
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly WebBrowser _core;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDisplayHandler"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="WebBrowser"/>.</param>
        public WebDisplayHandler(WebBrowser core)
        {
            _core = core;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OnAddressChange.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="url">The url<see cref="string"/>.</param>
        protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
        {
            if (frame.IsMain)
            {
                _core.OnAddressChanged(url);
            }
        }

        /// <summary>
        /// The OnConsoleMessage.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="level">The level<see cref="CefLogSeverity"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <param name="line">The line<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool OnConsoleMessage(CefBrowser browser, CefLogSeverity level, string message, string source, int line)
        {
            var e = new ConsoleMessageEventArgs(level, message, source, line);
            _core.OnConsoleMessage(e);

            return e.Handled;
        }

        /// <summary>
        /// The OnStatusMessage.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="value">The value<see cref="string"/>.</param>
        protected override void OnStatusMessage(CefBrowser browser, string value)
        {
            _core.OnTargetUrlChanged(value);
        }

        /// <summary>
        /// The OnTitleChange.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        protected override void OnTitleChange(CefBrowser browser, string title)
        {
            _core.OnTitleChanged(title);
        }

        /// <summary>
        /// The OnTooltip.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="text">The text<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool OnTooltip(CefBrowser browser, string text)
        {
            Console.WriteLine("OnTooltip: {0}", text);
            return false;
        }

        #endregion
    }
}
