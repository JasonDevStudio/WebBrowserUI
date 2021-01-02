using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CefGlue.Lib.Hanlers;
using Xilium.CefGlue;

namespace CefGlue.Lib.Framework
{
    /// <summary>
    /// App Runtime
    /// </summary>
    public class AppRuntime
    {
        /// <summary>
        ///  RunTime
        /// </summary>
        public static AppRuntime RunTime { get; } = new AppRuntime();

        /// <summary>
        /// 路由集合
        /// </summary>
        public List<RouteConfig> Routes { get; } = new List<RouteConfig>();

        /// <summary>
        /// Host Objects
        /// </summary>
        public Dictionary<string, object> HostObjects { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Api Resources
        /// </summary>
        public List<(string scheme, string domain, Assembly resourceAssembly)> ApiResourcesHandlers { get; } =
            new List<(string scheme, string domain, Assembly resourceAssembly)>();

        /// <summary>
        /// Embedded Resources
        /// </summary>
        public List<(string scheme, string domain, string dir, Assembly resourceAssembly)> EmbeddedResourcesHandlers { get; } =
            new List<(string scheme, string domain, string dir, Assembly resourceAssembly)>();

        /// <summary>
        /// Local Resources
        /// </summary>
        public List<(string scheme, string domain, string dir, Assembly resourceAssembly)> LocalResourcesHandlers { get; } =
            new List<(string scheme, string domain, string dir, Assembly resourceAssembly)>();
 
        /// <summary>
        /// 注册C#对象到脚本
        /// </summary> 
        /// <param name="name">对象名称</param>
        /// <param name="obj">obj</param>
        /// <returns>WinForms.WebView2</returns>
        public AppRuntime RegisterObjectToScript(string name, object obj)
        {
            if (!HostObjects.ContainsKey(name))
                HostObjects.Add(name, obj);
            return this;
        }

        /// <summary>
        /// 注册API域名 拦截
        /// </summary> 
        /// <param name="domain">需要拦截的域名 默认 http://api.app.service </param>
        /// <returns>WinForms.WebView2</returns>
        public AppRuntime RegisterApiDomain(Assembly assembly, string scheme = "http", string domain = "api.app.service")
        {
            if (!ApiResourcesHandlers.Any(m => m.scheme == scheme && m.domain == domain))
            {
                this.ApiResourcesHandlers.Add((scheme, domain, assembly));
                CefRuntime.RegisterSchemeHandlerFactory(scheme, domain, new ResourceHandlerFactory<ApiResourceHandler>{Scheme = scheme, Domain = domain, ResourceAssembly = assembly});
            }
 
            return this;
        }

        /// <summary>
        /// 注册内嵌资源拦截域名和程序集
        /// </summary>
        /// <param name="uri">uri</param>
        /// <param name="assembly">Assembly</param>
        /// <returns></returns>
        public AppRuntime RegisterEmbeddedResourceDomain(Assembly assembly,string scheme = "http", string domain = "main.app.service", string dir = "wwwroot")
        { 
            if (!EmbeddedResourcesHandlers.Any(m => m.scheme == scheme && m.domain == domain && m.dir.ToUpper() == dir.ToUpper()))
            {
                EmbeddedResourcesHandlers.Add((scheme, domain,dir, assembly));
                CefRuntime.RegisterSchemeHandlerFactory(scheme, domain, new ResourceHandlerFactory<EmbeddedResourceHandler> { Scheme = scheme, Domain = domain, Dir = dir,ResourceAssembly = assembly });
            }

            return this;
        }

        /// <summary>
        /// 注册内嵌资源拦截域名和程序集
        /// </summary>
        /// <param name="uri">uri</param>
        /// <param name="assembly">Assembly</param>
        /// <returns></returns>
        public AppRuntime RegisterLocalResourceDomain(Assembly assembly,string scheme = "http", string domain = "main.app.service", string dir = "wwwroot")
        {
            if (!LocalResourcesHandlers.Any(m =>
                m.scheme == scheme && m.domain == domain && m.dir.ToUpper() == dir.ToUpper()))
            {
                LocalResourcesHandlers.Add((scheme, domain,dir, assembly));
                CefRuntime.RegisterSchemeHandlerFactory(scheme, domain, new ResourceHandlerFactory<LocalResourceHandler>{Scheme = scheme, Domain = domain,Dir = dir,ResourceAssembly = assembly});
            }

            return this;
        }

        /// <summary>
        /// 注册数据模型
        /// </summary> 
        /// <param name="assembly">Assembly</param>
        /// <returns>WinForms.WebView2</returns>
        public AppRuntime RegisterDataModels(Assembly assembly)
        {
            this.Routes.AddRange(new DataModelProvider().ImportDataModelAssembly(assembly));
            return this;
        }

        /// <summary>
        /// 初始化浏览器
        /// </summary>
        /// <param name="settings">CefSettings</param>
        /// <param name="mainArgs">CefMainArgs</param>
        /// <param name="runtimePath">runtimePath</param>
        /// <param name="func">func</param>
        /// <returns>AppRuntime</returns>
        public AppRuntime Initialize(CefSettings settings = null, CefMainArgs mainArgs = null,string runtimePath = null,Func<AppRuntime> func = null)
        {
            runtimePath ??= Path.Combine(Path.GetDirectoryName(typeof(AppRuntime).Assembly.Location), "cef64");
            var userDataPath = Path.Combine(Path.GetDirectoryName(typeof(AppRuntime).Assembly.Location), "UserData");
            CefRuntime.Load(runtimePath);
            
            var cefSettings = new CefSettings();
            cefSettings.MultiThreadedMessageLoop = settings == null ? CefRuntime.Platform == CefRuntimePlatform.Windows :settings.MultiThreadedMessageLoop;
            cefSettings.LogSeverity = settings == null ? CefLogSeverity.Verbose : settings.LogSeverity;
            cefSettings.LogFile = settings == null ? "cef.log" : settings.LogFile; 
            cefSettings.RemoteDebuggingPort = settings == null ? 20480 : settings.RemoteDebuggingPort;
            cefSettings.NoSandbox = settings == null ? true : settings.NoSandbox;
            cefSettings.UserDataPath = settings == null ? userDataPath : cefSettings.UserDataPath;
            cefSettings.BrowserSubprocessPath = settings?.BrowserSubprocessPath;
            cefSettings.FrameworkDirPath = settings?.FrameworkDirPath;
            
            var formiumApp = new FormiumApp();
            mainArgs ??= new CefMainArgs(new string[0]);
            CefRuntime.ExecuteProcess(mainArgs, formiumApp, IntPtr.Zero);           
            CefRuntime.Initialize(mainArgs, cefSettings, formiumApp, IntPtr.Zero);

            func?.Invoke();
            
            return this;
        }
    }
}