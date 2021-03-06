﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using WebView2.Demo.DataModels;

namespace WebView2.Demo
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public partial class FrmMainWindow : Form
    {
        public FrmMainWindow()
        {
            InitializeComponent();
            var webview = new Microsoft.Web.WebView2.WinForms.WebView2 () { Dock = DockStyle.Fill} ;
            this.Controls.Add(webview);
            this.Size = new Size(1600, 900);
            this.InitializeWebview2Async(webview, null, null, null, () => 
               AppRuntime.RunTime.RegisterWebViewControl(webview).GoUri(webview, new Uri("http://main.app.service/index.html")));  
            
            //.GoUri(webview,new Uri("http://main.app.local/wwwroot/index.html"))
            // webview.Source = new Uri(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "wwwroot","index.html"));
        }

        /// <summary>
        /// Webview2 初始化
        /// </summary>
        /// <param name="webview">Microsoft.Web.WebView2.WinForms.WebView2</param>
        /// <param name="browserExecutableFolder">browserExecutableFolder</param>
        /// <param name="userDataFolder">userDataFolder</param>
        /// <param name="options">CoreWebView2EnvironmentOptions</param>
        /// <param name="func">Func</param>
        public async Task InitializeWebview2Async(Microsoft.Web.WebView2.WinForms.WebView2 webview, 
            string browserExecutableFolder = null,
            string userDataFolder = null,
            CoreWebView2EnvironmentOptions options = null,
            Func<AppRuntime> func = null)
        {
            userDataFolder = userDataFolder ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"UserData");
            AppRuntime.RunTime.WebView2Environment = await CoreWebView2Environment.CreateAsync(browserExecutableFolder, userDataFolder, options);
            await webview.EnsureCoreWebView2Async(AppRuntime.RunTime.WebView2Environment);
            
            if(func != null)
                webview.Invoke(func);
        } 
    }
}