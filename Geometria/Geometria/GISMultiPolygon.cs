using System;
using System.Collections.Generic;
using System.Text;

namespace Geometria.Geometry
{
    /// <summary>
    /// 用于GIS的，double类型的多边形集合。
    /// <para>继承自：GeometryMultiObject</para>
    /// </summary>
    public class GISMultiPolygon: GeometryMultiObject
    {
        #region 字段
        //所有多边形的数据
        protected List<GISPolygon> polygonList;
        protected RectBox mbr;
        protected bool mbrUpdateRequired = false;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public GISMultiPolygon()
        {
            this.id = 0;
            this.polygonList = new List<GISPolygon>();
            this.mbr = new RectBox();
        }

        /// <summary>
        /// 给定id和折线列表数据的构造
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gonList"></param>
        public GISMultiPolygon(uint id, List<GISPolygon> gonList)
        {
            this.id = id;
            this.polygonList = new List<GISPolygon>();
            foreach (GISPolygon polygon in gonList)
            {
                this.polygonList.Add(polygon.Clone());
            }
        }

        /// <summary>
        /// 给定id和单个折线段的构造。该MultiPolyline对象只有一条折线。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="polygon"></param>
        public GISMultiPolygon(uint id, GISPolygon polygon)
        {
            this.id = id;
            this.polygonList = new List<GISPolygon>();
            this.polygonList.Add(polygon.Clone());
        }
        #endregion

        #region 派生属性
        /// <summary>
        /// 获取该对象的元素数量
        /// </summary>
        public override int Count
        {
            get
            {
                return this.polygonList.Count;
            }
        }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        public override RectBox MBR
        {
            get
            {
                if (this.mbrUpdateRequired)
                {
                    this.UpdateMBR();
                    this.mbrUpdateRequired = false;
                }
                return this.mbr;
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 更新该对象的包围盒
        /// </summary>
        protected void UpdateMBR()
        {
            if (this.polygonList.Count > 0)
            {
                this.mbr = this.polygonList[0].GetMBR().Clone();
                foreach (GISPolygon polygon in this.polygonList)
                {
                    this.mbr += polygon.GetMBR();
                }
            }
            else
            {
                this.mbr = new RectBox();
            }

        }

        /// <summary>
        /// 获取该对象的深拷贝
        /// </summary>
        /// <returns></returns>
        public GISMultiPolygon Clone()
        {
            GISMultiPolygon cloned = new GISMultiPolygon(this.id, this.polygonList);
            cloned.mbr = this.mbr;
            return cloned;
        }

        /// <summary>
        /// 获取该MultiPolygon对象内包含的Polygon数量
        /// </summary>
        /// <returns></returns>
        public int GetElementCount()
        {
            return this.polygonList.Count;
        }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        /// <returns></returns>
        public override RectBox GetMBR()
        {
            if (this.mbrUpdateRequired)
            {
                this.UpdateMBR();
                this.mbrUpdateRequired = false;
            }
            return this.mbr;
        }

        /// <summary>
        /// 添加一个多边形
        /// </summary>
        /// <param name="polygon"></param>
        public void AddPolygon(GISPolygon polygon)
        {
            this.polygonList.Add(polygon);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 修改给定位置的多边形
        /// </summary>
        /// <param name="index"></param>
        /// <param name="polygon"></param>
        public void SetPolygon(int index, GISPolygon polygon)
        {
            this.polygonList[index] = polygon.Clone();
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 删除指定位置的多边形
        /// </summary>
        /// <param name="index"></param>
        public void DeletePolygon(int index)
        {
            this.polygonList.RemoveAt(index);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 获取指定位置的多边形
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GISPolygon GetPolygonAt(int index)
        {
            return this.polygonList[index].Clone();
        }
        #endregion
    }
}
