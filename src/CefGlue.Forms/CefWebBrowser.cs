namespace Xilium.CefGlue.WindowsForms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="CefWebBrowser" />.
    /// </summary>
    [ToolboxBitmap(typeof(CefWebBrowser))]
    public class CefWebBrowser : Control
    {
        #region Fields

        /// <summary>
        /// Defines the _browser.
        /// </summary>
        private CefBrowser _browser;

        /// <summary>
        /// Defines the _browserWindowHandle.
        /// </summary>
        private IntPtr _browserWindowHandle;

        /// <summary>
        /// Defines the _handleCreated.
        /// </summary>
        private bool _handleCreated;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefWebBrowser"/> class.
        /// </summary>
        public CefWebBrowser()
        {
            SetStyle(
                ControlStyles.ContainerControl
                | ControlStyles.ResizeRedraw
                | ControlStyles.FixedWidth
                | ControlStyles.FixedHeight
                | ControlStyles.StandardClick
                | ControlStyles.UserMouse
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.StandardDoubleClick
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.CacheText
                | ControlStyles.EnableNotifyMessage
                | ControlStyles.DoubleBuffer
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UseTextForAccessibility
                | ControlStyles.Opaque,
                false);

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.Selectable,
                true);

            StartUrl = "about:blank";
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
        /// Defines the BeforePopup.
        /// </summary>
        public event EventHandler<BeforePopupEventArgs> BeforePopup;

        /// <summary>
        /// Defines the BrowserCreated.
        /// </summary>
        public event EventHandler BrowserCreated;

        /// <summary>
        /// Defines the ConsoleMessage.
        /// </summary>
        public event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;

        /// <summary>
        /// Defines the LoadEnd.
        /// </summary>
        public event EventHandler<LoadEndEventArgs> LoadEnd;

        /// <summary>
        /// Defines the LoadError.
        /// </summary>
        public event EventHandler<LoadErrorEventArgs> LoadError;

        /// <summary>
        /// Defines the LoadingStateChange.
        /// </summary>
        public event EventHandler<LoadingStateChangeEventArgs> LoadingStateChange;

        /// <summary>
        /// Defines the LoadStarted.
        /// </summary>
        public event EventHandler<LoadStartEventArgs> LoadStarted;

        /// <summary>
        /// Defines the PluginCrashed.
        /// </summary>
        public event EventHandler<PluginCrashedEventArgs> PluginCrashed;

        /// <summary>
        /// Defines the RenderProcessTerminated.
        /// </summary>
        public event EventHandler<RenderProcessTerminatedEventArgs> RenderProcessTerminated;

        /// <summary>
        /// Defines the StatusMessage.
        /// </summary>
        public event EventHandler<StatusMessageEventArgs> StatusMessage;

        /// <summary>
        /// Defines the TitleChanged.
        /// </summary>
        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        /// <summary>
        /// Defines the Tooltip.
        /// </summary>
        public event EventHandler<TooltipEventArgs> Tooltip;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the Browser.
        /// </summary>
        public CefBrowser Browser
        {
            get { return _browser; }
        }

        /// <summary>
        /// Gets or sets the BrowserSettings.
        /// </summary>
        [Browsable(false)]
        public CefBrowserSettings BrowserSettings { get; set; }

        /// <summary>
        /// Gets or sets the StartUrl.
        /// </summary>
        [DefaultValue("about:blank")]
        public string StartUrl { get; set; }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// The InvalidateSize.
        /// </summary>
        public void InvalidateSize()
        {
            ResizeWindow(_browserWindowHandle, Width, Height);
        }

        /// <summary>
        /// The InvokeIfRequired.
        /// </summary>
        /// <param name="a">The a<see cref="Action"/>.</param>
        internal void InvokeIfRequired(Action a)
        {
            if (InvokeRequired)
                Invoke(a);
            else
                a();
        }

        /// <summary>
        /// The OnAddressChanged.
        /// </summary>
        /// <param name="e">The e<see cref="AddressChangedEventArgs"/>.</param>
        internal protected virtual void OnAddressChanged(AddressChangedEventArgs e)
        {
            Address = e.Address;

            var handler = AddressChanged;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// The OnBeforeClose.
        /// </summary>
        internal protected virtual void OnBeforeClose()
        {
            _browserWindowHandle = IntPtr.Zero;
            if (BeforeClose != null)
                BeforeClose(this, EventArgs.Empty);
        }

        /// <summary>
        /// The OnBeforePopup.
        /// </summary>
        /// <param name="e">The e<see cref="BeforePopupEventArgs"/>.</param>
        internal protected virtual void OnBeforePopup(BeforePopupEventArgs e)
        {
            if (BeforePopup != null)
                BeforePopup(this, e);
            else
                e.Handled = false;
        }

        /// <summary>
        /// The OnBrowserAfterCreated.
        /// </summary>
        /// <param name="browser">The browser<see cref="CefBrowser"/>.</param>
        internal protected virtual void OnBrowserAfterCreated(CefBrowser browser)
        {
            _browser = browser;
            _browserWindowHandle = _browser.GetHost().GetWindowHandle();
            ResizeWindow(_browserWindowHandle, Width, Height);

            if (BrowserCreated != null)
                BrowserCreated(this, EventArgs.Empty);
        }

        /// <summary>
        /// The OnConsoleMessage.
        /// </summary>
        /// <param name="e">The e<see cref="ConsoleMessageEventArgs"/>.</param>
        internal protected virtual void OnConsoleMessage(ConsoleMessageEventArgs e)
        {
            if (ConsoleMessage != null)
                ConsoleMessage(this, e);
            else
                e.Handled = false;
        }

        /// <summary>
        /// The OnLoadEnd.
        /// </summary>
        /// <param name="e">The e<see cref="LoadEndEventArgs"/>.</param>
        internal protected virtual void OnLoadEnd(LoadEndEventArgs e)
        {
            if (LoadEnd != null)
                LoadEnd(this, e);
        }

        /// <summary>
        /// The OnLoadError.
        /// </summary>
        /// <param name="e">The e<see cref="LoadErrorEventArgs"/>.</param>
        internal protected virtual void OnLoadError(LoadErrorEventArgs e)
        {
            if (LoadError != null)
                LoadError(this, e);
        }

        /// <summary>
        /// The OnLoadingStateChange.
        /// </summary>
        /// <param name="e">The e<see cref="LoadingStateChangeEventArgs"/>.</param>
        internal protected virtual void OnLoadingStateChange(LoadingStateChangeEventArgs e)
        {
            if (LoadingStateChange != null)
                LoadingStateChange(this, e);
        }

        /// <summary>
        /// The OnLoadStart.
        /// </summary>
        /// <param name="e">The e<see cref="LoadStartEventArgs"/>.</param>
        internal protected virtual void OnLoadStart(LoadStartEventArgs e)
        {
            if (LoadStarted != null)
                LoadStarted(this, e);
        }

        /// <summary>
        /// The OnPluginCrashed.
        /// </summary>
        /// <param name="e">The e<see cref="PluginCrashedEventArgs"/>.</param>
        internal protected virtual void OnPluginCrashed(PluginCrashedEventArgs e)
        {
            if (PluginCrashed != null)
                PluginCrashed(this, e);
        }

        /// <summary>
        /// The OnRenderProcessTerminated.
        /// </summary>
        /// <param name="e">The e<see cref="RenderProcessTerminatedEventArgs"/>.</param>
        internal protected virtual void OnRenderProcessTerminated(RenderProcessTerminatedEventArgs e)
        {
            if (RenderProcessTerminated != null)
                RenderProcessTerminated(this, e);
        }

        /// <summary>
        /// The OnStatusMessage.
        /// </summary>
        /// <param name="e">The e<see cref="StatusMessageEventArgs"/>.</param>
        internal protected virtual void OnStatusMessage(StatusMessageEventArgs e)
        {
            var handler = StatusMessage;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// The OnTitleChanged.
        /// </summary>
        /// <param name="e">The e<see cref="TitleChangedEventArgs"/>.</param>
        internal protected virtual void OnTitleChanged(TitleChangedEventArgs e)
        {
            Title = e.Title;

            var handler = TitleChanged;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// The OnTooltip.
        /// </summary>
        /// <param name="e">The e<see cref="TooltipEventArgs"/>.</param>
        internal protected virtual void OnTooltip(TooltipEventArgs e)
        {
            if (Tooltip != null)
                Tooltip(this, e);
            else
                e.Handled = false;
        }

        /// <summary>
        /// The CreateWebClient.
        /// </summary>
        /// <returns>The <see cref="CefWebClient"/>.</returns>
        protected virtual CefWebClient CreateWebClient()
        {
            return new CefWebClient(this);
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected override void Dispose(bool disposing)
        {
            if (_browser != null && disposing) // TODO: ugly hack to avoid crashes when CefWebBrowser are Finalized and underlying objects already finalized
            {
                var host = _browser.GetHost();
                if (host != null)
                {
                    host.CloseBrowser();
                    host.Dispose();
                }
                _browser.Dispose();
                _browser = null;
                _browserWindowHandle = IntPtr.Zero;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// The OnHandleCreated.
        /// </summary>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (DesignMode)
            {
                if (!_handleCreated) Paint += PaintInDesignMode;
            }
            else
            {
                var windowInfo = CefWindowInfo.Create();
                windowInfo.SetAsChild(Handle, new CefRectangle { X = 0, Y = 0, Width = Width, Height = Height });

                var client = CreateWebClient();

                var settings = BrowserSettings;
                if (settings == null) settings = new CefBrowserSettings { };

                CefBrowserHost.CreateBrowser(windowInfo, client, settings, StartUrl);
            }

            _handleCreated = true;
        }

        /// <summary>
        /// The OnResize.
        /// </summary>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_browserWindowHandle != IntPtr.Zero)
            {
                // Ignore size changes when form are minimized.
                var form = TopLevelControl as Form;
                if (form != null && form.WindowState == FormWindowState.Minimized)
                {
                    return;
                }

                ResizeWindow(_browserWindowHandle, Width, Height);
            }
        }

        /// <summary>
        /// The ResizeWindow.
        /// </summary>
        /// <param name="handle">The handle<see cref="IntPtr"/>.</param>
        /// <param name="width">The width<see cref="int"/>.</param>
        /// <param name="height">The height<see cref="int"/>.</param>
        private static void ResizeWindow(IntPtr handle, int width, int height)
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.SetWindowPos(handle, IntPtr.Zero,
                    0, 0, width, height,
                    SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder
                    );
            }
        }

        /// <summary>
        /// The PaintInDesignMode.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PaintEventArgs"/>.</param>
        private void PaintInDesignMode(object sender, PaintEventArgs e)
        {
            var width = this.Width;
            var height = this.Height;
            if (width > 1 && height > 1)
            {
                var brush = new SolidBrush(this.ForeColor);
                var pen = new Pen(this.ForeColor);
                pen.DashStyle = DashStyle.Dash;

                e.Graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);

                var fontHeight = (int)(this.Font.GetHeight(e.Graphics) * 1.25);

                var x = 3;
                var y = 3;

                e.Graphics.DrawString("CefWebBrowser", Font, brush, x, y + (0 * fontHeight));
                e.Graphics.DrawString(string.Format("StartUrl: {0}", StartUrl), Font, brush, x, y + (1 * fontHeight));

                brush.Dispose();
                pen.Dispose();
            }
        }

        #endregion
    }
}
