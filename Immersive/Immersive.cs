
using System.Runtime.InteropServices;

namespace Immersive
{
    public static class Immersive
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public static void EnableImmersiveDarkMode(IntPtr hwnd, bool enable)
        {
            int useImmersiveDarkMode = enable ? 1 : 0;
            DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useImmersiveDarkMode, Marshal.SizeOf(useImmersiveDarkMode));
        }


    }

}
