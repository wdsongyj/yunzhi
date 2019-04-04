using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceStack.OrmLite;
using YUNZHI.DAL;
using YUNZHI.DAL.Model;

namespace YUNZHI.Management.Manage
{
    public partial class YZHealthDetailList : System.Web.UI.Page
    {
        public YUNZHIDAL dal = new YUNZHIDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["__BaseInfo"] != null)
                {
                    this.pagerbind.PageSize = Int32.Parse(this.ddlPageSize.SelectedItem.Value);
                    BindAreaType();
                    BindRepeater();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert(\"登录已失效，请重新登录！\")", true);

                    Response.Redirect("Login.aspx");
                }
            }
        }

        /// <summary>
        /// 绑定医疗机构
        /// </summary>
        private void BindAreaType()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["YZConnString"].ConnectionString;
                var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

                using (var db = dbFactory.Open())
                {
                    var results = db.Select<YZ_Hospital>(x => x.IsEnable == true);

                    if (results != null && results.Count > 0)
                    {
                        ddlHName.Items.Clear();

                        ddlHName.DataSource = results;
                        ddlHName.DataTextField = "HName";
                        ddlHName.DataValueField = "HID";
                        ddlHName.DataBind();
                    }

                    ddlHName.Items.Insert(0, new ListItem("全部", "0"));//添加指定位置item
                    ddlHName.Items.FindByValue("0").Selected = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("编辑设备-医疗机构绑定：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Repeater数据绑定
        /// </summary>
        public void BindRepeater()
        {
            try
            {
                DataTable dt = null;

                int totalcount = 0;
                string hName = "";
                string ptName = "";
                string idNumber = "";
                string startDate = "1990-01-01";
                string endDate = "2050-12-31";

                if (txtPTName.Text != "")
                {
                    ptName = txtPTName.Text;
                }
                if (txtIDNumber.Text != "")
                {
                    idNumber = txtIDNumber.Text;
                }
                if (ddlHName.SelectedValue != "0")
                {
                    hName = ddlHName.SelectedItem.Text;
                }
                if (txtStartDate.Text != "")
                {
                    startDate = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
                }
                if (txtEndDate.Text != "")
                {
                    endDate = Convert.ToDateTime(txtEndDate.Text).AddDays(1).ToString("yyyy-MM-dd");
                }

                dt = dal.YZ_GetPatientsList("", hName, ptName, idNumber, startDate, endDate, this.pagerbind.PageSize, this.pagerbind.CurrentPageIndex, out totalcount);

                //绑定数据源
                if (dt != null)
                {
                    this.RepLetter.DataSource = dt.DefaultView;
                    this.RepLetter.DataBind();
                    this.pagerbind.RecordCount = totalcount;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
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
                this.pagerbind.CurrentPageIndex = 0;
                BindRepeater();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 翻页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pageBind_PageChange(object sender, EventArgs e)
        {
            try
            {
                BindRepeater();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pagerbind.PageSize = Int32.Parse(this.ddlPageSize.SelectedItem.Value);
            BindRepeater();
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveAllCache(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}