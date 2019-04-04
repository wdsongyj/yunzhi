using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using YUNZHI.DAL.Common;
using YUNZHI.DAL.DAL;
using YUNZHI.DAL.Model;
using YUNZHI.DAL.Utility;

namespace YUNZHI.DAL.BLL
{
    public class YunZhiBLL : IYunZhiBLL
    {
        private YunZhiDALNew _dal;

        public YunZhiDALNew YunZhiDal
        {
            get
            {
                if (this._dal == null)
                {
                    this._dal = new YunZhiDALNew();
                }
                return _dal;
            }
        }

        /// <summary>用户登录</summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public YunZhiResult UserLogin(string account, string pwd)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pwd))
                {
                    result.Msg = "用户名或密码不能为空!";
                    return result;
                }
                string md5Pwd = MD5Helper.MD5Encrypt(pwd);
                DataTable table = this.YunZhiDal.UserLogin(account, md5Pwd);
                if (table == null || table.Rows.Count == 0)
                {
                    result.Msg = "用户名或者密码错误！";
                }
                else
                {
                    result.Result = 1;
                    result.DataInfo = table;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "登录错误：" + ex.Message;
            }
            return result;
        }

        /// <summary>获取患者列表</summary>
        /// <param name="idCards"></param>
        /// <returns></returns>
        public YunZhiResult GetPatientsList(string idCards)
        {
            throw new NotImplementedException();
        }

