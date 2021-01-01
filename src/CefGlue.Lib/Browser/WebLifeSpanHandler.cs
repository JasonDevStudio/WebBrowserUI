namespace CefGlue.Lib.Browser
{
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="WebLifeSpanHandler" />.
    /// </summary>
    internal sealed class WebLifeSpanHandler : CefLifeSpanHandler
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly WebBrowser _core;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebLifeSpanHandler"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="WebBrowser"/>.</param>
        public WebLifeSpanHandler(WebBrowser core)
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
            // TODO: dispose core
            return false;
        }

        /// <summary>
        /// The OnAfterCreated.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        protected override void OnAfterCreated(CefBrowser browser)
        {
            base.OnAfterCreated(browser);

            _core.OnCreated(browser);
        }

        /// <summary>
        /// The OnBeforeClose.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        protected override void OnBeforeClose(CefBrowser browser)
        {
            base.OnBeforeClose(browser);

            _core.OnBeforeClose();
        }

        #endregion
    }
}
