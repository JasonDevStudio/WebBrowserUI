using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Web.WebView2.Core
{
    public class ApiResourceHandler
    {
        /// <summary>
        /// API 请求事件
        /// </summary>
        /// <param name="e">CoreWebView2WebResourceRequestedEventArgs</param>
        public static void WebApiRequested(CoreWebView2WebResourceRequestedEventArgs e)
        {
            // 判断是否为API 域名请求
            if (Regex.IsMatch(e.Request.Uri,@"^"+AppRuntime.RunTime.ApiDomain+"/{0,}.*",RegexOptions.IgnoreCase))
            {
                // 提取域名之后的函数名称路径
                var methodPath = Regex.Replace(e.Request.Uri, @"^"+AppRuntime.RunTime.ApiDomain+"/{0,}", string.Empty);
                
                // 剔除路径的参数, 提取函数名称
                methodPath = Regex.Replace(methodPath, @"\?.*",string.Empty).ToLower();

                // 查找 API 路由
                var route = AppRuntime.RunTime.Routes.FirstOrDefault(m => m.RoutePath == methodPath && m.RouteMethod.Method.ToUpper() == e.Request.Method.ToUpper());
                
                // 执行API
                e.Response = route?.ExecuteCore(e.Request);
            }
        }
    }
}