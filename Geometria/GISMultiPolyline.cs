using System;
using System.Collections.Generic;
using System.Text;

namespace Geometria
{
    /// <summary>
    /// 用于GIS的，double类型的折线集合。
    /// <para>继承自：GeometryMultiObject</para>
    /// </summary>
    class GISMultiPolyline : GeometryMultiObject
    {
        #region 字段
        //所有折线的数据
        protected List<GISPolyline> polylineList;
        protected RectBox mbr;
        protected bool mbrUpdateRequired = false;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public GISMultiPolyline()
        {
            this.id = 0;
            this.polylineList = new List<GISPolyline>();
            this.mbr = new RectBox();
        }

        /// <summary>
        /// 给定id和折线列表数据的构造
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineList"></param>
        public GISMultiPolyline(uint id, List<GISPolyline> lineList)
        {
            this.id = id;
            this.polylineList = new List<GISPolyline>();
            foreach (GISPolyline line in lineList)
            {
                this.polylineList.Add(line.Clone());
            }
        }

        /// <summary>
        /// 给定id和单个折线段的构造。该MultiPolyline对象只有一条折线。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="line"></param>
        public GISMultiPolyline(uint id, GISPolyline line)
        {
            this.id = id;
            this.polylineList = new List<GISPolyline>();
            this.polylineList.Add(line.Clone());
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
                return this.polylineList.Count;
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
            if (this.polylineList.Count > 0)
            {
                this.mbr = this.polylineList[0].GetMBR().Clone();
                foreach (GISPolyline line in this.polylineList)
                {
                    this.mbr += line.GetMBR();
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
        public GISMultiPolyline Clone()
        {
            GISMultiPolyline cloned = new GISMultiPolyline(this.id, this.polylineList);
            cloned.mbr = this.mbr;
            return cloned;
        }

        /// <summary>
        /// 获取该MultiPolyline对象内包含的Polyline数量
        /// </summary>
        /// <returns></returns>
        public int GetElementCount()
        {
            return this.polylineList.Count;
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
        /// 添加一条折线
        /// </summary>
        /// <param name="line"></param>
        public void AddPolyline(GISPolyline line)
        {
            this.polylineList.Add(line);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 修改给定位置的折线
        /// </summary>
        /// <param name="index"></param>
        /// <param name="line"></param>
        public void SetPolyline(int index, GISPolyline line)
        {
            this.polylineList[index] = line.Clone();
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 删除指定位置的折线
        /// </summary>
        /// <param name="index"></param>
        public void DeletePolyline(int index)
        {
            this.polylineList.RemoveAt(index);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 获取指定位置的折线
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GISPolyline GetPolylineAt(int index)
        {
            return this.polylineList[index].Clone();
        }
        #endregion
    }
}
