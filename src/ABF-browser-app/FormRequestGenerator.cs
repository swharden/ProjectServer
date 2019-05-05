using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABF_browser_app
{
    public partial class formRequestGenerator : Form
    {

        public AbfBrowser.LoggingTraceListener debugListener; 

        public formRequestGenerator()
        {
            InitializeComponent();

            debugListener = new AbfBrowser.LoggingTraceListener();
            System.Diagnostics.Debug.Listeners.Add(debugListener);

            tbRequest.Clear();
            tbResponse.Clear();
            tbDebugLog.Clear();
            cbAction.Items.AddRange(AbfBrowser.ActionTools.GetActionNames());
            cbAction.SelectedItem = cbAction.Items[0];
        }
        private void FormRequestGenerator_Load(object sender, EventArgs e)
        {
        }

        #region GUI bindings
        private void CbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildRequest();
        }

        private void TbPath_TextChanged(object sender, EventArgs e)
        {
            BuildRequest();
        }

        private void TbIdentifier_TextChanged(object sender, EventArgs e)
        {
            BuildRequest();
        }

        private void TbValue_TextChanged(object sender, EventArgs e)
        {
            BuildRequest();
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            BuildRequest();
        }

        #endregion

        public void BuildRequest()
        {
            // restart the benchmark timer
            debugListener.Clear();
            debugListener.RestartStopwatch();

            // build the action
            AbfBrowser.RequestAction action = (AbfBrowser.RequestAction)cbAction.SelectedIndex;
            AbfBrowser.Request request = new AbfBrowser.Request(action, tbPath.Text, tbIdentifier.Text, tbValue.Text);
            tbRequest.Text = request.GetJson();

            // give the action to the interactor, execute it, and collect the response
            AbfBrowser.Interactor interactor = new AbfBrowser.Interactor(request);
            AbfBrowser.Response response = interactor.Execute();
            
            // display the results
            tbResponse.Text = response.GetJson();
            tbDebugLog.Text = debugListener.GetLogAsString();
        }
    }
}
