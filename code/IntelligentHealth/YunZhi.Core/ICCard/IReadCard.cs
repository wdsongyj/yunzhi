using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YunZhi.Core.ICCard
{
    public interface IReadCard
    {
        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns></returns>
        IICCardModel ReadCard();
    }
}
