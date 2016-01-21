using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dothan.DzHelpers
{
    public class MemoryHelper
    {
        /// <summary>
        /// 获取当前进程的内存占用多少Byte。
        /// </summary>
        public static float GetCost_F()
        {
            Process p = Process.GetCurrentProcess();

            return GetCost_F(p.ProcessName);
        }

        /// <summary>
        /// 获取内存占用大小；单位Byte。
        /// </summary>
        public static float GetCost_F(string instanceName)
        {
            try
            {
                using (var p1 = new PerformanceCounter("Process", "Working Set - Private", instanceName))
                {
                    return p1.NextValue();
                }
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "] Exception: " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);

                return 0.0f;
            }
        }

        private static string GetCost(float size)
        {
            return (size / 1024 / 1024).ToString("0.0") + "MB";
        }

        /// <summary>
        /// 获取当前进程占用多少MB;
        /// </summary>
        public static string GetCost()
        {
            return GetCost(GetCost_F());
        }
    }
}
