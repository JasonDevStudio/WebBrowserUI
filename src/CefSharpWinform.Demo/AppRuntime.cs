using System.IO;
using System.Reflection;
using CefSharp;
using CefSharp.WinForms;

namespace CefSharpWinform.Demo
{
    public class AppRuntime
    {
        static string basePath = Path.GetDirectoryName(typeof(CefSharpWinform.Demo.Program).Assembly.Location);
        static string cefSharpWinformPath = Path.Combine(basePath, "Cef64", "CefSharp.WinForms.dll");
        static string cefSharpPath = Path.Combine(basePath, "Cef64", "CefSharp.dll");
        static string cefSharpOffScreenPath = Path.Combine(basePath, "Cef64", "CefSharp.OffScreen.dll");
        static string locales = Path.Combine(basePath, "Cef64", "locales");
        static string resource = Path.Combine(basePath, "Cef64");
        static string lib = Path.Combine(basePath, "Cef64", "libcef.dll");
        static string subprocessPath = Path.Combine(basePath, "Cef64", "CefSharp.BrowserSubprocess.exe");

        public static void Initialize()
        {
            var handler = new CefLibraryHandle(lib);
            if (!handler.IsInvalid)
            {
                var setting = new CefSharp.WinForms.CefSettings();
                setting.LocalesDirPath = locales;
                setting.ResourcesDirPath = resource;
                setting.BrowserSubprocessPath = subprocessPath;
                setting.LogFile = "ceflog.txt";
                Cef.Initialize(setting);
            }
        }
    }
}