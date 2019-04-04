using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Paway.Windows.Forms;
using YunZhi.Client.YZService;
using YunZhi.Util;

namespace YunZhi.Client
{
    public partial class FrmLoginNew : QQForm
    {

        #region 属性

        public string TxtTel { get; set; }

        public string TxtPwd { get; set; }
        
        public DataTable _UserInfo { get; set; }

        #endregion

        public FrmLoginNew()
        {
            InitializeComponent();
            this.backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            this.backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result is YunZhiResult)
            {
                YunZhiResult loginResult = e.Result as YunZhiResult;
                if (loginResult.Result == 1)
                {
                    this._UserInfo = loginResult.DataInfo;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(loginResult.Msg);
                }
            }
            this.label1.Visible = false;
            this.btnOK.Enabled = true;
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            YunZhiService service = new YunZhiService();
            try
            {                              
                YunZhiResult loginResult = service.UserLogin(this.TxtTel, this.TxtPwd);
                e.Result = loginResult;                
            }
            catch (Exception ex)
            {
                Logger.Log.Error("登录异常：" + ex.Message, ex);
            }
            finally
            {
                if (service != null)
                {
                    service.Abort();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }

        private void btnOK_Click(object sender, EventArgs e)
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
                this.TxtTel = txtTel;
                this.btnOK.Enabled = false;
                this.label1.Visible = true;
                this.backgroundWorker1.RunWorkerAsync();
            }           
        }
    }
}
