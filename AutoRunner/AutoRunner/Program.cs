
using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);


    private const uint MOUSEEVENTF_MOVE = 0x0001;
    private const uint KEYEVENTF_KEYDOWN = 0x0000;
    private const uint KEYEVENTF_KEYUP = 0x0002;
    public struct POINT
    {
        public int X;
        public int Y;
    }

    static void Main()
    {
        Console.WriteLine("Press 'Esc' to stop ");
        SimulateActivity();
    }

    static void SimulateActivity( )
    {
        while (true)
        {
            var arrowKeyList = new List<ConsoleKey> { ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow };
            Random r = new ();
            int rInt = r.Next(0, 4);
            var arrowKey = arrowKeyList[rInt];
            GetCursorPos(out POINT originalPosition);
            SetCursorPos(originalPosition.X + r.Next(1, 100), originalPosition.Y + r.Next(1, 100));
            mouse_event(MOUSEEVENTF_MOVE, r.Next(1, 100), r.Next(1, 100), 0, UIntPtr.Zero);
            SetCursorPos(originalPosition.X, originalPosition.Y);

            //// Pressing keyboad 
            ////keybd_event((byte)ConsoleKey.A, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            ////Thread.Sleep(1000); 
            ////keybd_event((byte)ConsoleKey.A, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            // Wait for 1 minute
            Thread.Sleep(r.Next(1000, 12000));
            for (int i = 0; i < r.Next(5, 1000); i++)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    return;
                }
                keybd_event((byte)arrowKey, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
                Thread.Sleep(r.Next(40, 500));
                keybd_event((byte)arrowKey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
        }
    }
}

