using System;
using System.Collections.Generic;
using System.Text;

namespace Geometria.Enumerates
{
    /// <summary>
    /// 要素数据类型枚举量
    /// </summary>
    public enum AttributeDataType
    {
        /// <summary>
        /// 单字节整数
        /// </summary>
        Byte,
        /// <summary>
        /// 双字节整数
        /// </summary>
        Int16,
        /// <summary>
        /// 四字节整数
        /// </summary>
        Int32,
        /// <summary>
        /// 八字节整数
        /// </summary>
        Int64,
        /// <summary>
        /// 单精度浮点数
        /// </summary>
        Single,
        /// <summary>
        /// 双精度浮点数
        /// </summary>
        Double,
        /// <summary>
        /// 字符串
        /// </summary>
        String
    }

    /// <summary>
    /// 地理对象的几何类型枚举量
    /// </summary>
    public enum GeometryObjectType
    {
        /// <summary>
        /// 单点
        /// </summary>
        Point,
        /// <summary>
        /// 折线段
        /// </summary>
        Polyline,
        /// <summary>
        /// 多边形
        /// </summary>
        Polygon,
        /// <summary>
        /// 点集
        /// </summary>
        MultiPoint,
        /// <summary>
        /// 折线段集
        /// </summary>
        MultiPolyline,
        /// <summary>
        /// 多边形集
        /// </summary>
        MultiPolygon,
        /// <summary>
        /// 啥都不是
        /// </summary>
        Null
    }
}
