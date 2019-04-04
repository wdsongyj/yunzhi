using Paway.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunZhi.Client.YZService;
using YunZhi.Model;
using YunZhi.Util;

namespace YunZhi.Client.Frm
{
    public partial class FrmDetail : _360Form
    {

        private BackgroundWorker _MainWork;

        private string _PID = string.Empty;
        private string _HID = string.Empty;
        private string _HRID = string.Empty;
        public FrmDetail()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this._MainWork = new BackgroundWorker();
            this._MainWork.DoWork += _MainWork_DoWork;
            this._MainWork.RunWorkerCompleted += _MainWork_RunWorkerCompleted;
        }
        public FrmDetail(string pid, string hid, string hrid)
            : this()
        {
            this._PID = pid;
            this._HID = hid;
            this._HRID = hrid;
            this._MainWork.RunWorkerAsync();
        }
        void _MainWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result is YunZhiResult)
            {
                YunZhiResult result = e.Result as YunZhiResult;
                if (result.Result == 0)
                {
                    MessageBox.Show(result.Msg);
                }
                else
                {
                    this.SetValue(result.DataList);
                }
            }
        }

        void _MainWork_DoWork(object sender, DoWorkEventArgs e)
        {
            YunZhiService service = null;
            try
            {
                service = new YunZhiService();
                e.Result = service.GetZhenLiaoListByPTID(this._PID, this._HID, this._HRID);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("获取诊疗详情错误：" + ex.Message, ex);
            }
            finally
            {
                if (service != null)
                {
                    service.Abort();
                }
            }
        }

        private void SetValue(DataTable[] sourceList)
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
                    this.lbl_Name.Text = patinetModel.PTName;
                    this.lbl_Sex.Text = patinetModel.PTSex == "0" ? "男" : "女";
                    this.lbl_Birthday.Text = string.IsNullOrEmpty(patinetModel.PTBrithday) == true ? "" : Convert.ToDateTime(patinetModel.PTBrithday).ToString("yyyy-MM-dd");
                    this.lbl_Tel.Text = patinetModel.PTTelPhone;
                    this.lbl_Address.Text = patinetModel.PTAddress;
                    this.lbl_Remark.Text = patinetModel.PTRemark;
                    this.lbl_IDCard.Text = patinetModel.PTIDNumber;
                }
                this.dataGridView1.DataSource = sourceList[1];
                List<YZ_Health_Record> recordList = new ModelHandler<YZ_Health_Record>().TableToList(sourceList[2]);
                if (recordList != null && recordList.Count > 0)
                {
                    YZ_Health_Record curModel = recordList[0];
                    if (string.IsNullOrEmpty(curModel.Data04) == true)
                    {
                        this.label12.Text = "";
                    }
                    else
                    {
                        this.label12.Text = curModel.Data04.Replace("\n", "");
                    }
                    if (string.IsNullOrEmpty(curModel.Data03) == true)
                    {
                        this.label13.Text = "";
                    }
                    else
                    {
                        this.label13.Text = curModel.Data03.Replace("\n", "");
                    }
                    if (string.IsNullOrEmpty(curModel.Data05) == true)
                    {
                        this.label14.Text = "";
                    }
                    else
                    {
                        this.label14.Text = curModel.Data05.Replace("\n", "");
                    }
                    if (string.IsNullOrEmpty(curModel.Data06) == true)
                    {
                        this.label15.Text = "";
                    }
                    else
                    {
                        this.label15.Text = curModel.Data06.Replace("\n", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("SetJiBenAndProjectInfo异常：" + ex.Message, ex);
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
