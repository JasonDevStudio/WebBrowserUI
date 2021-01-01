namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="LoadEndEventArgs" />.
    /// </summary>
    public class LoadEndEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadEndEventArgs"/> class.
        /// </summary>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="httpStatusCode">The httpStatusCode<see cref="int"/>.</param>
        public LoadEndEventArgs(CefFrame frame, int httpStatusCode)
        {
            Frame = frame;
            HttpStatusCode = httpStatusCode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Frame.
        /// </summary>
        public CefFrame Frame { get; private set; }

        /// <summary>
        /// Gets the HttpStatusCode.
        /// </summary>
        public int HttpStatusCode { get; private set; }

        #endregion
    }
}
