using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YunZhi.Model;

namespace YunZhi.Util
{
    public static class SourceHelper
    {
        public static YZ_Employee EmployeeInfo { get; set; }
        public static List<TreatmentModel> _TreatmentSource;
        

        private static void SetTreatmentData()
        {
            try
            {
                TreatmentModel model1 = new TreatmentModel();
                model1.CreateTime = "2018-03-01 16:48:25";
                model1.DoctorName = "王华";
                model1.ProjectDesc = "阴性";
                model1.ProjectName = "肺衣";
                model1.ProjectResult = "21.79";
                _TreatmentSource.Add(model1);

                TreatmentModel model2 = new TreatmentModel();
                model2.CreateTime = "2018-03-01 16:48:25";
                model2.DoctorName = "王华";
                model2.ProjectDesc = "阴性";
                model2.ProjectName = "肺衣";
                model2.ProjectResult = "21.79";
                _TreatmentSource.Add(model2);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
