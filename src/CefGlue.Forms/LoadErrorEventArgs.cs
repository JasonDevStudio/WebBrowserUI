namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="LoadErrorEventArgs" />.
    /// </summary>
    public class LoadErrorEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadErrorEventArgs"/> class.
        /// </summary>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="CefErrorCode"/>.</param>
        /// <param name="errorText">The errorText<see cref="string"/>.</param>
        /// <param name="failedUrl">The failedUrl<see cref="string"/>.</param>
        public LoadErrorEventArgs(CefFrame frame, CefErrorCode errorCode, string errorText, string failedUrl)
        {
            Frame = frame;
            ErrorCode = errorCode;
            ErrorText = errorText;
            FailedUrl = failedUrl;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ErrorCode.
        /// </summary>
        public CefErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// Gets the ErrorText.
        /// </summary>
        public string ErrorText { get; private set; }

        /// <summary>
        /// Gets the FailedUrl.
        /// </summary>
        public string FailedUrl { get; private set; }

        /// <summary>
        /// Gets the Frame.
        /// </summary>
        public CefFrame Frame { get; private set; }

        #endregion
    }
}
