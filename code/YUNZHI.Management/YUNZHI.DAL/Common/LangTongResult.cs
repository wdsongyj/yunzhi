using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YUNZHI.DAL.Common
{
    public class LangTongResult
    {
        public LangTongResult()
        {
            Ret = 1;
        }

        /// <summary>
        /// 返回结果标志，默认成功0，失败1 参数错误-1 tokent丢失-2
        /// </summary>
        public int Ret { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 数据结构
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public string TimeConsum { get; set; }
    }
}
