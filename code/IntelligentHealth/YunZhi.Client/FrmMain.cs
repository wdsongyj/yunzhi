using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunZhi.Model;
using YunZhi.Util;

namespace YunZhi.Client
{
    public partial class FrmMain : Form
    {
        BackgroundWorker _curBgWork;        

        public FrmMain()
        {
            InitializeComponent();
            this._curBgWork = new BackgroundWorker();
            this._curBgWork.DoWork += _curBgWork_DoWork;
            this._curBgWork.RunWorkerCompleted += _curBgWork_RunWorkerCompleted;
            this.timer1.Start();
            this.InitializeControl(); 
        }

        private void _curBgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblCurTime.Text = string.Format("当前时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            this.timer1.Start();
        }

        private void _curBgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        public FrmMain(string tel, string pwd)
            : this()
        {
            this.lblCurUser.Text = string.Format("[云智医疗测试医院-{0}]", tel);            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this._curBgWork.RunWorkerAsync();
        }

        private void tsMenuItem_Add_Click(object sender, EventArgs e)
        {
            Frm.FrmCreateArchives createArchives = new Frm.FrmCreateArchives();
            DialogResult result = createArchives.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetPartientSource();
            }
        }

        private void tsMenuItem_Search_Click(object sender, EventArgs e)
        {
            Frm.FrmSearchArchives search = new Frm.FrmSearchArchives();
            search.ShowDialog();
        }

        private void InitializeControl()
        {
            this.ClearControl();
            TreeNode parentNode = new TreeNode();
            parentNode.Name = "parentNode";
            parentNode.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.treeView_Patient.Nodes.Add(parentNode);                        
        }

        private void ClearControl()
        {
            this.label2.Text = "";
            this.label3.Text = "";
            this.label4.Text = "";
            this.label5.Text = "";
            this.label6.Text = "";
            this.label7.Text = "";
            this.treeView_Result.Nodes.Clear();
        }

        private void SetPartientSource()
        {
            //TreeNode parentNode = this.treeView_Patient.Nodes[0];
            //parentNode.Nodes.Clear();            
            //TreeNode tn;
            //foreach (ArchivesModel curMode in SourceHelper._ArchivesSource)
            //{
            //    tn = new TreeNode();
            //    tn.Text = curMode.UserName;
            //    tn.Tag = curMode;
            //    parentNode.Nodes.Add(tn);
            //}
            //parentNode.Expand();
        }

        private void treeView_Patient_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name.ToLower() == "parentnode")
            {
                this.ClearControl();
                return;
            }
            ArchivesModel model = e.Node.Tag as ArchivesModel;
            this.label2.Text = string.Format("姓名：{0}", model.UserName);
            this.label3.Text = string.Format("性别：{0}", model.Sex);
            this.label4.Text = string.Format("出生日期：{0}", model.Birthday.ToShortDateString());
            this.label5.Text = string.Format("联系方式：{0}", model.Tel);
            this.label6.Text = string.Format("家庭住址：{0}", model.Address);
            this.label7.Text = string.Format("现住址：{0}", model.AddressNew);
            this.SetKuaiJianResult();
        }

        private void SetKuaiJianResult()
        {
            try
            {
                //this.treeView_Result.Nodes.Clear();
                //TreeNode addNode;
                //foreach (ProjectModel model in SourceHelper._ProjectSource)
                //{
                //    addNode = new TreeNode();
                //    addNode.Text = string.Format("检查项目：{0}   测试结果：{1}   结果描述：{2}   检查时间：{3}", model.ProjectName, model.DeviceResult, model.DeviceDesc, model.ProjectTime);
                //    this.treeView_Result.Nodes.Add(addNode);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsb_Project_Click(object sender, EventArgs e)
        {
            Frm.FrmSearchProject search = new Frm.FrmSearchProject();
            search.ShowDialog();
        }

        private void tsb_zhenliao_Click(object sender, EventArgs e)
        {
            string url = "http://daps.doctorai.com.cn";
            Frm.FrmWebBrower webBrower = new Frm.FrmWebBrower(url);
            webBrower.Text = "智能诊疗助手";
            webBrower.Show();
        }

        private void tsb_xuanjiao_Click(object sender, EventArgs e)
        {
            string url = "http://www.91huayi.com";
            Frm.FrmWebBrower webBrower = new Frm.FrmWebBrower(url);
            webBrower.Text = "智能诊疗助手";
            webBrower.Show();
        }
    }
}
