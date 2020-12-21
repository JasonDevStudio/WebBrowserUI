using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
        /// Json Hander
        /// </summary>
        private const string CONTENT_TYPE_APPLICATION_JSON = "application/json";
 
        /// <summary>
        /// JsonSerializerOptions
        /// </summary>
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
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
            var response =
                WebView2Environment.CreateWebResourceResponse(stream, (int) statusCode, nameof(statusCode), null);
            response.Headers.AppendHeader("Content-Type", "application/json");
            response.Headers.AppendHeader("access-control-allow-origin", "*");
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
            var response =
                WebView2Environment.CreateWebResourceResponse(stream, (int) statusCode, nameof(statusCode), null);
            response.Headers.AppendHeader("Content-Type", "application/json");
            response.Headers.AppendHeader("access-control-allow-origin", "*");
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
            var response =
                WebView2Environment.CreateWebResourceResponse(stream, (int) statusCode, nameof(statusCode), null);
            response.Headers.AppendHeader("Content-Type", "application/json");
            response.Headers.AppendHeader("access-control-allow-origin", "*");
            return response;
        } 
        
        /// <summary>
        /// 判断是否JSON格式
        /// </summary>
        /// <param name="request">CoreWebView2WebResourceRequest</param>
        /// <returns>IsJson</returns>
        protected bool IsJson(CoreWebView2WebResourceRequest request)
        {
            var contentType = request.Headers.GetHeader("Content-Type");

            return string.IsNullOrWhiteSpace(contentType)
                ? false
                : Regex.IsMatch(contentType, @"application/json", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获取编码方式
        /// </summary>
        /// <param name="request">CoreWebView2WebResourceRequest</param>
        /// <returns>Encoding</returns>
        protected Encoding GetEncoding(CoreWebView2WebResourceRequest request)
        { 
                var encoding = request.Headers.GetHeader("Content-Type");;

                if (string.IsNullOrEmpty(encoding) || !encoding.Contains("charset=")) 
                    encoding = "utf-8"; 
                else
                {
                    // match "charset=xxx"
                    var match = Regex.Match(encoding, @"(?<=charset=)(([^;,\r\n]))*");

                    if (match.Success)
                    {
                        encoding = match.Value;
                    }
                }

                return Encoding.GetEncoding(encoding); 
        }
        
        /// <summary>
        ///  将 Request Json 数据反序列化
        /// </summary>
        /// <param name="request">CoreWebView2WebResourceRequest</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T</returns>
        protected T DeserializeRequestJson<T>(CoreWebView2WebResourceRequest request)
        {
            if (!IsJson(request)) return default;
            if (request.Content == null) return default;
            
            try
            {
                var buffer = new Span<byte>();
                request.Content.Read(buffer);
                var json = GetEncoding(request).GetString(buffer);
                return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions);
            }
            catch
            {
                return default;
            } 
        }
        
        /// <summary>
        /// 解析参数
        /// </summary>
        /// <param name="query">query string</param>
        /// <returns>NameValueCollection</returns>
        protected NameValueCollection GetQueryParameters(string query)
        {
            var dicParameters = new NameValueCollection();
            query = query.Trim('?');
            foreach (var pair in query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var keyvalue = pair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                switch (keyvalue.Length)
                {
                    case 2:
                        dicParameters.Add(keyvalue[0], Uri.UnescapeDataString(keyvalue[1]));
                        break;
                    case 1:
                        dicParameters.Add(keyvalue[0], null);
                        break;
                }
            }

            return dicParameters;
        }

        /// <summary>
        /// 解析参数
        /// </summary>
        /// <param name="request">CoreWebView2WebResourceRequest</param>
        /// <returns>NameValueCollection</returns>
        protected NameValueCollection GetQueryParameters(CoreWebView2WebResourceRequest request)
        {
            var buffer = new Span<byte>();
            request.Content.Read(buffer); 
            var query = GetEncoding(request).GetString(buffer);
            var dicParameters = new NameValueCollection();
            query = query.Trim('?');

            foreach (var pair in query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var keyvalue = pair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyvalue.Length == 2)
                {
                    dicParameters.Add(keyvalue[0], Uri.UnescapeDataString(keyvalue[1]));
                }
                else if (keyvalue.Length == 1)
                {
                    dicParameters.Add(keyvalue[0], null);
                }
            }

            return dicParameters;
        }
    }
}