using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunZhi.Util;
using YunZhi.Model;
using YunZhi.ControlLib;
using YunZhi.Client.YZService;

namespace YunZhi.Client.FrmCtr
{
    public partial class UCSearchProject : YZUserContrl
    {
        private BackgroundWorker _bgWork;
        private string _patientName = string.Empty;
        private string _curEID = string.Empty;
        private DateTime _StartDate;
        private DateTime _EndDate;

        public UCSearchProject()
        {
            InitializeComponent();
            this._bgWork = new BackgroundWorker();
            this._bgWork.DoWork += _MainWork_DoWork;
            this._bgWork.RunWorkerCompleted += _MainWork_RunWorkerCompleted;
        }

        void _MainWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result is object[])
            {
                try
                {
                    object[] result = e.Result as object[];
                    string method = result[0].ToString();
                    YunZhiResult res = result[1] as YunZhiResult;
                    if (res.Result == 0)
                    {
                        MessageBox.Show(res.Msg);
                        return;
                    }
                    switch (method)
                    {
                        case "getpartientlist":
                            this.SetPatientList(res.DataInfo);                            
                            break;
                        case "getpatientinfo":
                            this.SetJiBenAndProjectInfo(res.DataList);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                    MessageBox.Show("异常：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("参数错误");
            }
        }

        void _MainWork_DoWork(object sender, DoWorkEventArgs e)
        {
            YunZhiService service = null;
            try
            {
                service = new YunZhiService();
                if (e.Argument != null)
                {
                    string method = e.Argument.ToString();
                    switch (method.ToLower())
                    {
                        case "getpartientlist":
                            e.Result = new object[] { method.ToLower(), service.QueryProjectList(this._StartDate.ToString("yyyy-MM-dd 00:00:00"),this._EndDate.ToString("yyyy-MM-dd 23:59:59"),SourceHelper.EmployeeInfo.HID,this._patientName)};
                            break;
                        case "getpatientinfo":
                            e.Result = new object[] { method.ToLower(), service.GetProjectListAndPatientInfo(this._curEID,SourceHelper.EmployeeInfo.HID,this._StartDate.ToString("yyyy-MM-dd 00:00:00"),this._EndDate.ToString("yyyy-MM-dd 23:59:59"))};
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("获取诊疗记录异常：" + ex.Message, ex);
            }
            finally
            {
                if (service != null)
                {
                    service.Abort();
                }
            }
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this._bgWork.IsBusy == true)
            {
                MessageBox.Show("正在查询中，请稍等...");
                return;
            }
            DateTime startDate = this.dateTimePicker1.Value;
            DateTime endDate = this.dateTimePicker2.Value;
            if (startDate == null || endDate == null)
            {
                MessageBox.Show("开始日期或结束日期不能为空");
                return;
            }
            if (Convert.ToDateTime(startDate.ToString("yyyy-MM-dd 00:00:00")) > Convert.ToDateTime(endDate.ToString("yyyy-MM-dd 23:59:59")))
            {
                MessageBox.Show("开始日期不能大于结束日期");
                return;
            }
            this._patientName = this.txtPatientName.Text.Trim();
            this._StartDate = startDate;
            this._EndDate = endDate;
            this._bgWork.RunWorkerAsync("getpartientlist");
        }

        /// <summary>
        /// 设置看病记录
        /// </summary>
        public void SetPartientSource()
        {
            if (this._bgWork.IsBusy == false)
            {
                this._bgWork.RunWorkerAsync("getrecordlist");
            }
        }

        /// <summary>
        /// 树节点点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_Patient_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Name.ToLower() == "parentnode")
                {
                    this.ClearControl();
                    return;
                }
                if (this._bgWork.IsBusy == false)
                {
                    YZ_Health_Record curRecord = e.Node.Tag as YZ_Health_Record;
                    if (curRecord != null)
                    {
                        this._curEID = curRecord.PTID;
                        //首先获取到当前患者的id
                        this._bgWork.RunWorkerAsync("getpatientinfo");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("树节点点击事件异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// 清理数据
        /// </summary>
        private void ClearControl()
        {
            this.treeView_Result.Nodes.Clear();
        }


        /// <summary>
        /// 设置快检信息
        /// </summary>
        /// <param name="projectList"></param>
        private void SetKuaiJianResult(List<YZ_Project> projectList)
        {
            try
            {
                this.treeView_Result.Nodes.Clear();
                TreeNode addNode;
                if (projectList != null && projectList.Count > 0)
                {
                    foreach (YZ_Project model in projectList)
                    {
                        addNode = new TreeNode();
                        addNode.Text = string.Format("检查时间：{0}  检查项目：{1}  测试结果：{2}  结果描述：{3}  参考范围：{4}", model.ProCheckTime, model.ProName, model.ProCheckResult, string.IsNullOrEmpty(model.ProCheckRemark) == true ? "" : model.ProCheckRemark, model.Data03);
                        this.treeView_Result.Nodes.Add(addNode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("设置快检信息异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 设置患者信息列表
        /// </summary>
        /// <param name="table"></param>
        private void SetPatientList(DataTable table)
        {
            try
            {
                List<YZ_Health_Record> recordList = new ModelHandler<YZ_Health_Record>().TableToList(table);                
                this.treeView_Patient.Nodes.Clear();
                TreeNode tn;
                if (recordList.Count > 0)
                {
                    this._curEID = recordList[0].PTID;
                    foreach (YZ_Health_Record curMode in recordList)
                    {
                        tn = new TreeNode();
                        tn.Text = string.Format("{0}", curMode.PTName);
                        tn.Tag = curMode;
                        this.treeView_Patient.Nodes.Add(tn);
                    }
                    this._bgWork.RunWorkerAsync("getpatientinfo");
                }
                else
                {
                    this.treeView_Result.Nodes.Clear();
                    SetJiBenAndProjectInfo(null);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("设置就诊信息列表异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 设置基本信息
        /// </summary>
        /// <param name="source"></param>
        private void SetJiBenAndProjectInfo(DataTable[] sourceList)
        {
            if (sourceList == null || sourceList.Length == 0)
            {
                this.lblName.Text = "";
                this.label4.Text = "";
                this.label5.Text = "";
                this.label6.Text = "";
                return;
            }
            try
            {
                DataTable source = sourceList[0];
                List<YZ_Patients> patientList = new ModelHandler<YZ_Patients>().TableToList(source);
                if (patientList != null && patientList.Count > 0)
                {
                    YZ_Patients patinetModel = patientList[0];
                    this.lblName.Text = string.Format("姓名：{0}", patinetModel.PTName);
                    this.label4.Text = string.Format("性别：{0}", patinetModel.PTSex == "0" ? "男" : "女");                    
                    this.label5.Text = string.Format("联系方式：{0}", patinetModel.PTTelPhone);
                    this.label6.Text = string.Format("家庭住址：{0}", patinetModel.PTAddress);                    
                }
                DataTable projectSource = sourceList[1];
                List<YZ_Project> projectList = new ModelHandler<YZ_Project>().TableToList(projectSource);
                this.SetKuaiJianResult(projectList);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("SetJiBenAndProjectInfo异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
