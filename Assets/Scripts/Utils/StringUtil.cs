using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/**
 * 字符串工具
 * 功能为验证过
 * 可能会有问题
 * 用时请验证
 */
namespace Assets.Scripts.Utils
{
    public class StringUtil
    {
        public static string LV1_Split = ",";
        public static string LV2_Split = "";
        private static string NEW_LINE_REPLACER = Convert.ToChar(6).ToString();
        //忽略大小字母比较字符是否相等;
        public static bool EqualsIgnoreCase(string str1, string str2)
        {
            return str1.ToLower() == str2.ToLower();
        }
        //比较字符是否相等;
        public static bool Equals(string str1, string str2)
        {
            return str1 == str2;
        }
        //是否为Email地址;
        public static bool IsEmail(string email)
        {
            if (email == null)
            {
                return false;
            }
            email = Trim(email);
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Match result = Regex.Match(email, pattern);
            return result.Success;
        }

        //是否是数字字符串;
        public static bool IsInteger(string str)
        {
            if (str == null)
            {
                return false;
            }
            int result;
            return int.TryParse(str, out result);
        }

        //是否为Double型数据;
        public static bool IsDouble(string str)
        {
            str = Trim(str);
            if (str == null)
            {
                return false;
            }
            double result;
            return double.TryParse(str, out result);
        }
        //English;
        public static bool IsEnglish(string str)
        {
            if (str == null)
            {
                return false;
            }
            str = Trim(str);
            string pattern = @"/^[A-Za-z]+$/";
            Match result = Regex.Match(str, pattern);
            return result.Success;
        }

        //中文;
        public static bool IsChinese(string str)
        {
            if (null == str)
            {
                return false;
            }
            str = Trim(str);
            string pattern = @"/^[\u0391 -\uFFE5]+$/";
            Match result = Regex.Match(str, pattern);
            return result.Success;
        }

        //含有中文字符
        public static bool HasChineseChar(string str)
        {
            if (null == str)
            {
                return false;
            }
            str = Trim(str);
            string pattern = @"/[^\x00-\xff]/";
            Match result = Regex.Match(str, pattern);
            return result.Success;
        }

        //注册字符;
        public static bool HasAccountChar(string str, uint len)
        {
            if (str == null)
            {
                return false;
            }
            if (len < 10)
            {
                len = 15;
            }
            str = Trim(str);
            var pattern = string.Format(@"^[a-zA-Z0-9][a-zA-Z0-9_-]{0,{0}}$", len);
            Match result = Regex.Match(str, pattern);
            return result.Success;
        }

        //URL地址;
        public static bool IsURL(string str)
        {
            if (null == str)
            {
                return false;
            }
            str = Trim(str).ToLower();
            var pattern = @"/^http\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\\-&_~`@[\]\'+!]*([^<>\'\'])*$/";
            Match result = Regex.Match(str, pattern);
            return result.Success;
        }

        // 是否为空白;        
        public static bool IsWhitespace(string str)
        {
            switch (str)
            {
                case @" ":
                case @"\t":
                case @"\r":
                case @"\n":
                case @"\f":
                    return true;
                default:
                    return false;
            }
        }

        //去左右空格;
        public static string Trim(string str)
        {
            if (str == null)
            {
                return null;
            }
            return Rtrim(Ltrim(str));
        }

        //去左空格; 
        public static string Ltrim(string str)
        {
            if (null == str)
            {
                return null;
            }
            string pattern = @"/^\s*/";
            string result = Regex.Replace(str, pattern, "");
            return result;
        }

        //去右空格;
        public static string Rtrim(string str)
        {
            if (null == str)
            {
                return null;
            }
            string pattern = @"/\s*$/";
            return Regex.Replace(str, pattern, "");
        }

        //是否为前缀字符串;
        public static bool BeginsWith(string str, string prefix)
        {

            return (prefix == str.Substring(0, prefix.Length));
        }

