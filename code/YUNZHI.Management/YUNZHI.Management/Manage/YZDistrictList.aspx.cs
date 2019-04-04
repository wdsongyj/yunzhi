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
using System.IO;
using System.Data.OleDb;
using YUNZHI.DAL.DAL;

namespace YUNZHI.Management.Manage
{
    public partial class YZDistrictList : System.Web.UI.Page
    {
        public YUNZHIDAL dal = new YUNZHIDAL();
        public YunZhiDALNew dalNew = new YunZhiDALNew();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["__BaseInfo"] != null)
                {
                    BindAreaType();

                    this.pagerbind.PageSize = Int32.Parse(this.ddlPageSize.SelectedItem.Value);
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
        /// 绑定区域 省
        /// </summary>
        private void BindAreaType()
        {
            try
            {
                DataTable dt = dalNew.GetAreaLevel(0);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlProvince.Items.Clear();

                    ddlProvince.DataSource = dt.DefaultView;
                    ddlProvince.DataTextField = "DistName";
                    ddlProvince.DataValueField = "Data01";
                    ddlProvince.DataBind();
                }

                ddlProvince.Items.Insert(0, new ListItem("全部", "0"));//添加指定位置item
                ddlProvince.Items.FindByValue("0").Selected = true;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("区域管理-区域绑定：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 绑定市
        /// </summary>
        private void BindAreaUrbanType(int parentCode)
        {
            //try
            //{
            //    DataTable dtUrban = dalNew.GetAreaLevel(parentCode);

            //    if (dtUrban != null && dtUrban.Rows.Count > 0)
            //    {
            //        ddlUrban.Items.Clear();

            //        ddlUrban.DataSource = dtUrban.DefaultView;
            //        ddlUrban.DataTextField = "DistName";
            //        ddlUrban.DataValueField = "Data01";
            //        ddlUrban.DataBind();
            //    }

            //    ddlUrban.Items.Insert(0, new ListItem("--市--", "0"));//添加指定位置item
            //    ddlUrban.Items.FindByValue("0").Selected = true;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log.Error("区域管理-区域绑定：" + ex.Message, ex);
            //}
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
                string pCode = "";

                if (txtTitle.Text != "")
                {
                    hName = txtTitle.Text;
                }
                if (ddlProvince.SelectedValue != "0")
                {
                    pCode = ddlProvince.SelectedValue;
                }

                dt = dal.YZ_GetDistrictList("", hName, pCode, this.pagerbind.PageSize, this.pagerbind.CurrentPageIndex, out totalcount);

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

        protected void lbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["YZConnString"].ConnectionString;
                var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

                using (var db = dbFactory.Open())
                {
                    YZ_District faq = db.SingleById<YZ_District>(e.CommandName);
                    if (faq != null)
                    {
                        faq.IsEnable = false;
                        db.Update(faq);
                        BindRepeater();
                        RemoveAllCache("_LoginInit");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

        protected void lbBatchDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] sign = chdSelectedItems.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sbSQL = new StringBuilder("");
                foreach (string item in sign)
                {
                    sbSQL.Append("update [dbo].[YZ_District] set IsEnable=0 where ID='" + item + "'; ");
                }
                if (sbSQL.ToString().Length > 10)
                {
                    DBHelper help = new DBHelper();
                    help.ExecuteSql(sbSQL.ToString());

                    Logger.Log.Info(sbSQL.ToString());
                }

                chdSelectedItems.Value = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert(\"批量删除成功!\")", true);

                BindRepeater();

                RemoveAllCache("_LoginInit");
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

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProvinceID = Convert.ToInt32(ddlProvince.SelectedValue);

            BindAreaUrbanType(ProvinceID);
        }
    }
}