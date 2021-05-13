using System;
using System.Collections.Generic;
using System.Text;

namespace Geometria.Geometry
{
    /// <summary>
    /// 用于GIS的，double类型的点集合。
    /// <para>继承自：GeometryMultiObject</para>
    /// </summary>
    public class GISMultiPoint: GeometryMultiObject
    {
        #region 字段
        //所有顶点的数据
        protected List<GeoPoint> pointsList;
        protected RectBox mbr;
        protected bool mbrUpdateRequired = false;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public GISMultiPoint()
        {
            this.id = 0;
            this.pointsList = new List<GeoPoint>();
            this.mbr = new RectBox();
        }

        /// <summary>
        /// 给定id和点列数据的构造
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ptList"></param>
        public GISMultiPoint(uint id, List<GeoPoint> ptList)
        {
            this.id = id;
            this.pointsList = new List<GeoPoint>();
            foreach(GeoPoint pt in ptList)
            {
                this.pointsList.Add(pt.Clone());
            }
        }

        /// <summary>
        /// 给定id和单个点的构造。该MultiPoint对象只有一个点。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pt"></param>
        public GISMultiPoint(uint id, GeoPoint pt)
        {
            this.id = id;
            this.pointsList = new List<GeoPoint>();
            this.pointsList.Add(pt.Clone());
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
                return this.pointsList.Count;
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
            if (this.pointsList.Count > 0)
            {
                this.mbr = this.pointsList[0].GetMBR().Clone();
                foreach (GeoPoint pt in this.pointsList)
                {
                    this.mbr += pt.GetMBR();
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
        public GISMultiPoint Clone()
        {
            GISMultiPoint cloned = new GISMultiPoint(this.id, this.pointsList);
            cloned.mbr = this.mbr;
            return cloned;
        }

        /// <summary>
        /// 从另一个对象导入数据
        /// *似乎没什么用？
        /// </summary>
        /// <param name="src"></param>
        public void CloneFrom(GISMultiPoint src)
        {
            this.id = src.id;
            this.pointsList.InsertRange(0,src.pointsList);
        }

        /// <summary>
        /// 获取该MultiPoint对象内包含的Point数量
        /// </summary>
        /// <returns></returns>
        public int GetElementCount()
        {
            return this.pointsList.Count;
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
        /// 添加一个新点
        /// </summary>
        public void AddPoint(double x, double y)
        {
            this.pointsList.Add(new GeoPoint(x,y));
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 添加一个新点
        /// </summary>
        public void AddPoint(GeoPoint pt)
        {
            this.pointsList.Add(pt.Clone());
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 修改某个点的属性
        /// </summary>
        /// <param name="elementIndex">该点的index</param>
        public void SetPoint(int elementIndex, double x, double y)
        {
            this.pointsList[elementIndex] = new GeoPoint(x, y);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 修改某个点的属性
        /// </summary>
        /// <param name="elementIndex">该点的index</param>
        public void SetPoint(int elementIndex, GeoPoint pt)
        {
            this.pointsList[elementIndex] = pt.Clone();
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 删除一个点
        /// </summary>
        /// <param name="elementIndex"></param>
        public void DeletePoint(int elementIndex)
        {
            this.pointsList.RemoveAt(elementIndex);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 获取给定index处的点
        /// </summary>
        /// <param name="elementIndex"></param>
        /// <returns></returns>
        public GeoPoint GetPointAt(int elementIndex)
        {
            return this.pointsList[elementIndex];
        }
        #endregion
    }
}
