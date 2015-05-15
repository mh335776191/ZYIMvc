using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Comvert 的摘要说明
/// </summary>
public class Comvert
{

    #region 成员方法
    #region string
    /// <summary>
    /// 字符串如果为空，取默认值
    /// </summary>
    /// <param name="strBase">原始字符串</param>
    /// <param name="strDefault">默认字符串</param>
    /// <returns></returns>
    public static string GetDefaultString(string strBase, string strDefault)
    {
        //如果传入的字符串为空 则返回空串
        if (strBase == null || strBase == string.Empty || strBase == "&nbsp;")
        {
            return strDefault;
        }
        return strBase;
    }

    public static string GetString(object obj)
    {
        if ((obj == null) || (obj == DBNull.Value))
        {
            return string.Empty;
        }
        return obj.ToString().Trim();
    }
    #endregion

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

    #region double
    /// <summary>
    /// 将对象转换为 Decimal 类型
    /// </summary>
    /// <param name="objValue">要转换的值</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的 Decimal 类型结果</returns>
    public static double TypeToDouble(object objValue, double defValue)
    {
        if ((objValue == null)) return defValue;
        return TypeToDouble(objValue.ToString(), defValue);
    }
    /// <summary>
    /// 将对象转换为 Decimal 类型
    /// <remarks>
    /// 默认返回 0
    /// </remarks>
    /// </summary>
    /// <param name="objValue">要转换的值</param>
    /// <returns>转换后的 Decimal 类型结果</returns>
    public static double TypeToDouble(object objValue)
    {
        return TypeToDouble(objValue.ToString(), 0);
    }
    /// <summary>
    /// 将对象转换为 Decimal 类型
    /// <remarks>
    /// 默认返回 0
    /// </remarks>
    /// </summary>
    /// <param name="strValue">要转换的值</param>
    /// <returns>转换后的 Decimal 类型结果</returns>
    public static double TypeToDouble(string strValue)
    {
        if ((strValue == null)) return 0;
        return TypeToDouble(strValue, 0);
    }
    /// <summary>
    /// 将对象转换为 Decimal 类型
    /// </summary>
    /// <param name="strValue">要转换的值</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的 Decimal 类型结果</returns>
    public static double TypeToDouble(string strValue, double defValue)
    {
        if ((strValue == null) || (strValue.Length > 10)) return defValue;
        double intValue = defValue;
        if (strValue != null)
        {
            bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
            if (IsFloat) double.TryParse(strValue, out intValue);
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

    #region GetDistance
    //根据两点经纬度计算距离
    private static double rad(double d)
    {
        return d * Math.PI / 180.0;
    }
    /// <summary>
    /// 计算两点间的距离
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="lng1"></param>
    /// <param name="lat2"></param>
    /// <param name="lng2"></param>
    /// <returns></returns>
    public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
    {
        double EARTH_RADIUS = 6378.137;//地球半径
        double radLat1 = rad(lat1);
        double radLat2 = rad(lat2);
        double a = radLat1 - radLat2;
        double b = rad(lng1) - rad(lng2);
        double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
        result = Math.Round(result * EARTH_RADIUS * 10000) / 10;
        return result;
    }
    ////上面的方法等价的SQL语句，如坐标22.551743,113.937363
    //select Name,(
    //ACOS(
    //SIN((22.551743 * 3.1415) / 180 ) * SIN((latitude * 3.1415) / 180 ) +
    //COS((22.551743 * 3.1415) / 180 ) * COS((latitude * 3.1415) / 180 ) *
    //COS((113.937363 * 3.1415) / 180 - (longitude * 3.1415) / 180 )
    //) * 6378.137
    //) as distance
    //from Shop 
    //where ID!=1 and Latitude != 22.551819 
    //order by distance asc
    #endregion GetDistance

    #region Cache
    /// <summary>  
    /// 获取当前应用程序指定CacheKey的Cache值  
    /// </summary>  
    /// <param name="CacheKey"></param>  
    /// <returns></returns>  
    public static object GetCache(string CacheKey)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        return objCache[CacheKey];
    }

    /// <summary>  
    /// 查询指定CacheKey的Cache值是否存在  
    /// </summary>  
    /// <param name="CacheKey"></param>  
    /// <returns></returns>  
    public static bool IsExistsCache(string CacheKey)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        if (objCache[CacheKey] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>  
    /// 设置当前应用程序指定CacheKey的Cache值  
    /// </summary>  
    /// <param name="CacheKey"></param>  
    /// <param name="objObject"></param>  
    public static void SetCache(string CacheKey, object objObject)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject);
    }

    /// <summary>  
    /// 设置当前应用程序指定CacheKey的Cache值  
    /// </summary>  
    /// <param name="CacheKey"></param>  
    /// <param name="objObject"></param>  
    public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
    }

    /// <summary>  
    /// 设置数据缓存  
    /// </summary>  
    public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
    }

    /// <summary>  
    /// 移除指定数据缓存  
    /// </summary>  
    public static void RemoveAllCache(string CacheKey)
    {
        System.Web.Caching.Cache _cache = HttpRuntime.Cache;
        _cache.Remove(CacheKey);
    }
    /// <summary>  
    /// 移除全部缓存  
    /// </summary>  
    public static void RemoveAllCache()
    {
        System.Web.Caching.Cache _cache = HttpRuntime.Cache;
        IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
        while (CacheEnum.MoveNext())
        {
            _cache.Remove(CacheEnum.Key.ToString());
        }
    }
    #endregion cache
    #endregion 成员方法
}