namespace CefGlue.Lib.Browser
{
    using System;

    /// <summary>
    /// Defines the <see cref="AddressChangedEventArgs" />.
    /// </summary>
    public sealed class AddressChangedEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// Defines the _address.
        /// </summary>
        private readonly string _address;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressChangedEventArgs"/> class.
        /// </summary>
        /// <param name="address">The address<see cref="string"/>.</param>
        public AddressChangedEventArgs(string address)
        {
            _address = address;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Address.
        /// </summary>
        public string Address
        {
            get { return _address; }
        }

        #endregion
    }
}
