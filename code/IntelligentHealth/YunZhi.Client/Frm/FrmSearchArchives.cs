using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunZhi.Util;

namespace YunZhi.Client.Frm
{
    public partial class FrmSearchArchives : Form
    {
        public FrmSearchArchives()
        {
            InitializeComponent();
            //this.dataGridView1.DataSource = SourceHelper._PatientSource;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FrmDetail project = new FrmDetail();
            project.ShowDialog();
        }
    }
}
