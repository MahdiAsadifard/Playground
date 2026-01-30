namespace MouseMoverAppTray
{
    using MouseMoverAppTray.Utils;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Timers;
    public partial class Form1 : Form
    {

        private enum TrayItems
        {
            Start,
            Stop,
            Exit,
            Settings
        };

        List<ToolStripItem> items;

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
            items = new List<ToolStripItem>();
            var iconsPath = Path.Combine(Directory.GetCurrentDirectory(), "Icons");

            trayMenu = new ContextMenuStrip();
            items.Add(trayMenu.Items.Add(TrayItems.Start.ToString(), Image.FromFile(iconsPath + "\\" + "start.ico"), OnStart));
            items.Add(trayMenu.Items.Add(TrayItems.Stop.ToString(), Image.FromFile(iconsPath + "\\" + "stop.ico"), OnStop));
            items.Add(trayMenu.Items.Add(TrayItems.Settings.ToString(), Image.FromFile(iconsPath + "\\" + "settings.ico"), OnSettings));
            items.Add(trayMenu.Items.Add(TrayItems.Exit.ToString(), Image.FromFile(iconsPath + "\\" + "exit.ico"), OnExit));

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
            this.ResetTrayItemsColor();
            items[(int)TrayItems.Start].BackColor = Color.GreenYellow;

            timer = new Timer(TimeSpan.FromSeconds(trackBar_tick.Value));
            timer.Elapsed += (s, ev) => MoveMouse();
            timer.Start();
        }
        private void OnStop(object? sender, EventArgs e)
        {
            this.ResetTrayItemsColor();
            timer?.Stop();
        }

        private void OnSettings(object? sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void MoveMouse()
        {
            var space = Convert.ToInt32(label_pixel.Text);
            GetCursorPos(out POINT p);
            SetCursorPos(p.X + space, p.Y + space);
        }

        private void ResetTrayItemsColor()
        {
            foreach (ToolStripItem item in trayMenu.Items)
            {
                item.BackColor = SystemColors.Control;
            }
        }
        private void button_cancel_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void trackBar_tick_Scroll(object sender, EventArgs e)
        {
            label_second.Text = trackBar_tick.Value.ToString();
            button_save.Enabled = true;
        }

        private void trackBar_space_Scroll(object sender, EventArgs e)
        {
            label_pixel.Text = trackBar_space.Value.ToString();
            button_save.Enabled = true;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            OnStop(null, null);
            OnStart(null, null);
            button_save.Enabled = false;
        }
    }
}
