using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Google.Protobuf;

namespace Microsoft.Web.WebView2.Core
{
    /// <summary>
    /// ViewModelBase
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class ViewModelBase
    {
        /// <summary>
        /// This represents the WebView2 Environment.
        /// WebView2环境变量
        /// </summary>
        public CoreWebView2Environment WebView2Environment { get; set; }

        /// <summary>
        /// GRPC Proto 对象 To CoreWebView2WebResourceResponse
        /// </summary>
        /// <param name="data">IMessage</param>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <returns>CoreWebView2WebResourceResponse</returns>
        protected CoreWebView2WebResourceResponse Proto(IMessage data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var buffer = data.ToByteArray();
            var stream = new MemoryStream(buffer);
            var contType = $"content-type: application/json";
            var response =
                WebView2Environment.CreateWebResourceResponse(stream, (int) statusCode, nameof(statusCode), contType);
            return response;
        }

        /// <summary>
        /// 数据对象 To Json CoreWebView2WebResourceResponse
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <param name="jsonSerializerOptions">jsonSerializerOptions</param>
        /// <returns>CoreWebView2WebResourceResponse</returns>
        protected CoreWebView2WebResourceResponse Json(object data, HttpStatusCode statusCode = HttpStatusCode.OK,
            System.Text.Json.JsonSerializerOptions jsonSerializerOptions = null)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(data, jsonSerializerOptions);
            var buffer = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(buffer);
            var contType = $"content-type: application/json";
            var response = WebView2Environment.CreateWebResourceResponse(stream, (int) statusCode, nameof(statusCode), contType);
            response.Headers.AppendHeader("content-type", "application/json");
            return response;
        }

        /// <summary>
        /// 文本数据 To CoreWebView2WebResourceResponse
        /// </summary>
        /// <param name="data">字符串文本</param>
        /// <param name="statusCode">HttpStatusCode</param> 
        /// <returns></returns>
        protected CoreWebView2WebResourceResponse Text(string data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var stream = new MemoryStream(buffer);
            var contType = $"content-type: text/plain";
            var response = WebView2Environment.CreateWebResourceResponse(stream, (int) statusCode, nameof(statusCode), contType);
            response.Headers.AppendHeader("content-type", "text/plain");
            return response;
        }
    }
}