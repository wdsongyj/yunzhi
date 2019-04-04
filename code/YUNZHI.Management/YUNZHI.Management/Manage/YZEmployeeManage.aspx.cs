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
    public partial class YZEmployeeManage : System.Web.UI.Page
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
                Logger.Log.Error("编辑医生-医疗机构绑定：" + ex.Message, ex);
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

                        YZ_Employee EModel = db.SingleById<YZ_Employee>(hid);
                        txtEmpCode.Text = EModel.EmpCode;
                        txtEmpName.Text = EModel.EmpName;
                        txtEmpPwd.Text = EModel.EmpPwd;
                        txtEmpConfirmPwd.Text = EModel.EmpPwd;
                        txtEmpEMail.Text = EModel.EmpEMail;
                        txtEmpTelPhone.Text = EModel.EmpTelPhone;
                        txtEmpIDNumber.Text = EModel.EmpIDNumber;
                        ddlHName.SelectedValue = EModel.HID;
                    }

                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "2")
                    {
                        txtEmpCode.Enabled = false;
                    }
                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"] == "3")
                    {
                        txtEmpCode.Enabled = false;
                        txtEmpName.Enabled = false;
                        txtEmpPwd.Enabled = false;
                        txtEmpConfirmPwd.Enabled = false;
                        txtEmpEMail.Enabled = false;
                        txtEmpTelPhone.Enabled = false;
                        txtEmpIDNumber.Enabled = false;
                        ddlHName.Enabled = false;
                        btnSave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("编辑医生：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 保存，编辑医生
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
                    if (Request.QueryString["ID"] == null)  //添加医生
                    {
                        YZ_Employee EModel = new YZ_Employee();

                        EModel.EmpID = Guid.NewGuid().ToString("N");
                        EModel.EmpCode = txtEmpCode.Text;
                        EModel.EmpName = txtEmpName.Text;
                        EModel.EmpPwd = Validator.Md5(txtEmpPwd.Text);
                        EModel.EmpEMail = txtEmpEMail.Text;
                        EModel.EmpTelPhone = txtEmpTelPhone.Text;
                        EModel.EmpIDNumber = txtEmpIDNumber.Text;
                        EModel.HID = ddlHName.SelectedValue;
                        EModel.HospitalName = ddlHName.SelectedItem.Text;
                        EModel.EmpPinYin = "";
                        EModel.IsEnable = true;

                        db.Insert(EModel);
                    }
                    else //编辑医生
                    {
                        string hid = Request.QueryString["ID"].ToString();

                        YZ_Employee EModel = db.SingleById<YZ_Employee>(hid);
                        if (EModel != null)
                        {
                            EModel.EmpName = txtEmpName.Text;
                            EModel.EmpPwd = Validator.Md5(txtEmpPwd.Text);
                            EModel.EmpEMail = txtEmpEMail.Text;
                            EModel.EmpTelPhone = txtEmpTelPhone.Text;
                            EModel.EmpIDNumber = txtEmpIDNumber.Text;
                            EModel.HID = ddlHName.SelectedValue;
                            EModel.HospitalName = ddlHName.SelectedItem.Text;
                            db.Update(EModel);
                        }
                    }
                }

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>ClosePage(1);</script>");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("医生管理：" + ex.Message, ex);
            }
        }
    }
}