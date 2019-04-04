using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using YUNZHI.DAL;

namespace YUNZHI.Management
{
    public  class UserHelper
    {
        /// <summary>
        /// 获取用户AD登录账号（不带域名）
        /// </summary>
        /// <returns>如果未登录，则返回空</returns>
        public static string GetLoginName()
        {
            string str2 = "";//not logined

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                str2 = HttpContext.Current.User.Identity.Name.ToString();//0#.f|ubisqlm|admin
                if (str2.Contains("@"))
                {
                    str2 = str2.Split(new char[] { '@' })[0];
                }
                else
                {
                    if (str2.Contains("|"))//基于声明式身份验证
                    {
                        string[] tmp = str2.Split(new char[] { '|' });
                        if (tmp.Length > 0)
                        {
                            str2 = tmp[tmp.Length - 1];
                        }
                    }
                    else
                    {
                        str2 = str2.Split(new char[] { '\\' })[1];
                    }
                }

                //Fixed NTLM bug
                if (str2.Contains("\\"))
                {
                    str2 = str2.Split(new char[] { '\\' })[1];
                }

                //if (str2.ToLower() == "administrator")
                //    str2 = "admin";
                //if (str2.ToLower() == "admin")
                //    str2 = "000001";
            }
            return str2;
        }

        public static string GetK2UserID()
        {
            //根据是否使用K2是否集成AD设置。K2引擎集成AD为k2:domain\，K2引擎不集成AD为k2sql:，UBI引擎为sql:，UBI引擎暂不支持AD集成。
            //string UserIDPrefix = ConfigurationManager.AppSettings["UserIDPrefix"].ToString();    
            //return UserIDPrefix + EmpLogin.AD_Account;

            //platform33版本使用K2UserID即可！！
            //K2UserId拼接规则有变化，需要获取登录人的K2UserId
            if (HttpContext.Current.Session["__BaseInfo"] != null)
            {
                return ((string[])HttpContext.Current.Session["__BaseInfo"])[19];
            }
            else
            {
                return null;
            }
            
        }
        /// <summary>
        /// 获取用户AD登录账号（不带域名）
        /// </summary>
        /// <returns>如果未登录，则返回空</returns>
        public static string GetRealLoginName()
        {
            string str2 = "";//not logined

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                str2 = HttpContext.Current.User.Identity.Name.ToString();//0#.f|ubisqlm|admin
                if (str2.Contains("@"))
                {
                    str2 = str2.Split(new char[] { '@' })[0];
                }
                else
                {
                    if (str2.Contains("|"))//基于声明式身份验证
                    {
                        string[] tmp = str2.Split(new char[] { '|' });
                        if (tmp.Length > 0)
                        {
                            str2 = tmp[tmp.Length - 1];
                        }
                    }
                    else
                    {
                        str2 = str2.Split(new char[] { '\\' })[1];
                    }
                }

                //Fixed NTLM bug
                if (str2.Contains("\\"))
                {
                    str2 = str2.Split(new char[] { '\\' })[1];
                }

                if (str2.ToLower() == "administrator")
                    str2 = "admin";
            }
            return str2;
        }


        /// <summary>
        /// 获取用户AD登录账号（不带域名）,admin 转换了000001
        /// </summary>
        /// <returns>如果未登录，则返回空</returns>
        public static string GetLoginName2()
        {
            string str2 = "";//not logined

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                str2 = HttpContext.Current.User.Identity.Name.ToString();//0#.f|ubisqlm|admin
                if (str2.Contains("@"))
                {
                    str2 = str2.Split(new char[] { '@' })[0];
                }
                else
                {
                    if (str2.Contains("|"))//基于声明式身份验证
                    {
                        string[] tmp = str2.Split(new char[] { '|' });
                        if (tmp.Length > 0)
                        {
                            str2 = tmp[tmp.Length - 1];
                        }
                    }
                    else
                    {
                        str2 = str2.Split(new char[] { '\\' })[1];
                    }
                }

                //Fixed NTLM bug
                if (str2.Contains("\\"))
                {
                    str2 = str2.Split(new char[] { '\\' })[1];
                }

                if (str2.ToLower() == "administrator")
                    str2 = "admin";
            }

            if (str2.ToLower() == "admin")
                str2 = "000001";

            return str2;
        }

        /// <summary>
        /// 不特殊处理admin（首页快速搜索client方式用）
        /// </summary>
        /// <returns></returns>
        public static string GetLoginName3()
        {
            string str2 = "";//not logined

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                str2 = HttpContext.Current.User.Identity.Name.ToString();//0#.f|ubisqlm|admin
                if (str2.Contains("@"))
                {
                    str2 = str2.Split(new char[] { '@' })[0];
                }
                else
                {
                    if (str2.Contains("|"))//基于声明式身份验证
                    {
                        string[] tmp = str2.Split(new char[] { '|' });
                        if (tmp.Length > 0)
                        {
                            str2 = tmp[tmp.Length - 1];
                        }
                    }
                    else
                    {
                        str2 = str2.Split(new char[] { '\\' })[1];
                    }
                }

                //Fixed NTLM bug
                if (str2.Contains("\\"))
                {
                    str2 = str2.Split(new char[] { '\\' })[1];
                }

                if (str2.ToLower() == "administrator")
                    str2 = "admin";
            }

            //if (str2.ToLower() == "admin")
            //    str2 = "000001";

            return str2;
        }

        /// <summary>
        /// 在平台Session中获取用户登录名和K2UserID
        /// </summary>
        /// <param name="k2id"></param>
        /// <returns></returns>
        public static string GetCurrentUserID(ref string k2id)
        {
            string userLoginName = "";
            HttpContext context = HttpContext.Current;
            userLoginName = UserHelper.GetLoginName();
            if (userLoginName == "administrator")
            {
                userLoginName = "admin";
            }

            k2id = "sql:" + userLoginName;

            return userLoginName;
        }


        /// <summary>
        /// 阶段字符串，一个汉字算2个长度
        /// </summary>
        /// <param name="stringToSub">要处理的字符串</param>
        /// <param name="length">保留长度</param>
        /// <param name="endStr">后缀，默认为...，可以为空</param>
        /// <returns></returns>
        public static string GetFirstString(string stringToSub, int length,string endStr)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    nLength += 2;
                }
                else
                {
                    nLength = nLength + 1;
                }

                if (nLength <= length)
                {
                    sb.Append(stringChar[i]);
                }
                else
                {
                    break;
                }
            }
            if (sb.ToString() != stringToSub)
            {
                sb.Append(endStr);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 用来给ASHX传递的参数进行编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Escape(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                sb.Append((Char.IsLetterOrDigit(c)
                    || c == '-' || c == '_' || c == '\\'
                    || c == '/' || c == '.') ? c.ToString() : Uri.HexEscape(c));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 用来解码Escape编码后的内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnEscape(string str)
        {
            StringBuilder sb = new StringBuilder();
            int len = str.Length;
            int i = 0;
            while (i != len)
            {
                if (Uri.IsHexEncoding(str, i))
                    sb.Append(Uri.HexUnescape(str, ref i));
                else
                    sb.Append(str[i++]);
            }
            return sb.ToString();
        }
        public static string RequestJsonP(string strjson)
        {
            string Json = string.Empty;
            try
            {
                if (HttpContext.Current.Request.Params["callback"] != null)
                {
                    HttpContext.Current.Response.Charset = "UTF-8";
                    HttpContext.Current.Response.ContentType = "text/javascript";
                    //IIS里已经配置http
                    // context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    Json = HttpContext.Current.Request["callback"].ToString() + "({ \"success\": \"true\"," + "\"data\":" + strjson + "})";
                }
                else
                {
                    HttpContext.Current.Response.Charset = "UTF-8";
                    HttpContext.Current.Response.ContentType = "text/plain";
                    Json = strjson;
                }
                return Json;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return strjson;
            }
        }
    }
}
