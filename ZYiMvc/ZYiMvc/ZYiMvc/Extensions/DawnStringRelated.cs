// =================================================================== 
// 【宋】常用工具集（DawnXZ.Tools）合集
//====================================================================
//【宋杰军 @Copy Right 2008+】--【联系ＱＱ：6808240】--【请保留此注释】
//====================================================================
// 文件名称：DawnStringRelated.cs
// 项目名称：【宋】常用工具集
// 创建时间：2011-06-15
// 创建人员：宋杰军
// 负 责 人：宋杰军
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace ZY.Extensions
{
    /// <summary>
    /// 字符串相关操作类
    /// </summary>
    public class DawnStringRelated
    {

        #region Variable
        /// <summary>
        /// 回车、换行符
        /// </summary>
        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
        #endregion Variable

        #region 成员方法

        #region 常规方法
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="strValue">清理的字符串</param>
        /// <returns></returns>
        public static string ClearLastChar(string strValue)
        {
            return (string.IsNullOrEmpty(strValue)) ? null : strValue.Substring(0, strValue.Length - 1);
        }
        #endregion 常规方法

        #region 常规检测
        /// <summary>
        /// 查找一个字符串在另一个字符串中出现的次数
        /// </summary>
        /// <param name="strSource">源数据</param>
        /// <param name="strFind">查找数据</param>
        /// <returns>出现次数，默认为 0</returns>
        public static int GetOccurrenceNumber(string strSource, string strFind)
        {
            if (strSource.Length <= 0 | strFind.Length <= 0) return 0;
            int intSum = 0, intTemp = 0;
            do
            {
                intTemp = strSource.IndexOf(strFind, intTemp);
                if (intTemp != -1)
                {
                    intSum++;
                    intTemp += strFind.Length;
                }
            }
            while (intTemp != -1);
            return intSum;
        }
        #endregion 常规检测

        #region 检测字符串
        /// <summary>
        /// 检测字符串中是否存在指定的字符串
        /// </summary>
        /// <param name="str">要查找的字符串</param>
        /// <param name="stringarray">原字符串</param>
        /// <param name="strsplit">分隔标记</param>
        /// <returns>存在 / 不存在</returns>
        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if (string.IsNullOrEmpty(stringarray)) return false;
            str = str.ToLower();
            string[] stringArray = SplitString(stringarray.ToLower(), strsplit);
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (str.IndexOf(stringArray[i]) > -1) return true;
            }
            return false;
        }
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower()) return i;
                }
                else if (strSearch == stringArray[i])
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }
        #endregion 检测字符串

        #region 获取指定类型值
        /// <summary>
        /// 获取字符串中的数字
        /// </summary>
        /// <param name="strString">字符串</param>
        /// <returns></returns>
        public static int GetNumber(string strString)
        {
            int tmpVal = 0;
            try
            {
                Match m = Regex.Match(strString, @"\d+");
                if (m.Success) tmpVal = DawnTypeConverter.TypeToInt32(m.Value, 0);
            }
            catch { }
            return tmpVal;
        }
        /// <summary>
        /// 获取字符串中的汉字
        /// </summary>
        /// <param name="strString">字符串</param>
        /// <returns></returns>
        public static string GetChinese(string strString)
        {
            string tmpVal = null;
            try
            {
                Match m = Regex.Match(strString, @"[\u0391-\uFFE5/]+");
                if (m.Success) tmpVal = m.Value;
            }
            catch { }
            return tmpVal;
        }
        /// <summary>
        /// 获取汉字第一个拼音
        /// 所有汉字
        /// </summary>
        /// <param name="strChinese">汉字</param>
        /// <returns></returns>
        public static string GetSpellOfAll(string strChinese)
        {
            int len = strChinese.Length;
            string reVal = null;
            for (int i = 0; i < len; i++)
            {
                reVal += GetSpellOfFirst(strChinese.Substring(i, 1));
            }
            return reVal;
        }
        /// <summary>
        /// 获取汉字第一个拼音
        /// 仅第一个
        /// </summary>
        /// <param name="strChinese">汉字</param>
        /// <returns></returns>
        public static string GetSpellOfFirst(string strChinese)
        {
            byte[] arrCN = Encoding.Default.GetBytes(strChinese);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else
            {
                return strChinese;
            }
        }
        #endregion 获取指定类型值

        #region 清除回车、换行、空格
        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }
        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Match m = null;
            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }
        #endregion 清除回车、换行、空格

        #region 分割字符串
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">要分割的字符串</param>
        /// <param name="strSplit">分割标识</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0) return new string[] { strContent };
                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
            {
                return new string[0] { };
            }
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">要分割的字符串</param>
        /// <param name="strSplit">分割标识</param>
        /// <param name="count">数组大小</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] result = new string[count];
            string[] splited = SplitString(strContent, strSplit);
            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                {
                    result[i] = splited[i];
                }
                else
                {
                    result[i] = string.Empty;
                }
            }
            return result;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem)
        {
            return SplitString(strContent, strSplit, ignoreRepeatItem, 0);
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);
            return ignoreRepeatItem ? DistinctStringArray(result, maxElementLength) : result;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="minElementLength">单个元素最小长度</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int minElementLength, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);
            if (ignoreRepeatItem) result = DistinctStringArray(result);
            return PadStringArray(result, minElementLength, maxElementLength);
        }
        #endregion 分割字符串

        #region 替换字符串
        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        /// <param name="SourceString">源字符串</param>
        /// <param name="SearchString">要查找的值</param>
        /// <param name="ReplaceString">替换字符串</param>
        /// <param name="IsCaseInsensetive">是否忽略大小写</param>
        /// <returns>字符串</returns>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }
        #endregion 替换字符串

        #region 清除重复项
        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns>字符串数组</returns>
        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }
        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns>字符串数组</returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            Hashtable h = new Hashtable();
            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }
            string[] result = new string[h.Count];
            h.Keys.CopyTo(result, 0);
            return result;
        }
        #endregion 清除重复项

        #region 过滤数组元素
        /// <summary>
        /// 过滤字符串数组中每个元素为合适的大小
        /// 当长度小于minLength时，忽略掉,-1为不限制最小长度
        /// 当长度大于maxLength时，取其前maxLength位
        /// 如果数组中有null元素，会被忽略掉
        /// </summary>
        /// <param name="strArray">原始数组</param>
        /// <param name="minLength">单个元素最小长度</param>
        /// <param name="maxLength">单个元素最大长度</param>
        /// <returns>字符串数组</returns>
        public static string[] PadStringArray(string[] strArray, int minLength, int maxLength)
        {
            if (minLength > maxLength)
            {
                int t = maxLength;
                maxLength = minLength;
                minLength = t;
            }
            int iMiniStringCount = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (minLength > -1 && strArray[i].Length < minLength)
                {
                    strArray[i] = null;
                    continue;
                }
                if (strArray[i].Length > maxLength) strArray[i] = strArray[i].Substring(0, maxLength);
                iMiniStringCount++;
            }
            string[] result = new string[iMiniStringCount];
            for (int i = 0, j = 0; i < strArray.Length && j < result.Length; i++)
            {
                if (strArray[i] != null && strArray[i] != string.Empty)
                {
                    result[j] = strArray[i];
                    j++;
                }
            }
            return result;
        }
        #endregion 过滤数组元素

        #region Intercept 截取字符串

        #region 截取并替换
        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string InterceptGetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return InterceptGetSubString(p_SrcString, 0, p_Length, p_TailString);
        }
        /// <summary>
        /// 取指定长度的字符串超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string InterceptGetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;
            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                    {
                        return null;
                    }
                    else
                    {
                        return p_SrcString.Substring(p_StartIndex, ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                    }
                }
            }
            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);
                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;
                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {
                        //当不在有效范围内时,只取到字符串的结尾
                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }
                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;
                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3) nFlag = 1;
                        }
                        else
                        {
                            nFlag = 0;
                        }
                        anResultFlag[i] = nFlag;
                    }
                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1)) nRealLength = p_Length + 1;
                    bsResult = new byte[nRealLength];
                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);
                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }
            return myResult;
        }
        /// <summary>
        /// 取指定长度的字符串超出的部分用指定字符串代替
        /// Unicode 字符集
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="len">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的 Unicode 字符串</returns>
        public static string GetUnicodeSubString(string str, int len, string p_TailString)
        {
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                    {
                        byteCount += 2;
                    }
                    else
                    {
                        byteCount += 1; // 按英文字符计算加1
                    }
                    if (byteCount > len)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == len)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }
                if (pos >= 0) result = str.Substring(0, pos) + p_TailString;
            }
            else
            {
                result = str;
            }
            return result;
        }
        #endregion 截取并替换

        #region 标题相关
        /// <summary>
        /// 截断过长标题，中英文通用
        /// </summary>
        public static string InterceptByTitles(string strTitle, int strLength)
        {
            string temp = strTitle;
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= strLength) return temp;
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i) + "...";
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= strLength - 1) return temp + "";
            }
            return "";
        }
        /// <summary>
        /// 截断过长标题，默认以“...”代替
        /// </summary>
        public static string InterceptByTitle(string strTitle, int strLength)
        {
            return InterceptByTitle(strTitle, strLength, "...");
        }
        /// <summary>
        /// 截断过长标题，并以指定字符串替换
        /// </summary>
        public static string InterceptByTitle(string strTitle, int strLength, string strReplace)
        {
            if (strTitle.Length >= strLength + 1)
            {
                strTitle = strTitle.Substring(0, strLength) + strReplace;
                return strTitle;
            }
            else
            {
                return strTitle;
            }
        }
        #endregion 标题相关

        #region 截取指定长度
        /// <summary>
        /// 从字符串的指定位置开始截取到字符串的结尾
        /// </summary>
        /// <param name="strValue">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string InterceptCutString(string strValue, int startIndex)
        {
            return InterceptCutString(strValue, startIndex, strValue.Length);
        }
        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="strValue">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string InterceptCutString(string strValue, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }
                if (startIndex > strValue.Length) return "";
            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            if (strValue.Length - startIndex < length) length = strValue.Length - startIndex;
            return strValue.Substring(startIndex, length);
        }
        #endregion 截取指定长度

        #endregion Intercept 截取字符串

        #region 字符串转成整型数组
        /// <summary>
        /// 字符串转成整型数组
        /// </summary>
        /// <param name="idList">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static int[] StringToIntArray(string idList)
        {
            return StringToIntArray(idList, -1);
        }
        /// <summary>
        /// 字符串转成整型数组
        /// </summary>
        /// <param name="idList">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int[] StringToIntArray(string idList, int defValue)
        {
            if (string.IsNullOrEmpty(idList)) return null;
            string[] strArr = DawnStringRelated.SplitString(idList, ",");
            int[] intArr = new int[strArr.Length];
            for (int i = 0; i < strArr.Length; i++) intArr[i] = DawnTypeConverter.TypeToInt32(strArr[i], defValue);
            return intArr;
        }
        #endregion 字符串转成整型数组

        #region SQL 相关
        /// <summary>
        /// 改正sql语句中的转义字符
        /// </summary>
        /// <param name="strvalues">输入值</param>
        public static string SqlMash(string strvalues)
        {
            return (string.IsNullOrEmpty(strvalues)) ? null : strvalues.Replace("\'", "'");
        }
        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        /// <param name="strvalues">输入值</param>
        public static string SqlSwitch(string strvalues)
        {
            return (string.IsNullOrEmpty(strvalues)) ? null : strvalues.Replace("'", "''");
        }
        #endregion SQL 相关

        #region 比较两个Byte[]是否相等

        /// <summary>
        /// 比较两个字节数组是否相等
        /// </summary>
        /// <param name="b1">byte数组1</param>
        /// <param name="b2">byte数组2</param>
        /// <returns>是否相等</returns>
        public static bool PasswordEquals(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        #endregion

        #endregion 成员方法

    }
}
