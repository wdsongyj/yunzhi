using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using YunZhi.Core.ICCard;

namespace YunZhi.ICCard
{
    /// <summary>
    /// 华大读卡
    /// </summary> 
    public class HDReadCard : IReadCard
    {
        [DllImport("HDstdapi.dll", EntryPoint = "HD_InitComm")]
        static extern int HD_InitComm(int iport);

        [DllImport("HDstdapi.dll", EntryPoint = "HD_Authenticate")]
        static extern int HD_Authenticate();

        [DllImport("HDstdapi.dll", EntryPoint = "HD_CloseComm")]
        static extern int HD_CloseComm(int iport);

        [DllImport("HDstdapi.dll", EntryPoint = "HD_Read_BaseMsg")]
        static extern int HD_Read_BaseMsg(StringBuilder pBmpData, StringBuilder pName, StringBuilder pSex,
                                           StringBuilder pNation, StringBuilder pBirth, StringBuilder pAddress,
                                           StringBuilder pCertNo, StringBuilder pDepartment, StringBuilder pEffectdata,
                                           StringBuilder pExpire);
        #region IHDReadCard
        private ReadCardRetVal InitComm(ref int port)
        {
            ReadCardRetVal ret = new ReadCardRetVal();
            ret.init();
            try
            {
                //循环设置对应的端口
                ret.retVal = HD_InitComm(port);
                if (ret.retVal != 0)
                {
                    for (int iport = 1001; iport < 1017; iport++)
                    {
                        ret.retVal = HD_InitComm(iport);
                        if (ret.retVal == 0)
                        {
                            port = iport;
                            //记录端口到ini文件中，如果ini读取不对再重新循环                    
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret.retVal = -1;
                if (!ex.Message.Equals(""))
                    ret.retMsg = ex.Message;
            }
            return ret;
        }

        private ReadCardRetVal Authenticate()
        {
            ReadCardRetVal ret = new ReadCardRetVal();
            ret.init();
            try
            {
                ret.retVal = HD_Authenticate();
            }
            catch (Exception ex)
            {
                ret.retVal = -1;
                if (!ex.Message.Equals(""))
                    ret.retMsg = ex.Message;
            }
            return ret;
        }
        private ReadCardRetVal CloseComm(int iport)
        {
            ReadCardRetVal ret = new ReadCardRetVal();
            ret.init();
            try
            {
                //这华大说报错不处理，在前端就不判断了
                ret.retVal = HD_CloseComm(iport);
            }
            catch (Exception ex)
            {
                ret.retVal = -1;
                if (!ex.Message.Equals(""))
                    ret.retMsg = ex.Message;
            }
            return ret;
        }

        private HDICCardModel Read_BaseInfo(int iport)
        {
            HDICCardModel cardModel = new HDICCardModel(false);
            try
            {
                StringBuilder pBmpData = new StringBuilder(100);
                pBmpData.Append(System.Environment.CurrentDirectory);
                pBmpData.Append(@"\zp.bmp");
                StringBuilder pName = new StringBuilder(100);
                StringBuilder pSex = new StringBuilder(10);
                StringBuilder pNation = new StringBuilder(50);
                StringBuilder pBirth = new StringBuilder(20);
                StringBuilder pAddress = new StringBuilder(100);
                StringBuilder pCertNo = new StringBuilder(40);
                StringBuilder pDepartment = new StringBuilder(50);
                StringBuilder pEffectdata = new StringBuilder(20);
                StringBuilder pExpire = new StringBuilder(20);
                int retVal = HD_Read_BaseMsg(pBmpData, pName, pSex, pNation, pBirth, pAddress, pCertNo, pDepartment, pEffectdata, pExpire);
                if (retVal == 0)
                {
                    cardModel.Address = pAddress.ToString().Trim();
                    //跟之前的新中新卡具值一致处理 1900年01月01日
                    string birth = pBirth.ToString().Trim();
                    cardModel.Birthday = ToDateTime(birth);
                    cardModel.Issuer = pDepartment.ToString().Trim();
                    cardModel.ICCard = pCertNo.ToString().Trim();
                    cardModel.Sex = pSex.ToString().Trim();
                    //新中新 1 男, 2 女
                    if (cardModel.Sex == "女")
                    {
                        cardModel.SexCode = "2";
                    }
                    else
                    {
                        cardModel.SexCode = "1";
                    }
                    cardModel.Nation = pNation.ToString().Trim();
                    cardModel.Name = pName.ToString().Trim();
                    cardModel.StartEffectiveDate = ToDateTime(pEffectdata.ToString().Trim());
                    cardModel.EndEffectiveDate = ToDateTime(pExpire.ToString().Trim());
                    cardModel.State = true;
                    //carddata.PhotoFileName = pBmpData.ToString();
                }
            }
            catch (Exception ex)
            {
                cardModel.ErrorMessage = ex.Message;
            }
            return cardModel;
        }

        private DateTime ToDateTime(string strFormat)
        {
            string[] strformatArray = new string[] {
                "yyyyMMdd",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd",
                "yyyy年MM月dd日" ,
                "yyyy/MM/dd",
                "yyyyMMdd HHmmss"
            };
            return DateTime.ParseExact(strFormat, strformatArray, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
        #endregion


        public IICCardModel ReadCard()
        {
            HDICCardModel cardModel = new HDICCardModel(false);
            int iport = 1001;

            try
            {
                ReadCardRetVal ret = new ReadCardRetVal();
                ret.init();


                ret = InitComm(ref iport);

                if (ret.retVal == 0)
                {
                    //在这循环处理对应的内容
                    ret.init();
                    int xh = 0;
                    while (true)
                    {
                        //华大的设备需要放卡才会有反馈，默认20次不行需退出，不能死循环
                        ret = Authenticate();
                        if (ret.retVal == 0) break;
                        xh++;
                        if (xh > 20) break;
                        System.Threading.Thread.Sleep(200);
                    }
                    if (ret.retVal == 0)
                    {
                        ret.init();

                        cardModel = Read_BaseInfo(iport);
                    }
                }
                cardModel.ErrorMessage = GetReadCardMessage(ret.retVal);

            }
            catch (Exception ex)
            {
                cardModel.ErrorMessage = "读卡失败:" + ex.Message;
            }
            finally
            {
                CloseComm(iport);
            }
            return cardModel;
        }

        private string GetReadCardMessage(int retval)
        {
            string strMessage = "";
            switch(retval)
            {
                case 0:
                    strMessage = "读卡成功！";
                    break;
                case -1:
                    strMessage = "设备连接错";
                    break;
                case -2:
                    strMessage = "设备未建立连接(没有执行打开设备函数)";
                    break;
                case -3:
                    strMessage = "(动态库)加载失败";
                    break;
                case -4:
                    strMessage = "(发给动态库的)参数错";
                    break;
                case -5:
                    strMessage = "寻卡失败，请重新放置卡，重新读取。";
                    break;
                case -6:
                    strMessage = "选卡失败";
                    break;
                case -7:
                    strMessage = "读卡失败，请重新放置卡，重新读取。";
                    break;
                case -8:
                    strMessage = "读取追加信息失败";
                    break;
                case -9:
                    strMessage = "读取追加信息失败";
                    break;
                case -10:
                    strMessage = "管理通信失败";
                    break;
                case -11:
                    strMessage = "检验通信失败";
                    break;
                case -12:
                    strMessage = "管理通信模块不支持获取指纹";
                    break;
                case -999:
                    strMessage = "其他异常错误";
                    break;
            }
            return strMessage;
        }

    }
}
