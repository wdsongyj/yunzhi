using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_Health_Detail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Health_Detail
	{
		public YZ_Health_Detail()
		{}
		#region Model
		private string _hdid;
		private string _hrid;
		private string _proid;
		private DateTime? _hdcreated= DateTime.Now;
		private string _hdremark;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
		/// <summary>
		/// 
		/// </summary>
		public string HDID
		{
			set{ _hdid=value;}
			get{return _hdid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HRID
		{
			set{ _hrid=value;}
			get{return _hrid;}
		}
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
		public DateTime? HDCreated
		{
			set{ _hdcreated=value;}
			get{return _hdcreated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HDRemark
		{
			set{ _hdremark=value;}
			get{return _hdremark;}
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

