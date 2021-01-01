namespace CefGlue.Lib.Browser
{
    using System;

    /// <summary>
    /// Defines the <see cref="TargetUrlChangedEventArgs" />.
    /// </summary>
    public sealed class TargetUrlChangedEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// Defines the _targetUrl.
        /// </summary>
        private readonly string _targetUrl;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetUrlChangedEventArgs"/> class.
        /// </summary>
        /// <param name="targetUrl">The targetUrl<see cref="string"/>.</param>
        public TargetUrlChangedEventArgs(string targetUrl)
        {
            _targetUrl = targetUrl;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the TargetUrl.
        /// </summary>
        public string TargetUrl
        {
            get { return _targetUrl; }
        }

        #endregion
    }
}
