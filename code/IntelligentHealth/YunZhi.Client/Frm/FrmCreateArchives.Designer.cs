namespace YunZhi.Client.Frm
{
    partial class FrmCreateArchives
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateArchives));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_GetUser = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_AddressNew = new System.Windows.Forms.TextBox();
            this.txt_Address = new System.Windows.Forms.TextBox();
            this.txt_Tel = new System.Windows.Forms.TextBox();
            this.dtp_Birthday = new System.Windows.Forms.DateTimePicker();
            this.txt_IDCard = new System.Windows.Forms.TextBox();
            this.cbBox_Sex = new System.Windows.Forms.ComboBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.groupBox1.Controls.Add(this.btn_GetUser);
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 39);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // btn_GetUser
            // 
            this.btn_GetUser.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_GetUser.Location = new System.Drawing.Point(326, 12);
            this.btn_GetUser.Name = "btn_GetUser";
            this.btn_GetUser.Size = new System.Drawing.Size(75, 23);
            this.btn_GetUser.TabIndex = 0;
            this.btn_GetUser.Text = "读身份证";
            this.btn_GetUser.UseVisualStyleBackColor = true;
            this.btn_GetUser.Click += new System.EventHandler(this.btn_GetUser_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Save.Location = new System.Drawing.Point(427, 12);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.groupBox2.Controls.Add(this.txt_AddressNew);
            this.groupBox2.Controls.Add(this.txt_Address);
            this.groupBox2.Controls.Add(this.txt_Tel);
            this.groupBox2.Controls.Add(this.dtp_Birthday);
            this.groupBox2.Controls.Add(this.txt_IDCard);
            this.groupBox2.Controls.Add(this.cbBox_Sex);
            this.groupBox2.Controls.Add(this.txtUserName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(3, 78);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(532, 279);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "基本信息";
            // 
            // txt_AddressNew
            // 
            this.txt_AddressNew.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_AddressNew.Location = new System.Drawing.Point(105, 191);
            this.txt_AddressNew.Name = "txt_AddressNew";
            this.txt_AddressNew.Size = new System.Drawing.Size(376, 23);
            this.txt_AddressNew.TabIndex = 30;
            // 
            // txt_Address
            // 
            this.txt_Address.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Address.Location = new System.Drawing.Point(105, 151);
            this.txt_Address.Name = "txt_Address";
            this.txt_Address.Size = new System.Drawing.Size(376, 23);
            this.txt_Address.TabIndex = 29;
            // 
            // txt_Tel
            // 
            this.txt_Tel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Tel.Location = new System.Drawing.Point(105, 111);
            this.txt_Tel.Name = "txt_Tel";
            this.txt_Tel.Size = new System.Drawing.Size(145, 23);
            this.txt_Tel.TabIndex = 28;
            // 
            // dtp_Birthday
            // 
            this.dtp_Birthday.CustomFormat = "yyyy-MM-dd";
            this.dtp_Birthday.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtp_Birthday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Birthday.Location = new System.Drawing.Point(105, 66);
            this.dtp_Birthday.Name = "dtp_Birthday";
            this.dtp_Birthday.Size = new System.Drawing.Size(145, 23);
            this.dtp_Birthday.TabIndex = 27;
            this.dtp_Birthday.Value = new System.DateTime(1989, 4, 9, 1, 9, 0, 0);
            // 
            // txt_IDCard
            // 
            this.txt_IDCard.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_IDCard.Location = new System.Drawing.Point(336, 67);
            this.txt_IDCard.Name = "txt_IDCard";
            this.txt_IDCard.Size = new System.Drawing.Size(145, 23);
            this.txt_IDCard.TabIndex = 26;
            // 
            // cbBox_Sex
            // 
            this.cbBox_Sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBox_Sex.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBox_Sex.FormattingEnabled = true;
            this.cbBox_Sex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBox_Sex.Location = new System.Drawing.Point(335, 25);
            this.cbBox_Sex.Name = "cbBox_Sex";
            this.cbBox_Sex.Size = new System.Drawing.Size(145, 21);
            this.cbBox_Sex.TabIndex = 25;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.Location = new System.Drawing.Point(105, 26);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(145, 23);
            this.txtUserName.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(47, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 23;
            this.label7.Text = "现住址：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(35, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 22;
            this.label6.Text = "家庭住址：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(35, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 21;
            this.label5.Text = "联系电话：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(267, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "身份证号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(35, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "出生日期：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(291, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "性别：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(59, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "姓名：";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 65);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(532, 13);
            this.panel3.TabIndex = 21;
            // 
            // FrmCreateArchives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(538, 360);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShowMaxBox = true;
            this.IsShowMinBox = true;
            this.Name = "FrmCreateArchives";
            this.Padding = new System.Windows.Forms.Padding(3, 26, 3, 3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.SysButton = Paway.Windows.Forms.ESysButton.Close;
            this.Text = "新增健康档案";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_GetUser;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_AddressNew;
        private System.Windows.Forms.TextBox txt_Address;
        private System.Windows.Forms.TextBox txt_Tel;
        private System.Windows.Forms.DateTimePicker dtp_Birthday;
        private System.Windows.Forms.TextBox txt_IDCard;
        private System.Windows.Forms.ComboBox cbBox_Sex;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
    }
}