using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace eimg
{
    internal class WindowAffinityHelper
    {
        private const uint WDA_NONE = 0;
        private const uint WDA_MONITOR = 1;

        [DllImport("user32.dll")]
        private static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint affinity);

        internal static bool EnableMonitorOnlyDisplay(Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            return SetWindowDisplayAffinity(helper.Handle, WDA_MONITOR);
        }

        internal static bool DisableMonitorOnlyDisplay(Window window)
        {
            var helper = new WindowInteropHelper(window);
            return SetWindowDisplayAffinity(helper.Handle, WDA_NONE);
        }
    }
}