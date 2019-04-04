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
    public partial class YZDeviceManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["__BaseInfo"] != null)
                {
                    BindHName();
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
        /// 绑定医疗机构
        /// </summary>
        private void BindHName()
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
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("编辑设备-医疗机构绑定：" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 根据传递的ID获取设备详细信息
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

                        YZ_Device DModel = db.SingleById<YZ_Device>(hid);
                        txtDSN.Text = DModel.DSN;
                        ddlStatus.SelectedValue = DModel.DStatus;
                        ddlHName.SelectedValue = DModel.HID;
                    }

                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "2")
                    {
                        txtDSN.Enabled = false;
                    }
                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "3")
                    {
                        txtDSN.Enabled = false;
                        ddlStatus.Enabled = false;
                        ddlHName.Enabled = false;
                        btnSave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("编辑设备：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 保存，编辑设备
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
                    if (Request.QueryString["ID"] == null)  //添加设备
                    {
                        YZ_Device DModel = new YZ_Device();

                        DModel.DID = Guid.NewGuid().ToString("N");
                        DModel.DSN = txtDSN.Text;
                        DModel.DStatus = ddlStatus.SelectedValue;
                        DModel.HID = ddlHName.SelectedValue;
                        DModel.DCreated = DateTime.Now;
                        if (ddlStatus.SelectedValue == "1")
                        {
                            DModel.DActivateTime = DateTime.Now;
                        }
                        DModel.IsEnable = true;

                        db.Insert(DModel);
                    }
                    else //编辑设备
                    {
                        string hid = Request.QueryString["ID"].ToString();

                        YZ_Device DModel = db.SingleById<YZ_Device>(hid);
                        if (DModel != null)
                        {
                            DModel.DSN = txtDSN.Text;
                            DModel.DStatus = ddlStatus.SelectedValue;
                            DModel.HID = ddlHName.SelectedValue;
                            if (ddlStatus.SelectedValue == "1")
                            {
                                DModel.DActivateTime = DateTime.Now;
                            }

                            db.Update(DModel);
                        }
                    }
                }

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>ClosePage(1);</script>");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("设备管理：" + ex.Message, ex);
            }
        }
    }
}