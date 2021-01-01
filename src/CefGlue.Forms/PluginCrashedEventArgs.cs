namespace Xilium.CefGlue.WindowsForms
{
    using System;

    /// <summary>
    /// Defines the <see cref="PluginCrashedEventArgs" />.
    /// </summary>
    public class PluginCrashedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginCrashedEventArgs"/> class.
        /// </summary>
        /// <param name="pluginPath">The pluginPath<see cref="string"/>.</param>
        public PluginCrashedEventArgs(string pluginPath)
        {
            PluginPath = pluginPath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the PluginPath.
        /// </summary>
        public string PluginPath { get; private set; }

        #endregion
    }
}
