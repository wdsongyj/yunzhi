using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YunZhi.Model
{
    public class TreatmentModel
    {
        private string _createTime;

        public string CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        private string _doctorName;

        public string DoctorName
        {
            get { return _doctorName; }
            set { _doctorName = value; }
        }
        private string _projectResult;

        public string ProjectResult
        {
            get { return _projectResult; }
            set { _projectResult = value; }
        }
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }
        private string _projectDesc;

        public string ProjectDesc
        {
            get { return _projectDesc; }
            set { _projectDesc = value; }
        }
    }
}