        //是否为后缀字符串;
        public static bool EndsWith(string str, string suffix)
        {
            return (suffix == str.Substring(str.Length - suffix.Length));
        }

        //去除指定字符串;
        public static string Remove(string str, string remove)
        {
            return Regex.Replace(str, remove, "");
        }

        //字符串替换;
        public static string Replace(string str, string replace, string replaceWith)
        {
            string[] strs = str.Split(replace.ToCharArray());
            return string.Join(replaceWith, strs);
        }

        //utf16转utf8编码;
        public static string Utf16to8(string str)
        {
            List<string> outArray = new List<string>();
            int len = str.Length;
            for (int i = 0; i < len; i++)
            {
                int c = Convert.ToByte(i);
                if (c >= 0x0001 && c <= 0x007F)
                {
                    outArray.Add(str.Substring(i, 1));
                }
                else if (c > 0x07FF)
                {
                    string tem = Convert.ToChar(0xE0 | ((c >> 12) & 0x0F)).ToString();
                    tem += Convert.ToChar(((c >> 6) & 0x3F)).ToString();
                    tem += Convert.ToChar(0x80 | ((c >> 0) & 0x3F)).ToString();
                    outArray.Add(tem);
                }
                else
                {
                    string tem = Convert.ToChar(0xC0 | ((c >> 6) & 0x1F)).ToString();
                    tem += Convert.ToChar(0x80 | ((c >> 0) & 0x3F)).ToString();
                    outArray.Add(tem);
                }
            }
            return string.Join("", outArray.ToArray());
        }

        //utf8转utf16编码;
        public static string Utf8to16(string str)
        {
            List<string> outInt = new List<string>();
            int len = str.Length;
            uint i = 0;
            while (i < len)
            {
                int c = Convert.ToByte(i++);
                switch (c >> 4)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        // 0xxxxxxx
                        outInt.Add(Convert.ToByte(i - 1).ToString());
                        break;
                    case 12:
                    case 13:
                        // 110x xxxx   10xx xxxx
                        int char2 = Convert.ToByte(i++);
                        outInt.Add(Convert.ToChar(((c & 0x1F) << 6) | (char2 & 0x3F)).ToString());
                        break;
                    case 14:
                        // 1110 xxxx  10xx xxxx  10xx xxxx
                        int char3 = Convert.ToByte(i++);
                        int char4 = Convert.ToByte(i++);
                        outInt.Add(Convert.ToChar(((c & 0x0F) << 12) | ((char3 & 0x3F) << 6) | ((char4 & 0x3F) << 0)).ToString());
                        break;
                }
            }
            return string.Join("", outInt.ToArray());
        }

        public static string AutoReturn(string str, int c)
        {
            int l = str.Length;
            if (l < 0)
                return "";
            int i = c;
            string r = str.Substring(0, i);
            while (i <= l)
            {
                r += "\n";
                r += str.Substring(i, c);
                i += c;
            }
            return r;
        }

        public static string LimitStringLengthByByteCount(string str, int bc, string strExt = "...")
        {
            if (str == null || str == "")
            {
                return str;
            }
            else
            {
                int l = str.Length;
                int c = 0;
                string r = "";
                for (int i = 0; i < l; ++i)
                {
                    uint code = Convert.ToChar(i);
                    if (code > 0xffffff)
                    {
                        c += 4;
                    }
                    else if (code > 0xffff)
                    {
                        c += 3;
                    }
                    else if (code > 0xff)
                    {
                        c += 2;
                    }
                    else
                    {
                        ++c;
                    }

                    if (c < bc)
                    {
                        r += Convert.ToByte(i);
                    }
                    else if (c == bc)
                    {
                        r += Convert.ToByte(i);
                        r += strExt;
                        break;
                    }
                    else
                    {
                        r += strExt;
                        break;
                    }
                }
                return r;
            }
        }

