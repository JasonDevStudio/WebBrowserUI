namespace CefGlue.Lib.Browser
{
    using System;
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="WebBrowser" />.
    /// </summary>
    public sealed class WebBrowser
    {
        #region Fields

        /// <summary>
        /// Defines the _owner.
        /// </summary>
        private readonly object _owner;

        /// <summary>
        /// Defines the _settings.
        /// </summary>
        private readonly CefBrowserSettings _settings;

        /// <summary>
        /// Defines the _browser.
        /// </summary>
        private CefBrowser _browser;

        /// <summary>
        /// Defines the _client.
        /// </summary>
        private CefClient _client;

        /// <summary>
        /// Defines the _created.
        /// </summary>
        private bool _created;

        /// <summary>
        /// Defines the _startUrl.
        /// </summary>
        private string _startUrl;

        /// <summary>
        /// Defines the _windowHandle.
        /// </summary>
        private IntPtr _windowHandle;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBrowser"/> class.
        /// </summary>
        /// <param name="owner">The owner<see cref="object"/>.</param>
        /// <param name="settings">The settings<see cref="CefBrowserSettings"/>.</param>
        /// <param name="startUrl">The startUrl<see cref="string"/>.</param>
        public WebBrowser(object owner, CefBrowserSettings settings, string startUrl)
        {
            _owner = owner;
            _settings = settings;
            _startUrl = startUrl;
        }

        #endregion

        #region Events

        /// <summary>
        /// Defines the AddressChanged.
        /// </summary>
        public event EventHandler<AddressChangedEventArgs> AddressChanged;

        /// <summary>
        /// Defines the BeforeClose.
        /// </summary>
        public event EventHandler BeforeClose;

        /// <summary>
        /// Defines the ConsoleMessage.
        /// </summary>
        public event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;

        /// <summary>
        /// Defines the Created.
        /// </summary>
        public event EventHandler Created;

        /// <summary>
        /// Defines the LoadEnd.
        /// </summary>
        public event EventHandler<LoadEndEventArgs> LoadEnd;

        /// <summary>
        /// Defines the LoadError.
        /// </summary>
        public event EventHandler<LoadErrorEventArgs> LoadError;

        /// <summary>
        /// Defines the LoadingStateChanged.
        /// </summary>
        public event EventHandler<LoadingStateChangedEventArgs> LoadingStateChanged;

        /// <summary>
        /// Defines the LoadStarted.
        /// </summary>
        public event EventHandler<LoadStartEventArgs> LoadStarted;

        /// <summary>
        /// Defines the TargetUrlChanged.
        /// </summary>
        public event EventHandler<TargetUrlChangedEventArgs> TargetUrlChanged;

        /// <summary>
        /// Defines the TitleChanged.
        /// </summary>
        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CefBrowser.
        /// </summary>
        public CefBrowser CefBrowser
        {
            get { return _browser; }
        }

        /// <summary>
        /// Gets or sets the StartUrl.
        /// </summary>
        public string StartUrl
        {
            get { return _startUrl; }
            set { _startUrl = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Close.
        /// </summary>
        public void Close()
        {
            if (_browser != null)
            {
                var host = _browser.GetHost();
                host.CloseBrowser(true);
                host.Dispose();
                _browser.Dispose();
                _browser = null;
            }
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="windowInfo">The windowInfo<see cref="CefWindowInfo"/>.</param>
        public void Create(CefWindowInfo windowInfo)
        {
            if (_client == null)
            {
                _client = new WebClient(this);
            }

            CefBrowserHost.CreateBrowser(windowInfo, _client, _settings, StartUrl);
        }

        /// <summary>
        /// The OnAddressChanged.
        /// </summary>
        /// <param name="address">The address<see cref="string"/>.</param>
        internal void OnAddressChanged(string address)
        {
            var handler = AddressChanged;
            if (handler != null)
            {
                handler(this, new AddressChangedEventArgs(address));
            }
        }

        /// <summary>
        /// The OnBeforeClose.
        /// </summary>
        internal void OnBeforeClose()
        {
            _windowHandle = IntPtr.Zero;
            if (BeforeClose != null)
                BeforeClose(this, EventArgs.Empty);
        }

        /// <summary>
        /// The OnConsoleMessage.
        /// </summary>
        /// <param name="e">The e<see cref="ConsoleMessageEventArgs"/>.</param>
        internal void OnConsoleMessage(ConsoleMessageEventArgs e)
        {
            if (ConsoleMessage != null)
                ConsoleMessage(this, e);
            else
                e.Handled = false;
        }

        /// <summary>
        /// The OnCreated.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        internal void OnCreated(CefBrowser browser)
        {
            //if (_created) throw new InvalidOperationException("Browser already created.");
            _created = true;
            _browser = browser;

            var handler = Created;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The OnLoadEnd.
        /// </summary>
        /// <param name="e">The e<see cref="LoadEndEventArgs"/>.</param>
        internal void OnLoadEnd(LoadEndEventArgs e)
        {
            if (LoadEnd != null)
                LoadEnd(this, e);
        }

        /// <summary>
        /// The OnLoadError.
        /// </summary>
        /// <param name="e">The e<see cref="LoadErrorEventArgs"/>.</param>
        internal void OnLoadError(LoadErrorEventArgs e)
        {
            if (LoadError != null)
                LoadError(this, e);
        }

        /// <summary>
        /// The OnLoadingStateChanged.
        /// </summary>
        /// <param name="isLoading">The isLoading<see cref="bool"/>.</param>
        /// <param name="canGoBack">The canGoBack<see cref="bool"/>.</param>
        /// <param name="canGoForward">The canGoForward<see cref="bool"/>.</param>
        internal void OnLoadingStateChanged(bool isLoading, bool canGoBack, bool canGoForward)
        {
            var handler = LoadingStateChanged;
            if (handler != null)
            {
                handler(this, new LoadingStateChangedEventArgs(isLoading, canGoBack, canGoForward));
            }
        }

        /// <summary>
        /// The OnLoadStart.
        /// </summary>
        /// <param name="e">The e<see cref="LoadStartEventArgs"/>.</param>
        internal void OnLoadStart(LoadStartEventArgs e)
        {
            if (LoadStarted != null)
                LoadStarted(this, e);
        }

        /// <summary>
        /// The OnTargetUrlChanged.
        /// </summary>
        /// <param name="targetUrl">The targetUrl<see cref="string"/>.</param>
        internal void OnTargetUrlChanged(string targetUrl)
        {
            var handler = TargetUrlChanged;
            if (handler != null)
            {
                handler(this, new TargetUrlChangedEventArgs(targetUrl));
            }
        }

        /// <summary>
        /// The OnTitleChanged.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        internal void OnTitleChanged(string title)
        {
            var handler = TitleChanged;
            if (handler != null)
            {
                handler(this, new TitleChangedEventArgs(title));
            }
        }

        #endregion
    }
}
