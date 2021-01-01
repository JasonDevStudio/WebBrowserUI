namespace Xilium.CefGlue.WindowsForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines the <see cref="NativeMethods" />.
    /// </summary>
    internal static class NativeMethods
    {
        #region Methods

        /// <summary>
        /// The GetFocus.
        /// </summary>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        /// <summary>
        /// The SetWindowPos.
        /// </summary>
        /// <param name="hWnd">The hWnd<see cref="IntPtr"/>.</param>
        /// <param name="hWndInsertAfter">The hWndInsertAfter<see cref="IntPtr"/>.</param>
        /// <param name="X">The X<see cref="int"/>.</param>
        /// <param name="Y">The Y<see cref="int"/>.</param>
        /// <param name="cx">The cx<see cref="int"/>.</param>
        /// <param name="cy">The cy<see cref="int"/>.</param>
        /// <param name="uFlags">The uFlags<see cref="SetWindowPosFlags"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        #endregion
    }
}
