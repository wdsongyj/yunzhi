namespace YunZhi.Client
{
    partial class FrmLoginNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoginNew));
            this.btnOK = new Paway.Windows.Forms.QQButton();
            this.btnCancel = new Paway.Windows.Forms.QQButton();
            this.txt_tel = new Paway.Windows.Forms.QQTextBoxEx();
            this.txt_pwd = new Paway.Windows.Forms.QQTextBoxEx();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Image = null;
            this.btnOK.IsShowBorder = true;
            this.btnOK.Location = new System.Drawing.Point(343, 223);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Image = null;
            this.btnCancel.IsShowBorder = true;
            this.btnCancel.Location = new System.Drawing.Point(449, 223);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txt_tel
            // 
            this.txt_tel.BackColor = System.Drawing.Color.Transparent;
            this.txt_tel.Icon = null;
            this.txt_tel.IconIsButton = false;
            this.txt_tel.IsPasswordChat = '\0';
            this.txt_tel.IsSystemPasswordChar = false;
            this.txt_tel.Lines = new string[0];
            this.txt_tel.Location = new System.Drawing.Point(364, 152);
            this.txt_tel.MaxLength = 32767;
            this.txt_tel.MinimumSize = new System.Drawing.Size(20, 24);
            this.txt_tel.Multiline = false;
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.ReadOnly = false;
            this.txt_tel.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_tel.Size = new System.Drawing.Size(170, 24);
            this.txt_tel.TabIndex = 2;
            this.txt_tel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_tel.WaterColor = System.Drawing.Color.DarkGray;
            this.txt_tel.WaterText = "请输入用户名";
            this.txt_tel.WordWrap = true;
            // 
            // txt_pwd
            // 
            this.txt_pwd.BackColor = System.Drawing.Color.Transparent;
            this.txt_pwd.Icon = null;
            this.txt_pwd.IconIsButton = false;
            this.txt_pwd.IsPasswordChat = '*';
            this.txt_pwd.IsSystemPasswordChar = false;
            this.txt_pwd.Lines = new string[0];
            this.txt_pwd.Location = new System.Drawing.Point(364, 186);
            this.txt_pwd.MaxLength = 32767;
            this.txt_pwd.MinimumSize = new System.Drawing.Size(20, 24);
            this.txt_pwd.Multiline = false;
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.ReadOnly = false;
            this.txt_pwd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_pwd.Size = new System.Drawing.Size(170, 25);
            this.txt_pwd.TabIndex = 3;
            this.txt_pwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_pwd.WaterColor = System.Drawing.Color.DarkGray;
            this.txt_pwd.WaterText = "请输入密码";
            this.txt_pwd.WordWrap = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(251, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "正在登录，请稍等...";
            this.label1.Visible = false;
            // 
            // FrmLoginNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(622, 376);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_pwd);
            this.Controls.Add(this.txt_tel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsResize = false;
            this.IsShowMaxBox = true;
            this.IsShowMinBox = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLoginNew";
            this.Padding = new System.Windows.Forms.Padding(3, 26, 3, 3);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.SysButton = Paway.Windows.Forms.ESysButton.Close;
            this.ResumeLayout(false);

        }

        #endregion

        private Paway.Windows.Forms.QQButton btnOK;
        private Paway.Windows.Forms.QQButton btnCancel;
        private Paway.Windows.Forms.QQTextBoxEx txt_tel;
        private Paway.Windows.Forms.QQTextBoxEx txt_pwd;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;



    }
}