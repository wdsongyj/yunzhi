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
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["__BaseInfo"] = null;

                    //移出所有App的Session
                    HttpContext.Current.Session.RemoveAll();
                    HttpContext.Current.Request.Cookies.Clear();
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }
    }
}