using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Microsoft.Web.WebView2.Core
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
        /// WebResourceRequested Events
        /// </summary>
        public Dictionary<string, Action<CoreWebView2WebResourceRequestedEventArgs>> RequestHandlers { get; } = new Dictionary<string, Action<CoreWebView2WebResourceRequestedEventArgs>>();

        /// <summary>
        /// Host Objects
        /// </summary>
        public Dictionary<string, object> HostObjects { get; } = new Dictionary<string, object>();
         
        /// <summary>
        /// Embedded Resources
        /// </summary>
        public List<(string uri,string dir,Assembly resource)> ResourcesHandlers { get; } = new List<(string uri,string dir,Assembly resource)>();
        
        /// <summary>
        /// API 域名
        /// </summary>
        public string ApiDomain { get; set; } = "http://api.app.local";
        
        /// <summary>
        /// This represents the WebView2 Environment.
        /// WebView2环境变量
        /// </summary>
        public CoreWebView2Environment WebView2Environment { get; set; }
        
        /// <summary>
        /// 注册C#对象到脚本
        /// </summary> 
        /// <param name="name">对象名称</param>
        /// <param name="obj">obj</param>
        /// <returns>WinForms.WebView2</returns>
        public AppRuntime RegisterObjectToScript(string name, object obj)
        {
            if(!HostObjects.ContainsKey(name))
                HostObjects.Add(name, obj);
            return this;
        }
        
        /// <summary>
        /// 注册API域名 拦截
        /// </summary> 
        /// <param name="domain">需要拦截的域名 默认 http://api.app.local </param>
        /// <returns>WinForms.WebView2</returns>
        public AppRuntime RegisterApiDomain(string domain = "http://api.app.local")
        {
            this.ApiDomain = domain;
            
            if(!RequestHandlers.ContainsKey(nameof(ApiResourceHandler.WebApiRequested)))
                RequestHandlers.Add(nameof(ApiResourceHandler.WebApiRequested), ApiResourceHandler.WebApiRequested);
            
            return this;
        }

        /// <summary>
        /// 注册内嵌资源拦截域名和程序集
        /// </summary>
        /// <param name="uri">uri</param>
        /// <param name="assembly">Assembly</param>
        /// <returns></returns>
        public AppRuntime RegisterEmbeddedResourceDomain(string uri, string dir, Assembly assembly)
        {
            if(!ResourcesHandlers.Any(m=>m.uri == uri && m.dir.ToUpper() == dir.ToUpper()))
                ResourcesHandlers.Add((uri, dir, assembly));
            
            if(!RequestHandlers.ContainsKey(nameof(EmbeddedResourceHandler.EmbeddedResourcesRequested)))
                RequestHandlers.Add(nameof(EmbeddedResourceHandler.EmbeddedResourcesRequested), EmbeddedResourceHandler.EmbeddedResourcesRequested);
            
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
        /// 注册请求事件
        /// </summary> 
        /// <param name="handler">handler</param>
        /// <returns>AppRuntime</returns>
        public  AppRuntime RegisterRequestHandler(Action<CoreWebView2WebResourceRequestedEventArgs> handler)
        {
            if(!RequestHandlers.ContainsKey(nameof(handler)))
                RequestHandlers.Add(nameof(handler),handler);

            return this;
        }
        
        /// <summary>
        /// 删除请求事件
        /// </summary> 
        /// <param name="handler">handler</param>
        /// <returns>AppRuntime</returns>
        public AppRuntime RemoveRequestHandler(Action<CoreWebView2WebResourceRequestedEventArgs> handler)
        {
            if(RequestHandlers.ContainsKey(nameof(handler)))
                RequestHandlers.Remove(nameof(handler));

            return this;
        }

        /// <summary>
        /// 注册浏览器控件
        /// </summary>
        /// <param name="webview">WinForms.WebView2</param>
        /// <returns>AppRuntime</returns>
        public AppRuntime RegisterWebViewControl(WinForms.WebView2 webview)
        {
            webview.CoreWebView2.AddWebResourceRequestedFilter($"{this.ApiDomain}/*", CoreWebView2WebResourceContext.All);
 
            if (ResourcesHandlers.Any()) 
                foreach (var obj in ResourcesHandlers) 
                    webview.CoreWebView2.AddWebResourceRequestedFilter($"{obj.uri}/*", CoreWebView2WebResourceContext.All);  
             
            if(HostObjects.Any()) 
                foreach (var obj in HostObjects) 
                    webview.CoreWebView2.AddHostObjectToScript(obj.Key, obj);  
            
            webview.CoreWebView2.WebResourceRequested += CoreWebView2OnWebResourceRequested;
            return this;
        }

        /// <summary>
        ///  跳转地址
        /// </summary>
        /// <param name="webview">WinForms.WebView2</param>
        /// <param name="uri">Uri</param>
        /// <returns>AppRuntime</returns>
        public AppRuntime GoUri(WinForms.WebView2 webview, Uri uri)
        {
            webview.Source = uri;
            return this;
        }
        
        /// <summary>
        /// 注册 CoreWebView2OnWebResourceRequested 事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">CoreWebView2WebResourceRequestedEventArgs</param>
        private void CoreWebView2OnWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs e)
        {
            if (!RequestHandlers.Any()) return;
            
            foreach (var handler in RequestHandlers) 
                handler.Value?.Invoke(e); 
        }
    }
}