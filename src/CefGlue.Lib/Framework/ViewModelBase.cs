using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Google.Protobuf;
using WebResponse = CefGlue.Lib.Browser.WebResponse;

namespace CefGlue.Lib.Framework
{
    /// <summary>
    /// ViewModelBase
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class ViewModelBase
    {
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
        protected WebResponse Proto(IMessage data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var buffer = data.ToByteArray();
            var stream = new MemoryStream(buffer);
            var contentType = CONTENT_TYPE_APPLICATION_JSON;
            var response = new WebResponse { ContentStream = stream, HttpStatus = statusCode, MimeType = contentType };
            response.Headers.Add("Content-Type", contentType);
            response.Headers.Add("access-control-allow-origin", "*");
            return response;
        }

        /// <summary>
        /// 数据对象 To Json CoreWebView2WebResourceResponse
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <param name="jsonSerializerOptions">jsonSerializerOptions</param>
        /// <returns>CoreWebView2WebResourceResponse</returns>
        protected WebResponse Json(object data, HttpStatusCode statusCode = HttpStatusCode.OK,
            System.Text.Json.JsonSerializerOptions jsonSerializerOptions = null)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(data, jsonSerializerOptions);
            var buffer = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(buffer);
            var contentType = CONTENT_TYPE_APPLICATION_JSON;
            var response = new WebResponse { ContentStream = stream, HttpStatus = statusCode, MimeType = contentType };
            response.Headers.Add("Content-Type", contentType);
            response.Headers.Add("access-control-allow-origin", "*");
            return response;
        }

        /// <summary>
        /// 文本数据 To CoreWebView2WebResourceResponse
        /// </summary>
        /// <param name="data">字符串文本</param>
        /// <param name="statusCode">HttpStatusCode</param> 
        /// <returns></returns>
        protected WebResponse Text(string data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var stream = new MemoryStream(buffer);
            var contentType = "text/plain";
            var response = new WebResponse { ContentStream = stream, HttpStatus = statusCode, MimeType = contentType };
            response.Headers.Add("Content-Type", contentType);
            response.Headers.Add("access-control-allow-origin", "*");
            return response;
        }
 
        /// <summary>
        /// 解析参数
        /// </summary>
        /// <param name="queryString">query string</param>
        /// <returns>NameValueCollection</returns>
        protected NameValueCollection GetQueryParameters(string queryString)
        {
            var dicParameters = new NameValueCollection();
            queryString = queryString.Trim('?');
            queryString = queryString.IndexOf('?') > -1
                ? queryString.Substring(queryString.IndexOf('?') + 1)
                : queryString;
            foreach (var pair in queryString.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries))
            {
                var keyvalue = pair.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
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
    }
}