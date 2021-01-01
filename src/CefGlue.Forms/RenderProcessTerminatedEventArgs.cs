namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="RenderProcessTerminatedEventArgs" />.
    /// </summary>
    public class RenderProcessTerminatedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderProcessTerminatedEventArgs"/> class.
        /// </summary>
        /// <param name="status">The status<see cref="CefTerminationStatus"/>.</param>
        public RenderProcessTerminatedEventArgs(CefTerminationStatus status)
        {
            Status = status;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Status.
        /// </summary>
        public CefTerminationStatus Status { get; private set; }

        #endregion
    }
}
