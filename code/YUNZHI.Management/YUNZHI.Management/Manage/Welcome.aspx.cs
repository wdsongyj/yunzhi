using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YUNZHI.DAL;
using YUNZHI.DAL.DAL;

namespace YUNZHI.Management.Manage
{
    public partial class Welcome : System.Web.UI.Page
    {
        public YunZhiDALNew dal = new YunZhiDALNew();

        string _deviceData = "";
        public string DeviceData
        {
            set
            {
                this.ViewState["DeviceData"] = value;
            }
            get
            {
                if (this.ViewState["DeviceData"] == null)
                    return _deviceData;
                else
                    return this.ViewState["DeviceData"].ToString();
            }
        }

        string _healthName = "";
        public string HealthName
        {
            set
            {
                this.ViewState["HealthName"] = value;
            }
            get
            {
                if (this.ViewState["HealthName"] == null)
                    return _healthName;
                else
                    return this.ViewState["HealthName"].ToString();
            }
        }

        string _healthCount = "";
        public string HealthCount
        {
            set
            {
                this.ViewState["HealthCount"] = value;
            }
            get
            {
                if (this.ViewState["HealthCount"] == null)
                    return _healthCount;
                else
                    return this.ViewState["HealthCount"].ToString();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetHealthCount();
                GetDeviceUsage();
            }
        }


        /// <summary>
        /// 获取设备使用情况占比
        /// </summary>
        private void GetDeviceUsage()
        {
            DataTable dt = null;
            string startDate = "2000-01-01";
            string endDate = "2050-12-31";

            if (txtStartDate.Text != "")
            {
                startDate = txtStartDate.Text;
            }
            if (txtEndDate.Text != "")
            {
                endDate = txtEndDate.Text;
            }
            dt = dal.GetDeviceUsageList(startDate, endDate);
            if (dt != null && dt.Rows.Count > 0)
            {
                string devData = "";
                int ordered = 0;
                foreach (DataRow dr in dt.Rows)  //DeviceCount
                {
                    if (ordered == 0)
                    {
                        devData += "{name: '" + dr["DSN"].ToString() + "',"
                                    + " y: " + dr["UsedRate"].ToString() + ","
                                    + " sliced: true,  "
                                    + " selected: true "
                                    + "},";
                    }
                    else
                    {
                        devData += " ['" + dr["DSN"].ToString() + "', " + dr["UsedRate"].ToString() + ",],";
                    }

                    ordered++;
                }
                DeviceData = devData.TrimEnd(',');
            }
        }

        /// <summary>
        /// 获取各医院就诊情况数据
        /// </summary>
        private void GetHealthCount()
        {
            DataTable dt = null;
            string startDate = "2000-01-01";
            string endDate = "2050-12-31";

            if (txtStartDate.Text != "")
            {
                startDate = txtStartDate.Text;
            }
            if (txtEndDate.Text != "")
            {
                endDate = txtEndDate.Text;
            }
            dt = dal.GetHealthByHospital(startDate, endDate);
            if (dt != null && dt.Rows.Count > 0)
            {
                string hName = "";
                string hCount = "";
                foreach (DataRow dr in dt.Rows)
                {
                    hName += "'" + dr["HName"].ToString() + "'" + ",";
                    hCount += Convert.ToInt32(dr["PatientsCount"].ToString()) + ",";
                }
                HealthName = hName.TrimEnd(',');
                HealthCount = hCount.TrimEnd(',');
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                GetHealthCount();
                GetDeviceUsage();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtStartDate.Text = "";
                txtEndDate.Text = "";

                GetHealthCount();
                GetDeviceUsage();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }
    }
}