        public static Array GetCharsArray(string targetString, bool hasBlankSpace)
        {
            string tempString = targetString;
            if (hasBlankSpace == false)
            {
                tempString = Trim(targetString);
            }
            return tempString.Split(' ');
        }

        //private static float CHINESE_MAX = 0x9FFF;
        //private static float CHINESE_MIN = 0x4E00;

        //private static float LOWER_MAX = 0x007A;
        //private static float LOWER_MIN = 0x0061;

        //private static float NUMBER_MAX = 0x0039;
        //private static float NUMBER_MIN = 0x0030;

        //private static float UPPER_MAX = 0x005A;
        //private static float UPPER_MIN = 0x0041;
        /**
         * 返回一段字符串的字节长度（汉字一个字占2，其他占1）
         */
        public static int GetStringBytes(string str)
        {
            return GetStrActualLen(str);
            /*			var nint=0;
                        var lint=str.Length;
                        for(var iint=0; i < l; ++i)
                        {
                            var codeNumber=str.charCodeAt(i);
                            if (code >= CHINESE_MIN && code <= CHINESE_MAX)
                            {
                                n+=2;
                            }
                            else
                            {
                                ++n;
                            }
                        }
                        return n;*/
        }

        /**
         * 按字节长度截取字符串（汉字一个字占2，其他占1）
         */
        public static string SubstrByByteLen(string str, int len)
        {
            if (str == "" || str == null)
                return str;
            int n = 0;
            int l = str.Length;
            for (int i = 0; i < l; ++i)
            {
                string str1 = str.Substring(i);
                n += GetStrActualLen(str1);
                if (n > len)
                {
                    str = str.Substring(0, i - 1);
                    break;
                }
            }
            return str;
        }


        public static int GetStrActualLen(string sChars)
        {
            if (sChars == "" || sChars == null)
                return 0;
            else
                return Regex.Replace(sChars, @"/[^\x00-\xff]/g", "xx").Length;
        }

<<<<<<< HEAD
        public static bool IsEmptyString(string str)
        {
            return str == string.Empty;
        }
=======
    public static bool IsEmptyString(string str)
    {
        return str == string.Empty;
    }
>>>>>>> bb313cad1548109f2c8f9ae50416a8901c7b46c8



        public static bool IsNewlineOrEnter(uint code)
        {
            return code == 13 || code == 10;
        }

        public static string RemoveNewlineOrEnter(string str)
        {
            str = Regex.Replace(str, "\n", "");
            return Regex.Replace(str, "\r", "");
        }

        /**
         * 替换掉文本中的 '\n' 为 '\7'
         */
        public static string EscapeNewline(string txt)
        {
            return Regex.Replace(txt, "\n", NEW_LINE_REPLACER);
        }

        /**
         * 替换掉文本中的 '\7' 为  '\n'
         */
        public static string UnescapeNewline(string txt)
        {
            return Regex.Replace(txt, NEW_LINE_REPLACER, "\n");
        }

        /**
         * 判断哪些是全角字符,如果不含有返回空
         */
        public static string Judge(string s)
        {
            string temps = "";
            bool isContainQj = false;
            for (int i = 0; i < s.Length; i++)
            {
                //半角长度是一，特殊符号长度是三，汉字和全角长度是9
                if (Regex.Escape(s.Substring(i, i + 1)).Length > 3)
                {
                    temps += "'" + s.Substring(i, i + 1) + "' ";
                    isContainQj = true;
                }
            }
            if (isContainQj)
            {
            }
            return temps;
        }

