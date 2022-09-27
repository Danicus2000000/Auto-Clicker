using System;
using System.Runtime.InteropServices;

public class MouseEvents
{
    [Flags]
    public enum MouseEventFlags//different mouse events that can be pressed
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

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);//used to get cursor position from system

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);//wrapper for system mouse events
    public static MousePoint GetCursorPosition()//called to get mouse position from system
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    /// <summary>
    /// Does a mouse event
    /// </summary>
    /// <param name="value">The selected Mouse Event from the enum of options</param>
    public static void MouseEvent(MouseEventFlags value)
    {
        MousePoint position = GetCursorPosition();//get mouse position

        mouse_event//call a mouse event using position and requested call
            ((int)value,
             position.X,
             position.Y,
             0,
             0)
            ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint//stores layout for mousepoint and method to create it
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}