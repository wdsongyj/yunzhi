using System;
namespace YunZhi.Model
{
	/// <summary>
	/// YZ_Patients:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Patients
	{
		public YZ_Patients()
		{}
		#region Model
		private string _ptid;
		private string _ptname;
		private string _ptaddress;
		private string _pttelphone;
		private string _ptbrithday;
		private string _ptsex;
		private string _ptidnumber;
		private string _ptremark;
		private DateTime? _ptcreated= DateTime.Now;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
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
		public string PTName
		{
			set{ _ptname=value;}
			get{return _ptname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PTAddress
		{
			set{ _ptaddress=value;}
			get{return _ptaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PTTelPhone
		{
			set{ _pttelphone=value;}
			get{return _pttelphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PTBrithday
		{
			set{ _ptbrithday=value;}
			get{return _ptbrithday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PTSex
		{
			set{ _ptsex=value;}
			get{return _ptsex;}
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
		/// 
		/// </summary>
		public string PTRemark
		{
			set{ _ptremark=value;}
			get{return _ptremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PTCreated
		{
			set{ _ptcreated=value;}
			get{return _ptcreated;}
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

