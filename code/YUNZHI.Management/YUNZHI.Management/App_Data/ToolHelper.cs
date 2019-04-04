
using HY.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace HY.Web.Admin
{
    public class ToolHelper
    {
        /// <summary>
        /// 获取用户登录ID
        /// </summary>
        /// <returns></returns>
        public static string GetLoginAccount()
        {
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                EmployeeEntity empEnt = (EmployeeEntity)HttpContext.Current.Session["LoginUser"];
                return empEnt.AD_Account;
            }
            return "";
        }
        /// <summary>
        /// 获取用户登录名
        /// </summary>
        /// <returns></returns>
        public static string GetLoginName()
        {
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                EmployeeEntity empEnt = (EmployeeEntity)HttpContext.Current.Session["LoginUser"];
                return empEnt.Name;
            }
            return "";
        }

        /// <summary> 
        /// 对字符串进行sql格式化，并且符合like查询的格式。 
        /// </summary> 
        /// <param name="str">要转换的字符串</param> 
        /// <returns>格式化后的字符串</returns> 
        public static string ToLikeSql(string sqlstr)
        {
            if (sqlstr == null) return "";
            StringBuilder str = new StringBuilder(sqlstr);
            str.Replace("'", "''");
            str.Replace("[", "[[]");
            str.Replace("%", "[%]");
            str.Replace("_", "[_]");
            str.Replace("^", "[^]");
            return str.ToString();
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>   
        /// 获取Mac地址信息   
        /// </summary>   
        /// <param name="IP"></param>   
        /// <returns></returns>   
        public static string GetCustomerMac(string IP)
        {
            Int32 ldest = inet_addr(IP);
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string mac_dest = "";

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }

            return mac_dest;
        }

        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="MaxTitleLength"></param>
        /// <returns></returns>
        public static string subStringAndSuffix(string str, int MaxTitleLength)
        {
            //int MaxTitleLength = Int32.Parse(XMLHelper.LoadPopertyFromXML(HttpContext.Current, "MaxTitleLength"));

            Encoding _encoding = System.Text.Encoding.GetEncoding("GB2312");
            string strTmp = str;
            byte[] bytes = _encoding.GetBytes(str);
            if (bytes.Length > MaxTitleLength + 1)
            {
                strTmp = _encoding.GetString(bytes, 0, MaxTitleLength);
                if (strTmp.Substring(strTmp.Length - 1) == "?")
                {
                    strTmp = strTmp.Substring(0, strTmp.Length - 1);
                }
                strTmp += "...";
            }
            return strTmp;
        }
    }
}
