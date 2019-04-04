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
using System.Data;
using YUNZHI.DAL.DAL;

namespace YUNZHI.Management.Manage
{
    public partial class YZHospitalManage : System.Web.UI.Page
    {
        public YunZhiDALNew dalNew = new YunZhiDALNew();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["__BaseInfo"] != null)
                {
                    BindAreaType();
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
        /// 绑定省
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

                ddlProvince.Items.Insert(0, new ListItem("--省--", "0"));//添加指定位置item
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
            try
            {
                DataTable dtUrban = dalNew.GetAreaLevel(parentCode);

                if (dtUrban != null && dtUrban.Rows.Count > 0)
                {
                    ddlUrban.Items.Clear();

                    ddlUrban.DataSource = dtUrban.DefaultView;
                    ddlUrban.DataTextField = "DistName";
                    ddlUrban.DataValueField = "Data01";
                    ddlUrban.DataBind();
                }

                ddlUrban.Items.Insert(0, new ListItem("--市--", "0"));//添加指定位置item
                ddlUrban.Items.FindByValue("0").Selected = true;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("区域管理-区域绑定：" + ex.Message, ex);
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

                        YZ_Hospital hModel = db.SingleById<YZ_Hospital>(hid);
                        txtHCode.Text = hModel.HCode;
                        txtHName.Text = hModel.HName;
                        ddlProvince.SelectedValue = hModel.Data03;
                        ddlUrban.SelectedValue = hModel.Data04;
                    }

                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "2")
                    {

                    }
                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "3")
                    {
                        txtHCode.Enabled = false;
                        txtHName.Enabled = false;
                        ddlProvince.Enabled = false;
                        ddlUrban.Enabled = false;
                        btnSave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("编辑医疗机构：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 保存，编辑医疗机构
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
                        YZ_Hospital hModel = new YZ_Hospital();

                        if (ddlProvince.SelectedValue != "0")
                        {
                            if (ddlUrban.SelectedValue != "0")
                            {
                                hModel.HID = Guid.NewGuid().ToString("N");
                                hModel.HCode = txtHCode.Text;
                                hModel.HName = txtHName.Text;
                                hModel.Data03 = ddlProvince.SelectedValue;
                                hModel.Data04 = ddlUrban.SelectedValue;
                                hModel.HCreated = DateTime.Now;
                                hModel.IsEnable = true;

                                db.Insert(hModel);

                                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>ClosePage(1);</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('请选择“省”！')</script>");
                                return;
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('请选择“省”！')</script>");
                            return;
                        }
                    }
                    else //编辑医疗机构
                    {
                        string hid = Request.QueryString["ID"].ToString();

                        YZ_Hospital hModel = db.SingleById<YZ_Hospital>(hid);
                        if (hModel != null)
                        {
                            if (ddlProvince.SelectedValue != "0")
                            {
                                if (ddlUrban.SelectedValue != "0")
                                {

                                    hModel.HCode = txtHCode.Text;
                                    hModel.HName = txtHName.Text;
                                    hModel.Data03 = ddlProvince.SelectedValue;
                                    hModel.Data04 = ddlUrban.SelectedValue;
                                    db.Update(hModel);

                                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>ClosePage(2);</script>");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('请选择“省”！')</script>");
                                    return;
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('请选择“省”！')</script>");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("医疗机构管理：" + ex.Message, ex);
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProvinceID = Convert.ToInt32(ddlProvince.SelectedValue);

            BindAreaUrbanType(ProvinceID);
        }
    }
}