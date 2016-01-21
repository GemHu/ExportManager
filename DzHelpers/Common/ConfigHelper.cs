/// <summary>
/// @file   DZUtilityConfig.cs
///	@brief  INI 配置文件和注册表配置信息操作相关的函数。
/// @author	DothanTech 刘伟宏
/// 
/// Copyright(C) 2011~2014, DothanTech. All rights reserved.
/// </summary>

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Dothan.DzHelpers
{
    /// <summary>
    /// 配置信息操作的虚类。
    /// </summary>
    public abstract class Config
    {
        abstract public string GetValue(string csSection, string csKey, string csDefault);
        abstract public void SetValue(string csSection, string csKey, string csValue);
        virtual public int GetValue(string csSection, string csKey, int nDefault)
        {
            string value = GetValue(csSection, csKey, "");
            if (value == "") return nDefault;

            int parseValue = nDefault;
            if (int.TryParse(value, out parseValue))
                return parseValue;
            return nDefault;
        }
        virtual public bool GetValue(string csSection, string csKey, bool bDefault)
        {
            string value = GetValue(csSection, csKey, "");
            return _GetValue(value, bDefault);
        }
        virtual public Color GetValue(string csSection, string csKey, Color cDefault)
        {
            string value = GetValue(csSection, csKey, "");
            return _GetValue(value, cDefault);
        }
        virtual public Rectangle GetValue(string csSection, string csKey, Rectangle rcDefault)
        {
            string value = GetValue(csSection, csKey, "");
            return _GetValue(value, rcDefault);
        }
        virtual public void SetValue(string csSection, string csKey, int nValue)
        {
            SetValue(csSection, csKey, nValue.ToString());
        }
        virtual public void SetValue(string csSection, string csKey, bool bValue)
        {
            SetValue(csSection, csKey, _GetValue(bValue));
        }
        virtual public void SetValue(string csSection, string csKey, Color cValue)
        {
            SetValue(csSection, csKey, _GetValue(cValue));
        }
        virtual public void SetValue(string csSection, string csKey, Rectangle rcValue)
        {
            SetValue(csSection, csKey, _GetValue(rcValue));
        }

        // GetValue without default value
        public int GetValueI(string csSection, string csKey)
        { return GetValue(csSection, csKey, (int)0); }
        public bool GetValueB(string csSection, string csKey)
        { return GetValue(csSection, csKey, false); }
        public Color GetValueC(string csSection, string csKey)
        { return GetValue(csSection, csKey, Color.Empty); }
        public string GetValueS(string csSection, string csKey)
        { return GetValue(csSection, csKey, ""); }

        // Helper function to get value from string
        static public bool _GetValue(string strValue, bool bDefault)
        {
            if (string.IsNullOrEmpty(strValue)) return bDefault;

            return strValue.ToBool(bDefault);
        }
        static public string _GetValue(bool value)
        {
            return (value ? "Yes" : "No");
        }
        static public Color _GetValue(string strValue, Color cDefault)
        {
            if (string.IsNullOrEmpty(strValue)) return cDefault;

            Color value = Color.FromName(strValue);
            if (value.A != 0) return value;

            string[] colors = strValue.Split(',');
            if (colors == null) return cDefault;

            byte parseA = 0, parseR = 0, parseG = 0, parseB = 0;
            if (colors.Count() == 3)
            {
                if (!byte.TryParse(colors[0].Trim(), out parseR))
                    return cDefault;
                if (!byte.TryParse(colors[1].Trim(), out parseG))
                    return cDefault;
                if (!byte.TryParse(colors[2].Trim(), out parseB))
                    return cDefault;

                return Color.FromArgb(parseR, parseG, parseB);
            }
            else if (colors.Count() == 4)
            {
                if (!byte.TryParse(colors[0].Trim(), out parseA))
                    return cDefault;
                if (!byte.TryParse(colors[1].Trim(), out parseR))
                    return cDefault;
                if (!byte.TryParse(colors[2].Trim(), out parseG))
                    return cDefault;
                if (!byte.TryParse(colors[3].Trim(), out parseB))
                    return cDefault;

                return Color.FromArgb(parseA, parseR, parseG, parseB);
            }
            else
                return cDefault;
        }
        static public string _GetValue(Color color)
        {
            if (color.A == 255)
            {
                return ((int)color.R).ToString() + ", "
                     + ((int)color.G).ToString() + ", "
                     + ((int)color.B).ToString();
            }
            else
            {
                return ((int)color.A).ToString() + ", "
                     + ((int)color.R).ToString() + ", "
                     + ((int)color.G).ToString() + ", "
                     + ((int)color.B).ToString();
            }
        }
        static public Rectangle _GetValue(string strValue, Rectangle rcDefault)
        {
            if (string.IsNullOrEmpty(strValue)) return rcDefault;

            string[] pos = strValue.Split(',');
            if (pos == null) return rcDefault;

            int l = 0, t = 0, r = 0, b = 0;
            if (pos.Count() == 4)
            {
                if (!int.TryParse(pos[0], out l))
                    return rcDefault;
                if (!int.TryParse(pos[1], out t))
                    return rcDefault;
                if (!int.TryParse(pos[2], out r))
                    return rcDefault;
                if (!int.TryParse(pos[3], out b))
                    return rcDefault;

                return Rectangle.FromLTRB(l, t, r, b);
            }
            else
                return rcDefault;
        }
        static public string _GetValue(Rectangle rect)
        {
            return rect.Left + ", "
                 + rect.Top + ", "
                 + rect.Right + ", "
                 + rect.Bottom;
        }
    }

    /// <summary>
    /// INI 配置文件操作。
    /// </summary>
    public class IniFile : Config
    {
        public IniFile(string csIniFile) { theIniFile = csIniFile; }
        protected string theIniFile;

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string section,
            byte[] values, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key,
            string val, string filePath);
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileSection(string section,
            StringBuilder values, string filePath);

        override public string GetValue(string csSection, string csKey, string csDefault)
        {
            StringBuilder sbBuf = null;
            for (int size = 0x20; size < 0x10000; size += size)
            {
                sbBuf = new StringBuilder(size);
                if (GetPrivateProfileString(csSection, csKey, csDefault, sbBuf, size, theIniFile) < (size - 1))
                    break;
            }
            return sbBuf.ToString();
        }

        override public void SetValue(string csSection, string csKey, string csValue)
        {
            WritePrivateProfileString(csSection, csKey, csValue, theIniFile);
        }

        public string GetSection(string csSection, char sepChar)
        {
            byte[] sbBuf = null; int len = 0;
            for (int size = 0x200; size < 0x200000; size += size)
            {
                sbBuf = new byte[size];
                len = GetPrivateProfileSection(csSection, sbBuf, size, theIniFile);
                if (len < (size - 2))
                    break;
            }
            if (len <= 0)
                return "";

            string values = Encoding.Default.GetString(sbBuf, 0, len);
            if (sepChar == '\0')
                return values;

            return values.Replace('\0', sepChar);
        }

        public ArrayList GetSection(string csSection, string csKeyPrefix)
        {
            ArrayList saValue = new ArrayList();
            for (int idx = 0; ; ++idx)
            {
                string csKey = csKeyPrefix + idx.ToString();
                string value = GetValue(csSection, csKey, "");
                if (string.IsNullOrEmpty(value))
                    break;

                saValue.Add(value);
            }
            return saValue.Count <= 0 ? null : saValue;
        }

        public Dictionary<string, string> GetSection(string csSection)
        {
            Dictionary<string, string> kvp = new Dictionary<string, string>();

            this.LoopSections(
                (section) =>
                {
                    if (section.Equals(csSection, StringComparison.OrdinalIgnoreCase))
                        return true;
                    
                    return false;
                },
                (key, value) =>
                {
                    kvp[key] = value;
                    return true;
                });

            return kvp;
        }

        public void SetSection(string csSection, string csKeyValues, char sepChar)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(csKeyValues))
            {
                sb.Append('\0');
            }
            else
            {
                sb.Append(csKeyValues);
                sb.Append('\0');

                if (sepChar != '\0')
                    sb.Replace(sepChar, '\0');
            }

            sb.Append('\0');
            WritePrivateProfileSection(csSection, sb, theIniFile);
        }

        public void SetSection(string csSection, ICollection saKeyValue)
        {
            StringBuilder sb = new StringBuilder();
            if (saKeyValue == null || saKeyValue.Count <= 0)
            {
                sb.Append('\0');
            }
            else
            {
                foreach (var v in saKeyValue)
                {
                    string keyValue = v.ToString();
                    if (string.IsNullOrEmpty(keyValue))
                        continue;

                    sb.Append(keyValue);
                    sb.Append('\0');
                }
            }

            sb.Append('\0');
            WritePrivateProfileSection(csSection, sb, theIniFile);
        }

        public void SetSection(string csSection, IList saKey, IList saValue)
        {
            if (saKey == null || saKey.Count <= 0)
            {
                SetSection(csSection, null);
            }
            else
            {
                Debug.Assert(saKey.Count == saValue.Count);

                int maxKeyLen = 0;
                for (int i = 0; i < saKey.Count; ++i)
                {
                    Debug.Assert((saKey[i] as string) != null);
                    if (maxKeyLen < (saKey[i] as string).Length)
                        maxKeyLen = (saKey[i] as string).Length;
                }

                string[] saKeyValue = new string[saKey.Count];
                for (int i = 0; i < saKey.Count; ++i)
                {
                    string key = saKey[i].ToString();
                    if (key.Length < maxKeyLen)
                        key += new string(' ', maxKeyLen - key.Length);
                    string value = (saValue[i] == null ? "" : saValue[i].ToString());

                    saKeyValue[i] = (key + "=" + value);
                }

                SetSection(csSection, saKeyValue);
            }
        }

        public void SetSection(string csSection, string csKeyPrefix, IList saValue)
        {
            if (saValue == null || saValue.Count <= 0)
            {
                string[] saKeyValue = new string[1];
                saKeyValue[0] = ("COUNT=0");
                SetSection(csSection, saKeyValue);
            }
            else
            {
                int count = saValue.Count, numLen = 0;
                if (count >= 10000)
                    numLen = 5;
                else if (count >= 1000)
                    numLen = 4;
                else if (count >= 100)
                    numLen = 3;
                else if (count >= 10)
                    numLen = 2;
                else
                    numLen = 1;

                string[] saKeyValue = new string[count + 1];
                saKeyValue[0] = ("COUNT=" + count.ToString());
                for (int i = 0; i < count; ++i)
                {
                    string key = i.ToString();
                    if (key.Length < numLen)
                        key += new string(' ', numLen - key.Length);
                    key = csKeyPrefix + key;
                    string value = (saValue[i] == null ? "" : saValue[i].ToString());

                    saKeyValue[i + 1] = (key + "=" + value);
                }

                SetSection(csSection, saKeyValue);
            }
        }

        public bool CreateValue(string csSection, string csKeyPrefix, string csValue)
        {
            return CreateValue(csSection, csKeyPrefix, 0, csValue);
        }

        public bool CreateValue(string csSection, string csKeyPrefix, int iFrom, string csValue)
        {
            if (string.IsNullOrEmpty(csValue))
                return false;

            for (; ; ++iFrom)
            {
                string csKey = csKeyPrefix + iFrom.ToString();
                string value = GetValue(csSection, csKey, "");
                if (string.IsNullOrEmpty(value))
                {
                    if (iFrom == 0)
                        SetValue(csSection, "COUNT", 1);

                    SetValue(csSection, csKey, csValue);
                    DeleteKeys(csSection, csKeyPrefix, iFrom + 1);
                    SetValue(csSection, "COUNT", iFrom + 1);
                    return true;
                }

                if (csValue.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
        }

        public int ChangeValue(string csSection, string csKeyPrefix, string csOldValue, string csNewValue)
        {
            return ChangeValue(csSection, csKeyPrefix, 0, csOldValue, csNewValue);
        }

        public int ChangeValue(string csSection, string csKeyPrefix, int iFrom, string csOldValue, string csNewValue)
        {
            if (csOldValue.Equals(csNewValue, StringComparison.OrdinalIgnoreCase))
                return 0;

            int iCount = 0;
            for (; ; ++iFrom)
            {
                string csKey = csKeyPrefix + iFrom.ToString();
                string value = GetValue(csSection, csKey, "");
                if (string.IsNullOrEmpty(value))
                    break;

                if (value.Equals(csOldValue, StringComparison.OrdinalIgnoreCase))
                {
                    SetValue(csSection, csKey, csNewValue);
                    iCount++;
                }
            }
            return iCount;
        }

        public void DeleteKeys(string csSection, string csKeyPrefix, int iFrom)
        {
            for (; ; ++iFrom)
            {
                string csKey = csKeyPrefix + iFrom.ToString();
                if (string.IsNullOrEmpty(GetValue(csSection, csKey, "")))
                    break;

                SetValue(csSection, csKey, "");
            }
        }

        public int DeleteValue(string csSection, string csKeyPrefix, string csValue)
        {
            return DeleteValue(csSection, csKeyPrefix, 0, csValue);
        }

        public int DeleteValue(string csSection, string csKeyPrefix, int iFrom, string csValue)
        {
            if (string.IsNullOrEmpty(csValue))
                return 0;

            int iBase = iFrom, iCount = 0;
            for (; ; ++iFrom)
            {
                string csKey = csKeyPrefix + iFrom.ToString();
                string value = GetValue(csSection, csKey, "");
                if (string.IsNullOrEmpty(value))
                    break;

                if (value.Equals(csValue, StringComparison.OrdinalIgnoreCase))
                {
                    iCount++;
                }
                else
                {
                    if (iBase < iFrom)
                        SetValue(csSection, csKeyPrefix + iBase.ToString(), value);
                    iBase++;
                }
            }

            if (iBase < iFrom)
            {
                DeleteKeys(csSection, csKeyPrefix, iBase);
                SetValue(csSection, "COUNT", iFrom - iCount);
            }

            return iCount;
        }

        /// <summary>
        /// 对配置文件中的所有项目进行遍历。
        /// </summary>
        /// <param name="sectionFilter">对章节进行的过滤，参数是章节名称。返回 true 表示需要遍历章节的项目。</param>
        /// <param name="itemAction">对章节中的项目进行遍历，参数是项目名称和项目值。返回 true 表示继续遍历，否则停止遍历。</param>
        public void LoopSections(Func<string, bool> sectionFilter, Func<string, string, bool> itemAction)
        {
            if (!File.Exists(theIniFile)) return;

            try
            {
                using (StreamReader reader = new StreamReader(theIniFile, Encoding.Default))
                {
                    int pos, pos2, pos3;
                    string key, value, line;
                    bool loopItems = false;
                    while (true)
                    {
                        line = reader.ReadLine();
                        if (line == null) break;

                        // 空行？
                        line = line.Trim();
                        if (string.IsNullOrEmpty(line)) continue;
                        // 注释行？
                        if (line[0] == ';') continue;

                        if (line[0] == '[')
                        { // 章节行？
                            pos = line.IndexOf(']');
                            if (pos > 0)
                            {
                                line = line.Substring(1, pos - 1).Trim();
                                if (string.IsNullOrEmpty(line))
                                {
                                    loopItems = false;
                                }
                                else
                                {
                                    loopItems = sectionFilter(line);
                                }
                            }
                        }
                        else if (loopItems)
                        { // 需要遍历的章节？
                            pos = line.IndexOf('=');
                            if (pos > 0)
                            {
                                key = line.Substring(0, pos).Trim();
                                value = line.Substring(pos + 1).Trim();
                                if (!string.IsNullOrEmpty(key))
                                {
                                    // 去除 value 中的注释
                                    pos = 0;
                                    while (true)
                                    {
                                        pos2 = value.IndexOf(';', pos);
                                        if (pos2 < 0) break;
                                        pos3 = value.IndexOf('\"', pos);

                                        // 没有字符串引号，或字符串引号在注释中
                                        if (pos3 < 0 || pos3 > pos2)
                                        {
                                            value = value.Substring(0, pos2).TrimEnd();
                                            break;
                                        }

                                        // 找到匹配的引号，从引号后继续
                                        pos = value.IndexOf('\"', pos3 + 1) + 1;
                                        if (pos2 <= 0) break;
                                    }

                                    // 对项目执行动作
                                    if (!itemAction(key, value))
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "] Exception: " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);
            }
        }
    }

    /// <summary>
    /// 注册表操作。
    /// </summary>
    public class RegFile : Config
    {
        public RegFile(RegistryKey root, string path)
        {
            theRoot = root;
            theRegPath = path;
        }
        public RegFile(string path)
        {
            int intIndex = path.IndexOf("\\");

            if (intIndex < 0)
            {
                theRoot = Registry.CurrentUser;
                theRegPath = path;
            }
            else
            {
                string strRoot = path.Substring(0, intIndex).ToUpper();
                theRegPath = path.Substring(intIndex + 1);
                switch (strRoot)
                {
                    case "HKEY_CLASSES_ROOT":
                        theRoot = Registry.ClassesRoot;
                        break;
                    case "HKEY_CURRENT_CONFIG":
                        theRoot = Registry.CurrentConfig;
                        break;
                    case "HKEY_CURRENT_USER":
                        theRoot = Registry.CurrentUser;
                        break;
                    //case "HKEY_DYN_DATA":
                    //    theRoot = Registry.DynData;
                    //    break;
                    case "HKEY_LOCAL_MACHINE":
                        theRoot = Registry.LocalMachine;
                        break;
                    case "HKEY_PERFORMANCE_DATA":
                        theRoot = Registry.PerformanceData;
                        break;
                    case "HKEY_USERS":
                        theRoot = Registry.Users;
                        break;
                    default:
                        theRoot = Registry.CurrentUser;
                        theRegPath = path;
                        break;
                }
            }
        }
        protected RegistryKey theRoot;
        protected string theRegPath;
        protected RegistryKey theRegKey;
        protected bool theKeyWritable;

        override public string GetValue(string csSection, string csKey, string csDefault)
        {
            try
            {
                if (!OpenRegKey(false)) return csDefault;

                RegistryKey key = theRegKey;
                if (csSection != "")
                {
                    key = key.OpenSubKey(csSection, false);
                    if (key == null) return csDefault;
                }

                object value = key.GetValue(csKey);
                if (value == null) return csDefault;
                return value.ToString();
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "] Exception: " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);
                return csDefault;
            }
        }

        override public void SetValue(string csSection, string csKey, string csValue)
        {
            try
            {
                if (!OpenRegKey(true)) return;

                if (GetValueS(csSection, csKey) == csValue)
                    return;

                RegistryKey key = theRegKey;
                if (csSection != "")
                {
                    key = theRegKey.OpenSubKey(csSection, true);
                    if (key == null)
                        key = theRegKey.CreateSubKey(csSection);
                    if (key == null)
                        return;
                }

                key.SetValue(csKey, csValue);
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "] Exception: " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);
            }
        }

        protected bool OpenRegKey(bool writable)
        {
            try
            {
                if (theRegKey != null)
                {
                    if (!writable || theKeyWritable)
                        return true;

                    // should be writable key
                    theRegKey.Close();
                    theRegKey = null;
                }

                // open key
                theRegKey = theRoot.OpenSubKey(theRegPath, writable);
                if (theRegKey == null)
                {
                    theRegKey = theRoot.CreateSubKey(theRegPath);
                    writable = true;
                }
                if (theRegKey == null) return false;
                theKeyWritable = writable;

                // success
                return true;
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "] Exception: " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);
                return false;
            }
        }
    }

    /// <summary>
    /// 在一行文本中，通过 key:value 的方式保存参数设置。
    /// </summary>
    public class StringConfig
    {
        /// <summary>
        /// 构造对象。
        /// </summary>
        /// <param name="decl">key:value 方式定义的一行文本</param>
        public StringConfig(string decl)
        {
            this.Items = new Dictionary<string, KeyValuePair<string, string>>();

            if (string.IsNullOrEmpty(decl)) return;

            ArrayList alItems = decl.SplitComma(":");
            if (alItems != null && alItems.Count > 0)
            {
                char[] Separators = new char[] { ' ', '\t' };

                string key, value, str;
                key = (alItems[0] as string).Trim();
                if (alItems.Count == 1)
                {
                    value = string.Empty;
                    this.Items[key.ToUpper()] = new KeyValuePair<string, string>(key, value);
                }
                else
                {
                    for (int index = 1; index < alItems.Count - 1; ++index)
                    {
                        str = (alItems[index] as string).Trim();
                        int pos = str.LastIndexOfAny(Separators);
                        if (pos < 0)
                        {
                            value = string.Empty;
                            this.Items[key.ToUpper()] = new KeyValuePair<string, string>(key, value);
                            key = str;
                        }
                        else
                        {
                            value = Decode(str.Substring(0, pos).Trim());
                            this.Items[key.ToUpper()] = new KeyValuePair<string, string>(key, value);
                            key = str.Substring(pos + 1).Trim();
                        }
                    }
                    // 最后一个
                    value = Decode((alItems[alItems.Count - 1] as string).Trim());
                    this.Items[key.ToUpper()] = new KeyValuePair<string, string>(key, value);
                }
            }
        }

        protected Dictionary<string, KeyValuePair<string, string>> Items;

        /// <summary>
        /// 参数设置。
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数值</returns>
        public string this[string key]
        {
            get
            {
                string _key = key.ToUpper();
                if (this.Items.ContainsKey(_key))
                    return this.Items[_key].Value;
                return null;
            }
            set
            {
                this.Items[key.ToUpper()] = new KeyValuePair<string, string>(key, value ?? string.Empty);
            }
        }

        /// <summary>
        /// 得到关键字的个数。
        /// </summary>
        public int KeyCount { get { return this.Items.Count; } }

        /// <summary>
        /// 返回是否含有指定的关键字？
        /// </summary>
        /// <param name="key">关键字名称</param>
        /// <returns>是否含有指定的关键字？</returns>
        public bool ContainsKey(string key)
        {
            return this.Items.ContainsKey(key.ToUpper());
        }

        /// <summary>
        /// 删除一参数设置。
        /// </summary>
        /// <param name="key">参数名称</param>
        public void RemoveKey(string key)
        {
            this.Items.Remove(key.ToUpper());
        }

        /// <summary>
        /// 重载转化成字符串的函数，调试时可以很方便地显示对象信息。
        /// </summary>
        public override string ToString()
        {
            string str = string.Empty;
            foreach (KeyValuePair<string, string> kvp in this.Items.Values)
            {
                if (!string.IsNullOrEmpty(str))
                    str += ' ';
                str += string.Format("{0}:{1}", kvp.Key, Encode(kvp.Value));
            }
            return str;
        }

        protected const char EscapeChar = '`';
        protected const string EscapedChars = "`:\t\r\n";
        protected const string EscapeToChar = "`ctrnb";

        /// <summary>
        /// 对要保存在一行字符串中的参数值，进行转义编码，否则会影响解析。
        /// </summary>
        /// <param name="value">转义编码前的字符串</param>
        /// <returns>转义编码后的字符串</returns>
        public static string Encode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string encode;
            if (value.IndexOfAny(EscapedChars, 0) < 0)
            {
                encode = value;
            }
            else
            {
                encode = string.Empty;
                int start = 0, pos;
                while (true)
                {
                    pos = value.IndexOfAny(EscapedChars, start);
                    if (pos < 0)
                    {
                        encode += value.Substring(start);
                        break;
                    }
                    else
                    {
                        encode += value.Substring(start, pos);
                        encode += EscapeChar;
                        encode += EscapeToChar[EscapedChars.IndexOf(value[pos])];
                        start = pos + 1;
                    }
                }
            }

            // 将最后的一个空格，转义一下
            if (encode[encode.Length - 1] != ' ')
                return encode;
            return encode.Substring(0, encode.Length - 1)
                + EscapeChar + EscapeToChar[EscapeToChar.Length - 1];
        }

        /// <summary>
        /// 对要保存在一行字符串中的参数值，进行转义解码。
        /// </summary>
        /// <param name="value">需要转义解码的字符串</param>
        /// <returns>转义解码后的字符串</returns>
        public static string Decode(string value)
        {
            if (value.IndexOf(EscapeChar) < 0)
                return value;

            string decode = string.Empty;
            int start = 0, pos, index;
            while (true)
            {
                pos = value.IndexOf(EscapeChar, start);
                if (pos < 0 || pos + 1 >= value.Length)
                {
                    decode += value.Substring(start);
                    break;
                }
                else
                {
                    index = EscapeToChar.IndexOf(value[pos + 1]);
                    if (index < 0)
                    {
                        // 不认识的转义字符，忽略
                        decode += value.Substring(start, pos + 1 - start);
                        start = pos + 1;
                    }
                    else
                    {
                        decode += value.Substring(start, pos - start);
                        // 最后的空格也被转义了，只是空格没有被包含在 EscapedChars 中
                        decode += (index >= EscapedChars.Length ? ' ' : EscapedChars[index]);
                        start = pos + 2;
                    }
                }
            }

            return decode;
        }
    }
}
