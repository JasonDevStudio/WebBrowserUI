using System;
using System.Collections.Specialized;
using System.IO;
using Xilium.CefGlue;

namespace CefGlue.Lib.Browser
{
    /// <summary>
    /// Defines the <see cref="WebResponse" />.
    /// </summary>
    public sealed class WebResponse : IDisposable
    {
        #region Fields

        /// <summary>
        /// Defines the _resourceRequest.
        /// </summary>
        private readonly WebRequest _resourceRequest;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebResponse"/> class.
        /// </summary>
        public WebResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebResponse"/> class.
        /// </summary>
        /// <param name="request">The request<see cref="WebRequest"/>.</param>
        public WebResponse(WebRequest request)
        {
            _resourceRequest = request;

            MimeType = CefRuntime.GetMimeType(FileExtension);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Raw Data
        /// </summary>
        public byte[] RawData { get; set; }

        /// <summary>
        /// Gets or sets the ContentStream.
        /// </summary>
        public Stream ContentStream { get; set; }

        /// <summary>
        /// Gets the FileExtension.
        /// </summary>
        public string FileExtension => Path.GetExtension(FileName).TrimStart('.');

        /// <summary>
        /// Gets the FileName.
        /// </summary>
        public string FileName => Path.GetFileName(RelativePath);

        /// <summary>
        /// Gets a value indicating whether HasFileName.
        /// </summary>
        public bool HasFileName => !string.IsNullOrEmpty(FileName);

        /// <summary>
        /// Gets the Headers.
        /// </summary>
        public NameValueCollection Headers { get; } = new NameValueCollection();

        /// <summary>
        /// Gets or sets the HttpStatus.
        /// </summary>
        public System.Net.HttpStatusCode HttpStatus { get; set; } = System.Net.HttpStatusCode.OK;

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public long Length => ContentStream?.Length ?? 0;

        /// <summary>
        /// Gets or sets the MimeType.
        /// </summary>
        public string MimeType { get; set; } = "text/plain";

        /// <summary>
        /// Gets the RelativePath.
        /// </summary>
        public string RelativePath => $"{_resourceRequest?.Uri?.LocalPath ?? string.Empty}".Trim('/');

        /// <summary>
        /// StatusText
        /// </summary>
        public string StatusText { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The Content.
        /// </summary>
        /// <param name="buff">The buff<see cref="byte[]"/>.</param>
        /// <param name="contentType">The contentType<see cref="string"/>.</param>
        public void Content(byte[] buff, string contentType = null)
        {
            if (!string.IsNullOrEmpty(contentType))
            {
                MimeType = contentType;
            }

            Headers.Set("Content-Type", MimeType);

            if (ContentStream != null)
            {
                ContentStream.Dispose();
                ContentStream = null;
            }

            ContentStream = new MemoryStream(buff);

            HttpStatus = System.Net.HttpStatusCode.OK;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            if (ContentStream != null)
            {
                ContentStream.Close();
                ContentStream.Dispose();
                ContentStream = null;
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
