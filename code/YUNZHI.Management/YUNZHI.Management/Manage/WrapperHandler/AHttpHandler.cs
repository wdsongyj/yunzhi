using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.ComponentModel;
using System.Data;
using HY.Common.Utility.Utils;

namespace HY.Web.Admin.WrapperHandler
{
     [System.Security.SecuritySafeCritical]
    /// <summary>
    /// 事件处理基类
    /// </summary>
	public abstract class AHttpHandler : IHttpHandler, IRequiresSessionState
	{
		/// <summary>
		/// 请求方法
		/// </summary>
		protected string method;
		/// <summary>
		/// HTTP上下文
		/// </summary>
		protected HttpContext context;
		/// <summary>
		/// 自定义请求处理方法
		/// </summary>
		/// <param name="method"></param>
		public abstract void ProcessRequest(string method);
		/// <summary>
		/// IHttpHandler默认接口
		/// </summary>
		/// <param name="context"></param>
		public void ProcessRequest(HttpContext context)
		{
            //context.Response.ContentType = "application/json";

			this.context = context;

            //string Ddh = (context.Request["Ddh"] == null ? "" :  System.Web.HttpContext.Current.Server.UrlDecode(UserHelper.UnEscape(context.Request["Ddh"])));

              string method = "";

              if (context.Request.Params["method"] != null)
              {
                  method = context.Server.HtmlDecode(context.Request.Params["method"].ToString());
              }

			//method = context.Request["method"];
			ProcessRequest(method);
		}
		/// <summary>
		/// 异常处理
		/// </summary>
		/// <param name="message">错误信息</param>
		protected void ResponseException(string message)
		{
			ResponseException(message, 0, null);
		}
		/// <summary>
		/// 异常处理
		/// </summary>
		/// <param name="message">错误信息</param>
		protected void ResponseException(string message, string json)
		{
			ResponseException(message, "", 0, null);
		}
		/// <summary>
		/// 异常处理
		/// </summary>
		/// <param name="message">错误信息</param>
		/// <param name="errorCode">错误号</param>
		protected void ResponseException(string message, int errorCode)
		{
			ResponseException(message, errorCode, null);
		}
		/// <summary>
		/// 异常处理
		/// </summary>
		/// <param name="message">错误信息</param>
		/// <param name="errorCode">错误号</param>
		protected void ResponseException(string message, int errorCode, Exception e)
        {

            if (e != null && HttpContext.Current.Session["LoginUser"] != null)
            {
                Logger.Log.Error(ToolHelper.GetLoginAccount() + "-----Exception:", e);
                     
            }
			ResponseException(message, "", errorCode, null);
		}
		/// <summary>
		/// 异常处理
		/// </summary>
		/// <param name="message">错误信息</param>
		/// <param name="errorCode">错误号</param>
		/// <param name="e">错误源对象</param>
		protected void ResponseException(string message,string json, int errorCode, Exception e)
		{
			message = message.Replace("'", @"\'");
			context.Response.StatusCode = 500;
			context.Response.StatusDescription = message;

			if (json != null && json != "")
				json = "," + json;
			StringBuilder errorStrBuilder = new StringBuilder();
			errorStrBuilder.Append("{success:false,error:'" + message + "'" + json);
			if (errorCode != 0)
				errorStrBuilder.Append(" ,errorCode:" + errorCode.ToString());
            if (e != null)
            {
                Logger.Log.Error(ToolHelper.GetLoginAccount() + "-----Exception:", e);

                errorStrBuilder.Append(" ,errorDescription:'" + e.Message.Replace("'", @"\'") + "'");
            }
			errorStrBuilder.Append("}");
			context.Response.Write(errorStrBuilder.ToString());
		}

		/// <summary>
		/// 返回成功信息
		/// </summary>
		protected void ResponseSuccess()
		{
			string json = "{success:'true'}";
			context.Response.Write(json);
		}
		/// <summary>
		/// 返回成功信息
		/// </summary>
		/// <param name="json"></param>
		protected void ResponseSuccess(string json)
		{
			json = "{success:'true'," + json + "}";
			context.Response.Write(json);
		}

		/// <summary>
        /// 返回Json成功信息
        /// </summary>
        protected void ResponseSuccess(object json)
        {
			if (json != null)
			{
				PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(json);
				string[] result = new string[descriptors.Count];
				int i = 0;
				foreach (PropertyDescriptor descriptor in descriptors)
				{
					object obj2 = descriptor.GetValue(json);
					result[i++] = String.Format("{0}:'{1}'", descriptor.Name, obj2);
				}
				ResponseSuccess(result);
			}
        }

		/// <summary>
		/// 返回Json成功信息
		/// </summary>
		protected void ResponseSuccess(params string[] json)
		{
			ResponseSuccess(String.Join(",", json));
		}

		/// <summary>
		/// 执行完毕，发送缓冲中的所有数据
		/// </summary>
		protected void ResponseEnd()
		{
			context.Response.End();
		}
		/// <summary>
		/// 
		/// </summary>
		protected bool _IsReusable = false;
		/// <summary>
		/// 
		/// </summary>
		public bool IsReusable
		{
			get { return _IsReusable; }
		}
	}
}
