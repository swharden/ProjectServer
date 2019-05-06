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

            // build the request
            AbfBrowser.RequestAction action = (AbfBrowser.RequestAction)cbAction.SelectedIndex;
            AbfBrowser.MessageRequest request = new AbfBrowser.MessageRequest(action, tbPath.Text, tbIdentifier.Text, tbValue.Text);
            string requestJson = request.GetJson();

            // give the action to the interactor, execute it, and obtain the response
            AbfBrowser.Interactor interactor = new AbfBrowser.Interactor(requestJson);
            AbfBrowser.MessageResponse response = interactor.Execute();
            string responseJson = response.GetJson();

            // create a HTML page using the JSON as the only input
            string html = new AbfBrowser.DisplayMenu(responseJson).GetHTML();

            // update the GUI
            tbRequest.Text = requestJson;
            tbResponse.Text = responseJson;
            tbDebugLog.Text = debugListener.GetLogAsString();
            tbHtml.Text = html;
        }

    }
}
