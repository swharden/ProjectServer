namespace ABF_browser_app
{
    partial class FormServer
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
            this.components = new System.ComponentModel.Container();
            this.tbDebugLog = new System.Windows.Forms.TextBox();
            this.timerUpdateLogs = new System.Windows.Forms.Timer(this.components);
            this.tbServerLog = new System.Windows.Forms.TextBox();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.lblServingOn = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDebugLog
            // 
            this.tbDebugLog.BackColor = System.Drawing.SystemColors.Control;
            this.tbDebugLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDebugLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDebugLog.Location = new System.Drawing.Point(3, 16);
            this.tbDebugLog.Multiline = true;
            this.tbDebugLog.Name = "tbDebugLog";
            this.tbDebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDebugLog.Size = new System.Drawing.Size(1082, 300);
            this.tbDebugLog.TabIndex = 0;
            this.tbDebugLog.Text = "debug log";
            this.tbDebugLog.WordWrap = false;
            // 
            // timerUpdateLogs
            // 
            this.timerUpdateLogs.Enabled = true;
            this.timerUpdateLogs.Tick += new System.EventHandler(this.TimerUpdateLogs_Tick);
            // 
            // tbServerLog
            // 
            this.tbServerLog.BackColor = System.Drawing.SystemColors.Control;
            this.tbServerLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbServerLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbServerLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerLog.Location = new System.Drawing.Point(3, 16);
            this.tbServerLog.Multiline = true;
            this.tbServerLog.Name = "tbServerLog";
            this.tbServerLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerLog.Size = new System.Drawing.Size(1082, 176);
            this.tbServerLog.TabIndex = 1;
            this.tbServerLog.Text = "server log";
            this.tbServerLog.WordWrap = false;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunch.Location = new System.Drawing.Point(825, 18);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(75, 22);
            this.btnLaunch.TabIndex = 2;
            this.btnLaunch.Text = "launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.BtnLaunch_Click);
            // 
            // lblServingOn
            // 
            this.lblServingOn.Location = new System.Drawing.Point(12, 32);
            this.lblServingOn.Name = "lblServingOn";
            this.lblServingOn.Size = new System.Drawing.Size(176, 23);
            this.lblServingOn.TabIndex = 3;
            this.lblServingOn.Text = "Serving on: ?";
            this.lblServingOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.Location = new System.Drawing.Point(6, 19);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(813, 20);
            this.tbPath.TabIndex = 4;
            this.tbPath.Text = "C:/somewhere/cool/";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tbPath);
            this.groupBox2.Controls.Add(this.btnLaunch);
            this.groupBox2.Location = new System.Drawing.Point(194, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(906, 47);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Open Browser";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbDebugLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1088, 319);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Debug Log (last request execution only)";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.tbServerLog);
            this.groupBox4.Location = new System.Drawing.Point(12, 390);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1088, 195);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Server Log";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(12, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(176, 23);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "ABF Browser version 1.2345";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 597);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblServingOn);
            this.Name = "FormServer";
            this.Text = "ABF Browser (Developer Version)";
            this.Load += new System.EventHandler(this.FormServer_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbDebugLog;
        private System.Windows.Forms.Timer timerUpdateLogs;
        private System.Windows.Forms.TextBox tbServerLog;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Label lblServingOn;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblVersion;
    }
}