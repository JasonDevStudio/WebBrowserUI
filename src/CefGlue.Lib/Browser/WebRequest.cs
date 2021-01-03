using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Xilium.CefGlue;

namespace CefGlue.Lib.Browser
{
    /// <summary>
    /// Defines the <see cref="WebRequest" />.
    /// </summary>
    public sealed class WebRequest
    {
        #region Constants

        /// <summary>
        /// Defines the CONTENT_TYPE_APPLICATION_JSON.
        /// </summary>
        private const string CONTENT_TYPE_APPLICATION_JSON = "application/json";

        /// <summary>
        /// Defines the CONTENT_TYPE_FORM_URL_ENCODED.
        /// </summary>
        private const string CONTENT_TYPE_FORM_URL_ENCODED = "application/x-www-form-urlencoded";

        #endregion

        #region Fields

        /// <summary>
        /// Defines the _jsonSerializerOptions.
        /// </summary>
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebRequest"/> class.
        /// </summary>
        /// <param name="uri">The uri<see cref="Uri"/>.</param>
        /// <param name="method">The method<see cref="string"/>.</param>
        /// <param name="headers">The headers<see cref="NameValueCollection"/>.</param>
        /// <param name="postData">The postData<see cref="byte[]"/>.</param>
        /// <param name="uploadFiles">The uploadFiles<see cref="string[]"/>.</param>
        /// <param name="cefRequest">The cefRequest<see cref="CefRequest"/>.</param>
        public WebRequest(Uri uri, string method, NameValueCollection headers, byte[] postData, string[] uploadFiles,
            CefRequest cefRequest)
        {
            Uri = uri;
            Method = method;
            Headers = headers;
            RawData = postData;
            UploadFiles = uploadFiles;
            RawRequest = cefRequest;
            QueryString = ProcessQueryString(uri.Query);

            if (ContentType != null && ContentType.Contains(CONTENT_TYPE_FORM_URL_ENCODED) && RawData != null)
            {
                FormData = ProcessFormData(RawData);
            }
            else
            {
                FormData = new NameValueCollection();
            }

            if (IsJson && RawData != null)
            {
                try
                {
                    JsonData = JsonValue.Parse(Encoding.UTF8.GetString(RawData));
                }
                catch
                {
                    JsonData = string.Empty;
                }
            }
        }

        /// <summary>
        /// 创建 WebRequest
        /// </summary>
        /// <param name="request">CefRequest</param>
        /// <returns>WebRequest</returns>
        public static WebRequest Create(CefRequest request)
        {
            var uri = new Uri(request.Url);
            var headers = request.GetHeaderMap();
            (byte[] rawData, string[] uploadFiles) = GetPostData(request);
            var webRequest = new WebRequest(uri, request.Method, headers, rawData, uploadFiles, request);
            return webRequest;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ContentEncoding.
        /// </summary>
        public Encoding ContentEncoding
        {
            get
            {
                var encoding = ContentType;

                if (string.IsNullOrEmpty(encoding) || !encoding.Contains("charset="))
                {
                    encoding = "utf-8";
                }
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
        }

        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public string ContentType => Headers?.Get("Content-Type");

        /// <summary>
        /// Gets the FormData.
        /// </summary>
        public NameValueCollection FormData { get; } = null;

        /// <summary>
        /// Gets the Headers.
        /// </summary>
        public NameValueCollection Headers { get; }

        /// <summary>
        /// Gets a value indicating whether IsJson.
        /// </summary>
        public bool IsJson
        {
            get
            {
                if (string.IsNullOrEmpty(ContentType))
                {
                    return false;
                }

                return ContentType.Contains(CONTENT_TYPE_APPLICATION_JSON);
            }
        }

        /// <summary>
        /// Gets the JsonData.
        /// </summary>
        public JsonValue JsonData { get; } = null;

        /// <summary>
        /// Gets the Method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets the QueryString.
        /// </summary>
        public NameValueCollection QueryString { get; } = null;

        /// <summary>
        /// Gets the RawData.
        /// </summary>
        public byte[] RawData { get; }

        /// <summary>
        /// Gets the RawRequest.
        /// </summary>
        public CefRequest RawRequest { get; }

        /// <summary>
        /// Gets the RequestUrl.
        /// </summary>
        public string RequestUrl
        {
            get
            {
                var original = Uri.OriginalString;
                if (original.IndexOf("?") >= 0)
                {
                    return original.Substring(0, original.IndexOf("?"));
                }

                return original;
            }
        }

        /// <summary>
        /// Gets the StringContent.
        /// </summary>
        public string StringContent
        {
            get
            {
                if (RawData == null) return string.Empty;

                return ContentEncoding.GetString(RawData);
            }
        }

        /// <summary>
        /// Gets the UploadFiles.
        /// </summary>
        public string[] UploadFiles { get; }

        /// <summary>
        /// Gets the Uri.
        /// </summary>
        public Uri Uri { get; }

        #endregion

        #region Methods

        /// <summary>
        /// The DeserializeObjectFromJson.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        public T DeserializeObjectFromJson<T>()
        {
            if (IsJson)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(RawData), _jsonSerializerOptions);
                }
                catch
                {
                    return default;
                }
            }

            return default;
        }

        /// <summary>
        /// The ProcessFormData.
        /// </summary>
        /// <param name="rawData">The rawData<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="NameValueCollection"/>.</returns>
        private NameValueCollection ProcessFormData(byte[] rawData)
        {
            var query = ContentEncoding.GetString(rawData);

            var retval = new NameValueCollection();


            query = query.Trim('?');

            foreach (var pair in query.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries))
            {
                var keyvalue = pair.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                if (keyvalue.Length == 2)
                {
                    retval.Add(keyvalue[0], Uri.UnescapeDataString(keyvalue[1]));
                }
                else if (keyvalue.Length == 1)
                {
                    retval.Add(keyvalue[0], null);
                }
            }

            return retval;
        }

        /// <summary>
        /// The ProcessQueryString.
        /// </summary>
        /// <param name="query">The query<see cref="string"/>.</param>
        /// <returns>The <see cref="NameValueCollection"/>.</returns>
        private NameValueCollection ProcessQueryString(string query)
        {
            var retval = new NameValueCollection();

            query = query.Trim('?');
            foreach (var pair in query.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries))
            {
                var keyvalue = pair.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                if (keyvalue.Length == 2)
                {
                    retval.Add(keyvalue[0], Uri.UnescapeDataString(keyvalue[1]));
                }
                else if (keyvalue.Length == 1)
                {
                    retval.Add(keyvalue[0], null);
                }
            }

            return retval;
        }

        /// <summary>
        /// GetPostData
        /// </summary>
        /// <param name="request">CefRequest</param>
        /// <returns>byte[]</returns>
        public static (byte[] rawData, string[] uploadFiles) GetPostData(CefRequest request)
        {
            byte[] postData = null;
            var uploadFiles = new List<string>();

            if (request.PostData != null)
            {
                var items = request.PostData.GetElements();

                if (items != null && items.Length > 0)
                {
                    var bytes = new List<byte>();
                    foreach (var item in items)
                    {
                        var buffer = item.GetBytes();

                        //var size = (int)item.BytesCount;

                        switch (item.ElementType)
                        {
                            case CefPostDataElementType.Bytes:
                                bytes.AddRange(buffer);
                                break;
                            case CefPostDataElementType.File:
                                uploadFiles.Add(item.GetFile());
                                break;
                        }
                    }

                    postData = bytes.ToArray();
                    bytes = null;
                }
            }

            return (postData, uploadFiles.ToArray());
        }

        #endregion
    }
}