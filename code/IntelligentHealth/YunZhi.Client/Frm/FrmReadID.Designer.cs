namespace YunZhi.Client.Frm
{
    partial class FrmReadID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReadID));
            this.btnShutDown = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnShutDown
            // 
            this.btnShutDown.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnShutDown.Location = new System.Drawing.Point(317, 226);
            this.btnShutDown.Name = "btnShutDown";
            this.btnShutDown.Size = new System.Drawing.Size(82, 26);
            this.btnShutDown.TabIndex = 0;
            this.btnShutDown.Text = "关闭";
            this.btnShutDown.UseVisualStyleBackColor = false;
            this.btnShutDown.Click += new System.EventHandler(this.btnShutDown_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(17, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "找不到二代身份证读卡器接口";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmReadID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(162)))), ((int)(((byte)(232)))));
            this.BackgroundImage = global::YunZhi.Client.Properties.Resources.ID_Error;
            this.ClientSize = new System.Drawing.Size(404, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShutDown);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReadID";
            this.Padding = new System.Windows.Forms.Padding(3, 26, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.SysButton = Paway.Windows.Forms.ESysButton.Close;
            this.Text = "读取身份证";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShutDown;
        private System.Windows.Forms.Label label1;
    }
}