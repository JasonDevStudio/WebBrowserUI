namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="TooltipEventArgs" />.
    /// </summary>
    public class TooltipEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TooltipEventArgs"/> class.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        public TooltipEventArgs(string text)
        {
            Text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether Handled.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets the Text.
        /// </summary>
        public string Text { get; private set; }

        #endregion
    }
}
