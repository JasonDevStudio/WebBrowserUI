using CefGlue.Lib.Browser;
using CefGlue.Lib.Framework;
using System.Linq;
using System.Text.RegularExpressions;

namespace CefGlue.Lib.Hanlers
{
    /// <summary>
    /// Defines the <see cref="ApiResourceHandler" />.
    /// </summary>
    public class ApiResourceHandler : ResourceHandlerBase
    {
        #region Methods

        /// <summary>
        /// API 请求事件.
        /// </summary>
        /// <param name="request">The request<see cref="WebRequest"/>.</param>
        /// <returns>The <see cref="WebResponse"/>.</returns>
        public override WebResponse ExecuteCore(WebRequest request)
        {
            // 判断是否为API 域名请求
            if (Regex.IsMatch(request.RequestUrl, @"^" + this.Uri + "/{0,}.*", RegexOptions.IgnoreCase))
            {
                // 提取域名之后的函数名称路径
                var methodPath = Regex.Replace(request.RequestUrl, @"^" + this.Uri + "/{0,}", string.Empty);

                // 剔除路径的参数, 提取函数名称
                methodPath = Regex.Replace(methodPath, @"\?.*", string.Empty).ToLower();

                // 查找 API 路由
                var route = AppRuntime.RunTime.Routes.FirstOrDefault(m => m.RoutePath == methodPath && m.RouteMethod.Method.ToUpper() == request.Method.ToUpper());

                // 执行API
                return route?.ExecuteCore(request);
            }

            return default;
        }

        #endregion
    }
}
