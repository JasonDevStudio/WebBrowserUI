using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using MimeTypeExtension;

namespace Microsoft.Web.WebView2.Core
{
    public class EmbeddedResourceHandler
    {
        /// <summary>
        /// Embedded Resources 请求事件
        /// </summary>
        /// <param name="e">CoreWebView2WebResourceRequestedEventArgs</param>
        public static void EmbeddedResourcesRequested(CoreWebView2WebResourceRequestedEventArgs e)
        {
            var domain = string.Empty;
            var dir = string.Empty;
            Assembly resource = null;
            
            foreach (var resourcesHandler in AppRuntime.RunTime.ResourcesHandlers.Where(resourcesHandler => Regex.IsMatch(e.Request.Uri, @"^" + resourcesHandler.uri + "/{0,}.*",RegexOptions.IgnoreCase)))
            {
                domain = resourcesHandler.uri;
                dir = resourcesHandler.dir;
                resource = resourcesHandler.resource;
                break;
            }

            // 判断是否满足嵌入资源拦截
            if (!string.IsNullOrWhiteSpace(domain) && resource != null)
            { 
                // 提取域名之后的取资源路径
                var resourcePath = Regex.Replace(e.Request.Uri, @"^"+domain+"/{0,}", string.Empty);
                
                // 剔除路径的参数, 提取资源路径
                resourcePath = Regex.Replace(resourcePath, @"\?.*",string.Empty).ToLower();
                resourcePath = resourcePath.Replace("/", ".");

                var memiType = new Uri(e.Request.Uri).MimeType();
                var stream = resource.GetManifestResourceStream($"{resource.GetName().Name}.{dir}.{resourcePath}"); //当作为一个资源被嵌入后，资源的完整名称会由项目的默认命名空间与文件名组成
                if (stream != null)
                    e.Response = Resource(stream, memiType);
            }
        }
        
        /// <summary>
        /// CoreWebView2WebResourceResponse
        /// </summary>
        /// <param name="stream">资源流</param>
        /// <param name="contentType">contentType</param> 
        /// <param name="statusCode">HttpStatusCode</param> 
        /// <returns></returns>
        protected static CoreWebView2WebResourceResponse Resource(Stream stream, string contentType = "text/plain", HttpStatusCode statusCode = HttpStatusCode.OK)
        { 
            var response = AppRuntime.RunTime.WebView2Environment.CreateWebResourceResponse(stream, (int) statusCode, nameof(statusCode), null);
            response.Headers.AppendHeader("Content-Type", contentType);
            response.Headers.AppendHeader("access-control-allow-origin", "*");
            return response;
        }
    }
}