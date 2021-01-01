namespace CefGlue.Forms.Demo
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Xilium.CefGlue;

    /// <summary>
    /// Defines the <see cref="MainWindow" />.
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Defines the _handleCreated.
        /// </summary>
        private bool _handleCreated;

        /// <summary>
        /// Defines the _browser.
        /// </summary>
        private CefBrowser _browser;

        /// <summary>
        /// Defines the _browserWindowHandle.
        /// </summary>
        private IntPtr _browserWindowHandle;

        /// <summary>
        /// Gets or sets the StartUrl.
        /// </summary>
        public string StartUrl { get; set; }

        /// <summary>
        /// Gets or sets the BrowserSettings.
        /// </summary>
        public CefBrowserSettings BrowserSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

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
        /// The CreateWebClient.
        /// </summary>
        /// <returns>The <see cref="CefWebClient"/>.</returns>
        protected virtual CefWebClient CreateWebClient()
        {
            return new CefWebClient();
        }
    }
}
