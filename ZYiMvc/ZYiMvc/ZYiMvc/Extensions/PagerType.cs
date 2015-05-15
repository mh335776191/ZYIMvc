using System;

namespace ZY.Extensions
{
    /// <summary>
    /// 数据分页模式
    /// </summary>
    public enum PagerType
    {
        /// <summary>
        /// Ajax模式，Json数据模式
        /// </summary>
        Ajax,
        /// <summary>
        /// MVC 模式，Json数据模式
        /// </summary>
        Mvc,
        /// <summary>
        /// URL 链接传参，URL必须静态化
        /// </summary>
        Url
    }
}
