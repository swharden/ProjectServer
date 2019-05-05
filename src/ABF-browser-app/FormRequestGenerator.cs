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
        public formRequestGenerator()
        {
            InitializeComponent();
            tbRequest.Clear();
            tbResponse.Clear();
            cbAction.Items.AddRange(AbfBrowser.ActionTools.GetActionNames());
            cbAction.SelectedItem = cbAction.Items[0];
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
            Console.WriteLine("building request...");

            AbfBrowser.Action action = (AbfBrowser.Action)cbAction.SelectedIndex;
            AbfBrowser.Request request = new AbfBrowser.Request(action, tbPath.Text, tbIdentifier.Text, tbValue.Text);
            tbRequest.Text = request.GetJson();
        }

    }
}
