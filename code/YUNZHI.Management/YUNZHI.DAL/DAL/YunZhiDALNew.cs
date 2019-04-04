using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using YUNZHI.DAL.Common;

namespace YUNZHI.DAL.DAL
{
    public class YunZhiDALNew
    {
        private DBHelper helper;

        public DBHelper _Helper
        {
            get
            {
                if (helper == null)
                {
                    helper = new DBHelper();
                }
                return helper;
            }
        }


        /// <summary>用户登录</summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        public DataTable UserLogin(string account, string pwd)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@account", account);
            pars[1] = new SqlParameter("@pwd", pwd);
            string sqlStr = "SELECT * FROM YZ_Employee WHERE EmpCode=@account AND EmpPwd=@pwd;";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>得到项目列表（第三方调用）</summary>
        /// <param name="sn"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public DataTable GetProjectList(string sn, DateTime startTime, DateTime endTime, string token)
        {
            SqlParameter[] pars = new SqlParameter[3];
            pars[0] = new SqlParameter("@sn", sn);
            pars[1] = new SqlParameter("@startTime", startTime);
            pars[2] = new SqlParameter("@endTime", endTime);
            //先不验证Token
            string sqlStr = "SELECT * FROM YZ_Project WHERE DSN=@sn AND ProCheckTime>=@startTime and ProCheckTime<=@endTime order by ProCheckTime desc;";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>得到指定患者的项目列表</summary>
        /// <param name="idNumber"></param>
        /// <param name="hid"></param>
        /// <returns></returns>
        public DataTable GetProjectListByPatientID(string idNumber, string hid)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@IDNumber", idNumber);
            pars[1] = new SqlParameter("@HID", hid);
            string sqlStr = "select * from YZ_Project p inner join YZ_Device d on p.DSN=d.DSN where d.DStatus=1 and d.HID=@HID and p.PTIDNumber=@IDNumber order by ProCheckTime desc;";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>是否存在用户</summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public bool IsExistsPatient(string idNumber)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@IDNumber", idNumber);
            string sqlStr = "select COUNT(*) from YZ_Patients where PTIDNumber=@IDNumber";
            return _Helper.Exists(sqlStr, pars);
        }

