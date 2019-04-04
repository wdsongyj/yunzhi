using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunZhi.Util;
using YunZhi.Client.Frm;
using YunZhi.ControlLib;
using YunZhi.Client.YZService;

namespace YunZhi.Client.FrmCtr
{
    public partial class UCSearchArchives : YZUserContrl
    {
        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _PatientName = string.Empty;


        private BackgroundWorker _mainBgWork;

        public UCSearchArchives()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this._mainBgWork = new BackgroundWorker();
            this._mainBgWork.DoWork += _mainBgWork_DoWork;
            this._mainBgWork.RunWorkerCompleted += _mainBgWork_RunWorkerCompleted;            
        }

        void _mainBgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result is YunZhiResult)
            {
                YunZhiResult curResult = e.Result as YunZhiResult;
                if (curResult.Result == 0)
                {
                    MessageBox.Show(curResult.Msg);
                }
                else
                {
                    this.dataGridView1.DataSource = curResult.DataInfo;
                    if (curResult.DataInfo == null || curResult.DataInfo.Rows.Count == 0)
                    {
                        MessageBox.Show("没有查询出来数据");
                    }
                }
            }
            this.btnSearch.Enabled = true;
        }

        void _mainBgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            YunZhiService service = null;
            try
            {
                service = new YunZhiService();
                YunZhiResult result=service.QueryZhenLiaoList(this._StartDate.ToString("yyyy-MM-dd 00:00:00"), this._EndDate.ToString("yyyy-MM-dd 23:59:59"), SourceHelper.EmployeeInfo.HID, this._PatientName);
                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("获取档案信息异常：" + ex.Message, ex);
            }
            finally
            {
                if (service != null)
                {
                    service.Abort();
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //获取到用户ID和医院ID            
            string pID = this.dataGridView1.Rows[e.RowIndex].Cells["col_PTID"].Value.ToString();
            string hID = this.dataGridView1.Rows[e.RowIndex].Cells["col_HID"].Value.ToString();
            string hrID = this.dataGridView1.Rows[e.RowIndex].Cells["col_HRID"].Value.ToString();
            FrmDetail project = new FrmDetail(pID, hID, hrID);
            project.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this._mainBgWork.IsBusy == true)
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
            this._StartDate = startDate;
            this._EndDate = endDate;
            this._PatientName = this.txtPatientName.Text.Trim();
            this.btnSearch.Enabled = false;
            this._mainBgWork.RunWorkerAsync();
        }
    }
}
