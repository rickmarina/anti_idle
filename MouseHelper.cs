using System.Diagnostics;
using static WindowsNativeMethods;

internal class MouseHelper { 

    public static void MoveAndClick(int x, int y) {
        WindowsNativeMethods.POINT p = new WindowsNativeMethods.POINT(x,y);
        IntPtr handle = Process.GetCurrentProcess().Handle;
        WindowsNativeMethods.ClientToScreen(handle, ref p);
        WindowsNativeMethods.SetCursorPos(p.x, p.y);

        WindowsNativeMethods.MouseEvent(WindowsNativeMethods.MouseEventFlags.LeftDown | WindowsNativeMethods.MouseEventFlags.LeftUp);
    }

    public static POINT GetMouseCurrent() => WindowsNativeMethods.GetCursorPosition();
}