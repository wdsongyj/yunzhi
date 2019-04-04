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
    public partial class FrmReadID : QQForm
    {
        public FrmReadID()
        {
            InitializeComponent();
        }

        public FrmReadID(string msg)
            : this()
        {
            if (string.IsNullOrEmpty(msg) == false)
            {
                this.label1.Text = msg;
            }
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
