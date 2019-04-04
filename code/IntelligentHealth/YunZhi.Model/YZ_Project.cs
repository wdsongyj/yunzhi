using System;
namespace YunZhi.Model
{
	/// <summary>
	/// YZ_Project:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Project
	{
		public YZ_Project()
		{}
		#region Model
		private string _proid;
		private string _proname;
		private string _did;
		private string _dsn;
		private DateTime? _prochecktime= DateTime.Now;
		private string _procheckresult;
		private string _procheckremark;
		private string _ptid;
		private string _ptidnumber;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
		/// <summary>
		/// 
		/// </summary>
		public string ProID
		{
			set{ _proid=value;}
			get{return _proid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProName
		{
			set{ _proname=value;}
			get{return _proname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DID
		{
			set{ _did=value;}
			get{return _did;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DSN
		{
			set{ _dsn=value;}
			get{return _dsn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ProCheckTime
		{
			set{ _prochecktime=value;}
			get{return _prochecktime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProCheckResult
		{
			set{ _procheckresult=value;}
			get{return _procheckresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProCheckRemark
		{
			set{ _procheckremark=value;}
			get{return _procheckremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PTID
		{
			set{ _ptid=value;}
			get{return _ptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PTIDNumber
		{
			set{ _ptidnumber=value;}
			get{return _ptidnumber;}
		}
		/// <summary>
		/// 0：否，1：是
		/// </summary>
		public bool IsEnable
		{
			set{ _isenable=value;}
			get{return _isenable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Data01
		{
			set{ _data01=value;}
			get{return _data01;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Data02
		{
			set{ _data02=value;}
			get{return _data02;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Data03
		{
			set{ _data03=value;}
			get{return _data03;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Data04
		{
			set{ _data04=value;}
			get{return _data04;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Data05
		{
			set{ _data05=value;}
			get{return _data05;}
		}
		#endregion Model

	}
}

