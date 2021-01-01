namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="ConsoleMessageEventArgs" />.
    /// </summary>
    public class ConsoleMessageEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMessageEventArgs"/> class.
        /// </summary>
        /// <param name="level">The level<see cref="CefLogSeverity"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <param name="line">The line<see cref="int"/>.</param>
        public ConsoleMessageEventArgs(CefLogSeverity level, string message, string source, int line)
        {
            Level = level;
            Message = message;
            Source = source;
            Line = line;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether Handled.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets the Level.
        /// </summary>
        public CefLogSeverity Level { get; private set; }

        /// <summary>
        /// Gets the Line.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the Message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the Source.
        /// </summary>
        public string Source { get; private set; }

        #endregion
    }
}
