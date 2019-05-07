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
            this.SuspendLayout();
            // 
            // tbDebugLog
            // 
            this.tbDebugLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDebugLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDebugLog.Location = new System.Drawing.Point(12, 38);
            this.tbDebugLog.Multiline = true;
            this.tbDebugLog.Name = "tbDebugLog";
            this.tbDebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDebugLog.Size = new System.Drawing.Size(895, 289);
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
            this.tbServerLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerLog.Location = new System.Drawing.Point(12, 333);
            this.tbServerLog.Multiline = true;
            this.tbServerLog.Name = "tbServerLog";
            this.tbServerLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerLog.Size = new System.Drawing.Size(895, 335);
            this.tbServerLog.TabIndex = 1;
            this.tbServerLog.Text = "server log";
            this.tbServerLog.WordWrap = false;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(194, 9);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(75, 23);
            this.btnLaunch.TabIndex = 2;
            this.btnLaunch.Text = "launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.BtnLaunch_Click);
            // 
            // lblServingOn
            // 
            this.lblServingOn.Location = new System.Drawing.Point(12, 9);
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
            this.tbPath.Location = new System.Drawing.Point(275, 11);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(632, 20);
            this.tbPath.TabIndex = 4;
            this.tbPath.Text = "C:/somewhere/cool/";
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 680);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.lblServingOn);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.tbServerLog);
            this.Controls.Add(this.tbDebugLog);
            this.Name = "FormServer";
            this.Text = "ABF Browser Server";
            this.Load += new System.EventHandler(this.FormServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDebugLog;
        private System.Windows.Forms.Timer timerUpdateLogs;
        private System.Windows.Forms.TextBox tbServerLog;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Label lblServingOn;
        private System.Windows.Forms.TextBox tbPath;
    }
}