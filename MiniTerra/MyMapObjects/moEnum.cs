using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 值类型常数
    /// </summary>
    public enum moValueTypeConstant
    {
        dInt16=0,
        dInt32=1,
        dInt64=2,
        dSingle=3,
        dDouble=4,
        dText=5
    }
    /// <summary>
    /// 符号类型常数
    /// </summary>
    public enum moSymbolTypeConstant
    {
        SimpleMarkerSymbol=0,
        SimpleLineSymbol=1,
        SimpleFillSymbol=2
    }
    /// <summary>
    /// 简单点符号形状常数
    /// </summary>
    public enum moSimpleMarkerSymbolStyleConstant
    {
        Circle=0,
        SolidCircle=1,
        Triangle=2,
        SolidTriangle=3,
        Square=4,
        SolidSquare=5,
        CircleDot=6,
        CircleCircle=7
    }
    /// <summary>
    /// 简单线符号形状常数
    /// </summary>
    public enum moSimpleLineSymbolStyleConstant
    {
        Solid=0,
        Dash=1,
        Dot=2,
        DashDot=3,
        DashDotDot=4
    }
    /// <summary>
    /// 几何类型常数
    /// </summary>
    public enum moGeometryTypeConstant
    {
        Point=0,
        MultiPolyline=1,
        MultiPolygon=2
    }
    /// <summary>
    /// 渲染类型常数
    /// </summary>
    public enum moRendererTypeConstant
    {
        Simple=0,
        UniqueValue=1,
        ClassBreaks=2
    }
    public enum moTextSymbolAlignmentConstant
    {
        TopLeft=0,
        TopCenter=1,
        TopRight=2,
        CenterLeft=3,
        CenterCenter=4,
        CenterRight=5,
        BottomLeft=6,
        BottomCenter=7,
        BottomRight=8
    }

    public enum CoordinateSystemTypeConstant
    {
        GeographicCoordinate = 0,
        ProjectedCoordinate = 1
    }

    public enum AngleUnitType
    {
        Radian=0,
        Degree=1
    }

    public enum ProjectionType
    {
        Lambert=0,
        Albert=1,
        IsometricObliqueAzimuthProjection=2
    }

    public enum LinearUnitType
    {
        Meter=0
    }
}
