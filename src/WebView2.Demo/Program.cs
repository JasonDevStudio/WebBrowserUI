using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using WebView2.Demo.DataModels;

namespace WebView2.Demo
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppRuntime.RunTime.RegisterDataModels(typeof(Program).Assembly)
                .RegisterApiDomain("http://api.app.service")
                .RegisterEmbeddedResourceDomain("http://main.app.service", "wwwroot",typeof(Program).Assembly)
                .RegisterLocalResourceDomain("http://local.app.service","wwwroot",typeof(Program).Assembly);
                // .RegisterObjectToScript(nameof(BridgeModel), new BridgeModel());
            
            Application.Run(new FrmMainWindow());
        }
    }
}