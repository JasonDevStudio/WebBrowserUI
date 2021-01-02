using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefGlue.Lib.Framework;
using Xilium.CefGlue;

namespace CefGlue.Forms.Demo
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // CefRuntime.Load(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "cef64"));
                //
                // var settings = new CefSettings();
                // settings.MultiThreadedMessageLoop = CefRuntime.Platform == CefRuntimePlatform.Windows;
                // settings.LogSeverity = CefLogSeverity.Verbose;
                // settings.LogFile = "cef.log";
                // // settings.ResourcesDirPath = System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath);
                // settings.RemoteDebuggingPort = 20480;
                // settings.NoSandbox = true;
                //
                // var mainArgs = new CefMainArgs(new string[0]);
                // var formiumApp = new FormiumApp();
                // var exitCode = CefRuntime.ExecuteProcess(mainArgs, formiumApp, IntPtr.Zero);
                // //Console.WriteLine("CefRuntime.ExecuteProcess() returns {0}", exitCode);
                // //if (exitCode != -1)
                // //    return exitCode;
                //
                // //// guard if something wrong
                // //foreach (var arg in args) { if (arg.StartsWith("--type=")) { return -2; } }
                //
                // CefRuntime.Initialize(mainArgs, settings, formiumApp, IntPtr.Zero);

                var runtimePath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "cef64");
                if (!Environment.Is64BitProcess)
                    runtimePath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "cef32");

                AppRuntime.RunTime.Initialize(runtimePath: runtimePath,
                    func: () => AppRuntime.RunTime.RegisterApiDomain(typeof(Program).Assembly, "http", "api.app.service")
                        .RegisterEmbeddedResourceDomain(typeof(Program).Assembly, "http", "main.app.service")
                        .RegisterLocalResourceDomain(typeof(Program).Assembly, "http", "local.app.service")
                        .RegisterDataModels(typeof(Program).Assembly));

                Application.Run(new MainWindow());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}