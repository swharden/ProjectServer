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
            lblVersion.Text = $"ABF Browser Version {AbfBrowser.Configuration.version}";
            SetLaunchFolderToSomewhereThatExists();
        }

        private void SetLaunchFolderToSomewhereThatExists()
        {
            string[] suggestedFolders =
            {
                @"X:\Data\SD\Piriform Oxytocin\00 pilot experiments\2018-01-25 sine pyr oxt",
                @"D:\demoData\abfs-2019",
                @"X:\data",
                @"C:",
            };
            foreach(string folder in suggestedFolders)
            {
                if (System.IO.Directory.Exists(folder))
                {
                    tbPath.Text = folder;
                    return;
                }
            }
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
                tbDebugLog.SelectionStart = tbDebugLog.GetFirstCharIndexOfCurrentLine();
                tbDebugLog.ScrollToCaret();
            }

            string serverLog = server.GetLog();
            if (serverLog.Length != tbServerLog.Text.Length)
            {
                tbServerLog.Text = serverLog;
                tbServerLog.SelectionStart = tbServerLog.Text.Length;
                tbServerLog.SelectionStart = tbServerLog.GetFirstCharIndexOfCurrentLine();
                tbServerLog.ScrollToCaret();
            }
        }
    }
}
