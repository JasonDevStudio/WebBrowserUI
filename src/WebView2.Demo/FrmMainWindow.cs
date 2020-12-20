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
            this.Size = new Size(1600, 900);
            InitializeWebview2Async(webview, null, null, null, () => 
                webview.RegisterApiDomain()
                    .RegisterDataModels(this.GetType().Assembly)
                    .RegisterObjectToScript(nameof(FrmMainWindow), this));
        }

        /// <summary>
        /// Webview2 初始化
        /// </summary>
        /// <param name="webview">Microsoft.Web.WebView2.WinForms.WebView2</param>
        /// <param name="browserExecutableFolder">browserExecutableFolder</param>
        /// <param name="userDataFolder">userDataFolder</param>
        /// <param name="options">CoreWebView2EnvironmentOptions</param>
        /// <param name="func">Func</param>
        public async void InitializeWebview2Async(Microsoft.Web.WebView2.WinForms.WebView2 webview, 
            string browserExecutableFolder = null,
            string userDataFolder = null,
            CoreWebView2EnvironmentOptions options = null,
            Func<Microsoft.Web.WebView2.WinForms.WebView2> func = null)
        {
            userDataFolder = userDataFolder ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"UserData");
            var env = await CoreWebView2Environment.CreateAsync(browserExecutableFolder, userDataFolder, options);
            await webview.EnsureCoreWebView2Async(env).ConfigureAwait(false);
            func?.Invoke();
        } 
    }
}