namespace CefGlue.Lib.Browser
{
    using System;
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="WebClient" />.
    /// </summary>
    public sealed class WebClient : CefClient
    {
        #region Fields

        /// <summary>
        /// Defines the _core.
        /// </summary>
        private readonly WebBrowser _core;

        /// <summary>
        /// Defines the _displayHandler.
        /// </summary>
        private readonly WebDisplayHandler _displayHandler;

        /// <summary>
        /// Defines the _lifeSpanHandler.
        /// </summary>
        private readonly WebLifeSpanHandler _lifeSpanHandler;

        /// <summary>
        /// Defines the _loadHandler.
        /// </summary>
        private readonly WebLoadHandler _loadHandler;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebClient"/> class.
        /// </summary>
        /// <param name="core">The core<see cref="WebBrowser"/>.</param>
        public WebClient(WebBrowser core)
        {
            _core = core;
            _lifeSpanHandler = new WebLifeSpanHandler(_core);
            _displayHandler = new WebDisplayHandler(_core);
            _loadHandler = new WebLoadHandler(_core);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether DumpProcessMessages.
        /// </summary>
        internal static bool DumpProcessMessages { get; set; }

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
        /// The OnProcessMessageReceived.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="sourceProcess">The sourceProcess<see cref="CefProcessId"/>.</param>
        /// <param name="message">The message<see cref="CefProcessMessage"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (DumpProcessMessages)
            {
                Console.WriteLine("Client::OnProcessMessageReceived: SourceProcess={0}", sourceProcess);
                Console.WriteLine("Message Name={0} IsValid={1} IsReadOnly={2}", message.Name, message.IsValid, message.IsReadOnly);
                var arguments = message.Arguments;
                for (var i = 0; i < arguments.Count; i++)
                {
                    var type = arguments.GetValueType(i);
                    object value;
                    switch (type)
                    {
                        case CefValueType.Null: value = null; break;
                        case CefValueType.String: value = arguments.GetString(i); break;
                        case CefValueType.Int: value = arguments.GetInt(i); break;
                        case CefValueType.Double: value = arguments.GetDouble(i); break;
                        case CefValueType.Bool: value = arguments.GetBool(i); break;
                        default: value = null; break;
                    }

                    Console.WriteLine("  [{0}] ({1}) = {2}", i, type, value);
                }
            }

            //var handled = DemoApp.BrowserMessageRouter.OnProcessMessageReceived(browser, sourceProcess, message);
            //if (handled) return true;

            if (message.Name == "myMessage2" || message.Name == "myMessage3") return true;

            return false;
        }

        #endregion
    }
}
