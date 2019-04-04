using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceStack.OrmLite;
using YUNZHI.DAL;
using YUNZHI.DAL.Model;

namespace YUNZHI.Management.Manage
{
    public partial class YZDistrictManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {         
                if (Session["__BaseInfo"] != null)
                {
                    BindModel();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert(\"登录已失效，请重新登录！\")", true);

                    Response.Redirect("Login.aspx");
                }
            }
        }

        /// <summary>
        /// 根据传递的ID获取医疗机构详细信息
        /// </summary>
        public void BindModel()
        {
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["YZConnString"].ConnectionString;
                    var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

                    using (var db = dbFactory.Open())
                    {
                        string hid = Request.QueryString["ID"].ToString();

                        YZ_District hModel = db.SingleById<YZ_District>(hid);
                        txtAreaName.Text = hModel.DistName;
                    }

                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "2")
                    {

                    }
                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "3")
                    {
                        txtAreaName.Enabled = false;
                        btnSave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("编辑区域：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 保存，编辑区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["YZConnString"].ConnectionString;
                var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

                using (var db = dbFactory.Open())
                {
                    if (Request.QueryString["ID"] == null)  //添加医疗机构
                    {
                        YZ_District hModel = new YZ_District();

                        hModel.DistID = Guid.NewGuid().ToString("N");
                        hModel.DistName = txtAreaName.Text;
                        hModel.DistCreated = DateTime.Now;
                        hModel.IsEnable = true;

                        db.Insert(hModel);
                    }
                    else //编辑医疗机构
                    {
                        string hid = Request.QueryString["ID"].ToString();

                        YZ_District hModel = db.SingleById<YZ_District>(hid);
                        if (hModel != null)
                        {
                            hModel.DistName = txtAreaName.Text;
                            db.Update(hModel);
                        }
                    }
                }

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>ClosePage(1);</script>");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("区域管理：" + ex.Message, ex);
            }
        }
    }
}