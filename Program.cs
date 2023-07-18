using System.Reflection.Metadata.Ecma335;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static NativeMethods;

Console.WriteLine("Anti idle");

PowerHelper.ForceSystemAwake(); 

var lastMousePosition = MouseHelper.GetMouseCurrent();

const int SECONDS_DELAY = 25; 

while (true) {
    //Wait thread 
    Thread.Sleep(new TimeSpan(0,0,SECONDS_DELAY));

    //Check if current mouse position is the same that previous one, if so, then move the mouse to avoid idle
    var current = MouseHelper.GetMouseCurrent();
    Console.WriteLine($"current x:{current.x} y:{current.y}");
    if (current.x == lastMousePosition.x && current.y == lastMousePosition.y) {
        MouseHelper.MoveAndClick(0,0);
    }
    lastMousePosition = current;
    Console.WriteLine("ping");
}


internal class PowerHelper
{
    public static void ForceSystemAwake()
    {
        NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS |
                                              NativeMethods.EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                                              NativeMethods.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                              NativeMethods.EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
    }

    public static void ResetSystemDefault()
    {
        NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS);
    }
}

internal class MouseHelper { 

    public static void MoveAndClick(int x, int y) {
        NativeMethods.POINT p = new NativeMethods.POINT(x,y);
        IntPtr handle = Process.GetCurrentProcess().Handle;
        NativeMethods.ClientToScreen(handle, ref p);
        NativeMethods.SetCursorPos(p.x, p.y);

        NativeMethods.MouseEvent(NativeMethods.MouseEventFlags.LeftDown | NativeMethods.MouseEventFlags.LeftUp);
    }

    public static POINT GetMouseCurrent() => NativeMethods.GetCursorPosition();
}

internal static partial class NativeMethods
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out POINT lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
    [DllImport("User32.Dll")]
    public static extern long SetCursorPos(int x, int y);
    [DllImport("User32.Dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);
    public static POINT GetCursorPosition()
    {
        POINT currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new POINT(0, 0); }
        return currentMousePoint;
    }
    public static void MouseEvent(MouseEventFlags value)
    {
        POINT position = GetCursorPosition();

        mouse_event
            ((int)value,
             position.x,
             position.y,
             0,
             0)
            ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;

        public POINT(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001

        // Legacy flag, should not be used.
        // ES_USER_PRESENT = 0x00000004
    }

    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }
}