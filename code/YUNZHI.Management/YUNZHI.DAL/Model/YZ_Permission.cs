using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_Permission:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Permission
	{
		public YZ_Permission()
		{}
		#region Model
		private string _pid;
		private string _puserid;
		private string _pusername;
		private DateTime? _pcreated= DateTime.Now;
		private DateTime? _lastdate;
		private int? _plevel;
		private string _plevelnae;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
		/// <summary>
		/// 
		/// </summary>
		public string PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PUserID
		{
			set{ _puserid=value;}
			get{return _puserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PUserName
		{
			set{ _pusername=value;}
			get{return _pusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PCreated
		{
			set{ _pcreated=value;}
			get{return _pcreated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastDate
		{
			set{ _lastdate=value;}
			get{return _lastdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Plevel
		{
			set{ _plevel=value;}
			get{return _plevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PLevelNae
		{
			set{ _plevelnae=value;}
			get{return _plevelnae;}
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

