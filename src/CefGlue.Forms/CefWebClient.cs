namespace Xilium.CefGlue.WindowsForms
{
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="CefWebClient" />.
    /// </summary>
    public class CefWebClient : CefClient
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly CefWebBrowser _core;

        /// <summary>
        /// Defines the _displayHandler.
        /// </summary>
        private readonly CefWebDisplayHandler _displayHandler;

        /// <summary>
        /// Defines the _lifeSpanHandler.
        /// </summary>
        private readonly CefWebLifeSpanHandler _lifeSpanHandler;

        /// <summary>
        /// Defines the _loadHandler.
        /// </summary>
        private readonly CefWebLoadHandler _loadHandler;

        /// <summary>
        /// Defines the _requestHandler.
        /// </summary>
        private readonly CefWebRequestHandler _requestHandler;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefWebClient"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="CefWebBrowser"/>.</param>
        public CefWebClient(CefWebBrowser core)
        {
            _core = core;
            _lifeSpanHandler = new CefWebLifeSpanHandler(_core);
            _displayHandler = new CefWebDisplayHandler(_core);
            _loadHandler = new CefWebLoadHandler(_core);
            _requestHandler = new CefWebRequestHandler(_core);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Core.
        /// </summary>
        protected CefWebBrowser Core
        {
            get { return _core; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The GetDisplayHandler.
        /// </summary>
        /// <returns>The <see cref="CefDisplayHandler"/>.</returns>
        protected override CefDisplayHandler GetDisplayHandler()
        {
            return _displayHandler;
        }

        /// <summary>
        /// The GetLifeSpanHandler.
        /// </summary>
        /// <returns>The <see cref="CefLifeSpanHandler"/>.</returns>
        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return _lifeSpanHandler;
        }

        /// <summary>
        /// The GetLoadHandler.
        /// </summary>
        /// <returns>The <see cref="CefLoadHandler"/>.</returns>
        protected override CefLoadHandler GetLoadHandler()
        {
            return _loadHandler;
        }

        /// <summary>
        /// The GetRequestHandler.
        /// </summary>
        /// <returns>The <see cref="CefRequestHandler"/>.</returns>
        protected override CefRequestHandler GetRequestHandler()
        {
            return _requestHandler;
        }

        #endregion
    }
}
