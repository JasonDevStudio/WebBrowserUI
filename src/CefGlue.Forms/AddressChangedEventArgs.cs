namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="AddressChangedEventArgs" />.
    /// </summary>
    public class AddressChangedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressChangedEventArgs"/> class.
        /// </summary>
        /// <param name="frame">The frame<see cref="CefFrame"/>.</param>
        /// <param name="address">The address<see cref="string"/>.</param>
        public AddressChangedEventArgs(CefFrame frame, string address)
        {
            Address = address;
            Frame = frame;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the Frame.
        /// </summary>
        public CefFrame Frame { get; private set; }

        #endregion
    }
}
