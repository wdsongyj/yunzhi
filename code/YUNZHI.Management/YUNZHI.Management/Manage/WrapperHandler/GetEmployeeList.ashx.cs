using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;
using System.Collections;
using HY.Common.Utility.Utils;
using HY.Web.Admin.WrapperHandler;
using System.Configuration;
using ServiceStack.OrmLite;
using YUNZHI.DAL.Model;

namespace YUNZHI.Management.Manage.WrapperHandler
{
    [System.Security.SecuritySafeCritical]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class GetEmployeeList : AHttpHandler
    {

        public override void ProcessRequest(string method)
        {
            switch (method)
            {
                case "GetNewsList":
                    GetNewsList(context);
                    break;
                default:
                    break;
            }
        }

        public virtual void GetNewsList(HttpContext contex)
        {
            string code = "0";
            string msg = "";

            try
            {
                ArrayList alllist = new ArrayList();

                string ptId = Guid.NewGuid().ToString("N"); //患者ID
                string ptName = context.Request["PTName"].ToString();//患者姓名
                string ptAddress = context.Request["PTAddress"].ToString();//家庭住址
                string ptBrithDay = context.Request["PTBrithday"].ToString();//生日
                string ptSex = context.Request["PTSex"].ToString();//性别
                string ptIDNumber = context.Request["PTIDNumber"].ToString();//身份证号
                string ptRemark = context.Request["PTRemark"].ToString();//备注

                string hrID = Guid.NewGuid().ToString("N");// 就诊ID
                string EID = context.Request["EID"].ToString();//医生账号ID
                string HID = context.Request["HID"].ToString();//医疗设备ID
                string hrRemark = context.Request["HRRemark"].ToString();//备注

                string hdID = Guid.NewGuid().ToString("N");// 就诊详情ID
                string proID = context.Request["ProID"].ToString();//项目ID
                string hdRemark = context.Request["HDRemark"].ToString();//备注

                string connectionString = ConfigurationManager.ConnectionStrings["YZConnString"].ConnectionString;
                var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

                using (var db = dbFactory.Open())
                {
                    //患者信息
                    YZ_Patients yzpModel = new YZ_Patients();
                    yzpModel.PTID = ptId;
                    yzpModel.PTName = ptName;
                    yzpModel.PTAddress = ptAddress;
                    yzpModel.PTBrithday = ptBrithDay;
                    yzpModel.PTSex = ptSex;
                    yzpModel.PTIDNumber = ptIDNumber;
                    yzpModel.PTRemark = ptRemark;
                    yzpModel.PTCreated = DateTime.Now;
                    yzpModel.IsEnable = true;
                    db.Insert(yzpModel);

                    //就诊
                    YZ_Health_Record yzhr = new YZ_Health_Record();
                    yzhr.HRID = hrID;
                    yzhr.HID = HID;
                    yzhr.EID = EID;
                    yzhr.PTID = ptId;
                    yzhr.HRCreated = DateTime.Now;
                    yzhr.HRRemark = hrRemark;
                    yzhr.IsEnable = true;
                    db.Insert(yzhr);

                    //就诊详情
                    YZ_Health_Detail yzhd = new YZ_Health_Detail();
                    yzhd.HDID = hdID;
                    yzhd.HRID = hrID;
                    yzhd.ProID = proID;
                    yzhd.HDCreated = DateTime.Now;
                    yzhd.HDRemark = hdRemark;
                    db.Insert(yzhd);

                    code = "1";
                    msg = "新增成功.";         
                }

                EmployeeList el = new EmployeeList();
                el.Emp_Code = "test";
                el.Emp_Name = "test";
                alllist.Add(el);

                //string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(alllist);
                //contex.Response.Write(strJson);
            }
            catch (Exception ex)
            {
                code = "0";
                msg = ex.Message;
                Logger.Log.Error(ex.Message, ex);           
            }

            string strJson = UserHelper.RequestJsonP("{\"code\":\"" + code + "\",\"msg\":\"" + msg + "\"}");
            context.Response.Write(strJson);
        }
    }

    [Serializable]
    public class EmployeeList
    {
        public string Emp_Code { get; set; }
        public string Emp_Name { get; set; }
    }
}