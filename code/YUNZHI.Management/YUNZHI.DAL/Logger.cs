using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace YUNZHI.DAL
{
    /// <summary>
    /// 静态日志类，自动在运行目录下创建一个log文件夹及log文件
    /// </summary>
    public class Logger
    {
        // Fields
        private static object lockHelper = new object();
        private static ILog log = null;

        // Properties
        public static ILog Log
        {
            get
            {
                if (log == null)
                {
                    lock (lockHelper)
                    {
                        if (log == null)
                        {
                            XmlConfigurator.Configure();
                            log = LogManager.GetLogger("logger");
                        }
                    }
                }
                return log;
            }
        }
    }


}
