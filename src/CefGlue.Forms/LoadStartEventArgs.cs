namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="LoadStartEventArgs" />.
    /// </summary>
    public class LoadStartEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadStartEventArgs"/> class.
        /// </summary>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        public LoadStartEventArgs(CefFrame frame)
        {
            Frame = frame;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Frame.
        /// </summary>
        public CefFrame Frame { get; private set; }

        #endregion
    }
}
