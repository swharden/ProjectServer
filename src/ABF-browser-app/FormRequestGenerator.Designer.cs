namespace ABF_browser_app
{
    partial class formRequestGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAction = new System.Windows.Forms.ComboBox();
            this.gbPath = new System.Windows.Forms.GroupBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbRequest = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbIdentifier = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tbResponse = new System.Windows.Forms.TextBox();
            this.lblExecutionTime = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbDebugLog = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tbHtml = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.gbPath.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAction);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 41);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action";
            // 
            // cbAction
            // 
            this.cbAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAction.FormattingEnabled = true;
            this.cbAction.Location = new System.Drawing.Point(3, 16);
            this.cbAction.Name = "cbAction";
            this.cbAction.Size = new System.Drawing.Size(184, 21);
            this.cbAction.TabIndex = 0;
            this.cbAction.SelectedIndexChanged += new System.EventHandler(this.CbAction_SelectedIndexChanged);
            // 
            // gbPath
            // 
            this.gbPath.Controls.Add(this.tbPath);
            this.gbPath.Location = new System.Drawing.Point(12, 59);
            this.gbPath.Name = "gbPath";
            this.gbPath.Size = new System.Drawing.Size(190, 41);
            this.gbPath.TabIndex = 2;
            this.gbPath.TabStop = false;
            this.gbPath.Text = "Path";
            // 
            // tbPath
            // 
            this.tbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPath.Location = new System.Drawing.Point(3, 16);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(184, 20);
            this.tbPath.TabIndex = 0;
            this.tbPath.Text = "D:\\demoData\\abfs-2019";
            this.tbPath.TextChanged += new System.EventHandler(this.TbPath_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbRequest);
            this.groupBox3.Location = new System.Drawing.Point(208, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 232);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Request";
            // 
            // tbRequest
            // 
            this.tbRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRequest.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRequest.Location = new System.Drawing.Point(3, 16);
            this.tbRequest.Multiline = true;
            this.tbRequest.Name = "tbRequest";
            this.tbRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbRequest.Size = new System.Drawing.Size(382, 213);
            this.tbRequest.TabIndex = 0;
            this.tbRequest.Text = "JSON";
            this.tbRequest.WordWrap = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbIdentifier);
            this.groupBox4.Location = new System.Drawing.Point(12, 106);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(190, 41);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Identifier";
            // 
            // tbIdentifier
            // 
            this.tbIdentifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbIdentifier.Location = new System.Drawing.Point(3, 16);
            this.tbIdentifier.Name = "tbIdentifier";
            this.tbIdentifier.Size = new System.Drawing.Size(184, 20);
            this.tbIdentifier.TabIndex = 0;
            this.tbIdentifier.Text = "17703011.abf";
            this.tbIdentifier.TextChanged += new System.EventHandler(this.TbIdentifier_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbValue);
            this.groupBox2.Location = new System.Drawing.Point(12, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 41);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Value";
            // 
            // tbValue
            // 
            this.tbValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbValue.Location = new System.Drawing.Point(3, 16);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(184, 20);
            this.tbValue.TabIndex = 0;
            this.tbValue.Text = "1234";
            this.tbValue.TextChanged += new System.EventHandler(this.TbValue_TextChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.tbResponse);
            this.groupBox7.Location = new System.Drawing.Point(208, 250);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(388, 460);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Response";
            // 
            // tbResponse
            // 
            this.tbResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResponse.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbResponse.Location = new System.Drawing.Point(3, 16);
            this.tbResponse.Multiline = true;
            this.tbResponse.Name = "tbResponse";
            this.tbResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResponse.Size = new System.Drawing.Size(382, 441);
            this.tbResponse.TabIndex = 1;
            this.tbResponse.Text = "JSON";
            this.tbResponse.WordWrap = false;
            // 
            // lblExecutionTime
            // 
            this.lblExecutionTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExecutionTime.AutoSize = true;
            this.lblExecutionTime.Enabled = false;
            this.lblExecutionTime.Location = new System.Drawing.Point(93, 692);
            this.lblExecutionTime.Name = "lblExecutionTime";
            this.lblExecutionTime.Size = new System.Drawing.Size(56, 13);
            this.lblExecutionTime.TabIndex = 10;
            this.lblExecutionTime.Text = "123.45 ms";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.tbDebugLog);
            this.groupBox5.Location = new System.Drawing.Point(602, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(773, 232);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Debug Log";
            // 
            // tbDebugLog
            // 
            this.tbDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDebugLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDebugLog.Location = new System.Drawing.Point(3, 16);
            this.tbDebugLog.Multiline = true;
            this.tbDebugLog.Name = "tbDebugLog";
            this.tbDebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDebugLog.Size = new System.Drawing.Size(767, 213);
            this.tbDebugLog.TabIndex = 0;
            this.tbDebugLog.Text = "console";
            this.tbDebugLog.WordWrap = false;
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Location = new System.Drawing.Point(12, 687);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 12;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.tbHtml);
            this.groupBox6.Location = new System.Drawing.Point(602, 250);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(773, 460);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "HTML";
            // 
            // tbHtml
            // 
            this.tbHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHtml.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHtml.Location = new System.Drawing.Point(3, 16);
            this.tbHtml.Multiline = true;
            this.tbHtml.Name = "tbHtml";
            this.tbHtml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbHtml.Size = new System.Drawing.Size(767, 441);
            this.tbHtml.TabIndex = 0;
            this.tbHtml.Text = "HTML";
            this.tbHtml.WordWrap = false;
            // 
            // formRequestGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 722);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.lblExecutionTime);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbPath);
            this.Controls.Add(this.groupBox1);
            this.Name = "formRequestGenerator";
            this.Text = "ABF Browser - Request Generator";
            this.Load += new System.EventHandler(this.FormRequestGenerator_Load);
            this.groupBox1.ResumeLayout(false);
            this.gbPath.ResumeLayout(false);
            this.gbPath.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.GroupBox gbPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.TextBox tbIdentifier;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbRequest;
        private System.Windows.Forms.TextBox tbResponse;
        private System.Windows.Forms.Label lblExecutionTime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbDebugLog;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox tbHtml;
    }
}

