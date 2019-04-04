using System;
namespace YUNZHI.DAL.Model
{
	/// <summary>
	/// YZ_Health_Record:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YZ_Health_Record
	{
		public YZ_Health_Record()
		{}
		#region Model
		private string _hrid;
		private string _ptid;
		private string _ptname;
		private string _hid;
		private string _eid;
		private DateTime? _hrcreated= DateTime.Now;
		private string _hrremark;
		private bool _isenable= true;
		private int? _data01;
		private int? _data02;
		private string _data03;
		private string _data04;
		private string _data05;
        private string _data06;
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
		public string HID
		{
			set{ _hid=value;}
			get{return _hid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EID
		{
			set{ _eid=value;}
			get{return _eid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? HRCreated
		{
			set{ _hrcreated=value;}
			get{return _hrcreated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HRRemark
		{
			set{ _hrremark=value;}
			get{return _hrremark;}
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

        public string Data06
        {
            set { _data06 = value; }
            get { return _data06; }
        }
		#endregion Model

	}
}

