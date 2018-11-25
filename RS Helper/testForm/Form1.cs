using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS_Helper
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            notifyIcon1.Visible = true;
            MouseHook.Start();
            MouseHook.MouseAction += new EventHandler(Event);
            MouseHook.MouseMove += new EventHandler(MoveEvent);
            notifyIcon1.MouseClick += new MouseEventHandler(notifyIcon_BalloonTipClicked);
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }
        private void Event(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(progressBar1, 1);
            timer1.Enabled = false;
            AnimateProgBar(600);
        }
        private void MoveEvent(object sender, EventArgs e)
        {
            Point pt = new Point();
            GetCursorPos(ref pt);
            progressBar1.Location = new Point(pt.X - (progressBar1.Width / 2), pt.Y - (progressBar1.Height * 2));
            label1.Text = pt.X.ToString();
        }
        #region Progress bar
        public void AnimateProgBar(int milliSeconds)
        {
            if (!timer1.Enabled)
            {
                progressBar1.Value = 0;
                timer1.Interval = (int)Math.Ceiling((double)milliSeconds / 100);
                timer1.Enabled = true;
            }
        }
        #endregion
        #region Timers
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 1;
                progressBar1.Refresh();
            }
            else timer1.Enabled = false; ModifyProgressBarColor.SetState(progressBar1, 2);
        }
        #endregion

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                notifyIcon1.BalloonTipText = "Buying gf";
                notifyIcon1.ShowBalloonTip(10000);
            }
        }
        void notifyIcon_BalloonTipClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //form that opens where you can choose amount of ticks
                //choose which window you want to display the form on.
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipText = "Credits to null." + Environment.NewLine + "My Discord: null#9904";
            notifyIcon1.ShowBalloonTip(10000);
        }
    }
}
