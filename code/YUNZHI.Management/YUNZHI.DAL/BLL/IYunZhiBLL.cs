using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YUNZHI.DAL.Common;
using YUNZHI.DAL.Model;

namespace YUNZHI.DAL.BLL
{
    public interface IYunZhiBLL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        YunZhiResult UserLogin(string account, string pwd);


        YunZhiResult GetPatientsList(string idCards);

        /// <summary>
        /// 得到项目列表
        /// </summary>
        /// <param name="sn">设备号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        YunZhiResult GetProjectList(string sn, DateTime startTime, DateTime endTime, string token);

        /// <summary>
        /// 得到患者就诊记录通过医生ID和时间
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        YunZhiResult GetCurrentHealthListByDocId(string docId);

        YunZhiResult GetPatientInfoByIDNumber(string idNumber);

        /// <summary>
        /// 判断是否存在客户
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        bool IsExistsPatient(string idNumber);

        /// <summary>
        /// 添加客户诊疗信息
        /// </summary>
        /// <param name="ptid"></param>
        /// <param name="ptname"></param>
        /// <param name="hid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        int AddHealthRecode(string ptid, string ptname, string hid, string eid);

        /// <summary>
        /// 得到客户信息通过客户ID
        /// </summary>
        /// <param name="ptid"></param>
        /// <returns></returns>
        YunZhiResult GetPatientInfoByPTID(string ptid);

        /// <summary>
        /// 得到用户基本信息和在在医院的项目信息
        /// </summary>
        /// <param name="ptid"></param>
        /// <param name="hid"></param>        
        /// <param name="hrid"></param>
        /// <returns></returns>
        YunZhiResult GetZhenLiaoListByPTID(string ptid, string hid,string hrid);

        /// <summary>
        /// 查询诊疗记录
        /// </summary>
        /// <param name="dateTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="hid">医院id</param>
        /// <param name="pname">患者名称，模糊查询</param>
        /// <returns></returns>
        YunZhiResult QueryZhenLiaoList(string startTime, string endTime, string hid, string pname);

        /// <summary>
        /// 查询快检信息（返回客户信息）
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="pname"></param>
        /// <returns></returns>
        YunZhiResult QueryProjectList(string startTime, string endTime, string hid, string pname);

        /// <summary>得到项目列表和客户信息</summary>
        /// <param name="eid"></param>
        /// <param name="hid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        YunZhiResult GetProjectListAndPatientInfo(string eid, string hid, string startTime, string endTime);

        /// <summary>删除诊疗记录</summary>
        /// <param name="hrid"></param>
        /// <returns></returns>
        YunZhiResult DelHealthRecode(string hrid);
       
        /// <summary>修改诊疗主诉等信息</summary>
        /// <param name="hrid"></param>
        /// <param name="data03"></param>
        /// <param name="data04"></param>
        /// <param name="data05"></param>
        /// <param name="data06"></param>
        /// <returns></returns>
        YunZhiResult UpdateRecorInfo(string hrid, string data03, string data04, string data05, string data06, DataTable table,List<string> delID);

        /// <summary>发送数据到朗通获取结果信息</summary>
        /// <param name="hosID"></param>
        /// <param name="hosName"></param>
        /// <param name="project"></param>
        LangTongResult SendLongTongData(string data03, string data04, string data05, string hosid, string hosname, string docid, string docName, string recordid);
    
    }
}
