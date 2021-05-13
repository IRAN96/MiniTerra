using System;
using System.Collections.Generic;
using System.Text;

using Geometria.Geometry;
using Geometria.Attributes;
using Geometria.Enumerates;

namespace Geometria.Layers
{
    /// <summary>
    /// GIS使用的，单个地理实体对象。
    /// </summary>
    public class Feature
    {
        #region 字段
        //几何类型
        private GeometryObjectType geometryType;
        private GeometryObject geometry;
        public AttributeList attributes;
        //todo: Symbols

        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        protected Feature()
        {
            this.geometryType = GeometryObjectType.Null;
        }

        /// <summary>
        /// 给定空间数据和属性数据的构造
        /// <para>几何类型由程序自行判断。</para>
        /// </summary>
        /// <param name="geo">空间数据</param>
        /// <param name="attr">属性数据</param>
        public Feature(GeometryObject geo, AttributeList attr)
        {
            this.SetGeometry(geo);
            this.attributes = attr;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取自身的几何类型
        /// </summary>
        public GeometryObjectType GeometryType
        {
            get
            {
                return this.geometryType;
            }
        }

        /// <summary>
        /// 获取或设置自身的几何信息
        /// </summary>
        public GeometryObject Geometry
        {
            set
            {
                this.SetGeometry(value);
            }

            get
            {
                return this.geometry;
            }
        }

        /// <summary>
        /// 获取自身的ID
        /// </summary>
        public uint ID
        {
            get
            {
                return this.geometry.id;
            }
        }

        /// <summary>
        /// 获取自身的包围盒
        /// </summary>
        public RectBox MBR
        {
            get
            {
                return this.geometry.GetMBR();
            }
        }
        #endregion

        #region 私有方法
        //设置自身的几何对象
        protected void SetGeometry(GeometryObject geo)
        {
            //按geo的种类进行分类
            if (geo.GetType() == Type.GetType("Geometria.Geometry.GeoPoint"))
            {
                this.geometryType = GeometryObjectType.Point;
            }
            else if(geo.GetType() == Type.GetType("Geometria.Geometry.GISPolyline"))
            {
                this.geometryType = GeometryObjectType.Polyline;
            }
            else if (geo.GetType() == Type.GetType("Geometria.Geometry.GISPolygon"))
            {
                this.geometryType = GeometryObjectType.Polygon;
            }
            else if (geo.GetType() == Type.GetType("Geometria.Geometry.GISMultiPoint"))
            {
                this.geometryType = GeometryObjectType.MultiPoint;
            }
            else if (geo.GetType() == Type.GetType("Geometria.Geometry.GISMultiPolyline"))
            {
                this.geometryType = GeometryObjectType.MultiPolyline;
            }
            else if (geo.GetType() == Type.GetType("Geometria.Geometry.GISMultiPolygon"))
            {
                this.geometryType = GeometryObjectType.MultiPolygon;
            }
            else
            {
                throw new Exception("未知几何类型。");
            }

            this.geometry = geo;
        }
        #endregion

        #region 基础方法
        /// <summary>
        /// 获取自身的拷贝
        /// </summary>
        /// <returns></returns>
        public Feature Clone()
        {
            Feature cloned = new Feature();

            //按geometry的种类进行分类
            if (this.geometryType == GeometryObjectType.Point)
            {
                cloned.geometry = ((GeoPoint)this.geometry).Clone();
            }
            else if (this.geometryType == GeometryObjectType.Polyline)
            {
                cloned.geometry = ((GISPolyline)this.geometry).Clone();
            }
            else if (this.geometryType == GeometryObjectType.Polygon)
            {
                cloned.geometry = ((GISPolygon)this.geometry).Clone();
            }
            else if (this.geometryType == GeometryObjectType.MultiPoint)
            {
                cloned.geometry = ((GISMultiPoint)this.geometry).Clone();
            }
            else if (this.geometryType == GeometryObjectType.MultiPolyline)
            {
                cloned.geometry = ((GISMultiPolyline)this.geometry).Clone();
            }
            else if (this.geometryType == GeometryObjectType.MultiPolygon)
            {
                cloned.geometry = ((GISMultiPolygon)this.geometry).Clone();
            }
            else
            {
                throw new Exception("未知几何类型。");
            }

            cloned.geometryType = this.geometryType;
            if(this.attributes != null)
            {
                cloned.attributes = this.attributes.Clone();
            }
            return cloned;
        }

        /// <summary>
        /// 获取自身的包围盒
        /// </summary>
        /// <returns></returns>
        public RectBox GetMBR()
        {
            return this.geometry.GetMBR();
        }
        #endregion
    }
}
