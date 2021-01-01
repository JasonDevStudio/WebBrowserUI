namespace CefGlue.Lib.Browser
{
    using System;

    /// <summary>
    /// Defines the <see cref="TitleChangedEventArgs" />.
    /// </summary>
    public sealed class TitleChangedEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// Defines the _title.
        /// </summary>
        private readonly string _title;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleChangedEventArgs"/> class.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        public TitleChangedEventArgs(string title)
        {
            _title = title;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        #endregion
    }
}
