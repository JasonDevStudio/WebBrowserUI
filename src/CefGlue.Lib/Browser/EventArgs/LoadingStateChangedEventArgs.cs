namespace CefGlue.Lib.Browser
{
    using System;

    /// <summary>
    /// Defines the <see cref="LoadingStateChangedEventArgs" />.
    /// </summary>
    public sealed class LoadingStateChangedEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// Defines the _canGoBack.
        /// </summary>
        private readonly bool _canGoBack;

        /// <summary>
        /// Defines the _canGoForward.
        /// </summary>
        private readonly bool _canGoForward;

        /// <summary>
        /// Defines the _isLoading.
        /// </summary>
        private readonly bool _isLoading;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingStateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="isLoading">The isLoading<see cref="bool"/>.</param>
        /// <param name="canGoBack">The canGoBack<see cref="bool"/>.</param>
        /// <param name="canGoForward">The canGoForward<see cref="bool"/>.</param>
        public LoadingStateChangedEventArgs(bool isLoading, bool canGoBack, bool canGoForward)
        {
            _isLoading = isLoading;
            _canGoBack = canGoBack;
            _canGoForward = canGoForward;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether CanGoBack.
        /// </summary>
        public bool CanGoBack
        {
            get { return _canGoBack; }
        }

        /// <summary>
        /// Gets a value indicating whether CanGoForward.
        /// </summary>
        public bool CanGoForward
        {
            get { return _canGoForward; }
        }

        /// <summary>
        /// Gets a value indicating whether Loading.
        /// </summary>
        public bool Loading
        {
            get { return _isLoading; }
        }

        #endregion
    }
}
