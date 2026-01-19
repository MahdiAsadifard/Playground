namespace MouseMoverAppTray
{
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Timers;
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private Timer timer;

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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var iconsPath = Path.Combine(Directory.GetCurrentDirectory(), "Icons");

            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Start", Image.FromFile(iconsPath + "\\" + "start.ico"), OnStart);
            trayMenu.Items.Add("Stop", Image.FromFile(iconsPath + "\\" + "stop.ico"), OnStop);
            trayMenu.Items.Add("Exit", Image.FromFile(iconsPath + "\\" + "exit.ico"), OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Mouse Mover";
            trayIcon.Icon = new Icon(iconsPath + "\\" + "mouse.ico");
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }


        private void OnExit(object? sender, EventArgs e)
        {
            Application.Exit();
        }
        private void OnStart(object? sender, EventArgs e)
        {
            timer = new Timer(TimeSpan.FromSeconds(5));
            timer.Elapsed += (s, ev) => MoveMouse();
            timer.Start();
        }
        private void OnStop(object? sender, EventArgs e)
        {
            timer?.Stop();
        }

        private void MoveMouse()
        {
            GetCursorPos(out POINT p);
            SetCursorPos(p.X + 1, p.Y + 1);
        }
    }
}
