using System.Runtime.InteropServices;

internal class Program
{
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT point);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
        public override string ToString() => $"X={X}, Y={Y}";
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Press ESC to stop... \n\n");

        int i = 0;

        while (true)
        {
            // Stop when ESC is pressed
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nStopping program...");
                break;
            }

            // Get current cursor position
            GetCursorPos(out POINT current);
            //Console.WriteLine($"Current: {current}");

            // Move cursor
            int newX = 1 + i;
            int newY = 1 + i;
            SetCursorPos(newX, newY);

            // Confirm new position
            GetCursorPos(out POINT updated);
            //Console.WriteLine($"Moved To: {updated}");
            Console.Write($"{i}-");

            i++;

            Thread.Sleep(2000); // wait 2 seconds
        }
    }
}