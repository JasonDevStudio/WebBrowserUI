namespace CefGlue.Demo
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="MainControl" />.
    /// </summary>
    public class MainControl : Control
    {
        #region Fields

        /// <summary>
        /// Defines the _handleCreated.
        /// </summary>
        private bool _handleCreated;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainControl"/> class.
        /// </summary>
        public MainControl()
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

            this.StartUrl = "http://www.google.com";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the BrowserSettings.
        /// </summary>
        public CefBrowserSettings BrowserSettings { get; set; }

        /// <summary>
        /// Gets or sets the StartUrl.
        /// </summary>
        public string StartUrl { get; set; }

        /// <summary>
        /// Gets or sets the WebBrowser
        /// Defines the WebBrowser.....
        /// </summary>
        public CefGlue.Lib.Browser.WebBrowser WebBrowser { get; set; }

        /// <summary>
        /// Gets or sets the CefWebClient
        /// CefWebClient.....
        /// </summary>
        public CefGlue.Lib.Browser.WebClient WebClient { get; set; }

        /// <summary>
        /// Gets or sets the WindowInfo
        /// WindowInfo.....
        /// </summary>
        public CefWindowInfo WindowInfo { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// ShowDevTools.
        /// </summary>
        public void ShowDevTools()
        {
            var host = this.WebBrowser.CefBrowser.GetHost();
            var wi = CefWindowInfo.Create();
            wi.SetAsPopup(IntPtr.Zero, "DevTools");
            var client = new CefGlue.Lib.Browser.WebClient(this.WebBrowser);
            host.ShowDevTools(wi, client, this.BrowserSettings, new CefPoint(0, 0));
        }

        /// <summary>
        /// The OnHandleCreated.
        /// </summary>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (this.DesignMode)
            {
                if (!_handleCreated) Paint += PaintInDesignMode;
            }
            else
            {
                this.WindowInfo = CefWindowInfo.Create();
                this.WindowInfo.SetAsChild(Handle, new CefRectangle { X = 0, Y = 0, Width = Width, Height = Height });
                this.BrowserSettings = new CefBrowserSettings { };
                this.WebBrowser = new CefGlue.Lib.Browser.WebBrowser(this, this.BrowserSettings, this.StartUrl);
                this.WebBrowser.Create(this.WindowInfo);
                this.WebClient = new CefGlue.Lib.Browser.WebClient(this.WebBrowser);
                // this.WebBrowser.Created += (sender, args) => this.WebBrowser.CefBrowser.GetHost().ShowDevTools(this.WindowInfo, this.WebClient, this.BrowserSettings, new CefPoint());
            }

            _handleCreated = true;
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

        /// <summary>
        /// The WebBrowser_Created.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void WebBrowser_Created(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