        /// <summary>获取患者信息通过身份证号码</summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public DataTable GetPatientInfoByIDNumber(string idNumber)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@IDNumber", idNumber);
            string sqlStr = "select * from YZ_Patients where PTIDNumber=@IDNumber";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>添加就诊记录</summary>
        /// <param name="ptid"></param>
        /// <param name="ptname"></param>
        /// <param name="hid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public int AddHealthRecode(string ptid, string ptname, string hid, string eid)
        {
            SqlParameter[] pars = new SqlParameter[7];
            pars[0] = new SqlParameter("@HRID", Guid.NewGuid().ToString("N"));
            pars[1] = new SqlParameter("@PTID", ptid);
            pars[2] = new SqlParameter("@PTName", ptname);
            pars[3] = new SqlParameter("@HID", hid);
            pars[4] = new SqlParameter("@EID", eid);
            pars[5] = new SqlParameter("@HRCreated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            pars[6] = new SqlParameter("@IsEnable", true);
            string sqlStr = @"insert into YZ_Health_Record (HRID,PTID,PTName,HID,EID,HRCreated,IsEnable) 
                                values(@HRID,@PTID,@PTName,@HID,@EID,@HRCreated,@IsEnable)";
            return _Helper.ExecuteSql(sqlStr, pars);
        }


        /// <summary>添加就诊记录</summary>
        ///<param name="hrid"></param>
        /// <returns></returns>
        public int DelHealthRecode(string hrid)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@HRID", hrid);
            string sqlStr = @"delete from YZ_Health_Record where HRID=@HRID ";
            return _Helper.ExecuteSql(sqlStr, pars);
        }
        /// <summary>获取就诊记录通过医生Id</summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public DataTable GetCurrentHealthListByDocId(string docId)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@EID", docId);
            pars[1] = new SqlParameter("@CurTime", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            string sqlStr = "select * from YZ_Health_Record where EID=@EID and HRCreated>=@CurTime order by HRCreated desc";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>获取患者信息通过患者ID</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetPatientInfoByPTID(string id)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@PTID", id);
            string sqlStr = "select * from YZ_Patients where PTID=@PTID";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>得到诊疗信息</summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="pname"></param>
        /// <returns></returns>
        public DataTable QueryZhenLiaoList(string startTime, string endTime, string hid, string pname)
        {
            SqlParameter[] pars = new SqlParameter[4];
            pars[0] = new SqlParameter("@StartTime", startTime);
            pars[1] = new SqlParameter("@EndTime", endTime);
            pars[2] = new SqlParameter("@HID", hid);
            pars[3] = new SqlParameter("@PName", "%" + pname + "%");
            string sqlStr = @"select r.HRID,r.PTID,r.PTName,r.HRCreated,r.HID,e.HospitalName,e.EmpName from YZ_Health_Record r inner join YZ_Employee e on r.EID=e.EmpID
                                where r.HRCreated>=@StartTime and r.HRCreated<=@EndTime and r.HID=@HID ";
            if (string.IsNullOrEmpty(pname) == false)
            {
                sqlStr = @"select r.HRID,r.PTID,r.PTName,r.HRCreated,r.HID,e.HospitalName,e.EmpName from YZ_Health_Record r inner join YZ_Employee e on r.EID=e.EmpID
                                where r.HID=@HID and r.PTName like @PName ";
                //sqlStr = sqlStr + " or r.PTName like @PName";
            }
            sqlStr = sqlStr + " order by r.HRCreated desc";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>
        /// 根据医院ID和开始结束时间来查找病人
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="pname"></param>
        /// <returns></returns>
        public DataTable GetProjectPatientDistinct(string startTime, string endTime, string hid, string pname)
        {
            SqlParameter[] pars = new SqlParameter[4];
            pars[0] = new SqlParameter("@StartTime", startTime);
            pars[1] = new SqlParameter("@EndTime", endTime);
            pars[2] = new SqlParameter("@HID", hid);
            pars[3] = new SqlParameter("@PName", "%" + pname + "%");
            string sqlStr = @"SELECT distinct PTName,PTID  from YZ_Health_Record 
                                where HID=@HID and HRCreated>=@StartTime and HRCreated<=@EndTime ";
            if (string.IsNullOrEmpty(pname) == false)
            {
                sqlStr = @"SELECT distinct PTName,PTID  from YZ_Health_Record 
                                where HID=@HID and PTName like @PName ";
                //sqlStr = sqlStr + " or PTName like @PName ";
            }
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>
        /// 通过选择条件查询诊疗信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hid"></param>
        /// <param name="idnumber"></param>
        /// <returns></returns>
        public DataTable GetZhenLiaoBySearch(string startTime, string endTime, string hid, string idnumber)
        {
            SqlParameter[] pars = new SqlParameter[4];
            pars[0] = new SqlParameter("@StartTime", startTime);
            pars[1] = new SqlParameter("@EndTime", endTime);
            pars[2] = new SqlParameter("@HID", hid);
            pars[3] = new SqlParameter("@IDNumber", idnumber);
            string sqlStr = @"select * from YZ_Project 
                              where DSN in (select DSN from YZ_Device where HID=@HID) and ProCheckTime>=@StartTime and ProCheckTime<=@EndTime and PTIDNumber=@IDNumber order by ProCheckTime desc";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /*-------------------------------------报表--------------------------------*/
        /// <summary>
        /// 查找时间段内设备使用情况占比
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeviceUsageList(string startTime, string endTime)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@StartTime", startTime);
            pars[1] = new SqlParameter("@EndTime", endTime);

            string sqlStr = @"select ROW_NUMBER() OVER (order by ProCheckTime desc) AS RowNumber,ProID,ProName,DID,DSN,ProCheckTime into #AllTable  
                            from [yunzhi].[dbo].YZ_Project
                            where IsEnable=1 and (ProCheckTime>=@StartTime and ProCheckTime<=@EndTime)
                            order by ProCheckTime desc

                            declare @totalCount int
                            select @totalCount=COUNT(*) from #AllTable

                            select * from(
                                select y_d.DSN,COUNT(y_p.ProID) as DeviceCount,
                                CAST(CONVERT(DECIMAL(18,2),isnull(COUNT(y_p.ProID),0)/CAST(ISNULL(NULLIF(@totalCount,0),1) AS FLOAT)*100)AS VARCHAR(10)) AS UsedRate
                                from [yunzhi].[dbo].[YZ_Device] y_d
                                left join #AllTable y_p on y_d.DSN=y_p.DSN
                                where y_d.IsEnable=1 and y_d.DStatus=1
                                group by y_d.DSN
                            ) tt order by DeviceCount";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>
        /// 获取所有医院的就诊情况
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetHealthByHospital(string startTime, string endTime)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@StartTime", startTime);
            pars[1] = new SqlParameter("@EndTime", endTime);
            string sqlStr = @"select ROW_NUMBER() OVER (order by HRCreated desc) AS RowNumber,* into #AllTable  
                            from [yunzhi].[dbo].[YZ_Health_Record]
                            where IsEnable=1 and (HRCreated>=@StartTime and HRCreated<=@EndTime)
                            order by HRCreated desc

                            SELECT y_h.[HID],y_h.[HName],count(y_h_r.HRID) PatientsCount
                            FROM [yunzhi].[dbo].[YZ_Hospital] y_h
                            left join #AllTable y_h_r on y_h.HID=y_h_r.HID
                            where y_h.IsEnable=1
                            group by y_h.HID,y_h.[HName]";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /*-------------------------------------区域--------------------------------*/
        /// <summary>
        /// 级联获取区域名称
        /// </summary>
        /// <param name="parentCode">父级Code</param>
        /// <param name="title">名称</param>
        /// <returns></returns>
        public DataTable GetAreaLevel(int parentCode)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@ParentCode", parentCode);

            string sqlStr = @"select * from [yunzhi].[dbo].[YZ_District] where IsEnable=1 and  Data02=@ParentCode order by Data01";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        public int UpdateRecorInfo(string hrid, string data03, string data04, string data05, string data06)
        {
            SqlParameter[] pars = new SqlParameter[5];
            pars[0] = new SqlParameter("@HRID", hrid);
            pars[1] = new SqlParameter("@Data03", data03);
            pars[2] = new SqlParameter("@Data04", data04);
            pars[3] = new SqlParameter("@Data05", data05);
            pars[4] = new SqlParameter("@Data06", data06);
            string sqlStr = @"update YZ_Health_Record set Data03=@Data03,Data04=@Data04,Data05=@Data05,Data06=@Data06 where HRID=@HRID ";
            return _Helper.ExecuteSql(sqlStr, pars);
        }

        public DataTable GetRecordInfo(string hrid)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@HRID", hrid);
            string sqlStr = @"select Data03,Data04,Data05,Data06 from YZ_Health_Record where HRID=@HRID ";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        public DataTable GetDrugList(string hrid)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@HRID", hrid);
            string sqlStr = @"select DrugID,HRID,DrugName,DrugYF,DrugYL from YZ_Health_Drug where HRID=@HRID order by xh ";
            return _Helper.QueryDataTable(sqlStr, pars);
        }


        public int UpdateRecorInfo(string hrid, string data03, string data04, string data05, string data06, DataTable table,List<string> delID)
        {
            int oldCount = GetCountBYHRID(hrid);
            List<CommandInfo> commandList=new List<CommandInfo>();
            SqlParameter[] pars = new SqlParameter[5];
            pars[0] = new SqlParameter("@HRID", hrid);
            pars[1] = new SqlParameter("@Data03", data03);
            pars[2] = new SqlParameter("@Data04", data04);
            pars[3] = new SqlParameter("@Data05", data05);
            pars[4] = new SqlParameter("@Data06", data06);
            string sqlStr = @"update YZ_Health_Record set Data03=@Data03,Data04=@Data04,Data05=@Data05,Data06=@Data06 where HRID=@HRID ";
            commandList.Add(new CommandInfo() { CommandText = sqlStr, Parameters = pars });
            int addCount = 0;
            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRowState state = table.Rows[i].RowState;
                    if (state == DataRowState.Unchanged || state == DataRowState.Detached || state==DataRowState.Deleted)
                    {
                        continue;
                    }
                    string tranSql = string.Empty;
                    if (state == DataRowState.Added)
                    {
                        addCount++;
                        SqlParameter[] curpars = new SqlParameter[6];
                        curpars[0] = new SqlParameter("@DrugID", Guid.NewGuid().ToString("N"));
                        curpars[1] = new SqlParameter("@HRID", hrid);
                        curpars[2] = new SqlParameter("@DrugName", table.Rows[i]["DrugName"].ToString());
                        curpars[3] = new SqlParameter("@DrugYF", table.Rows[i]["DrugYF"].ToString());
                        curpars[4] = new SqlParameter("@DrugYL", table.Rows[i]["DrugYL"].ToString());
                        curpars[5] = new SqlParameter("@xh", addCount+oldCount);
                        tranSql = @"insert into YZ_Health_Drug (DrugID,HRID,DrugName,DrugYF,DrugYL,xh) 
                                            values(@DrugID,@HRID,@DrugName,@DrugYF,@DrugYL,@xh)";
                        commandList.Add(new CommandInfo() { CommandText = tranSql, Parameters = curpars });
                    }                    
                    else if (state == DataRowState.Modified)
                    {
                        SqlParameter[] curpars = new SqlParameter[5];
                        curpars[0] = new SqlParameter("@DrugID", table.Rows[i]["DrugID"].ToString());
                        curpars[1] = new SqlParameter("@HRID", hrid);
                        curpars[2] = new SqlParameter("@DrugName", table.Rows[i]["DrugName"].ToString());
                        curpars[3] = new SqlParameter("@DrugYF", table.Rows[i]["DrugYF"].ToString());
                        curpars[4] = new SqlParameter("@DrugYL", table.Rows[i]["DrugYL"].ToString());                        
                        tranSql = @"update YZ_Health_Drug set DrugName=@DrugName,DrugYF=@DrugYF,DrugYL=@DrugYL where
                                    DrugID=@DrugID and HRID=@HRID";
                        commandList.Add(new CommandInfo() { CommandText = tranSql, Parameters = curpars });
                    }
                }
            }
            if (delID != null && delID.Count > 0)
            {
                foreach (string curID in delID)
                {
                    SqlParameter[] curpars = new SqlParameter[1];
                    curpars[0] = new SqlParameter("@DrugID", curID);
                    string tranSql = @"delete from YZ_Health_Drug where DrugID=@DrugID ";
                    commandList.Add(new CommandInfo() { CommandText = tranSql, Parameters = curpars });                 
                }
            }
            return _Helper.ExecuteSqlTran(commandList);
        }

        public int GetCountBYHRID(string hrid)
        {
            SqlParameter[] curpars = new SqlParameter[1];
            curpars[0] = new SqlParameter("@HRID", hrid);
            string sqlStr = @"select case when  max(xh) is null then 0 else max(xh) end from  YZ_Health_Drug where HRID=@HRID";
            return Convert.ToInt32(_Helper.GetSingle(sqlStr, curpars).ToString());
        }


        /*-----------------------------把朗通接口数据存到数据库中--------------------------------------*/
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddLangTong(string projectID,string recordID,string ptID,string ptName,string ptIDNumber,string data)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YZ_LangTong(");
            strSql.Append("LTID,LT_ProjectID,LT_RecordID,LT_PTID,LT_PTName,LT_PTIDNumber,LT_Data,LT_CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@LTID,@LT_ProjectID,@LT_RecordID,@LT_PTID,@LT_PTName,@LT_PTIDNumber,@LT_Data,@LT_CreateTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@LTID", SqlDbType.NVarChar,50),
					new SqlParameter("@LT_ProjectID", SqlDbType.NVarChar,50),
					new SqlParameter("@LT_RecordID", SqlDbType.NVarChar,50),
					new SqlParameter("@LT_PTID", SqlDbType.NVarChar,50),
					new SqlParameter("@LT_PTName", SqlDbType.NVarChar,256),
					new SqlParameter("@LT_PTIDNumber", SqlDbType.NVarChar,256),
					new SqlParameter("@LT_Data", SqlDbType.NVarChar,-1),
					new SqlParameter("@LT_CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = Guid.NewGuid().ToString("N");
            parameters[1].Value = projectID;
            parameters[2].Value = recordID;
            parameters[3].Value = ptID;
            parameters[4].Value = ptName;
            parameters[5].Value = ptIDNumber;
            parameters[6].Value = data;
            parameters[7].Value = DateTime.Now;
            int rows = _Helper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>获取医院信息通过设备号</summary>
        /// <param name="dsn"></param>
        /// <returns></returns>
        public DataTable GetHospitalInfoByDeviceDSN(string dsn)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@DSN", dsn);
            string sqlStr = @"select h.HID,h.HCode,h.HName from YZ_Device d inner join YZ_Hospital h on d.HID=h.HID where d.DStatus=1 and h.IsEnable=1 and  d.DSN=@DSN";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>获取当天最新的就诊信息</summary>
        /// <param name="hid"></param>
        /// <param name="ptid"></param>
        /// <returns></returns>
        public DataTable GetLastRecordInfoByHosIDAndPTID(string hid, string ptid)
        {
            SqlParameter[] pars = new SqlParameter[3];
            pars[0] = new SqlParameter("@HID", hid);
            pars[1] = new SqlParameter("@PTID", ptid);
            pars[2] = new SqlParameter("@HCreate", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            string sqlStr = @"select top 1 r.HRID,r.HRCreated,r.Data03,r.Data04,r.Data05,r.Data06,e.EmpName,e.EmpID from YZ_Health_Record r inner join YZ_Employee e on r.EID=e.EmpID where r.HID=@HID and r.PTID=@PTID and r.HRCreated>@HCreate  order by r.HRCreated desc;";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        public DataTable GetLangTongData(string recoreid, string ptid)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@RecordID", recoreid);
            pars[1] = new SqlParameter("@PTID", ptid);
            string sqlStr = @"select LTID,LT_ProjectID,LT_Data from YZ_LangTong where LT_RecordID=@RecordID and LT_PTID=@PTID;";
            return _Helper.QueryDataTable(sqlStr, pars);
        }

        /// <summary>根据记录ID获取患者信息</summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public DataTable GetPatientInfoByRecordID(string recordID)
        {
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@RecordID", recordID);
            string sqlStr = @"select p.PTBrithday,p.PTSex,p.PTIDNumber from YZ_Health_Record r inner join YZ_Patients p on r.PTID=p.PTID
                              where r.HRID=@RecordID";
            return _Helper.QueryDataTable(sqlStr, pars);
        }


        /// <summary>得到指定患者的项目列表</summary>
        /// <param name="idNumber"></param>
        /// <param name="hid"></param>
        /// <returns></returns>
        public DataTable GetProjectByPatientIDNumber(string idNumber, string hid)
        {
            SqlParameter[] pars = new SqlParameter[3];
            pars[0] = new SqlParameter("@IDNumber", idNumber);
            pars[1] = new SqlParameter("@HID", hid);
            pars[2] = new SqlParameter("@CreateTime", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            string sqlStr = "select top 1 * from YZ_Project p inner join YZ_Device d on p.DSN=d.DSN where d.DStatus=1 and d.HID=@HID and p.PTIDNumber=@IDNumber and p.ProCheckTime=@CreateTime order by ProCheckTime desc;";
            return _Helper.QueryDataTable(sqlStr, pars);
        }
    }
}
