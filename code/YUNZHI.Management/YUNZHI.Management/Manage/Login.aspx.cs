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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoginV2();
        }

        private void LoginV2()
        {
            YunZhiDALNew dal = new YunZhiDALNew();

            string userName = username.Text;
            string pwd = password.Text;

            if(userName=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert(\"请输入账号！!\")", true);
                return;
            }
            if (pwd == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert(\"请输入密码！!\")", true);
                return;
            }

            //0-登录账号 1-显示名 2-邮箱 3-电话 4-所在医院 5-身份证号
            string[] baseInfo = new string[6] { "", "", "", "", "", "" };

            DataTable dtInfo = dal.UserLogin(userName, EncodePassword(pwd));

            if (dtInfo.Rows.Count > 0)
            {
                string EmpName = dtInfo.Rows[0]["EmpName"] == null ? "" : dtInfo.Rows[0]["EmpName"].ToString();
                string EmpEMail = dtInfo.Rows[0]["EmpEMail"] == null ? "" : dtInfo.Rows[0]["EmpEMail"].ToString();
                string EmpTelPhone = dtInfo.Rows[0]["EmpTelPhone"] == null ? "" : dtInfo.Rows[0]["EmpTelPhone"].ToString();
                string HospitalName = dtInfo.Rows[0]["HospitalName"] == null ? "" : dtInfo.Rows[0]["HospitalName"].ToString();
                string EmpIDNumber = dtInfo.Rows[0]["EmpIDNumber"] == null ? "" : dtInfo.Rows[0]["EmpIDNumber"].ToString();

                baseInfo = new string[6] { userName, EmpName, EmpEMail, EmpTelPhone, HospitalName, EmpIDNumber };

                Session["__BaseInfo"] = baseInfo;

                Response.Redirect("Index.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert(\"错误的用户名或密码！\")", true);
                return;
            }  
        }

        public string EncodePassword(string pass)
        {
            return StringUtils.EncryptByMD5(pass);
        }
    }
}