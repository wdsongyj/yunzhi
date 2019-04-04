using Paway.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunZhi.Client.YZService;
using YunZhi.Model;

namespace YunZhi.Client.Frm
{
    public partial class FrmUpdateRecord : QQForm
    {
        private string _HRID = string.Empty;
        public FrmUpdateRecord()
        {
            InitializeComponent();
        }

        public FrmUpdateRecord(YZ_Health_Record record)            
        {
            InitializeComponent();
            this._HRID = record.HRID;
            this.txt03.Text = record.Data03;
            this.txt04.Text = record.Data04;
            this.txt05.Text = record.Data05;
            this.txt06.Text = record.Data06;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string data03=txt03.Text.Trim();
                string data04=txt04.Text.Trim();
                string data05=txt05.Text.Trim();
                string data06=txt06.Text.Trim();
                YunZhiResult res = new YunZhiService().UpdateRecorInfo(this._HRID, data03, data04, data05, data06,null,null);
                MessageBox.Show(res.Msg);
                if (res.Result == 1)
                {
                    this.DialogResult = DialogResult.OK;                    
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