        /**
         * 汉字、全角数字和全角字母都是双字节码，第一个字节的值减去160表示该字在字库中的区
           码，第二个字节的值减去160为位码，如‘啊’的16进制编码为B0   A1，换算成十进制数就是
           176和161，分别减去160后就是16和1，即‘啊’字的区位码是1601，同样数字和字母的区位
           码也是如此，如‘0’是0316，‘1’是0317等，因此判断汉字及全角字符基本上只要看其连
           续的两个字节是否大于160，至于半角字符和数字则更简单了，只要到ASCII码表中查一查就
           知道了。
         * //删除oldstr空格，把全角转换成半角
           //根据汉字字符编码规则：连续两个字节都大于160，
           //全角符号第一字节大部分为163
           //～，全角空格第一字节都是161，不知道怎么区分？
         * /
           /**
         * 把含有的全角字符转成半角
         */
        public static string ChangeToBj(string s)
        {
            if (null == s)
                return null;
            string temps = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (Regex.Escape(s.Substring(i, i + 1)).Length > 3)
                {
                    string temp = s.Substring(i, i + 1);
                    if (Convert.ToChar(temp.Substring(0, 1)) > 60000)
                    {
                        //区别汉字
                        int code = Convert.ToByte(0) - 65248;
                        string newt = Convert.ToChar(code).ToString();
                        temps += newt;
                    }
                    else
                    {
                        if (Convert.ToChar(temp.Substring(0, 1)) == 12288)
                            temps += " ";
                        else
                            temps += s.Substring(i, i + 1);
                    }
                }
                else
                {
                    temps += s.Substring(i, i + 1);
                }
            }
            return temps;
        }

        /**
         * 把含有的半角字符转成全角
         */
        public static string ChangeToQj(string s)
        {
            if (null == s)
                return null;
            string temps = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (Regex.Escape(s.Substring(i, i + 1)).Length > 3)
                {
                    string temp = s.Substring(i, i + 1);
                    if (Convert.ToChar(temp.Substring(0, 1)) > 60000)
                    {
                        //区别汉字
                        int code = Convert.ToByte(0) + 65248;
                        string newt = Convert.ToChar(code).ToString();
                        temps += newt;
                    }
                    else
                    {
                        temps += s.Substring(i, i + 1);
                    }
                }
                else
                {
                    temps += s.Substring(i, i + 1);
                }
            }
            return temps;
        }

        /**
         * 在不够指定长度的字符串前补零
         * @param str
         * @param len
         * @return
         *
         */
        public static string RenewZero(string str, int len)
        {
            string bul = "";
            int strLen = str.Length;
            if (strLen < len)
            {
                for (int i = 0; i < len - strLen; i++)
                {
                    bul += "0";
                }
                return bul + str;
            }
            else
            {
                return str;
            }
        }

        /**
         * 检查字符串是否符合正则表达式
         */
        public static bool IsUpToRegExp(string str, string reg)
        {
            if (str != null && reg != null)
            {

                return Regex.Match(str, reg) != null;
            }
            else
                return false;
        }

        /**
         * 是否含有/0结束符的不正常格式的字符串
         */
        public static bool IsErrorFormatString(string str, int len)
        {
            if (str == null || (len != 0 && str.Length > len))
                return true;
            else
                return str.IndexOf(Convert.ToChar(0)) != -1;
        }

        /**
         * 返回格式化后的金钱字符串,如1000000 -> 1,000,000
         */
        public static string GetFormatMoney(float money)
        {
            string moneyStr = money.ToString();
            List<string> formatMoney = new List<string>();
            for (var index = -1; moneyStr.Substring(moneyStr.Length + index, 1) != string.Empty; index -= 3)
            {
                if (Math.Abs(index - 2) >= moneyStr.Length)
                    formatMoney.Add(moneyStr.Substring(0, moneyStr.Length + index + 1));
                else
                    formatMoney.Add(moneyStr.Substring(index - 2, 3));
            }
            formatMoney.Reverse();
            return string.Join(",", formatMoney.ToArray());
        }

        /**
         * 正整数转为中文数字
         * 最大到十位
         */
        //private static const Array ChineseNumberTable = [ 0x96f6, 0x4e00, 0x4e8c, 0x4e09, 0x56db, 0x4e94, 0x516d, 0x4e03, 0x516b, 0x4e5d, 0x5341 ];
        //public static string uintToChineseNumber(uint u)
        //{
        //    if (u <= 10)
        //    {
        //        return Convert.ToChar(ChineseNumberTable[u]);
        //    }
        //    else
        //    if (u < 20)
        //    {
        //        return Convert.ToChar(ChineseNumberTable[10], ChineseNumberTable[u - 10]);
        //    }
        //    else
        //    if (u < 100)
        //    {
        //        uint t = Math.Floor(u / 10);
        //        uint tt = u % 10;
        //        if (tt > 0)
        //        {
        //            return Convert.ToChar(ChineseNumberTable[t], ChineseNumberTable[10], ChineseNumberTable[tt]);
        //        }
        //        else
        //        {
        //            return Convert.ToChar(ChineseNumberTable[t], ChineseNumberTable[10]);
        //        }
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}




        /**
         * 
         * @param str		需要分析的字符串
         * @param fnOnSplit	分析回调函数  fnOnSplit(string str)void
         * 
         */
        //public static bool Lv1ParseString(string str , Function fnOnSplit) {
        //			if (str == null || str == "") {
        //				return false;
        //			}

        //			var rbool = false;
        //			for each(var tstring in str.split(LV1_Split))
        //{
        //    fnOnSplit(t);
        //    r = true;
        //}
        //			return r;
        //		}

        //		/**
        //		 * 
        //		 * @param str		需要分析的字符串
        //		 * @param fnOnSplit	分析回调函数  fnOnSplit(strSplitsArray<string>)void
        //		 * 
        //		 */		
        //		public static  Lv2ParseString(string str, fnOnSplitFunction)bool {
        //			if (str == null || str == "") {
        //				return false;
        //			}

        //			var rbool = false;
        //			for each(var tstring in str.split(LV2_Split))
        //{
        //    if (t != null && t == "")
        //    {
        //        var aArray = str.split(LV1_Split);
        //        if (a.Length > 1)
        //        {
        //            fnOnSplit(a);
        //            r = true;
        //        }
        //        else
        //        {
        //            //return;
        //        }
        //    }
        //}
        //			return r;
        //		}

        /**
         * 
         * @param infos				信息数组
         * @param fnGetInfoString	fnGetInfoString(infoObject)string
         * @return 
         * 
         */
        //		public static  GetLv1SplitString(infosArray/*<Object>*/, fnGetInfoStringFunction)string {
        //			if (infos == null || infos.Length == 0) {
        //				return "";
        //			}

        //			var lint = infos.Length;
        //var rstring = fnGetInfoString(infos[0]);
        //var iint = 1;
        //			while (i<l) {
        //				r += LV1_Split;
        //				r += fnGetInfoString(infos[i]);
        //				++i;
        //			}
        //			return r;
        //		}

        /**
         * 
         * @param infos				信息数组
         * @param fnGetInfoString	fnGetInfoString(infoObject, strLv2Sepstring)string
         * @return 
         * 
         */
        //		public static  GetLv2SplitString(infosArray/*<Object>*/, fnGetInfoStringFunction)string {
        //			if (infos == null || infos.Length == 0) {
        //				return "";
        //			}

        //			var lint = infos.Length;
        //var rstring = fnGetInfoString(infos[0], LV2_Split);
        //var iint = 1;
        //			while (i<l) {
        //				r += LV1_Split;
        //				r += fnGetInfoString(infos[i], LV2_Split);
        //				++i;
        //			}
        //			return r;
        //		}

        /**
         * 去掉换行符
         * @param $originalTxt
         * @return 
         */
        public static string StripLineBreak(string originalTxt)
        {
            if (null != originalTxt)
            {
                return Regex.Replace(originalTxt, @"/\r|\n/g", "");
            }
            else
            {
                return "";
            }
        }
    }
}