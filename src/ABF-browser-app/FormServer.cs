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
        public AbfBrowser.WebServerManager server;
        public AbfBrowser.LoggingTraceListener debugListener;

        public FormServer()
        {
            InitializeComponent();
            debugListener = new AbfBrowser.LoggingTraceListener();
            System.Diagnostics.Debug.Listeners.Add(debugListener);
            server = new AbfBrowser.WebServerManager();
            lblServingOn.Text = $"Serving on: {server.url}";
            tbDebugLog.Clear();
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            //BtnLaunch_Click(null, null);
        }

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"{server.url}?display=frames&path={tbPath.Text}");
        }

        private void TimerUpdateLogs_Tick(object sender, EventArgs e)
        {
            string debugLog = debugListener.GetLogAsString();
            if (debugLog.Length != tbDebugLog.Text.Length)
            {
                tbDebugLog.Text = debugLog;
                tbDebugLog.SelectionStart = tbDebugLog.Text.Length;
                tbDebugLog.ScrollToCaret();
            }

            string serverLog = server.GetLog();
            if (serverLog.Length != tbServerLog.Text.Length)
            {
                tbServerLog.Text = serverLog;
                tbServerLog.SelectionStart = tbServerLog.Text.Length;
                tbServerLog.ScrollToCaret();
            }
        }
    }
}
