namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="LoadingStateChangeEventArgs" />.
    /// </summary>
    public class LoadingStateChangeEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingStateChangeEventArgs"/> class.
        /// </summary>
        /// <param name="isLoading">The isLoading<see cref="bool"/>.</param>
        /// <param name="canGoBack">The canGoBack<see cref="bool"/>.</param>
        /// <param name="canGoForward">The canGoForward<see cref="bool"/>.</param>
        public LoadingStateChangeEventArgs(bool isLoading, bool canGoBack, bool canGoForward)
        {
            IsLoading = isLoading;
            CanGoBack = canGoBack;
            CanGoForward = canGoForward;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether CanGoBack.
        /// </summary>
        public bool CanGoBack { get; private set; }

        /// <summary>
        /// Gets a value indicating whether CanGoForward.
        /// </summary>
        public bool CanGoForward { get; private set; }

        /// <summary>
        /// Gets a value indicating whether IsLoading.
        /// </summary>
        public bool IsLoading { get; private set; }

        #endregion
    }
}
