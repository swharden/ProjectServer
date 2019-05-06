using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABF_browser_app
{
    public partial class FormServer : Form
    {
        public AbfBrowser.SimpleServerManager server;
        public AbfBrowser.LoggingTraceListener debugListener;

        public FormServer()
        {
            InitializeComponent();
            debugListener = new AbfBrowser.LoggingTraceListener();
            System.Diagnostics.Debug.Listeners.Add(debugListener);
            server = new AbfBrowser.SimpleServerManager();
            lblServingOn.Text = $"Serving on: {server.url}";
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            tbDebugLog.Clear();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            tbDebugLog.Text = debugListener.GetLogAsString();
            tbDebugLog.SelectionStart = tbDebugLog.Text.Length;
            tbDebugLog.ScrollToCaret();

            tbServerLog.Text = server.GetLog();
            tbServerLog.SelectionStart = tbServerLog.Text.Length;
            tbServerLog.ScrollToCaret();
        }

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(server.url);
        }
    }
}
