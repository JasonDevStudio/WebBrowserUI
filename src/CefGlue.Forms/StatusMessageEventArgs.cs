namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="StatusMessageEventArgs" />.
    /// </summary>
    public sealed class StatusMessageEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// Defines the _value.
        /// </summary>
        private readonly string _value;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMessageEventArgs"/> class.
        /// </summary>
        /// <param name="value">The value<see cref="string"/>.</param>
        public StatusMessageEventArgs(string value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Value.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        #endregion
    }
}
