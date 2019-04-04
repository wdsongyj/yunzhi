using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using YUNZHI.DAL;
using YUNZHI.DAL.Common;
using YUNZHI.DAL.BLL;
using System.Configuration;
using ServiceStack.OrmLite;
using YUNZHI.DAL.Model;
using HY.Web.Admin;
using System.Web.Script.Serialization;

namespace YUNZHI.Management.Webservice
{
    /// <summary>
    /// YunZhiService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class YunZhiService : System.Web.Services.WebService
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
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>用户登录</summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult UserLogin(string account, string pwd)
        {
            return _BLL.UserLogin(account, pwd);
        }


        /// <summary>根据患者身份证号获取就诊详情</summary>
        /// <param name="ptid"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetPatientsList(string idCard)
        {
            DataTable dt = null;
            try
            {
                YUNZHIDAL dal = new YUNZHIDAL();
                dt = dal.YZ_GetPatientsListByIDCard(idCard);
                return dt;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("获取患者就诊详情：" + ex.Message, ex);
                return dt;
            }
        }

        /// <summary>添加诊疗记录</summary>
        /// <param name="name">名称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="birtyary">生日</param>
        /// <param name="sex">性别</param>
        /// <param name="idnumber">身份证号</param>
        /// <param name="remark">描述</param>
        /// <param name="hid">医院Id</param>
        /// <param name="eid">医生id</param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult AddPatientInfo(string name, string address, string tel, string birtyary, string sex, string idnumber, string remark, string hid, string eid)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(idnumber) == true)
                {
                    return result;
                }
                bool isExists = _BLL.IsExistsPatient(idnumber);
                if (isExists == false)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["YZConnString"].ConnectionString;
                    var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

                    using (var db = dbFactory.Open())
                    {
                        YZ_Patients yzp = new YZ_Patients();
                        yzp.PTID = Guid.NewGuid().ToString("N");
                        yzp.PTName = name;
                        yzp.PTAddress = address;
                        yzp.PTTelPhone = tel;
                        yzp.PTBrithday = birtyary;
                        yzp.PTSex = sex;
                        yzp.PTIDNumber = idnumber;
                        yzp.PTRemark = remark;
                        yzp.PTCreated = DateTime.Now;
                        yzp.IsEnable = true;
                        int rid = (int)db.Insert(yzp);
                        if (rid > 0)
                        {
                            result.Result = 1;
                            result.Msg = rid.ToString();
                        }
                        else
                        {
                            result.Msg = "添加失败";
                        }
                    }
                }
                result = this._BLL.GetPatientInfoByIDNumber(idnumber);
                if (result.Result == 1)
                {
                    List<YZ_Patients> patientList = new ModelHandler<YZ_Patients>().TableToList(result.DataInfo);
                    if (patientList != null && patientList.Count > 0)
                    {
                        YZ_Patients currentPatient = patientList[0];
                        int addResult = this._BLL.AddHealthRecode(currentPatient.PTID, currentPatient.PTName, hid, eid);
                        if (addResult <= 0)
                        {
                            result.Result = 0;
                            result.Msg = "添加失败";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Msg = "添加患者错误：" + ex.Message;
                result.Result = 0;
                Logger.Log.Error("添加患者错误：" + ex.Message, ex);
            }
            return result;
        }

        /// <summary>得到医生当天看病的记录</summary>
        /// <param name="docId">医生id</param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult GetHealthy_RecordListByDocId(string docId)
        {
            return this._BLL.GetCurrentHealthListByDocId(docId);
        }

        /// <summary>得到患者信息通过患者ID</summary>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult GetPatientInfoByPTID(string patientID)
        {
            return this._BLL.GetPatientInfoByPTID(patientID);
        }

        /// <summary>得到诊疗信息通过用户ID(用户基本信息和项目列表）</summary>
        /// <param name="ptid"></param>
        /// <param name="hid"></param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult GetZhenLiaoListByPTID(string ptid, string hid,string hrid)
        {
            return this._BLL.GetZhenLiaoListByPTID(ptid, hid,hrid);
        }

        /// <summary>查询诊疗记录</summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="pname"></param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult QueryZhenLiaoList(string startTime, string endTime, string hid, string pname)
        {
            return this._BLL.QueryZhenLiaoList(startTime, endTime, hid, pname);
        }

        /// <summary>查询快检信息（返回客户列表信息）</summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="pname"></param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult QueryProjectList(string startTime, string endTime, string hid, string pname)
        {
            return this._BLL.QueryProjectList(startTime, endTime, hid, pname);
        }

        /// <summary>得到项目列表和客户信息</summary>
        /// <param name="eid"></param>
        /// <param name="hid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult GetProjectListAndPatientInfo(string eid, string hid, string startTime, string endTime)
        {
            return this._BLL.GetProjectListAndPatientInfo(eid, hid, startTime, endTime);
        }

        /// <summary>
        /// 删除诊疗信息
        /// </summary>
        /// <param name="hrid"></param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult DelHealthRecode(string hrid)
        {
            return this._BLL.DelHealthRecode(hrid);
        }

        /// <summary>
        /// 设置诊疗信息
        /// </summary>
        /// <param name="hrid"></param>
        /// <param name="data03"></param>
        /// <param name="data04"></param>
        /// <param name="data05"></param>
        /// <param name="data06"></param>
        /// <returns></returns>
        [WebMethod]
        public YunZhiResult UpdateRecorInfo(string hrid, string data03, string data04, string data05, string data06,DataTable table,List<string> delID)
        {
            return this._BLL.UpdateRecorInfo(hrid, data03, data04, data05, data06, table, delID);
        }

        [WebMethod]
        public YunZhiResult SetDrugInfo(DataTable table)
        {
            return this._BLL.SetDrugInfo(table);
        }

        [WebMethod]
        public YunZhiResult GetZhiHuiTuiSongData(string data03, string data04, string data05, string hosid, string hosname, string docid, string docName, string recordid)
        {
            LangTongResult res = this._BLL.SendLongTongData(data03, data04, data05, hosid, hosname, docid, docName, recordid);
            YunZhiResult result = new YunZhiResult();
            if (res.Ret == 0)
            {
                result.Result = 1;
                result.Msg = new JavaScriptSerializer().Serialize(res.Data);
            }
            else
            {
                result.Msg = res.Msg;
                result.Result = 0;
            }
            return result;
        }
    }
}
