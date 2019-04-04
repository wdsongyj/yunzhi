using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace YunZhi.ICCard
{
   public interface IHDReadCard
    {
        //初始化
        ReadCardRetVal InitComm(ref int port);
        //认证 -->获取卡信息
        ReadCardRetVal Authenticate();

        //读取基本信息
        ReadCardRetVal Read_BaseInfo(ref IDCardData carddata, int iport);

        //关闭
        ReadCardRetVal CloseComm(int iport);
    }

    //读卡返回的值
    public struct ReadCardRetVal
    {
        public int retVal;
        public string retMsg;

        public void init()
        {
            this.retVal = -1;
            this.retMsg = "未放身份证或身份证驱动没正确安装！";
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct IDCardData
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name; //姓名   
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
        public string Sex;   //性别
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Nation; //名族
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string Born; //出生日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
        public string Address; //住址
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        public string IDCardNo; //身份证号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string GrantDept; //发证机关
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string UserLifeBegin; // 有效开始日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string UserLifeEnd;  // 有效截止日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        public string reserved; // 保留
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string PhotoFileName; // 照片路径
    }
}
