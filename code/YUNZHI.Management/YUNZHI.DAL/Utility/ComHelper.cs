using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using YUNZHI.DAL.Common;

namespace YUNZHI.DAL.Utility
{
    public class ComHelper
    {

        internal const string _ServerPath = "http://101.37.151.162:9090/icss-web/push/at/start";

        /// <summary>
        /// post方法（httpwebRequest) 
        /// </summary>
        /// <param name="methodPage"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static LangTongResult PostHttp(string body)
        {
            
            LangTongResult obj;
            HttpWebResponse httpWebResponse = null;
            HttpWebRequest httpWebRequest = null;
            StreamReader streamReader = null;
            try
            {
                string contentType = "application/x-www-form-urlencoded";
                httpWebRequest = (HttpWebRequest)WebRequest.Create(_ServerPath);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Timeout = 20000;
                string paraUrlCode = "datajson=" + System.Web.HttpUtility.UrlEncode(body);
                
                byte[] btBodys = System.Text.Encoding.UTF8.GetBytes(paraUrlCode);
                httpWebRequest.ContentLength = btBodys.Length;
                httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                string responseContent = streamReader.ReadToEnd();
                httpWebResponse.Close();
                streamReader.Close();
                httpWebRequest.Abort();
                if (string.IsNullOrEmpty(responseContent) == false)
                {
                    JavaScriptSerializer json = new JavaScriptSerializer();
                    obj = json.Deserialize<LangTongResult>(responseContent);
                }
                else
                {
                    obj = new LangTongResult();
                    obj.Msg = "接口错误-1101";
                    obj.Ret = 1;
                }
            }
            catch (Exception ex)
            {
                obj = new LangTongResult();
                obj.Msg = "接口错误-1102：" + ex.Message;
                obj.Ret = 1;
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
            return obj;
        }

        internal static T DESerializer<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
