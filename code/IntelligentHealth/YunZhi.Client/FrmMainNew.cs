using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Paway.Windows.Forms;
using Paway.Windows.Forms.Metro;
using Paway.Windows.Helper;
using YunZhi.Client.FrmCtr;
using YunZhi.Model;
using YunZhi.Util;

namespace YunZhi.Client
{
    public partial class FrmMainNew : _360Form
    {

        private YZ_Employee _CurEmployeeInfo;

        BackgroundWorker _curBgWork; 
        public FrmMainNew()
        {
            InitializeComponent();
            this._curBgWork = new BackgroundWorker();
            this._curBgWork.DoWork += _curBgWork_DoWork;
            this._curBgWork.RunWorkerCompleted += _curBgWork_RunWorkerCompleted;
            this.timer1.Start();
            this.IsShowMaxBox = true;
            this.IsShowMinBox = true;            
            this.WindowState = FormWindowState.Maximized;
        }

        public FrmMainNew(YZ_Employee employee)
            : this()
        {
            this._CurEmployeeInfo = employee;
            SourceHelper.EmployeeInfo = this._CurEmployeeInfo;
            this.lblCurUser.Text = string.Format("[{0}-{1}]", this._CurEmployeeInfo.HospitalName,this._CurEmployeeInfo.EmpName);            
        }


        private void _curBgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblCurTime.Text = string.Format("当前时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            this.timer1.Start();
        }

        private void _curBgWork_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void toolBar1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolBar1.SelectedIndex == 3 || toolBar1.SelectedIndex == 4)
            {
                string url = "http://daps.doctorai.com.cn";
                string title = "智能诊疗助手";
                if (toolBar1.SelectedIndex == 3)
                {
                    url = "http://www.91huayi.com";
                    title = "宣教";
                }
                Frm.FrmWebBrower webBrower = new Frm.FrmWebBrower(url);
                webBrower.Text = title;  
                webBrower.Show();
                return;
            }


            foreach (System.Windows.Forms.Control contr in this.panel1.Controls)
            {
                contr.Hide();
            }
            if (toolBar1.SelectedIndex == 0)
            {
                if (this.panel1.Controls["UCArchives"] == null)
                {
                    UCArchives ctl = new UCArchives(this._CurEmployeeInfo.HospitalName,this._CurEmployeeInfo.EmpName,
                        this._CurEmployeeInfo.HID,this._CurEmployeeInfo.EmpID);
                    ctl.Dock = DockStyle.Fill;
                    this.panel1.Controls.Add(ctl);
                }
                else
                {
                    UCArchives curArchives = this.panel1.Controls["UCArchives"] as UCArchives;
                    curArchives.SetPartientSource();
                    curArchives.Show();
                }
            }
            else if (toolBar1.SelectedIndex == 1)
            {
                if (this.panel1.Controls["UCSearchArchives"] == null)
                {
                    UCSearchArchives ctl = new UCSearchArchives();
                    ctl.Dock = DockStyle.Fill;
                    this.panel1.Controls.Add(ctl);
                }
                else
                    this.panel1.Controls["UCSearchArchives"].Show();
            }
            else if (toolBar1.SelectedIndex == 2)
            {
                if (this.panel1.Controls["UCSearchProject"] == null)
                {
                    UCSearchProject ctl = new UCSearchProject();
                    ctl.Dock = DockStyle.Fill;
                    this.panel1.Controls.Add(ctl);
                }
                else
                    this.panel1.Controls["UCSearchProject"].Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this._curBgWork.RunWorkerAsync();
        }

        private void FrmMainNew_Load(object sender, EventArgs e)
        {
            toolBar1.Items.RemoveAt(3);
        }

        private void tsMenu_AddArchives_Click(object sender, EventArgs e)
        {
            Frm.FrmCreateArchives create = new Frm.FrmCreateArchives();
            create.Owner = this;
            create.ShowDialog();
        }

        private void FrmMainNew_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.SysMenuState == EMouseState.Down)
            {
                this.contextMenuStrip1.Show(this, new Point(e.Location.X - 15, e.Location.Y + 8));
            }
        }
    }
}
