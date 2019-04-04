using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunZhi.Client.Frm;
using YunZhi.Model;
using YunZhi.Util;
using YunZhi.ControlLib;

namespace YunZhi.Client.FrmCtr
{
    public partial class UCCreateArchives : YZUserContrl
    {
        private FrmReadID anotherForm;
        public UCCreateArchives()
        {
            InitializeComponent();
            this.InitializeSource();
        }

        private void InitializeSource()
        {
            this.cbBox_Sex.Items.Add("男");
            this.cbBox_Sex.Items.Add("女");
            this.cbBox_Sex.SelectedIndex = 1;
        }

        private void btn_GetUser_Click(object sender, EventArgs e)
        {
            anotherForm = new FrmReadID();
            //this.Hide();
            anotherForm.ShowDialog();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = this.txtUserName.Text.Trim();
                string sex = this.cbBox_Sex.SelectedIndex.ToString();
                DateTime birthday = this.dtp_Birthday.Value;
                string tel = this.txt_Tel.Text.Trim();
                string address = this.txt_Address.Text.Trim();
                string addressNew = this.txt_AddressNew.Text.Trim();
                string idcard = this.txt_IDCard.Text.Trim();
                ArchivesModel addArchivess = new ArchivesModel();
                addArchivess.UserName = userName;
                addArchivess.Tel = tel;
                addArchivess.Sex = sex;
                addArchivess.IdCard = idcard;
                addArchivess.Birthday = birthday;
                addArchivess.Address = address;
                addArchivess.AddressNew = addressNew;
                //SourceHelper._ArchivesSource.Add(addArchivess);
                MessageBox.Show("保存成功");
                this.ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearData()
        {
            this.txtUserName.Text.Trim();
            this.dtp_Birthday.Value = DateTime.Now;
            this.txt_Tel.Text = "";
            this.txt_Address.Text = "";
            this.txt_AddressNew.Text = "";
            this.txt_IDCard.Text = "";
        }
    }
}
