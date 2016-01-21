using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
    /// <summary>
    /// 文件导入进度信息。
    /// </summary>
    public class ProcessInfo
    {
        public ProcessInfo()
        {

        }

        /// <summary>
        /// 目标文件名陈全路径。
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 目标文件所在的文件夹路径，用于进行文件的分类管理。
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件处理进度：
        ///     1：表示待处理；
        ///     2：表示正在处理；
        ///     3：表示已经处理完成；
        /// </summary>
        public EImportStatus ProcessStatus { get; set; }
    }
}