        /// <summary>得到项目列表</summary>
        /// <param name="sn">设备SN号码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="token">系统分配的token，测试可以任意填写</param>
        /// <returns></returns>
        public YunZhiResult GetProjectList(string sn, DateTime startTime, DateTime endTime, string token)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(sn))
                {
                    result.Msg = "sn不能为空";
                    return result;
                }
                if (string.IsNullOrEmpty(token))
                {
                    result.Msg = "token错误!";
                    return result;
                }
                if (startTime == null || startTime.ToString() == "" || endTime == null || endTime.ToString() == "")
                {
                    result.Msg = "开始时间或结束时间不能为空!";
                    return result;
                }


                DataTable table = this.YunZhiDal.GetProjectList(sn, startTime, endTime, token);
                result.Result = 1;
                result.DataInfo = table;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "获取项目数据错误：" + ex.Message;
            }
            return result;
        }


        /// <summary>获取当前医生的看病记录通过医生ID</summary>
        /// <param name="docId">医生id</param>
        /// <returns></returns>
        public YunZhiResult GetCurrentHealthListByDocId(string docId)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                DataTable table = this.YunZhiDal.GetCurrentHealthListByDocId(docId);
                result.Result = 1;
                result.DataInfo = table;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "获取就诊记录错误：" + ex.Message;
            }
            return result;
        }

        /// <summary>获取患者信息通过身份证号</summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public YunZhiResult GetPatientInfoByIDNumber(string idNumber)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                DataTable table = this.YunZhiDal.GetPatientInfoByIDNumber(idNumber);
                result.Result = 1;
                result.DataInfo = table;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "获取患者信息：" + ex.Message;
            }
            return result;
        }

        /// <summary>获取患者信息通过患者ID</summary>
        /// <param name="ptid"></param>
        /// <returns></returns>
        public YunZhiResult GetPatientInfoByPTID(string ptid)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                DataTable table = this.YunZhiDal.GetPatientInfoByPTID(ptid);
                if (table == null || table.Rows.Count == 0)
                {
                    result.Msg = "没有查询到数据";
                }
                else
                {
                    result.Result = 1;
                    result.DataInfo = table;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "获取患者数据错误：" + ex.Message;
            }
            return result;
        }

        /// <summary>是否存在该患者</summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public bool IsExistsPatient(string idNumber)
        {
            return this.YunZhiDal.IsExistsPatient(idNumber);
        }

        /// <summary>添加诊疗记录</summary>
        /// <param name="ptid"></param>
        /// <param name="ptname"></param>
        /// <param name="hid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public int AddHealthRecode(string ptid, string ptname, string hid, string eid)
        {
            return this.YunZhiDal.AddHealthRecode(ptid, ptname, hid, eid);
        }

        public YunZhiResult Demo(string ptid)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                DataTable table = new DataTable();
                result.Result = 1;
                result.DataInfo = table;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "获取患者数据错误：" + ex.Message;
            }
            return result;
        }

        /// <summary>得到用户基本信息和项目列表</summary>
        /// <param name="ptid"></param>
        /// <param name="hid"></param> 
        /// <param name="hrid"></param>
        /// <returns></returns>
        public YunZhiResult GetZhenLiaoListByPTID(string ptid, string hid, string hrid)
        {
            YunZhiResult result = new YunZhiResult();
            if (string.IsNullOrEmpty(ptid) == true || string.IsNullOrEmpty(hid) == true)
            {
                result.Msg = "参数不能为空";
                return result;
            }
            try
            {
                DataTable patientSource = this.YunZhiDal.GetPatientInfoByPTID(ptid);
                //通过ptid得到ptidnumber
                List<YZ_Patients> patientList = new ModelHandler<YZ_Patients>().TableToList(patientSource);
                List<DataTable> tableList = new List<DataTable>();
                tableList.Add(patientSource);
                if (patientList != null && patientList.Count > 0)
                {
                    string ptidnumber = patientList[0].PTIDNumber;
                    DataTable projectSource = this.YunZhiDal.GetProjectListByPatientID(ptidnumber, hid);
                    tableList.Add(projectSource);

                    DataTable recordData = this.YunZhiDal.GetRecordInfo(hrid);
                    tableList.Add(recordData);

                    //药品信息
                    DataTable drugInfo = this.YunZhiDal.GetDrugList(hrid);
                    tableList.Add(drugInfo);
                }
                else
                {
                    tableList.Add(new DataTable());
                    tableList.Add(new DataTable());
                    tableList.Add(new DataTable());
                }
                result.Result = 1;
                result.DataList = tableList;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "获取患者基本信息和检查项目错误：" + ex.Message;
            }
            return result;
        }

        /// <summary>查询诊疗记录</summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="pname"></param>
        /// <returns></returns>
        public YunZhiResult QueryZhenLiaoList(string startTime, string endTime, string hid, string pname)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(startTime) == true || string.IsNullOrEmpty(endTime) == true || string.IsNullOrEmpty(hid) == true)
                {
                    result.Msg = "时间或者医院不能为空";
                    return result;
                }
                if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
                {
                    result.Msg = "开始日期不能大于结束日期";
                    return result;
                }
                startTime = Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00");
                endTime = Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:59");
                DataTable table = this.YunZhiDal.QueryZhenLiaoList(startTime, endTime, hid, pname);
                result.Result = 1;
                result.DataInfo = table;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "获取诊疗数据错误：" + ex.Message;
            }
            return result;
        }

        /// <summary>查询快检信息（返回客户列表信息）</summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="pname"></param>
        /// <returns></returns>
        public YunZhiResult QueryProjectList(string startTime, string endTime, string hid, string pname)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(startTime) == true || string.IsNullOrEmpty(endTime) == true || string.IsNullOrEmpty(hid) == true)
                {
                    result.Msg = "时间或者医院不能为空";
                    return result;
                }
                if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
                {
                    result.Msg = "开始日期不能大于结束日期";
                    return result;
                }
                startTime = Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00");
                endTime = Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:59");
                DataTable table = this.YunZhiDal.GetProjectPatientDistinct(startTime, endTime, hid, pname);
                result.Result = 1;
                result.DataInfo = table;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "查询快检信息错误：" + ex.Message;
            }
            return result;
        }

        /// <summary>得到项目列表和客户信息</summary>
        /// <param name="eid"></param>
        /// <param name="hid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public YunZhiResult GetProjectListAndPatientInfo(string eid, string hid, string startTime, string endTime)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(startTime) == true || string.IsNullOrEmpty(endTime) == true || string.IsNullOrEmpty(hid) == true)
                {
                    result.Msg = "时间或者医院不能为空";
                    return result;
                }
                if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
                {
                    result.Msg = "开始日期不能大于结束日期";
                    return result;
                }
                List<DataTable> tableList = new List<DataTable>();
                DataTable patientTable = this.YunZhiDal.GetPatientInfoByPTID(eid);
                tableList.Add(patientTable);
                List<YZ_Patients> patientList = new ModelHandler<YZ_Patients>().TableToList(patientTable);
                startTime = Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00");
                endTime = Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:59");
                if (patientList != null && patientList.Count > 0)
                {
                    string idNumber = patientList[0].PTIDNumber;
                    DataTable table = this.YunZhiDal.GetZhenLiaoBySearch(startTime, endTime, hid, idNumber);
                    tableList.Add(table);
                    result.Result = 1;
                    result.DataList = tableList;
                }
                else
                {
                    result.Msg = "客户不存在";
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "得到项目列表和客户信息错误：" + ex.Message;
            }
            return result;
        }


        public YunZhiResult DelHealthRecode(string hrid)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(hrid) == true)
                {
                    result.Msg = "参数不能为空";
                    return result;
                }
                int delResult = this.YunZhiDal.DelHealthRecode(hrid);
                if (delResult > 0)
                {
                    result.Result = 1;
                    result.Msg = "删除成功";
                }
                else
                {
                    result.Msg = "删除失败!";
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "得到项目列表和客户信息错误：" + ex.Message;
            }
            return result;
        }

        public YunZhiResult UpdateRecorInfo(string hrid, string data03, string data04, string data05, string data06, DataTable table, List<string> delID)
        {
            YunZhiResult result = new YunZhiResult();
            try
            {
                if (string.IsNullOrEmpty(hrid) == true)
                {
                    result.Msg = "参数不能为空";
                    return result;
                }
                int delResult = this.YunZhiDal.UpdateRecorInfo(hrid, data03, data04, data05, data06, table, delID);

                if (delResult > 0)
                {
                    result.Result = 1;
                    result.Msg = "操作成功";
                }
                else
                {
                    result.Msg = "操作失败!";
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result.Msg = "UpdateRecorInfo：" + ex.Message;
            }
            return result;
        }

        public YunZhiResult SetDrugInfo(DataTable table)
        {
            YunZhiResult result = new YunZhiResult();
            return result;
        }


        public LangTongResult SendLongTongData(string data03, string data04, string data05, string hosid, string hosname, string docid, string docName, string recordid)
        {
            LangTongResult res = new LangTongResult();
            try
            {
                System.Text.StringBuilder jsonStr = new System.Text.StringBuilder();
                string curPTSex = string.Empty;
                string curAge = string.Empty;
                string curPTIDNumber = string.Empty;
                //根据记录id查找用户性别和年龄，以及身份证号
                DataTable patientTable = this.YunZhiDal.GetPatientInfoByRecordID(recordid);
                if (patientTable != null && patientTable.Rows != null && patientTable.Rows.Count > 0)
                {
                    string ptSex = patientTable.Rows[0]["PTSex"].ToString();
                    if (ptSex == "0")
                    {
                        curPTSex = "1";
                    }
                    else
                    {
                        curPTSex = "2";
                    }
                    string ptBirthday = patientTable.Rows[0]["PTBrithday"].ToString();
                    DateTime curTime = Convert.ToDateTime(ptBirthday);
                    DateTime now = DateTime.Today;
                    int age = now.Year - curTime.Year;
                    if (curTime > now.AddYears(-age))
                    {
                        age--;
                    }
                    curAge = age.ToString();
                    curPTIDNumber = patientTable.Rows[0]["PTIDNumber"].ToString();
                }
                else
                {
                    Logger.Log.Error("LTInter:错误-10007,获取不到用户信息;");
                    res.Msg = "LTInter:错误-10007,获取不到用户信息";
                    return res;
                }
                jsonStr.Append("{");
                jsonStr.Append(string.Format("\"hospitalId\":\"{0}\",", hosid));
                jsonStr.Append(string.Format("\"hospitalName\":\"{0}\",", hosname));
                jsonStr.Append(string.Format("\"deptId\":\"{0}\",", "001"));
                jsonStr.Append(string.Format("\"doctorId\":\"{0}\",", docid));
                jsonStr.Append(string.Format("\"deptName\":\"部门\","));
                jsonStr.Append(string.Format("\"doctorName\":\"{0}\",", docName));
                jsonStr.Append(string.Format("\"recordId\":\"{0}\",", recordid));
                jsonStr.Append(string.Format("\"age\":\"{0}\",", curAge));
                jsonStr.Append(string.Format("\"sexType\" : \"{0}\",", curPTSex));
                jsonStr.Append(string.Format("\"type\" : \"1\","));
                jsonStr.Append(string.Format("\"symptomJson\" : \"{0}\",", data04));//主述Data04
                jsonStr.Append(string.Format("\"pastJson\" : \"{0}\",", data03));//病史Data03
                jsonStr.Append(string.Format("\"otherJson\" : \"\","));
                jsonStr.Append(string.Format("\"vitalsJson\" : \"\","));
                jsonStr.Append(string.Format("\"disJson\":\"{0}\",", data05));//诊断结果Data05
                jsonStr.Append("\"labsJson\" : {");//化验
                jsonStr.Append(string.Format("\"age\":\"{0}\",", curAge));
                jsonStr.Append(string.Format("\"sexType\":\"{0}\",", curPTSex));
                jsonStr.Append("\"assay\":[{");

                //根据身份证号获取化验结果
                DataTable projectTable = this.YunZhiDal.GetProjectByPatientIDNumber(curPTIDNumber, hosid);
                if (projectTable != null && projectTable.Rows != null && projectTable.Rows.Count > 0)
                {
                    string proName = projectTable.Rows[0]["ProName"].ToString();
                    string proResult = projectTable.Rows[0]["ProCheckResult"].ToString();
                    jsonStr.Append(string.Format("\"assayName\": \"{0}\",", proName));
                    jsonStr.Append(string.Format("\"assayCode\": \"10002023\","));
                    jsonStr.Append(string.Format("\"assayValue\": \"{0}\",", proResult));
                    jsonStr.Append(string.Format("\"max\": \"0\","));
                    jsonStr.Append(string.Format("\"min\": \"0\","));
                    jsonStr.Append(string.Format("\"units\":\"\","));
                    jsonStr.Append(string.Format("\"otherState\":\"\""));
                }
                jsonStr.Append("}]");
                jsonStr.Append("}");
                jsonStr.Append("}");
                string body = jsonStr.ToString();
                res = ComHelper.PostHttp(body);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("LTInter:错误-10009" + ex.ToString());
                res.Ret = 1;
                res.Msg = "LTInter:错误-10009" + ex.Message;
            }
            return res;
        }
    }
}
