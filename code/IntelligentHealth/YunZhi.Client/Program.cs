using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using YunZhi.Model;
using YunZhi.Util;

namespace YunZhi.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process instance = RunningInstance();
            if (instance == null)
            {
                FrmLoginNew login = new FrmLoginNew();
                DialogResult res = login.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (login._UserInfo != null && login._UserInfo.Rows.Count > 0)
                    {
                        List<YZ_Employee> employeeList=new ModelHandler<YZ_Employee>().TableToList(login._UserInfo);
                        if (employeeList != null && employeeList.Count > 0)
                        {
                            Application.Run(new FrmMainNew(employeeList[0]));
                        }                        
                    }                    
                }
                else
                {
                    return;
                }
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        /// <summary>
        /// 获取正在运行的实例，没有运行的实例返回null;
        /// </summary>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 显示已运行的程序。
        /// </summary>
        public static void HandleRunningInstance(Process instance)
        {
            MessageBox.Show("应用程序已经被打开");
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }
    }
}
