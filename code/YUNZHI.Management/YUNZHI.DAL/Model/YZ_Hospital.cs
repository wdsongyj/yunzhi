using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_Hospital:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Hospital
	{
		public YZ_Hospital()
		{}
		#region Model
		private string _hid;
		private string _hcode;
		private string _hname;
		private DateTime? _hcreated= DateTime.Now;
		private string _distid;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
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
		public string HCode
		{
			set{ _hcode=value;}
			get{return _hcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HName
		{
			set{ _hname=value;}
			get{return _hname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? HCreated
		{
			set{ _hcreated=value;}
			get{return _hcreated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DistID
		{
			set{ _distid=value;}
			get{return _distid;}
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

