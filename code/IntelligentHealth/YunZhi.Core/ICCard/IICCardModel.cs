using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YunZhi.Core.ICCard
{
    /// <summary>
    /// 读卡信息接口
    /// </summary>
    public interface IICCardModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        string Sex { get; set; }

        /// <summary>
        /// 性别编码 
        /// </summary>
        string SexCode { get; set; }

        /// <summary>
        /// 民族编码 使用GB-3304——91  中的数字编码
        /// </summary>
        string NationCode { get; set; }

        /// <summary>
        /// 民族名称
        /// </summary>
        string Nation { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        string ICCard { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        DateTime Birthday { get; set; }

        /// <summary>
        /// 身份证的有效日期：开始日期
        /// </summary>
        DateTime StartEffectiveDate { get; set; }

        /// <summary>
        /// 身份证的有效日期：截止日期
        /// </summary>
        DateTime EndEffectiveDate { get; set; }  

        /// <summary>
        /// 住址
        /// </summary>
        string Address { get; set; } 

        /// <summary>
        /// 身份证的发行机构
        /// </summary>
        string Issuer { get; set; } 

        /// <summary>
        /// 照片
        /// </summary>
        string Picture { get; set; } 

        /// <summary>
        /// 读卡状态：true  读卡成功  false :读卡失败
        /// </summary>
        bool State { get; set; }

        /// <summary>
        /// 读卡错误信息
        /// </summary>
        string ErrorMessage { get; set; }
    }
}
