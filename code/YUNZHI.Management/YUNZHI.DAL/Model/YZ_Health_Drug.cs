using System;
namespace YUNZHI.DAL.Model
{
    /// <summary>
    /// YZ_Health_Drug:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class YZ_Health_Drug
    {
        public YZ_Health_Drug()
        { }
        #region Model
        private string _drugid;
        private string _hrid;
        private string _drugname;
        private string _drugyf;
        private string _drugyl;
        private DateTime? _createtime = DateTime.Now;
        private int _xh = 0;
        /// <summary>
        /// 
        /// </summary>
        public string DrugID
        {
            set { _drugid = value; }
            get { return _drugid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HRID
        {
            set { _hrid = value; }
            get { return _hrid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugName
        {
            set { _drugname = value; }
            get { return _drugname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugYF
        {
            set { _drugyf = value; }
            get { return _drugyf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugYL
        {
            set { _drugyl = value; }
            get { return _drugyl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        public int XH
        {
            set { _xh = value; }
            get { return _xh; }
        }
        #endregion Model
    }
}

