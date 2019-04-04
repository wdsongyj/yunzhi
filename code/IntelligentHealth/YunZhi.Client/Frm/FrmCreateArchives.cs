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
using YunZhi.Util;

namespace YunZhi.Client.Frm
{
    public partial class FrmCreateArchives : QQForm
    {

        public FrmCreateArchives()
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
            try
            {
                var a = new YunZhi.ICCard.HDReadCard();
                YunZhi.Core.ICCard.IICCardModel cardModel = a.ReadCard();
                if (cardModel.State)
                {
                    this.txtUserName.Text = cardModel.Name;
                    this.cbBox_Sex.Text = cardModel.Sex;
                    this.dtp_Birthday.Value = cardModel.Birthday;
                    this.txt_Address.Text = cardModel.Address;
                    this.txt_IDCard.Text = cardModel.ICCard;
                    this.txt_Tel.Text = string.Empty;
                }
                else
                {
                    new FrmReadID(cardModel.ErrorMessage).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读卡失败！");
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            this.SaveOption();
        }

        private void SaveOption()
        {
            YunZhiService service = null;
            try
            {
                string userName = this.txtUserName.Text.Trim();
                if (string.IsNullOrEmpty(userName))
                {
                    MessageBox.Show("请输入姓名！");
                    return;
                }
                string idcard = this.txt_IDCard.Text.Trim();
                if (string.IsNullOrEmpty(idcard))
                {
                    //TODO Services
                    MessageBox.Show("请输入身份证号！"); //不输入身份证号，服务端保存失败，返回的msg="" 
                    return;
                }

                string sex = this.cbBox_Sex.SelectedIndex.ToString();
                DateTime birthday = this.dtp_Birthday.Value;
                string tel = this.txt_Tel.Text.Trim();
                string address = this.txt_Address.Text.Trim();
                string addressNew = this.txt_AddressNew.Text.Trim();
                service = new YunZhiService();
                YunZhiResult addResult = service.AddPatientInfo(userName, address, tel, birthday.ToString("yyyy-MM-dd hh:mm:ss"), sex, idcard, addressNew, SourceHelper.EmployeeInfo.HID, SourceHelper.EmployeeInfo.EmpID);
                if (addResult.Result == 1)
                {
                    MessageBox.Show("添加成功");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    string strMessgage = string.IsNullOrEmpty(addResult.Msg) ? "添加失败" : addResult.Msg;
                    MessageBox.Show(strMessgage);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("添加异常：" + ex.Message, ex);
            }
            finally
            {
                if (service != null)
                {
                    service.Abort();
                }
            }
        }
    }
}
