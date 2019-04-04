using Paway.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YunZhi.Client.Frm
{
    public partial class FrmWebBrower : QQForm
    {
        public FrmWebBrower()
        {
            InitializeComponent();
            this.webBrowser1.ScriptErrorsSuppressed = true;
        }

        public FrmWebBrower(string url)
            : this()
        {
            this.webBrowser1.Url = new Uri(url);
        }
    }
}
