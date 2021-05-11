using System;
using System.Collections.Generic;
using System.Text;

namespace Geometria
{
    /// <summary>
    /// 用于GIS的，double类型的多边形类。其外环和内环分开记录，并且遵循外环逆时针、内环顺时针方向的标准。
    /// <para>继承自：GeometryObject</para>
    /// </summary>
    public class GISPolygon: GeometryObject
    {
        #region 字段
        //外轮廓
        protected GISRing outerRing;
        //内轮廓（可以有多个）
        protected List<GISRing> innerRingList;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public GISPolygon()
        {
            this.id = 0;
            this.outerRing = new GISRing();
            this.innerRingList = new List<GISRing>();
        }

        /// <summary>
        /// 给定ID和外边界点列数组的构造。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outerRingPts"></param>
        public GISPolygon(uint id, GeoPoint[] outerRingPts)
        {
            this.id = id;
            this.outerRing = new GISRing(0, outerRingPts);
            this.innerRingList = new List<GISRing>();

            this.outerRing.Standardrize(true, true);
        }

        /// <summary>
        /// 给定ID和外边界点列的构造。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outerRingPts">外边界点列</param>
        public GISPolygon(uint id, List<GeoPoint> outerRingPts)
        {
            this.id = id;
            this.outerRing = new GISRing(0,outerRingPts);
            this.innerRingList = new List<GISRing>();

            this.outerRing.Standardrize(true, true);
        }

        /// <summary>
        /// 给定ID和外边界的构造。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outerRing">外边界</param>
        public GISPolygon(uint id, GISRing outerRing)
        {
            this.id = id;
            this.outerRing = outerRing.Clone();
            this.innerRingList = new List<GISRing>();

            this.outerRing.Standardrize(true, true);
        }
        #endregion

        #region 派生属性
        /// <summary>
        /// 获取自身外环
        /// </summary>
        public GISRing OuterRing
        {
            get
            {
                return this.outerRing.Clone();
            }
        }

