namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="TitleChangedEventArgs" />.
    /// </summary>
    public class TitleChangedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleChangedEventArgs"/> class.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        public TitleChangedEventArgs(string title)
        {
            Title = title;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title { get; private set; }

        #endregion
    }
}
