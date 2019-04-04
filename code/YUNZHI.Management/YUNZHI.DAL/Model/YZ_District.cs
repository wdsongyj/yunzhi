using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_District:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_District
	{
		public YZ_District()
		{}
		#region Model
		private string _distid;
		private string _distname;
        private DateTime? _distcreated = DateTime.Now;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
		/// <summary>
		/// 
		/// </summary>
		public string DistID
		{
			set{ _distid=value;}
			get{return _distid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DistName
		{
			set{ _distname=value;}
			get{return _distname;}
		}
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DistCreated
        {
            set { _distcreated = value; }
            get { return _distcreated; }
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

