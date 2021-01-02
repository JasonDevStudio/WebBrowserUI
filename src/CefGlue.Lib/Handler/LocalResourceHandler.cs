using CefGlue.Lib.Browser;
using CefGlue.Lib.Framework;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xilium.CefGlue;

namespace CefGlue.Lib.Hanlers
{
    /// <summary>
    /// 本地资源处理程序.
    /// </summary>
    public class LocalResourceHandler : ResourceHandlerBase
    {
        #region Methods

        /// <summary>
        /// Embedded Resources 请求事件.
        /// </summary>
        /// <param name="request">The request<see cref="WebRequest"/>.</param>
        /// <returns>The <see cref="WebResponse"/>.</returns>
        public WebResponse LocalResourcesRequested(WebRequest request)
        {
            var resourceAssemblyPath = Path.GetDirectoryName(this.ResourceAssembly.Location);
            
            // 判断是否满足嵌入资源拦截
            if (!string.IsNullOrWhiteSpace(this.Domain) && !string.IsNullOrWhiteSpace(resourceAssemblyPath))
            {
                // 提取域名之后的取资源路径
                var resourcePath = Regex.Replace(request.RequestUrl, @"^" + this.Uri + "/{0,}", string.Empty);

                // 剔除路径的参数, 提取资源路径
                resourcePath = Regex.Replace(resourcePath, @"\?.*", string.Empty).ToLower();

                var memiType = CefRuntime.GetMimeType(resourcePath);
                var fileInfo = new FileInfo($"{resourceAssemblyPath}.{this.Dir}.{resourcePath}");
                if (fileInfo.Exists)
                {
                    var stream = fileInfo.OpenRead();
                    if (stream != null)
                        return Resource(stream, memiType);
                }
            }

            return default;
        }

        #endregion
    }
}
