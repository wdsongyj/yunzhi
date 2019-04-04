using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_Setting:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Setting
	{
		public YZ_Setting()
		{}
		#region Model
		private string _sid;
		private string _stitle;
		private string _skey;
		private string _svalue;
		private DateTime? _screated= DateTime.Now;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
		/// <summary>
		/// 
		/// </summary>
		public string SID
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string STitle
		{
			set{ _stitle=value;}
			get{return _stitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SKey
		{
			set{ _skey=value;}
			get{return _skey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SValue
		{
			set{ _svalue=value;}
			get{return _svalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SCreated
		{
			set{ _screated=value;}
			get{return _screated;}
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

