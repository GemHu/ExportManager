/// <summary>
/// @file   StringExtension.cs
///	@brief  String 类的一些扩展函数。
/// @author	DothanTech 刘伟宏
/// 
/// Copyright(C) 2011~2014, DothanTech. All rights reserved.
/// </summary>

using System;
using System.Collections;

namespace Dothan.DzHelpers
{
    /// <summary>
    /// String 类的一些扩展函数。
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 将字符串转化为 bool 类型。
        /// </summary>
        public static bool ToBool(this string This)
        {
            return This.ToBool(false);
        }

        /// <summary>
        /// 将字符串转化为 bool 类型。
        /// </summary>
        public static bool ToBool(this string This, bool defaultValue)
        {
            if (string.IsNullOrEmpty(This))
                return defaultValue;

            bool parseValue = defaultValue;
            if (bool.TryParse(This, out parseValue))
                return parseValue;

            switch (This[0])
            {
                case '-':
                case '0':
                case 'N':
                case 'n':
                case 'F':
                case 'f':
                default: return false;
                case '+':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case 'Y':
                case 'y':
                case 'T':
                case 't': return true;
                case 'o':
                case 'O':
                    if (This.Length == 1)
                        return false;
                    return This[1] == 'n'
                        || This[1] == 'N';
            }
        }

        /// <summary>
        /// 空格字符。
        /// </summary>
        public const string SpaceChars = " \t\r\n";

        /// <summary>
        /// 以空格和TAB，分割字符串。和 string.Split() 不同的是，本分割函数会将
        /// 引号括起来的字符串，分割成一个项目，尽管字符串中可能会有空格。
        /// </summary>
        public static ArrayList SplitComma(this string This)
        {
            return This.SplitComma(SpaceChars);
        }

        /// <summary>
        /// 以空格和TAB，分割字符串。和 string.Split() 不同的是，本分割函数会将
        /// 引号括起来的字符串，分割成一个项目，尽管字符串中可能会有空格。
        /// </summary>
        /// <param name="separator">分割字符</param>
        public static ArrayList SplitComma(this string This, string separator)
        {
            ArrayList alItems = new ArrayList();

            if (!string.IsNullOrEmpty(This))
            {
                int start = 0, curr, pos2; char ch;
                for (curr = 0; curr < This.Length; ++curr)
                {
                    ch = This[curr];
                    if (separator.IndexOf(ch) >= 0)
                    {
                        if (curr > start)
                        {
                            alItems.Add(This.Substring(start, curr - start));
                        }
                        start = curr + 1;
                    }
                    else if (ch == '\'' || ch == '\"')
                    {
                        if (curr > start)
                        {
                            alItems.Add(This.Substring(start, curr - start));
                        }
                        pos2 = This.IndexOf(ch, curr + 1) + 1;
                        if (pos2 <= 0)
                        {
                            alItems.Add(This.Substring(curr));
                            curr = start = This.Length;
                        }
                        else
                        {
                            alItems.Add(This.Substring(curr, pos2 - curr));
                            curr = start = pos2;
                        }
                    }
                }

                if (start < This.Length)
                    alItems.Add(This.Substring(start));
            }

            return alItems;
        }

        /// <summary>
        /// .NET 自带的 IndexOfAny，参数是 char[]，用起来不方便，这边采用 string 作为查找参数。
        /// </summary>
        public static int IndexOfAny(this string This, string anyOf)
        {
            return This.IndexOfAny(anyOf, 0);
        }

        /// <summary>
        /// .NET 自带的 IndexOfAny，参数是 char[]，用起来不方便，这边采用 string 作为查找参数。
        /// </summary>
        public static int IndexOfAny(this string This, string anyOf, int startIndex)
        {
            if (string.IsNullOrEmpty(This))
                return -1;
            if (string.IsNullOrEmpty(anyOf))
                return -1;
            if (startIndex >= This.Length)
                return -1;

            for (; startIndex < This.Length; ++startIndex)
            {
                if (anyOf.IndexOf(This[startIndex]) >= 0)
                    return startIndex;
            }

            return -1;
        }

        /// <summary>
        /// Label/TextBlock 等控件显示文本时，如果内容中有下划线，则此时会被当成快捷键，
        /// 此时需要一个下划线字符，换成两个下划线字符。
        /// </summary>
        public static string NoShortcut(this string This)
        {
            return This.Replace("_", "__");
        }

        /// <summary>
        /// .NET 3.5 中只支持两个字符串的路径合并，不支持多个字符串的路径合并，因此这边重新实现一下该函数。
        /// </summary>
        public static string PathCombine(this string This, params string[] list)
        {
            char[] trimChars = new char[] { '\\' };
            string ret = This.TrimEnd(trimChars);
            foreach (string str in list)
                ret = System.IO.Path.Combine(ret, str.TrimStart(trimChars));
            return ret;
        }

        /// <summary>
        /// 返回指定字符串是否是类似 “FILE0” “FILE1”（Prefix + Index）格式的字符串。
        /// </summary>
        public static bool IsPrefixIndex(this string This, string prefix)
        {
            if (!This.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return false;
            if (This.Length <= prefix.Length)
                return false;

            char digit = This[prefix.Length];
            return (digit >= '0' && digit <= '9');
        }
    }
}
