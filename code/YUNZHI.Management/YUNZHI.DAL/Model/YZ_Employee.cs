using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_Employee:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Employee
	{
		public YZ_Employee()
		{}
		#region Model
		private string _empid;
		private string _empcode;
		private string _empname;
		private string _emppwd;
		private string _empemail;
		private string _emptelphone;
		private string _hid;
		private string _hospitalname;
		private string _emppinyin;
		private string _empidnumber;
		private bool _isenable= true;
		/// <summary>
		/// 
		/// </summary>
		public string EmpID
		{
			set{ _empid=value;}
			get{return _empid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpCode
		{
			set{ _empcode=value;}
			get{return _empcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpName
		{
			set{ _empname=value;}
			get{return _empname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpPwd
		{
			set{ _emppwd=value;}
			get{return _emppwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpEMail
		{
			set{ _empemail=value;}
			get{return _empemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpTelPhone
		{
			set{ _emptelphone=value;}
			get{return _emptelphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HID
		{
			set{ _hid=value;}
			get{return _hid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HospitalName
		{
			set{ _hospitalname=value;}
			get{return _hospitalname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpPinYin
		{
			set{ _emppinyin=value;}
			get{return _emppinyin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpIDNumber
		{
			set{ _empidnumber=value;}
			get{return _empidnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsEnable
		{
			set{ _isenable=value;}
			get{return _isenable;}
		}
		#endregion Model

	}
}

