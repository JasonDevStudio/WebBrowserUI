using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharpWinform.Demo
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            var basePath = Path.GetDirectoryName(typeof(CefSharpWinform.Demo.Program).Assembly.Location);
            var paths = new string[]
            {
                Path.Combine(basePath, "Cef64", "CefSharp.dll"),
                Path.Combine(basePath, "Cef64", "CefSharp.WinForms.dll"),
                Path.Combine(basePath, "Cef64", "CefSharp.OffScreen.dll")
            };

            foreach (var path in paths) 
                Assembly.LoadFrom(path);
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppRuntime.Initialize();
             
            Application.Run(new Form1());
        }
    }
}