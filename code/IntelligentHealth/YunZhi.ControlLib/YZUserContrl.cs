using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace YunZhi.ControlLib
{
   public class YZUserContrl:UserControl
    {
        public YZUserContrl():base()
        {
           
            this.Load += YZUserContrl_Load; 
        }

        private void YZUserContrl_Load(object sender, EventArgs e)
        {
            this.SetDefalutStyle();
        }
    }
}
