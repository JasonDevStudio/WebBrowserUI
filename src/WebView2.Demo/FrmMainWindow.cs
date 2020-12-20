using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace WebView2.Demo
{
    public partial class FrmMainWindow : Form
    {
        public FrmMainWindow()
        {
            InitializeComponent();
            var webview = new Microsoft.Web.WebView2.WinForms.WebView2 () { Dock = DockStyle.Fill} ;
            this.Controls.Add(webview);
            InitializeAsync(webview);
        }

        /// <summary>
        /// webview2 初始化
        /// </summary>
        /// <param name="webview">WebView2</param>
        public async void InitializeAsync(Microsoft.Web.WebView2.WinForms.WebView2 webview)
        {
            var userDataFolder = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location),"UserData");
            var env = await CoreWebView2Environment.CreateAsync(userDataFolder:userDataFolder);
            await webview.EnsureCoreWebView2Async(env);
        }
    }
}