        /// <summary>
        /// 获取自身的包围盒
        /// </summary>
        public override RectBox MBR
        {
            get
            {
                return this.outerRing.GetMBR();
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 检查自身的外环是否合法。修改外环的顶点时使用。
        /// </summary>
        /// <returns></returns>
        protected bool OuterRingValidityTest()
        {
            bool result = true;
            foreach (GISRing inner in this.innerRingList)
            {
                //内环与外环相交 or 内环有顶点不在外环内部
                if (this.outerRing.Intersects(inner) || !this.outerRing.Contains(inner.GetPointAt(0)))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 检查自身的内环是否合法。修改内环的顶点时使用。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected bool InnerRingValidityTest(int index)
        {
            bool result = true;
            //先与外环进行测试
            if (this.innerRingList[index].Intersects(this.outerRing))
            {
                result = false;
            }
            //再与内环进行测试
            else
            {
                for (int i = 0; i < this.innerRingList.Count; ++i)
                {
                    if (i == index)
                    {
                        //跳过自己与自己查交
                        continue;
                    }
                    else if (this.innerRingList[index].Intersects(innerRingList[i]) ||
                        this.innerRingList[index].Contains(innerRingList[i].GetPointAt(0)))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获得自身的深拷贝
        /// </summary>
        /// <returns></returns>
        public GISPolygon Clone()
        {
            GISPolygon cloned = new GISPolygon(this.id, this.outerRing);
            foreach(GISRing inner in this.innerRingList)
            {
                cloned.innerRingList.Add(inner.Clone());
            }
            return cloned;
        }

        /// <summary>
        /// 获得自身的包围盒
        /// </summary>
        /// <returns></returns>
        public override RectBox GetMBR()
        {
            return this.outerRing.GetMBR();
        }

        /// <summary>
        /// 设置多边形的外边界。这一方法将会清空所有的内边界。
        /// </summary>
        /// <param name="outerRing"></param>
        public void SetOuterRing(GISRing outerRing)
        {
            outerRing.Standardrize(true, false);
            this.outerRing = outerRing.Clone();
            this.innerRingList.Clear();
        }

        /// <summary>
        /// 获得自身的外边界
        /// </summary>
        /// <returns></returns>
        public GISRing GetOuterRing()
        {
            return this.outerRing.Clone();
        }

        /// <summary>
        /// 获得给定index的内边界
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GISRing GetInnerRing(int index)
        {
            return this.innerRingList[index].Clone();
        }

        /// <summary>
        /// 添加内边界。方法返回所给的内边界是否成功被添加。
        /// 要求内边界完全位于外边界内部，且在其他内边界外。
        /// </summary>
        /// <param name="innerRing"></param>
        /// <returns></returns>
        public bool AddInnerRing(GISRing innerRing)
        {
            //检查是否在外边界内
            if (outerRing.Contains(innerRing))
            {
                //检查内边界之间是否相离
                bool innerIntersection = false;
                foreach(GISRing inner in this.innerRingList)
                {
                    if (!inner.Seperates(innerRing))
                    {
                        innerIntersection = true;
                        break;
                    }
                }

                if (innerIntersection)
                {
                    return false;
                }
                else
                {
                    //新的内边界在外边界内，且在已有内边界外，则可以添加
                    innerRing.Standardrize(true, false);
                    this.innerRingList.Add(innerRing.Clone());
                    return true;
                }
                
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除给定index处的内边界
        /// </summary>
        /// <param name="index"></param>
        public void DeleteInnerRing(int index)
        {
            this.innerRingList.RemoveAt(index);
        }

        /// <summary>
        /// 修改自身外环上的某个点。返回修改是否成功。
        /// </summary>
        /// <param name="ptIndex">外环上点的index</param>
        /// <param name="pt">目标位置</param>
        /// <returns></returns>
        public bool SetOuterRingPoint(int ptIndex, GeoPoint pt)
        {
            //备份原位置
            GeoPoint originalPt = this.outerRing.GetPointAt(ptIndex);
            this.outerRing.SetPoint(ptIndex, pt);
            bool valid = this.OuterRingValidityTest();

            if (valid)
            {
                return true;
            }
            else
            {
                //还原
                this.outerRing.SetPoint(ptIndex, originalPt);
                return false;
            }
        }

        /// <summary>
        /// 在外环上添加一个点。返回修改是否成功。
        /// </summary>
        /// <param name="ptIndex">外环上被添加点后方一个点的index</param>
        /// <param name="pt">目标位置</param>
        /// <returns></returns>
        public bool AddOuterRingPoint(int ptIndex, GeoPoint pt)
        {
            this.outerRing.AddPoint(ptIndex, pt);
            bool valid = this.OuterRingValidityTest();

            if (valid)
            {
                return true;
            }
            else
            {
                //还原
                this.outerRing.DeletePoint(ptIndex);
                return false;
            }
        }

        /// <summary>
        /// 删除外环上的一个点。返回修改是否成功。
        /// </summary>
        /// <param name="ptIndex">外环上被删除点的index</param>
        /// <returns></returns>
        public bool DeleteOuterRingPoint(int ptIndex)
        {
            GeoPoint originalPt = this.outerRing.GetPointAt(ptIndex);
            this.outerRing.DeletePoint(ptIndex);
            bool valid = this.OuterRingValidityTest();

            if (valid)
            {
                return true;
            }
            else
            {
                //还原
                this.outerRing.AddPoint(ptIndex, originalPt);
                return false;
            }
        }

        /// <summary>
        /// 修改自身内环上的某个点。返回修改是否成功。
        /// </summary>
        /// <param name="ringIndex">内环的index</param>
        /// <param name="ptIndex">内环上点的index</param>
        /// <param name="pt">目标位置</param>
        /// <returns></returns>
        public bool SetInnerRingPoint(int ringIndex, int ptIndex, GeoPoint pt)
        {
            
            //备份原位置
            GeoPoint originalPt = this.innerRingList[ringIndex].GetPointAt(ptIndex);
            this.innerRingList[ringIndex].SetPoint(ptIndex, pt);
            bool valid = this.InnerRingValidityTest(ringIndex);

            if (valid)
            {
                return true;
            }
            else
            {
                //还原
                this.innerRingList[ringIndex].SetPoint(ptIndex, originalPt);
                return false;
            }
        }

        /// <summary>
        /// 在内环上添加一个点。返回修改是否成功。
        /// </summary>
        /// <param name="ringIndex">内环的index</param>
        /// <param name="ptIndex">内环上被添加点后方一个点的index</param>
        /// <param name="pt">目标位置</param>
        /// <returns></returns>
        public bool AddInnerRingPoint(int ringIndex, int ptIndex, GeoPoint pt)
        {
            this.innerRingList[ringIndex].AddPoint(ptIndex, pt);
            bool valid = this.InnerRingValidityTest(ringIndex);

            if (valid)
            {
                return true;
            }
            else
            {
                //还原
                this.innerRingList[ringIndex].DeletePoint(ptIndex);
                return false;
            }
        }

        /// <summary>
        /// 删除内环上的一个点。返回修改是否成功。
        /// </summary>
        /// <param name="ringIndex">内环的index</param>
        /// <param name="ptIndex">内环上被删除点的index</param>
        /// <returns></returns>
        public bool DeleteInnerRingPoint(int ringIndex, int ptIndex)
        {
            GeoPoint originalPt = this.innerRingList[ringIndex].GetPointAt(ptIndex);
            this.innerRingList[ringIndex].DeletePoint(ptIndex);
            bool valid = this.InnerRingValidityTest(ringIndex);

            if (valid)
            {
                return true;
            }
            else
            {
                //还原
                this.innerRingList[ringIndex].AddPoint(ptIndex, originalPt);
                return false;
            }
        }
        #endregion

        #region 几何关系
        /// <summary>
        /// 检查自身是否与给定线段相交
        /// </summary>
        /// <param name="seg"></param>
        /// <returns></returns>
        public bool Intersects(Segment seg)
        {
            bool result = this.outerRing.Intersects(seg);
            if (!result)
            {
                foreach(GISRing inner in this.innerRingList)
                {
                    if (inner.Intersects(seg))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 检查自身是否与给定折线段相交
        /// </summary>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public bool Intersects(GISPolyline polyline)
        {
            bool result = this.outerRing.Intersects(polyline);
            if (!result)
            {
                foreach (GISRing inner in this.innerRingList)
                {
                    if (inner.Intersects(polyline))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 检查自身是否与给定多边形的边界相交
        /// </summary>
        /// <param name="tar"></param>
        /// <returns></returns>
        public bool Intersects(GISPolygon tar)
        {
            bool result = this.Intersects(tar.outerRing);
            if (!result)
            {
                foreach(GISRing inner in tar.innerRingList)
                {
                    if (this.Intersects(inner))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 两多边形的边界求交
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool Intersects(GISPolygon A, GISPolygon B)
        {
            return A.Intersects(B);
        }

        /// <summary>
        /// 检查给定点是否在多边形内
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool Contains(GeoPoint pt)
        {
            bool result = this.outerRing.Contains(pt);
            if (result)
            {
                foreach(GISRing inner in this.innerRingList)
                {
                    if (inner.Contains(pt))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 检查给定线段是否在多边形内
        /// </summary>
        /// <param name="seg"></param>
        /// <returns></returns>
        public bool Contains(Segment seg)
        {
            return !this.Intersects(seg) && this.Contains(seg.startPoint);
        }

        /// <summary>
        /// 检查给定折线是否在多边形内
        /// </summary>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public bool Contains(GISPolyline polyline)
        {
            return !this.Intersects(polyline) && this.Contains(polyline.GetPointAt(0));
        }

        /// <summary>
        /// 检查给定环所围成区域是否在多边形内
        /// </summary>
        /// <param name="ring"></param>
        /// <returns></returns>
        public bool ContainsArea(GISRing ring)
        {
            //这一部分直接套用多边形的内环添加逻辑。
            //检查是否在外边界内
            if (outerRing.Contains(ring))
            {
                //检查内边界之间是否相离
                bool innerIntersection = false;
                foreach (GISRing inner in this.innerRingList)
                {
                    if (!inner.Seperates(ring))
                    {
                        innerIntersection = true;
                        break;
                    }
                }

                if (innerIntersection)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查给定环所围区域是否全部在多边形外
        /// </summary>
        /// <param name="ring"></param>
        /// <returns></returns>
        public bool SeperatesArea(GISRing ring)
        {
            //情况1：这个环落在多边形外边界之外
            if (this.outerRing.Seperates(ring))
            {
                return true;
            }
            else
            {
                bool result = false;
                //情况2：这个环落在某个内边界之内
                foreach(GISRing inner in this.innerRingList)
                {
                    if (inner.Contains(ring))
                    {
                        result = true;
                        break;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// 检查给定多边形所围成区域是否在自身内部
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public bool ContainsArea(GISPolygon polygon)
        {
            //首先目标的外边界在自身外边界内
            bool result = this.outerRing.Contains(polygon.outerRing);
            if (result)
            {
                //其次自身的所有内边界应该在目标之外（这一点需要画点例子去看）
                foreach(GISRing inner in this.innerRingList)
                {
                    if (!polygon.SeperatesArea(inner))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 检查给定多边形所围成区域是否全部在自身外
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public bool SeperatesArea(GISPolygon polygon)
        {
            return this.SeperatesArea(polygon.outerRing);
        }
        #endregion
    }
}
