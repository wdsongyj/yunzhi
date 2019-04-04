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

namespace YunZhi.Client.Frm
{
    public partial class FrmSearchProject : Form
    {
        public FrmSearchProject()
        {
            InitializeComponent();
            this.SetKuaiJianResult();
        }

        private void SetKuaiJianResult()
        {
            try
            {
                this.treeView_Result.Nodes.Clear();
                TreeNode addNode;
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
    }
}
