using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YunZhi.Client
{
    public partial class FrmLogin : Form
    {
        #region 属性

        public string TxtTel { get; set; }

        public string TxtPwd { get; set; }

        #endregion


        public FrmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitEvent(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginEvent(object sender, EventArgs e)
        {            
            string txtTel = this.txt_tel.Text.Trim();
            string txtPwd = this.txt_pwd.Text.Trim();
            if (string.IsNullOrEmpty(txtTel) == true || string.IsNullOrEmpty(txtPwd) == true)
            {
                MessageBox.Show("请输入用户名或密码！");
                return;
            }
            else
            {
                this.TxtPwd = txtPwd;
                this.TxtTel = txtPwd;
                this.DialogResult = DialogResult.OK;
            }
            this.Close();            
        }
    }
}
