using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace YUNZHI.DAL
{
    public class YUNZHIDAL
    {

        /// <summary>
        /// 获取医疗机构管理列表 带分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public DataTable YZ_GetHospitalList(string userId, string hName, string hCode, string hAreaProvinceName, string hAreaUrban, int pageSize, int pageIndex, out int totalcount)
        {
            totalcount = 0;
            try
            {
                DBHelper help = new DBHelper();
                SqlParameter[] pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@YZTitle", hName);
                pars[2] = new SqlParameter("@YZHCode", hCode);
                pars[3] = new SqlParameter("@YZAreaProvince", hAreaProvinceName);
                pars[4] = new SqlParameter("@YZAreaUrban", hAreaUrban);
                pars[5] = new SqlParameter("@PageSize", pageSize);
                pars[6] = new SqlParameter("@PageIndex", pageIndex);
                pars[7] = new SqlParameter("@totalCount", totalcount);
                pars[7].Direction = System.Data.ParameterDirection.Output;

                DataSet ds = help.GetDataByProcedure("YZ_GetHospitalList", pars);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (pars[7].Value != null)
                    {
                        totalcount = Convert.ToInt32(pars[7].Value);
                    }
                    else
                    {
                        totalcount = 0;
                    }
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取区域管理列表 带分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="distName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public DataTable YZ_GetDistrictList(string userId, string distName, string parentCode, int pageSize, int pageIndex, out int totalcount)
        {
            totalcount = 0;
            try
            {
                DBHelper help = new DBHelper();
                SqlParameter[] pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@YZTitle", distName);
                pars[2] = new SqlParameter("@ParentCode", parentCode);
                pars[3] = new SqlParameter("@PageSize", pageSize);
                pars[4] = new SqlParameter("@PageIndex", pageIndex);
                pars[5] = new SqlParameter("@totalCount", totalcount);
                pars[5].Direction = System.Data.ParameterDirection.Output;

                DataSet ds = help.GetDataByProcedure("YZ_GetDistrictList", pars);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (pars[5].Value != null)
                    {
                        totalcount = Convert.ToInt32(pars[5].Value);
                    }
                    else
                    {
                        totalcount = 0;
                    }
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取医生管理列表 带分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="hName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public DataTable YZ_GetEmployeeList(string userId, string userName, string hName, int pageSize, int pageIndex, out int totalcount)
        {
            totalcount = 0;
            try
            {
                DBHelper help = new DBHelper();
                SqlParameter[] pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@YZUserName", userName);
                pars[2] = new SqlParameter("@YZHName", hName);
                pars[3] = new SqlParameter("@PageSize", pageSize);
                pars[4] = new SqlParameter("@PageIndex", pageIndex);
                pars[5] = new SqlParameter("@totalCount", totalcount);
                pars[5].Direction = System.Data.ParameterDirection.Output;

                DataSet ds = help.GetDataByProcedure("YZ_GetEmployeeList", pars);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (pars[5].Value != null)
                    {
                        totalcount = Convert.ToInt32(pars[5].Value);
                    }
                    else
                    {
                        totalcount = 0;
                    }
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取设备管理列表 带分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sn"></param>
        /// <param name="hName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public DataTable YZ_GetDeviceList(string userId, string sn, string hName, string status, int pageSize, int pageIndex, out int totalcount)
        {
            totalcount = 0;
            try
            {
                DBHelper help = new DBHelper();
                SqlParameter[] pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@YZSN", sn);
                pars[2] = new SqlParameter("@YZHName", hName);
                pars[3] = new SqlParameter("@YZStatus", status);
                pars[4] = new SqlParameter("@PageSize", pageSize);
                pars[5] = new SqlParameter("@PageIndex", pageIndex);
                pars[6] = new SqlParameter("@totalCount", totalcount);
                pars[6].Direction = System.Data.ParameterDirection.Output;

                DataSet ds = help.GetDataByProcedure("YZ_GetDeviceList", pars);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (pars[6].Value != null)
                    {
                        totalcount = Convert.ToInt32(pars[6].Value);
                    }
                    else
                    {
                        totalcount = 0;
                    }
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 患者就诊表详情
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hName"></param>
        /// <param name="patName"></param>
        /// <param name="patIDNum"></param>
        /// <param name="patStarDate"></param>
        /// <param name="patEndDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public DataTable YZ_GetPatientsList(string userId, string hName, string patName, string patIDNum, string patStartDate, string patEndDate, int pageSize, int pageIndex, out int totalcount)
        {
            totalcount = 0;
            try
            {
                DBHelper help = new DBHelper();
                SqlParameter[] pars = new SqlParameter[9];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@YZHName", hName);
                pars[2] = new SqlParameter("@YZPatName", patName);
                pars[3] = new SqlParameter("@YZPatIDNumber", patIDNum);
                pars[4] = new SqlParameter("@YZPatStartDate", patStartDate);
                pars[5] = new SqlParameter("@YZPatEndDate", patEndDate);
                pars[6] = new SqlParameter("@PageSize", pageSize);
                pars[7] = new SqlParameter("@PageIndex", pageIndex);
                pars[8] = new SqlParameter("@totalCount", totalcount);
                pars[8].Direction = System.Data.ParameterDirection.Output;

                DataSet ds = help.GetDataByProcedure("YZ_GetPatientsList", pars);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (pars[8].Value != null)
                    {
                        totalcount = Convert.ToInt32(pars[8].Value);
                    }
                    else
                    {
                        totalcount = 0;
                    }
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据身份证号获取用户的就诊信息
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <returns></returns>
        public DataTable YZ_GetPatientsListByIDCard(string idCard)
        {
            try
            {
                DBHelper help = new DBHelper();
                SqlParameter[] pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@IDCard", idCard);

                DataSet ds = help.GetDataByProcedure("YZ_GetPatientsListByPTID", pars);

                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return null;
            }
        }


        public DataTable YZ_User_login(string account, string pwd)
        {
            try
            {
                DBHelper help = new DBHelper();
                SqlParameter[] pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@account", account);
                string sqlStr = "SELECT * FROM YZ_Employee WHERE EmpCode=@account;";
                DataTable table = help.QueryDataTable(sqlStr, pars);
                return table;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return null;
            }
        }
    }
}
