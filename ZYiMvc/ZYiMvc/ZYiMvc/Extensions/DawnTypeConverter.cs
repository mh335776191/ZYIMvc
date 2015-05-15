// =================================================================== 
// 【宋】常用工具集（DawnXZ.Tools）合集
//====================================================================
//【宋杰军 @Copy Right 2008+】--【联系ＱＱ：6808240】--【请保留此注释】
//====================================================================
// 文件名称：DawnTypeParse.cs
// 项目名称：【宋】常用工具集
// 创建时间：2011-06-12
// 创建人员：宋杰军
// 负 责 人：宋杰军
// 版权申明：本类方法由 Discuz!NT 3.5.2 源码获得，并进行了功能升级
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;
using System.Text.RegularExpressions;
using System.Drawing;
using Microsoft.VisualBasic;

namespace ZY.Extensions
{
    /// <summary>
    /// 数据类型转换器
    /// </summary>
    public class DawnTypeConverter
    {

        #region 成员方法

        #region String
        /// <summary>
        /// 将对象转换为 Boolean 类型
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Boolean 类型结果</returns>
        public static string TypeToString(string strValue, string defValue)
        {
            if (!string.IsNullOrEmpty(strValue))
            {
                return strValue;
            }
            return defValue;
        }
        #endregion String

        #region Boolean
        /// <summary>
        /// 将对象转换为 Boolean 类型
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Boolean 类型结果</returns>
        public static bool TypeToBoolean(object objValue, bool defValue)
        {
            if (objValue != null) return TypeToBoolean(objValue, defValue);
            return defValue;
        }
        /// <summary>
        /// 将对象转换为 Boolean 类型
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Boolean 类型结果</returns>
        public static bool TypeToBoolean(string strValue, bool defValue)
        {
            if (strValue != null)
            {
                if (string.Compare(strValue.ToLower(), "true", true) == 0) return true;
                if (string.Compare(strValue.ToLower(), "false", true) == 0) return false;
            }
            return defValue;
        }
        #endregion Boolean

        #region TinyInt
        /// <summary>
        /// 将对象转换为 TinyInt 类型
        /// 默认返回 0
        /// 范围：0 — 255
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <returns>转换后的 TinyInt 类型结果</returns>
        public static byte TypeToTinyInt(object objValue)
        {
            return TypeToTinyInt(objValue, 0);
        }
        /// <summary>
        /// 将对象转换为 TinyInt 类型
        /// 范围：0 — 255
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 TinyInt 类型结果</returns>
        public static byte TypeToTinyInt(object objValue, byte defValue)
        {
            if (objValue != null) return TypeToTinyInt(objValue.ToString(), defValue);
            return defValue;
        }
        /// <summary>
        /// 将对象转换为 TinyInt 类型
        /// 默认返回 0
        /// 范围：0 — 255
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <returns>转换后的 TinyInt 类型结果</returns>
        public static byte TypeToTinyInt(string strValue)
        {
            return TypeToTinyInt(strValue, 0);
        }
        /// <summary>
        /// 将对象转换为 TinyInt 类型
        /// 范围：0 — 255
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 TinyInt 类型结果</returns>
        public static byte TypeToTinyInt(string strValue, byte defValue)
        {
            if (string.IsNullOrEmpty(strValue) || strValue.Trim().Length > 3 || !DawnValidator.NumIsByteTinyint(strValue)) return defValue;
            byte rv;
            if (Byte.TryParse(strValue, out rv)) return rv;
            //return Convert.ToTinyInt(TypeToFloat(strValue, defValue));
            try
            {
                return Convert.ToByte(strValue);
            }
            catch
            {
                return defValue;
            }
        }
        #endregion TinyInt

