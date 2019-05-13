using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace netcore2_logging.Helper
{
    /// <summary>
    /// 版本號擴充函式
    /// </summary>
    public class VersionNumberExtensions
    {
        /// <summary>
        /// 取得當前版本號
        /// </summary>
        public static string GetVersion
        {
            get
            {
                var EntryAssembly = Assembly.GetEntryAssembly();
                var VersionInfo = FileVersionInfo.GetVersionInfo(EntryAssembly.Location);
                return VersionInfo.FileVersion;
            }
        }
    }
}
