using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YunZhi.Model
{
    public class PrintModel
    {
        public PrintModel()
        {
            this.YaoPinList = new List<YaoPinModel>();
        }

        /// <summary>
        /// 患者名称
        /// </summary>
        public string PTName { get; set; }
        /// <summary>
        /// 患者性别
        /// </summary>
        public string PTSex { get; set; }
        /// <summary>
        /// 患者年龄
        /// </summary>
        public string PTNianLing { get; set; }

        /// <summary>
        /// 主述
        /// </summary>
        public string ZhuShu { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string ZhenDuan { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public string JiuZhenTime { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string HosName { get; set; }

        public List<YaoPinModel> YaoPinList { get; set; }
    }

    public class YaoPinModel
    {
        /// <summary>
        /// 药品名称
        /// </summary>
        public string YaoPinName { get; set; }

        /// <summary>
        /// 用法
        /// </summary>
        public string YongFa { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public string YongLiang { get; set; }
    }
}
