using FastReport;
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

    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class UCArchives : YZUserContrl
    {
        private BackgroundWorker _bgWork;

        private string _patientID = string.Empty;
        private string _hrid = string.Empty;
        DataTable yaoPinDataTable = new DataTable();
        private string curHosName = string.Empty;
        private string curEmpName = string.Empty;
        private string curHosID = string.Empty;
        private string curEmpID = string.Empty;

        public UCArchives(string hosname = "", string empName = "", string hosID = "", string empID = "")
        {
            InitializeComponent();
            curHosName = hosname;
            curEmpName = empName;
            curHosID = hosID;
            curEmpID = empID;
            this.dataGridView1.AutoGenerateColumns = false;
            this._bgWork = new BackgroundWorker();
            this._bgWork.DoWork += _bgWork_DoWork;
            this._bgWork.RunWorkerCompleted += _bgWork_RunWorkerCompleted;
            this.InitializeControl();
            this.SetPartientSource();
        }

        /// <summary>
        /// 异步开始调用服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _bgWork_DoWork(object sender, DoWorkEventArgs e)
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
                        case "getrecordlist":
                            e.Result = new object[] { method.ToLower(), service.GetHealthy_RecordListByDocId(SourceHelper.EmployeeInfo.EmpID) };
                            break;
                        case "getpatientinfo":
                            if (string.IsNullOrEmpty(this._patientID))
                            {
                                e.Result = null;
                            }
                            else
                            {
                                e.Result = new object[] { method.ToLower(), service.GetZhenLiaoListByPTID(this._patientID, SourceHelper.EmployeeInfo.HID, this._hrid) };
                            }
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

        /// <summary>
        /// 异步完成之后执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                        case "getrecordlist":
                            this.SetRecordList(res.DataInfo);
                            if (res.DataInfo != null && res.DataInfo.Rows.Count > 0)
                            {
                                this._bgWork.RunWorkerAsync("getpatientinfo");
                            }
                            else
                            {
                                ClearControl();
                            }
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

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeControl()
        {
            try
            {
                this.ClearControl();
                TreeNode parentNode = new TreeNode();
                parentNode.Name = "parentNode";
                parentNode.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.treeView_Patient.Nodes.Add(parentNode);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("初始化控件失败：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        private void ClearControl()
        {
            this.label2.Text = "";
            this.label3.Text = "";
            this.label4.Text = "";
            this.label5.Text = "";
            this.label6.Text = "";
            this.label7.Text = "";
            this.txtbs.Text = "";
            this.txtbz.Text = "";
            this.txtjg.Text = "";
            this.txtzs.Text = "";
            this.yaoPinDataTable = new DataTable();
            this.dataGridView1.DataSource = this.yaoPinDataTable;
            //调用清空
            if (this.treeView_Patient.Nodes != null && this.treeView_Patient.Nodes.Count > 0)
            {
                RunMethod(this.webBrowser1, "clearHtml", new string[] { "" });
            }
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
                    this._patientID = string.Empty;
                    this._hrid = string.Empty;
                    return;
                }
                if (this._bgWork.IsBusy == false)
                {
                    YZ_Health_Record curRecord = e.Node.Tag as YZ_Health_Record;
                    if (curRecord != null)
                    {
                        this._patientID = curRecord.PTID;
                        this._hrid = curRecord.HRID;
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

        private void SetWenZhenInfo(YZ_Health_Record model)
        {
            if (string.IsNullOrEmpty(model.Data03) == true)
            {
                this.txtbs.Text = "";
            }
            else
            {
                this.txtbs.Text = model.Data03.Replace("\n", "\r\n");
            }
            if (string.IsNullOrEmpty(model.Data04) == true)
            {
                this.txtzs.Text = "";
            }
            else
            {
                this.txtzs.Text = model.Data04.Replace("\n", "\r\n");
            }
            if (string.IsNullOrEmpty(model.Data05) == true)
            {
                this.txtjg.Text = "";
            }
            else
            {
                this.txtjg.Text = model.Data05.Replace("\n", "\r\n");
            }
            if (string.IsNullOrEmpty(model.Data06) == true)
            {
                this.txtbz.Text = "";
            }
            else
            {
                this.txtbz.Text = model.Data06.Replace("\n", "\r\n");
            }
        }

        /// <summary>
        /// 设置快检信息
        /// </summary>
        /// <param name="projectList"></param>
        private void SetKuaiJianResult(List<YZ_Project> projectList)
        {
            try
            {
                //this.treeView_Result.Nodes.Clear();
                //TreeNode addNode;
                //if (projectList != null && projectList.Count > 0)
                //{
                //    foreach (YZ_Project model in projectList)
                //    {
                //        addNode = new TreeNode();
                //        addNode.Text = string.Format("检查时间：{0}     检查项目：{1}   测试结果：{2}   结果描述：{3}", model.ProCheckTime, model.ProName, model.ProCheckResult, model.ProCheckRemark);
                //        this.treeView_Result.Nodes.Add(addNode);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.Log.Error("设置快检信息异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 设置就诊信息列表
        /// </summary>
        /// <param name="table"></param>
        private void SetRecordList(DataTable table)
        {
            try
            {
                List<YZ_Health_Record> recordList = new ModelHandler<YZ_Health_Record>().TableToList(table);
                TreeNode parentNode = this.treeView_Patient.Nodes[0];
                parentNode.Nodes.Clear();
                TreeNode tn;
                if (recordList.Count > 0)
                {
                    this._patientID = recordList[0].PTID;
                    this._hrid = recordList[0].HRID;
                    int curIndex = 0;
                    foreach (YZ_Health_Record curMode in recordList)
                    {
                        curIndex++;
                        tn = new TreeNode();
                        DateTime curTime = (DateTime)curMode.HRCreated;
                        tn.Text = string.Format("{0}({1})", curMode.PTName, curTime.ToString("HH:mm:ss"));
                        tn.Tag = curMode;
                        parentNode.Nodes.Add(tn);
                    }
                }
                parentNode.Expand();
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
                return;
            }
            try
            {
                DataTable source = sourceList[0];
                List<YZ_Patients> patientList = new ModelHandler<YZ_Patients>().TableToList(source);
                if (patientList != null && patientList.Count > 0)
                {
                    YZ_Patients patinetModel = patientList[0];
                    this.label2.Text = string.Format("姓名：{0}", patinetModel.PTName);
                    this.label3.Text = string.Format("性别：{0}", patinetModel.PTSex == "0" ? "男" : "女");
                    this.label4.Text = string.Format("出生日期：{0}", string.IsNullOrEmpty(patinetModel.PTBrithday) == true ? "" : Convert.ToDateTime(patinetModel.PTBrithday).ToString("yyyy-MM-dd"));
                    this.label5.Text = string.Format("联系方式：{0}", patinetModel.PTTelPhone);
                    this.label6.Text = string.Format("家庭住址：{0}", patinetModel.PTAddress);
                    this.label7.Text = string.Format("现住址：{0}", patinetModel.PTRemark);
                    //调用清空
                    if (this.treeView_Patient.Nodes != null && this.treeView_Patient.Nodes.Count > 0)
                    {
                        RunMethod(this.webBrowser1, "clearHtml", new string[] { "" });
                    }
                }
                DataTable projectSource = sourceList[1];
                List<YZ_Project> projectList = new ModelHandler<YZ_Project>().TableToList(projectSource);
                this.SetKuaiJianResult(projectList);

                List<YZ_Health_Record> recordList = new ModelHandler<YZ_Health_Record>().TableToList(sourceList[2]);
                if (recordList != null && recordList.Count > 0)
                {
                    this.SetWenZhenInfo(recordList[0]);
                }
                this.yaoPinDataTable = sourceList[3];
                this.dataGridView1.DataSource = this.yaoPinDataTable;
                _DelGuid = new List<string>();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("SetJiBenAndProjectInfo异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Frm.FrmCreateArchives create = new Frm.FrmCreateArchives();
            create.Owner = this.ParentForm;
            DialogResult resu = create.ShowDialog();
            if (resu == DialogResult.OK)
            {
                this._bgWork.RunWorkerAsync("getrecordlist");
            }
        }

        private void treeView_Patient_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode selectNode = this.treeView_Patient.GetNodeAt(e.X, e.Y);
                if (selectNode != null)
                {
                    if (selectNode.Level == 1)
                    {
                        selectNode.ContextMenuStrip = this.contextMenuStrip1;
                    }
                }
            }
        }

        private void toolMenuItemDel_Click(object sender, EventArgs e)
        {
            try
            {
                int count = this.treeView_Patient.Nodes.Count;
                if (count == 0)
                {
                    MessageBox.Show("请选择要删除的档案！");
                    return;
                }

                if (MessageBox.Show("是否要删除", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    TreeNode node = this.treeView_Patient.SelectedNode;
                    if (node == null)
                    {
                        MessageBox.Show("请选择要删除的档案！");
                        return;
                    }
                    YZ_Health_Record curRecord = node.Tag as YZ_Health_Record;
                    if (curRecord != null)
                    {
                        YunZhiResult res = new YunZhiService().DelHealthRecode(curRecord.HRID);
                        MessageBox.Show(res.Msg);
                        if (res.Result == 1)
                        {
                            this._bgWork.RunWorkerAsync("getrecordlist");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择要删除的档案！");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("toolMenuItemDel_Click异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolMenuItemAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int count = this.treeView_Patient.Nodes.Count;
                if (count == 0)
                {
                    MessageBox.Show("请选择档案！");
                    return;
                }

                TreeNode node = this.treeView_Patient.SelectedNode;
                if (node == null)
                {
                    MessageBox.Show("请选择档案！");
                    return;
                }
                YZ_Health_Record curRecord = node.Tag as YZ_Health_Record;
                if (curRecord != null)
                {
                    //弹出修改界面
                    Frm.FrmUpdateRecord record = new Frm.FrmUpdateRecord(curRecord);
                    DialogResult res = record.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        this._bgWork.RunWorkerAsync("getrecordlist");
                    }
                }
                else
                {
                    MessageBox.Show("请选择档案！");
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("toolMenuItemDel_Click异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable ddd = this.yaoPinDataTable.GetChanges();
                string data03 = this.txtbs.Text.Trim();
                string data04 = this.txtzs.Text.Trim();
                string data05 = this.txtjg.Text.Trim();
                string data06 = this.txtbz.Text.Trim();
                if (string.IsNullOrEmpty(data04) == true)
                {
                    MessageBox.Show("请输入主述信息！");
                    return;
                }
                if (string.IsNullOrEmpty(this._hrid) == true)
                {
                    MessageBox.Show("请选择患者！");
                    return;
                }
                YunZhiResult res = new YunZhiService().UpdateRecorInfo(this._hrid, data03, data04, data05, data06, ddd, this._DelGuid.ToArray());
                this._bgWork.RunWorkerAsync("getpatientinfo");
                MessageBox.Show(res.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string GetPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._hrid) == true)
                {
                    MessageBox.Show("请选择患者！");
                    return;
                }
                YunZhiService service = new YunZhiService();
                try
                {
                    if (System.IO.File.Exists(GetPath() + "chufang.frx") == false)
                    {
                        MessageBox.Show("未找到处方打印模板！");
                        return;
                    }
                    YunZhiResult res = service.GetZhenLiaoListByPTID(this._patientID, SourceHelper.EmployeeInfo.HID, this._hrid);
                    if (res.Result == 0)
                    {
                        MessageBox.Show(res.Msg);
                        return;
                    }
                    DataTable[] sourceList = res.DataList;
                    //获取当前信息，然后打印
                    if (sourceList == null || sourceList.Length == 0)
                    {
                        return;
                    }
                    List<PrintModel> printList = new List<PrintModel>();
                    PrintModel model = new PrintModel();
                    DataTable source = sourceList[0];
                    List<YZ_Patients> patientList = new ModelHandler<YZ_Patients>().TableToList(source);
                    if (patientList != null && patientList.Count > 0)
                    {
                        YZ_Patients patinetModel = patientList[0];
                        model.PTName = patinetModel.PTName;
                        if (string.IsNullOrEmpty(patinetModel.PTBrithday) == false)
                        {
                            DateTime curTime = Convert.ToDateTime(patinetModel.PTBrithday);
                            DateTime now = DateTime.Today;
                            int age = now.Year - curTime.Year;
                            if (curTime > now.AddYears(-age))
                            {
                                age--;
                            }
                            model.PTNianLing = age.ToString();
                        }
                        model.PTSex = patinetModel.PTSex == "0" ? "男" : "女";
                    }
                    DataTable projectSource = sourceList[1];
                    List<YZ_Project> projectList = new ModelHandler<YZ_Project>().TableToList(projectSource);
                    this.SetKuaiJianResult(projectList);

                    List<YZ_Health_Record> recordList = new ModelHandler<YZ_Health_Record>().TableToList(sourceList[2]);
                    if (recordList != null && recordList.Count > 0)
                    {
                        model.ZhenDuan = recordList[0].Data05;
                        model.ZhuShu = recordList[0].Data04;
                    }
                    model.HosName = this.curHosName;
                    if (sourceList[3] != null && sourceList[3].Rows != null && sourceList[3].Rows.Count > 0)
                    {
                        foreach (DataRow row in sourceList[3].Rows)
                        {
                            model.YaoPinList.Add(new YaoPinModel()
                            {
                                YaoPinName = row["DrugName"].ToString(),
                                YongFa = row["DrugYF"].ToString(),
                                YongLiang = row["DrugYL"].ToString()
                            });
                        }
                        printList.Add(model);
                    }
                    Report report = new Report();
                    report.Load(GetPath() + "chufang.frx");
                    report.RegisterData(printList, "CFDS");
                    report.Prepare();
                    report.ShowPrepared();
                    report.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("SetJiBenAndProjectInfo异常：" + ex.Message, ex);
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelYP_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("确定要删除该条记录吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    object obj = this.dataGridView1.SelectedRows[0].Cells["col_ID"].Value;
                    if (obj != null)
                    {
                        if (string.IsNullOrEmpty(obj.ToString()) == false)
                        {
                            this._DelGuid.Add(obj.ToString());
                        }
                    }
                    DataRowView drv = this.dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
                    drv.Delete();
                }
            }

        }

        public List<string> _DelGuid = new List<string>();

        private void btnAddYP_Click(object sender, EventArgs e)
        {
            DataRow row = this.yaoPinDataTable.NewRow();
            this.yaoPinDataTable.Rows.Add(row);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data03 = this.txtbs.Text.Trim();
            string data04 = this.txtzs.Text.Trim();
            string data05 = this.txtjg.Text.Trim();
            string hosid = this.curHosID;
            string hosname = this.curHosName;
            string docid = this.curEmpID;
            string docName = this.curEmpName;
            string recordid = this._hrid;
            YunZhiService service = new YunZhiService();
            YunZhiResult result = service.GetZhiHuiTuiSongData(data03, data04, data05, hosid, hosname, docid, docName, recordid);
            if (result.Result == 1)
            {
                string tuisongData = result.Msg;
                //解析推送消息就行显示在界面上
                //调用清空
                RunMethod(this.webBrowser1, "filllongtong", new string[] { tuisongData });
            }
            else
            {
                MessageBox.Show(result.Msg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private object RunMethod(WebBrowser webrowser, string scriptName, object[] args)
        {
            return webrowser.Document.InvokeScript(scriptName, args);
        }

        private void RunCallBackMethod(Object func, object[] args)
        {
            Type type = func.GetType();
            type.InvokeMember("", System.Reflection.BindingFlags.InvokeMethod, null, func, args);
        }

        private void UCArchives_Load(object sender, EventArgs e)
        {
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.ObjectForScripting = this;
            string path = Application.StartupPath + @"\LangTong.html";
            this.webBrowser1.Url = new System.Uri(path, System.UriKind.Absolute);
        }

    }
}
