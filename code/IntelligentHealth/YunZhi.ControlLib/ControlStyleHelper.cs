using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace YunZhi.ControlLib
{
    public static class ControlStyleHelper
    {
        private static Color dfBackground = Color.FromArgb(251, 251, 251);
        private static Font dfFontSize = new Font("宋体",14);
        public static void SetDefaultBackground(this Control ctrl)
        {
            ctrl.BackColor = dfBackground;
        } 

        public static void SetBackground(this Control ctrl,Color color)
        {
            ctrl.BackColor = color;
        }

        public static void SetDefalutStyle(this Control ctrl)
        {
            ctrl.BackColor = dfBackground;
            ctrl.Font = dfFontSize;
        } 
    }
}