        #region Int16
        /// <summary>
        /// 将对象转换为 Int16 类型
        /// 默认返回 0
        /// 范围：-32,768 — 32,767
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <returns>转换后的 Int16 类型结果</returns>
        public static short TypeToInt16(object objValue)
        {
            return TypeToInt16(objValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Int16 类型
        /// 范围：-32,768 — 32,767
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Int16 类型结果</returns>
        public static short TypeToInt16(object objValue, short defValue)
        {
            if (objValue != null) return TypeToInt16(objValue.ToString(), defValue);
            return defValue;
        }
        /// <summary>
        /// 将对象转换为 Int16 类型
        /// 默认返回 0
        /// 范围：-32,768 — 32,767
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <returns>转换后的 Int16 类型结果</returns>
        public static short TypeToInt16(string strValue)
        {
            return TypeToInt16(strValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Int16 类型
        /// 范围：-32,768 — 32,767
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Int16 类型结果</returns>
        public static short TypeToInt16(string strValue, short defValue)
        {
            if (string.IsNullOrEmpty(strValue) || strValue.Trim().Length > 6 || !Regex.IsMatch(strValue.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$")) return defValue;
            short rv;
            if (Int16.TryParse(strValue, out rv)) return rv;
            //return Convert.ToInt16(TypeToFloat(strValue, defValue));
            try
            {
                return Convert.ToInt16(strValue);
            }
            catch
            {
                return defValue;
            }
        }
        #endregion Int16

        #region Int32
        /// <summary>
        /// 将对象转换为 Int32 类型
        /// 默认返回 0
        /// 范围：-2,147,483,648 — 2,147,483,647
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <returns>转换后的 Int32 类型结果</returns>
        public static int TypeToInt32(object objValue)
        {
            return TypeToInt32(objValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Int32 类型
        /// 范围：-2,147,483,648 — 2,147,483,647
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Int32 类型结果</returns>
        public static int TypeToInt32(object objValue, int defValue)
        {
            if (objValue != null) return TypeToInt32(objValue.ToString(), defValue);
            return defValue;
        }
        /// <summary>
        /// 将对象转换为 Int32 类型
        /// 默认返回 0
        /// 范围：-2,147,483,648 — 2,147,483,647
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <returns>转换后的 Int32 类型结果</returns>
        public static int TypeToInt32(string strValue)
        {
            return TypeToInt32(strValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Int32 类型
        /// 范围：-2,147,483,648 — 2,147,483,647
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Int32 类型结果</returns>
        public static int TypeToInt32(string strValue, int defValue)
        {
            if (string.IsNullOrEmpty(strValue) || strValue.Trim().Length > 11 || !Regex.IsMatch(strValue.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$")) return defValue;
            int rv;
            if (Int32.TryParse(strValue, out rv)) return rv;
            return Convert.ToInt32(TypeToFloat(strValue, defValue));
        }
        #endregion Int32

        #region Int64
        /// <summary>
        /// 将对象转换为 Int64 类型
        /// 默认返回 0
        /// 范围：-9,223,372,036,854,775,808 — 9,223,372,036,854,775,807
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <returns>转换后的 Int64 类型结果</returns>
        public static long TypeToInt64(object objValue)
        {
            return TypeToInt64(objValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Int64 类型
        /// 范围：-9,223,372,036,854,775,808 — 9,223,372,036,854,775,807
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Int64 类型结果</returns>
        public static long TypeToInt64(object objValue, long defValue)
        {
            if (objValue != null) return TypeToInt64(objValue.ToString(), defValue);
            return defValue;
        }
        /// <summary>
        /// 将对象转换为 Int64 类型
        /// 默认返回 0
        /// 范围：-9,223,372,036,854,775,808 — 9,223,372,036,854,775,807
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <returns>转换后的 Int64 类型结果</returns>
        public static long TypeToInt64(string strValue)
        {
            return TypeToInt64(strValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Int64 类型
        /// 范围：-9,223,372,036,854,775,808 — 9,223,372,036,854,775,807
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Int64 类型结果</returns>
        public static long TypeToInt64(string strValue, long defValue)
        {
            if (string.IsNullOrEmpty(strValue) || strValue.Trim().Length > 20 || !Regex.IsMatch(strValue.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$")) return defValue;
            long rv;
            if (Int64.TryParse(strValue, out rv)) return rv;
            //return Convert.ToInt64(TypeToFloat(strValue, defValue));
            try
            {
                return Convert.ToInt64(strValue);
            }
            catch
            {
                return defValue;
            }
        }
        #endregion Int64

        #region Float
        /// <summary>
        /// 将对象转换为 Float 类型
        /// 范围：- 1.79E+308 to -2.23E-308, 0 and 2.23E-308 to 1.79E+308
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Float 类型结果</returns>
        public static float TypeToFloat(object objValue, float defValue)
        {
            if ((objValue == null)) return defValue;
            return TypeToFloat(objValue.ToString(), defValue);
        }
        /// <summary>
        /// 将对象转换为 Float 类型
        /// 默认返回 0
        /// 范围：- 1.79E+308 to -2.23E-308, 0 and 2.23E-308 to 1.79E+308
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <returns>转换后的 Float 类型结果</returns>
        public static float TypeToFloat(object objValue)
        {
            return TypeToFloat(objValue.ToString(), 0);
        }
        /// <summary>
        /// 将对象转换为 Float 类型
        /// 默认返回 0
        /// 范围：- 1.79E+308 to -2.23E-308, 0 and 2.23E-308 to 1.79E+308
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <returns>转换后的 Float 类型结果</returns>
        public static float TypeToFloat(string strValue)
        {
            if ((strValue == null)) return 0;
            return TypeToFloat(strValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Float 类型
        /// 范围：- 1.79E+308 to -2.23E-308, 0 and 2.23E-308 to 1.79E+308
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Float 类型结果</returns>
        public static float TypeToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10)) return defValue;
            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat) float.TryParse(strValue, out intValue);
            }
            return intValue;
        }
        #endregion Float

        #region Decimal
        /// <summary>
        /// 将对象转换为 Decimal 类型
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Decimal 类型结果</returns>
        public static decimal TypeToDecimal(object objValue, decimal defValue)
        {
            if ((objValue == null)) return defValue;
            return TypeToDecimal(objValue.ToString(), defValue);
        }
        /// <summary>
        /// 将对象转换为 Decimal 类型
        /// <remarks>
        /// 默认返回 0
        /// </remarks>
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <returns>转换后的 Decimal 类型结果</returns>
        public static decimal TypeToDecimal(object objValue)
        {
            return TypeToDecimal(objValue.ToString(), 0);
        }
        /// <summary>
        /// 将对象转换为 Decimal 类型
        /// <remarks>
        /// 默认返回 0
        /// </remarks>
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <returns>转换后的 Decimal 类型结果</returns>
        public static decimal TypeToDecimal(string strValue)
        {
            if ((strValue == null)) return 0;
            return TypeToDecimal(strValue, 0);
        }
        /// <summary>
        /// 将对象转换为 Decimal 类型
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 Decimal 类型结果</returns>
        public static decimal TypeToDecimal(string strValue, decimal defValue)
        {
            if ((strValue == null) || (strValue.Length > 10)) return defValue;
            decimal intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat) decimal.TryParse(strValue, out intValue);
            }
            return intValue;
        }
        #endregion decimal

        #region DateTime
        /// <summary>
        /// 将对象转换为 DateTime 类型
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static DateTime StrToDateTime(int year)
        {
            return new DateTime(year, 1, 1);
        }
        /// <summary>
        /// 将对象转换为 DateTime 类型
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public static DateTime StrToDateTime(int year, int month)
        {
            return new DateTime(year, month, 1);
        }
        /// <summary>
        /// 将对象转换为 DateTime 类型
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="day">日期</param>
        /// <returns></returns>
        public static DateTime StrToDateTime(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }
        /// <summary>
        /// 将对象转换为 DateTime 类型
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 DateTime 类型结果</returns>
        public static DateTime StrToDateTime(string strValue, DateTime defValue)
        {
            if (!string.IsNullOrEmpty(strValue))
            {
                DateTime dateTime;
                if (DateTime.TryParse(strValue, out dateTime)) return dateTime;
            }
            return defValue;
        }
        /// <summary>
        /// 将对象转换为 DateTime 类型
        /// 默认返回 当前时间
        /// </summary>
        /// <param name="strValue">要转换的值</param>
        /// <returns>转换后的 DateTime 类型结果</returns>
        public static DateTime StrToDateTime(string strValue)
        {
            return StrToDateTime(strValue, DateTime.Now);
        }
        /// <summary>
        /// 将对象转换为 DateTime 类型
        /// 默认返回 当前时间
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <returns>转换后的 DateTime 类型结果</returns>
        public static DateTime ObjectToDateTime(object objValue)
        {
            return StrToDateTime(objValue.ToString());
        }
        /// <summary>
        /// 将对象转换为 DateTime 类型
        /// </summary>
        /// <param name="objValue">要转换的值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的 DateTime 类型结果</returns>
        public static DateTime ObjectToDateTime(object objValue, DateTime defValue)
        {
            return StrToDateTime(objValue.ToString(), defValue);
        }
        #endregion DateTime

        #region Color
        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="strColor">颜色字符串</param>
        /// <returns></returns>
        public static Color ToColor(string strColor)
        {
            int red, green, blue = 0;
            char[] rgb;
            strColor = strColor.TrimStart('#');
            strColor = Regex.Replace(strColor.ToLower(), "[g-zG-Z]", "");
            switch (strColor.Length)
            {
                case 3:
                    rgb = strColor.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                case 6:
                    rgb = strColor.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                default:
                    return Color.FromName(strColor);
            }
        }
        #endregion Color

        #region 特殊数值转换
        /// <summary>
        /// 将long型数值转换为Int32类型
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int SafeInt32(object objNum)
        {
            if (objNum == null) return 0;
            string strNum = objNum.ToString();
            if (DawnValidator.NumIsNumeric(strNum))
            {
                if (strNum.ToString().Length > 9)
                {
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }
                return Int32.Parse(strNum);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }
        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824) return ((double)(bytes / 1073741824)).ToString("0") + "G";
            if (bytes > 1048576) return ((double)(bytes / 1048576)).ToString("0") + "M";
            if (bytes > 1024) return ((double)(bytes / 1024)).ToString("0") + "K";
            return bytes.ToString() + "Bytes";
        }
        #endregion 特殊数值转换

        #region Base64
        /// <summary>
        /// 将普通字符串转换为Base64字符串
        /// </summary>
        /// <param name="theStr">待转换字符串</param>
        /// <returns>Base64字符串</returns>
        public static string ToBase64Encode(string theStr)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(theStr)).Replace("+", "%2B");
        }
        /// <summary>
        /// 将Base64字符串转换为普通字符串
        /// </summary>
        /// <param name="theStr">待转换字符串</param>
        /// <returns>普通字符串</returns>
        public static string ToBase64Decode(string theStr)
        {
            return System.Text.Encoding.Default.GetString(Convert.FromBase64String(theStr.Replace("%2B", "+")));
        }
        #endregion Base64

        #endregion 成员方法

    }
}
