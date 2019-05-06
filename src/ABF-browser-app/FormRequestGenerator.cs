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
            cbAction.SelectedItem = cbAction.Items[1];
        }

        private void FormRequestGenerator_Load(object sender, EventArgs e)
        {
            var server = new AbfBrowser.WebServer.ServerManager();
        }

        #region GUI bindings
        private void CbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildAndExecuteRequestUsingGuiValues();
        }

        private void TbPath_TextChanged(object sender, EventArgs e)
        {
            BuildAndExecuteRequestUsingGuiValues();
        }

        private void TbIdentifier_TextChanged(object sender, EventArgs e)
        {
            BuildAndExecuteRequestUsingGuiValues();
        }

        private void TbValue_TextChanged(object sender, EventArgs e)
        {
            BuildAndExecuteRequestUsingGuiValues();
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            BuildAndExecuteRequestUsingGuiValues();
        }

        #endregion

        public void BuildAndExecuteRequestUsingGuiValues()
        {
            // restart the benchmark timer
            debugListener.Clear();

            // build the request, execute it, and collect the HTML
            AbfBrowser.RequestAction action = (AbfBrowser.RequestAction)cbAction.SelectedIndex;
            AbfBrowser.MessageRequest request = new AbfBrowser.MessageRequest(action, tbPath.Text, tbIdentifier.Text, tbValue.Text);
            AbfBrowser.Interactor interactor = new AbfBrowser.Interactor(request);
            AbfBrowser.Display displayer = interactor.Execute();
            string html = displayer.GetHTML();

            // update the GUI
            tbRequest.Text = request.GetJson();
            tbResponse.Text = interactor.response.GetJson();
            tbDebugLog.Text = debugListener.GetLogAsString();
            tbHtml.Text = html;
        }

    }
}
