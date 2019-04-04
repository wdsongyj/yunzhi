using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YunZhi.Model
{
    public class ProjectModel
    {
        private string _projectTime;

        public string ProjectTime
        {
            get { return _projectTime; }
            set { _projectTime = value; }
        }

        private string _projectUploadTime;

        public string ProjectUploadTime
        {
            get { return _projectUploadTime; }
            set { _projectUploadTime = value; }
        }

        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }
        private string _projectPH;

        public string ProjectPH
        {
            get { return _projectPH; }
            set { _projectPH = value; }
        }
        private string _deviceSN;

        public string DeviceSN
        {
            get { return _deviceSN; }
            set { _deviceSN = value; }
        }
        private string _deviceAddress;

        public string DeviceAddress
        {
            get { return _deviceAddress; }
            set { _deviceAddress = value; }
        }
        private string _deviceResult;

        public string DeviceResult
        {
            get { return _deviceResult; }
            set { _deviceResult = value; }
        }
        private string _deviceDesc;

        public string DeviceDesc
        {
            get { return _deviceDesc; }
            set { _deviceDesc = value; }
        }
    }
}
