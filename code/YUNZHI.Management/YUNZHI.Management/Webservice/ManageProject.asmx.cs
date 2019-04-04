using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ServiceStack.OrmLite;
using YUNZHI.DAL;
using YUNZHI.DAL.Model;
using System.Web.Script.Services;
using YUNZHI.DAL.BLL;
using YUNZHI.DAL.Common;
using YUNZHI.DAL.Utility;

namespace YUNZHI.Management.Webservice
{
    /// <summary>
    /// ManageProject 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class ManageProject : System.Web.Services.WebService
    {
        private YunZhiBLL bll;

        public YunZhiBLL _BLL
        {
            get
            {
                if (bll == null)
                {
                    bll = new YunZhiBLL();
                }
                return bll;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 插入快检信息
        /// </summary>
        /// <param name="proName">项目名称</param>
        /// <param name="did">DID</param>
        /// <param name="dsn">设备序列表</param>
        /// <param name="checkTime">检测时间</param>
        /// <param name="checkResult">检测结果</param>
        /// <param name="checkRemark">检测结果描述</param>
        /// <param name="ptid">患者ID</param>
        /// <param name="idCard">患者身份证号</param>
        /// <returns></returns>
        [WebMethod]
        public bool InsertProject(string proName, string did, string dsn, DateTime checkTime, string checkResult, string checkRemark, string ptid, string idCard, string ckfw)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["YZConnString"].ConnectionString;
                var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

                using (var db = dbFactory.Open())
                {
                    YZ_Project yzpModel = new YZ_Project();
                    yzpModel.ProID = Guid.NewGuid().ToString("N");
                    yzpModel.ProName = proName; //项目名称
                    yzpModel.DID = did; //DID 设备id
                    yzpModel.DSN = dsn; //设备序列表
                    if (checkTime != null && checkTime.ToString() != "")
                    {
                        yzpModel.ProCheckTime = checkTime;  //检测时间
                    }
                    else
                    {
                        yzpModel.ProCheckTime = DateTime.Now;  //检测时间
                    }
                    yzpModel.ProCheckResult = checkResult;  //检测结果
                    yzpModel.ProCheckRemark = string.IsNullOrEmpty(checkRemark) == true ? " " : checkRemark; //检测结果描述
                    yzpModel.PTID = ptid;  //患者ID
                    yzpModel.PTIDNumber = idCard;  //患者身份证号
                    yzpModel.IsEnable = true; //是否可用
                    yzpModel.Data03 = ckfw;
                    int rid = (int)db.Insert(yzpModel);
                    if (rid > 0)
                    {
                        //通过身份证查询患者信息
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("更新快检信息失败，对外接口：" + ex.Message, ex);
                return false;
            }
        }

        [WebMethod]
        public YunZhiResult GetProjectList(string sn, DateTime startTime, DateTime endTime, string token)
        {
            return _BLL.GetProjectList(sn, startTime, endTime, token);
        }
    }
}
