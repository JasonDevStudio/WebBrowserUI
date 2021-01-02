using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using Xilium.CefGlue;
using WebResponse = CefGlue.Lib.Browser.WebResponse;

namespace CefGlue.Lib.Hanlers
{
    /// <summary>
    /// Defines the <see cref="ResourceHandlerBase" />.
    /// </summary>
    public abstract class ResourceHandlerBase : CefResourceHandler
    {
        #region Fields

        /// <summary>
        /// Defines the respData.
        /// </summary>
        public WebResponse respData;

        /// <summary>
        /// Defines the _requestNo.
        /// </summary>
        private static int _requestNo;

        /// <summary>
        /// Defines the pos.
        /// </summary>
        private int pos;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Dir.
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// Gets or sets the Domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the ResourceAssembly.
        /// </summary>
        public Assembly ResourceAssembly { get; set; }

        /// <summary>
        /// Gets or sets the Scheme.
        /// </summary>
        public string Scheme { get; set; } = "http";

        /// <summary>
        /// Gets or sets the Uri.
        /// </summary>
        public string Uri => $"{Scheme}://{Domain}";
        
        #endregion

        #region Methods

        /// <summary>
        /// The Cancel.
        /// </summary>
        protected override void Cancel()
        {
        }

        /// <summary>
        /// The GetResponseHeaders.
        /// </summary>
        /// <param name="response">The response<see cref="CefResponse"/>.</param>
        /// <param name="responseLength">The responseLength<see cref="long"/>.</param>
        /// <param name="redirectUrl">The redirectUrl<see cref="string"/>.</param>
        protected override void GetResponseHeaders(CefResponse response, out long responseLength, out string redirectUrl)
        {
            response.MimeType = respData.MimeType ?? "text/plain";
            response.Status = (int)respData.HttpStatus;
            response.StatusText = respData.StatusText;

            var headers = new NameValueCollection(StringComparer.InvariantCultureIgnoreCase);
            headers.Add("Cache-Control", "private");

            foreach (var item in respData.Headers.AllKeys)
                headers.Add(item, respData.Headers[item]);

            response.SetHeaderMap(headers);

            responseLength = respData.Length;
            redirectUrl = null;
        }

        /// <summary>
        /// The Open.
        /// </summary>
        /// <param name="request">The request<see cref="CefRequest"/>.</param>
        /// <param name="handleRequest">The handleRequest<see cref="bool"/>.</param>
        /// <param name="callback">The callback<see cref="CefCallback"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool Open(CefRequest request, out bool handleRequest, CefCallback callback)
        {
            // Backwards compatibility. ProcessRequest will be called.
            callback.Dispose();
            handleRequest = false;
            return false;
        }

        /// <summary>
        /// The ProcessRequest.
        /// </summary>
        /// <param name="request">The request<see cref="CefRequest"/>.</param>
        /// <param name="callback">The callback<see cref="CefCallback"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool ProcessRequest(CefRequest request, CefCallback callback)
        {
            var webRequest = CefGlue.Lib.Browser.WebRequest.Create(request);
            this.respData = this.ExecuteCore(webRequest);  
            callback.Continue();
            return true;
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="dataOut">The dataOut<see cref="IntPtr"/>.</param>
        /// <param name="bytesToRead">The bytesToRead<see cref="int"/>.</param>
        /// <param name="bytesRead">The bytesRead<see cref="int"/>.</param>
        /// <param name="callback">The callback<see cref="CefResourceReadCallback"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool Read(IntPtr dataOut, int bytesToRead, out int bytesRead, CefResourceReadCallback callback)
        {
            // Backwards compatibility. ReadResponse will be called.
            callback.Dispose();
            bytesRead = -1;
            return false;
        }

        /// <summary>
        /// The ReadResponse.
        /// </summary>
        /// <param name="response">The response<see cref="Stream"/>.</param>
        /// <param name="bytesToRead">The bytesToRead<see cref="int"/>.</param>
        /// <param name="bytesRead">The bytesRead<see cref="int"/>.</param>
        /// <param name="callback">The callback<see cref="CefCallback"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        {
            if (bytesToRead == 0 || pos >= respData.Length)
            {
                bytesRead = 0;
                return false;
            }
            else
            {
                var br = (int)Math.Min(respData.Length - pos, bytesToRead);
                respData.ContentStream.CopyTo(response, br);
                pos += br;
                bytesRead = br;
                return true;
            }
        }

        /// <summary>
        /// WebResponse.
        /// </summary>
        /// <param name="stream">资源流.</param>
        /// <param name="contentType">contentType.</param>
        /// <param name="statusCode">HttpStatusCode.</param>
        /// <param name="statusText">statusText.</param>
        /// <returns>.</returns>
        protected virtual WebResponse Resource(Stream stream, string contentType = "text/plain", HttpStatusCode statusCode = HttpStatusCode.OK, string statusText = null)
        {
            var response = new WebResponse { ContentStream = stream, HttpStatus = statusCode, StatusText = statusText, MimeType = contentType };
            response.Headers.Add("Content-Type", contentType);
            response.Headers.Add("access-control-allow-origin", "*");
            return response;
        }

        /// <summary>
        /// The Skip.
        /// </summary>
        /// <param name="bytesToSkip">The bytesToSkip<see cref="long"/>.</param>
        /// <param name="bytesSkipped">The bytesSkipped<see cref="long"/>.</param>
        /// <param name="callback">The callback<see cref="CefResourceSkipCallback"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool Skip(long bytesToSkip, out long bytesSkipped, CefResourceSkipCallback callback)
        {
            bytesSkipped = (long)CefErrorCode.Failed;
            return false;
        }

        /// <summary>
        /// ExecuteCore
        /// </summary>
        /// <param name="request">WebRequest</param>
        /// <returns>WebResponse</returns>
        public abstract WebResponse ExecuteCore(CefGlue.Lib.Browser.WebRequest request);

        #endregion
    }
}
