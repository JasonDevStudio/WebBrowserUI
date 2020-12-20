using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Microsoft.Web.WebView2.Core
{
    /// <summary>
    /// WebView Register
    /// </summary>
    public static class WebViewRegister
    {
        /// <summary>
        /// 路由集合
        /// </summary>
        public static readonly List<RouteConfig> Routes = new List<RouteConfig>();

        /// <summary>
        /// WebResourceRequested Events
        /// </summary>
        public static readonly Dictionary<string, Action<CoreWebView2WebResourceRequestedEventArgs>> RequestHandlers =
            new Dictionary<string, Action<CoreWebView2WebResourceRequestedEventArgs>>();
        
        /// <summary>
        /// API 域名
        /// </summary>
        public static string ApiDomain { get; private set; }
        
        /// <summary>
        /// 注册C#对象到脚本
        /// </summary>
        /// <param name="webview">WinForms.WebView2</param>
        /// <param name="name">对象名称</param>
        /// <param name="obj">obj</param>
        /// <returns>WinForms.WebView2</returns>
        public static WinForms.WebView2 RegisterObjectToScript(this Microsoft.Web.WebView2.WinForms.WebView2 webview, string name, object obj)
        {
            webview.CoreWebView2.AddHostObjectToScript(name, obj);
            return webview;
        }
        
        /// <summary>
        /// 注册API域名 拦截
        /// </summary>
        /// <param name="webview">WinForms.WebView2</param>
        /// <param name="domain">需要拦截的域名 默认 http://api.app.local </param>
        /// <returns>WinForms.WebView2</returns>
        public static WinForms.WebView2 RegisterApiDomain(this Microsoft.Web.WebView2.WinForms.WebView2 webview,
            string domain = "http://api.app.local")
        {
            ApiDomain = domain;
            webview.CoreWebView2.AddWebResourceRequestedFilter($"{ApiDomain}/*", CoreWebView2WebResourceContext.All);
            webview.CoreWebView2.WebResourceRequested += CoreWebView2OnWebResourceRequested;
            
            if(RequestHandlers.ContainsKey(nameof(WebApiRequested)))
                RequestHandlers.Add(nameof(WebApiRequested), WebApiRequested);
            return webview;
        }

        /// <summary>
        /// API 请求事件
        /// </summary>
        /// <param name="e">CoreWebView2WebResourceRequestedEventArgs</param>
        public static void WebApiRequested(CoreWebView2WebResourceRequestedEventArgs e)
        {
            // 判断是否为API 域名请求
            if (Regex.IsMatch(e.Request.Uri,@"^{"+ApiDomain+"}/{0,}.*",RegexOptions.IgnoreCase))
            {
                // 提取域名之后的函数名称路径
                var methodPath = Regex.Replace(e.Request.Uri, @"^{"+ApiDomain+"}/{0,}", string.Empty);
                
                // 剔除路径的参数, 提取函数名称
                methodPath = Regex.Replace(methodPath, @"\?.*",string.Empty).ToLower();

                // 查找 API 路由
                var route = Routes.FirstOrDefault(m => m.RoutePath == methodPath);
                
                // 执行API
                e.Response = route?.ExecuteCore(e.Request);
            }
        }
        
        /// <summary>
        /// 注册 CoreWebView2OnWebResourceRequested 事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">CoreWebView2WebResourceRequestedEventArgs</param>
        private static void CoreWebView2OnWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs e)
        {
            if (RequestHandlers == null || !RequestHandlers.Any()) return;
            foreach (var handler in RequestHandlers) 
                handler.Value?.Invoke(e); 
        }

        /// <summary>
        /// 注册数据模型
        /// </summary>
        /// <param name="webview">WinForms.WebView2</param>
        /// <param name="assembly">Assembly</param>
        /// <returns>WinForms.WebView2</returns>
        public static WinForms.WebView2 RegisterDataModels(this Microsoft.Web.WebView2.WinForms.WebView2 webview, Assembly assembly)
        {
            Routes.AddRange(new DataModelProvider().ImportDataModelAssembly(assembly));
            return webview;
        }

        /// <summary>
        /// 注册请求事件
        /// </summary>
        /// <param name="webview">WinForms.WebView2</param>
        /// <param name="handler">handler</param>
        /// <returns>WinForms.WebView2</returns>
        public static WinForms.WebView2 RegisterRequestHandler(this Microsoft.Web.WebView2.WinForms.WebView2 webview,
            Action<CoreWebView2WebResourceRequestedEventArgs> handler)
        {
            if(RequestHandlers.ContainsKey(nameof(handler)))
                RequestHandlers.Add(nameof(handler),handler);

            return webview;
        }
        
        /// <summary>
        /// 删除请求事件
        /// </summary>
        /// <param name="webview">WinForms.WebView2</param>
        /// <param name="handler">handler</param>
        /// <returns>WinForms.WebView2</returns>
        public static WinForms.WebView2 RemoveRequestHandler(this Microsoft.Web.WebView2.WinForms.WebView2 webview,
            Action<CoreWebView2WebResourceRequestedEventArgs> handler)
        {
            if(RequestHandlers.ContainsKey(nameof(handler)))
                RequestHandlers.Remove(nameof(handler));

            return webview;
        }
    }
}