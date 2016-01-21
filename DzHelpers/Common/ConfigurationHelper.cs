using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace Dothan.DzHelpers
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// 打开默认配置文件中制定的Section；
        /// </summary>
        public static object GetSection(string sectionName)
        {
            return GetConfiguration().GetSection(sectionName);
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return GetConfiguration().ConnectionStrings.ConnectionStrings; }
        }

        public static KeyValueConfigurationCollection AppSettings
        {
            get { return GetConfiguration().AppSettings.Settings; }
        }

        /// <summary>
        /// 打开默认的配置文件；
        /// </summary>
        public static Configuration GetConfiguration()
        {
            string configFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Connection.config");
            return GetConfiguration(configFile);
        }

        /// <summary>
        /// 打开指定的配置文件；
        /// </summary>
        public static Configuration GetConfiguration(string configFile)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configFile;

            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
    }
}
