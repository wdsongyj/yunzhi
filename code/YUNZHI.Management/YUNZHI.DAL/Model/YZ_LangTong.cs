using System;
namespace YUNZHI.DAL.Model
{
    /// <summary>
    /// YZ_LangTong:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class YZ_LangTong
    {
        public YZ_LangTong()
        { }
        #region Model
        private string _ltid;
        private string _lt_projectid;
        private string _lt_recordid;
        private string _lt_ptid;
        private string _lt_ptname;
        private string _lt_ptidnumber;
        private string _lt_data;
        private DateTime? _lt_createtime = DateTime.Now;
        /// <summary>
        /// 主键
        /// </summary>
        public string LTID
        {
            set { _ltid = value; }
            get { return _ltid; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string LT_ProjectID
        {
            set { _lt_projectid = value; }
            get { return _lt_projectid; }
        }
        /// <summary>
        /// 看病记录ID
        /// </summary>
        public string LT_RecordID
        {
            set { _lt_recordid = value; }
            get { return _lt_recordid; }
        }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string LT_PTID
        {
            set { _lt_ptid = value; }
            get { return _lt_ptid; }
        }
        /// <summary>
        /// 患者名称
        /// </summary>
        public string LT_PTName
        {
            set { _lt_ptname = value; }
            get { return _lt_ptname; }
        }
        /// <summary>
        /// 患者身份证号
        /// </summary>
        public string LT_PTIDNumber
        {
            set { _lt_ptidnumber = value; }
            get { return _lt_ptidnumber; }
        }
        /// <summary>
        /// 朗通数据
        /// </summary>
        public string LT_Data
        {
            set { _lt_data = value; }
            get { return _lt_data; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? LT_CreateTime
        {
            set { _lt_createtime = value; }
            get { return _lt_createtime; }
        }
        #endregion Model

    }

    [Serializable]

    public partial class YZ_AddLangTong
    {
        public YZ_AddLangTong()
        {

        }
        private string _hosID;

        public string hosID
        {
            get { return _hosID; }
            set { _hosID = value; }
        }
        private string _hosName;

        public string hosName
        {
            get { return _hosName; }
            set { _hosName = value; }
        }
        private string _doctorId;

        public string doctorId
        {
            get { return _doctorId; }
            set { _doctorId = value; }
        }
        private string _doctorName;

        public string doctorName
        {
            get { return _doctorName; }
            set { _doctorName = value; }
        }
        private string _recordID;

        public string recordID
        {
            get { return _recordID; }
            set { _recordID = value; }
        }
        private YZ_Project _yz_Project;

        public YZ_Project yz_Project
        {
            get { return _yz_Project; }
            set { _yz_Project = value; }
        }

        private string _age;

        public string age
        {
            get { return _age; }
            set { _age = value; }
        }

        private string _sexType;

        public string sexType
        {
            get { return _sexType; }
            set { _sexType = value; }
        }

        private string _data03;

        public string Data03
        {
            get { return _data03; }
            set { _data03 = value; }
        }

        private string _data04;

        public string Data04
        {
            get { return _data04; }
            set { _data04 = value; }
        }

        private string _data05;

        public string Data05
        {
            get { return _data05; }
            set { _data05 = value; }
        }
    }
}

