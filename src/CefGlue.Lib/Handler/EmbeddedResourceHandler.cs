using CefGlue.Lib.Browser;
using System.Text.RegularExpressions;

namespace CefGlue.Lib.Hanlers
{
    /// <summary>
    /// Defines the <see cref="EmbeddedResourceHandler" />.
    /// </summary>
    public class EmbeddedResourceHandler : ResourceHandlerBase
    {
        #region Methods

        /// <summary>
        /// Embedded Resources 请求事件.
        /// </summary>
        /// <param name="request">The request<see cref="WebRequest"/>.</param>
        /// <returns>The <see cref="WebResponse"/>.</returns>
        public override WebResponse ExecuteCore(WebRequest request)
        { 
            // 判断是否满足嵌入资源拦截
            if (!string.IsNullOrWhiteSpace(this.Domain) && this.ResourceAssembly != null)
            {
                // 提取域名之后的取资源路径
                var resourcePath = Regex.Replace(request.RequestUrl, @"^" + this.Uri + "/{0,}", string.Empty);

                // 剔除路径的参数, 提取资源路径
                resourcePath = Regex.Replace(resourcePath, @"\?.*", string.Empty).ToLower();

                var memiType = MimeMapping.MimeUtility.GetMimeMapping(resourcePath);

                resourcePath = resourcePath.Replace("/", ".");

                var stream = this.ResourceAssembly.GetManifestResourceStream($"{this.ResourceAssembly.GetName().Name}.{this.Dir}.{resourcePath}"); //当作为一个资源被嵌入后，资源的完整名称会由项目的默认命名空间与文件名组成
                if (stream != null)
                    return Resource(stream, memiType);
            }

            return default;
        }

        #endregion
    }
}
