using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_Device:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Device
	{
		public YZ_Device()
		{}
		#region Model
		private string _did;
		private string _dsn;
		private string _dstatus;
		private DateTime? _dcreated= DateTime.Now;
		private DateTime? _dactivatetime= DateTime.Now;
		private string _hid;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
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
		public string DStatus
		{
			set{ _dstatus=value;}
			get{return _dstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DCreated
		{
			set{ _dcreated=value;}
			get{return _dcreated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DActivateTime
		{
			set{ _dactivatetime=value;}
			get{return _dactivatetime;}
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

