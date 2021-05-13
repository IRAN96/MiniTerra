using System;
using System.Collections.Generic;
using System.Text;

namespace Geometria.Geometry
{
    /// <summary>
    /// 用于GIS的，double类型的折线类。
    /// <para>继承自：GeometryObject</para>
    /// </summary>
    public class GISPolyline: GeometryObject
    {
        #region 字段
        //所有顶点的数据
        protected List<GeoPoint> pointsList;
        //包围盒
        protected RectBox mbr;
        protected bool mbrUpdateRequired = false;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public GISPolyline()
        {
            this.id = 0;
            this.pointsList = new List<GeoPoint>();
            this.mbr = new RectBox();
        }

        /// <summary>
        /// 给定ID和折线段点列的构造
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pts"></param>
        public GISPolyline(uint id, GeoPoint[] pts)
        {
            this.id = id;
            this.pointsList = new List<GeoPoint>();
            this.pointsList.InsertRange(0, pts);
            this.UpdateMBR();
        }

        /// <summary>
        /// 给定ID和折线段点列的构造
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="pts">折线段的点列</param>
        public GISPolyline(uint id, List<GeoPoint> pts)
        {
            this.id = id;
            this.pointsList = new List<GeoPoint>();
            this.pointsList.InsertRange(0, pts);
            this.UpdateMBR();
        }

        public GISPolyline(GISRing ring)
        {
            this.id = ring.id;
            this.pointsList = ring.pointsList;
            this.mbr = ring.mbr;
            this.mbrUpdateRequired = ring.mbrUpdateRequired;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 更新自身的包围盒。
        /// </summary>
        protected void UpdateMBR()
        {
            if(this.pointsList.Count > 0)
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
        /// 获得给定下标位置的节点与其后节点之间连成的边。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected Segment EdgeAt(int index)
        {
            //未作index越界处理. 合法范围: 0 <= index <= len - 2
            return new Segment(this.pointsList[index], this.pointsList[index + 1]);
        }

        /// <summary>
        /// 获得给定下标位置的节点前后边的方向变化（后一条边相对于前一条边）
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double DirectionChangeAt(int index)
        {
            //未作index越界处理. 合法范围: 1 <= index <= len - 2
            Segment previousSegment = this.EdgeAt(index - 1);
            Segment nextSegment = this.EdgeAt(index);
            return Segment.GetRelativeDirection(nextSegment, previousSegment);
        }
        #endregion

        #region 派生属性
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

        /// <summary>
        /// 获取自身长度
        /// </summary>
        public double Length
        {
            get
            {
                double len = 0;
                for(int index = 0;index < this.pointsList.Count - 1; ++index)
                {
                    len += this.EdgeAt(index).GetLength();
                }
                return len;
            }
        }

        /// <summary>
        /// 获取自身所含顶点的数量
        /// </summary>
        public int Count
        {
            get
            {
                return this.pointsList.Count;
            }
        }

        /// <summary>
        /// 获取自身点列的拷贝
        /// </summary>
        public List<GeoPoint> PointsList
        {
            get
            {
                List<GeoPoint> lst = new List<GeoPoint>();
                foreach (GeoPoint pt in this.pointsList)
                {
                    lst.Add(pt.Clone());
                }
                return lst;
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获得自身的深拷贝
        /// </summary>
        /// <returns></returns>
        public GISPolyline Clone()
        {
            GISPolyline copied = new GISPolyline();
            foreach(GeoPoint pt in this.pointsList)
            {
                copied.pointsList.Add(pt.Clone());
            }
            copied.mbr = this.mbr.Clone();
            return copied;
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
        /// 获取给定index处的顶点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GeoPoint GetPointAt(int index)
        {
            return this.pointsList[index];
        }

        /// <summary>
        /// 获取所有顶点。结果以GeoPoint的数组形式给出。
        /// </summary>
        /// <returns></returns>
        public GeoPoint[] GetAllPoints()
        {
            return this.pointsList.ToArray();
        }

        /// <summary>
        /// 在指定位置插入一个节点
        /// </summary>
        /// <param name="formerPointIndex">插入位置的后方点的index</param>
        /// <param name="pt">插入点的数据</param>
        public void InsertPoint(int formerPointIndex,GeoPoint pt)
        {
            //未作index越界处理
            this.pointsList.Insert(formerPointIndex, pt);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 在折线段结尾添加一个节点
        /// </summary>
        /// <param name="pt"></param>
        public void AddPoint(GeoPoint pt)
        {
            this.pointsList.Add(pt);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 修改指定位置点的数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pt"></param>
        public void SetPoint(int index, GeoPoint pt)
        {
            //未作index越界处理
            this.pointsList[index] = pt;
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 移动指定位置的节点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vec"></param>
        public void MovePoint(int index, Vector2D vec)
        {
            //未作index越界处理
            this.pointsList[index].MoveBy(vec);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 删除指定位置的节点
        /// </summary>
        /// <param name="index"></param>
        public void DeletePoint(int index)
        {
            //未作index越界处理
            this.pointsList.RemoveAt(index);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 规范化该Polyline对象。主要的操作是剔除点列中相邻且重复的点。
        /// </summary>
        public void Standardrize()
        {
            GeoPoint lastPoint = new GeoPoint(), currentPoint = new GeoPoint();
            List<int> removalIndex = new List<int>();
            //选出需要删除的点
            for (int index = 0; index < this.pointsList.Count; ++index)
            {
                lastPoint = currentPoint;
                currentPoint = this.pointsList[index];
                if (index > 0 && lastPoint == currentPoint)
                {
                    removalIndex.Add(index);
                }
            }
            //执行
            removalIndex.Reverse();
            foreach (int index in removalIndex)
            {
                this.pointsList.RemoveAt(index);
            }
        }

        /// <summary>
        /// 掉转该Polyline的方向。直接修改自身属性。
        /// </summary>
        public void Reverse()
        {
            this.pointsList.Reverse();
        }

        /// <summary>
        /// 掉转该Polyline的方向。生成新的Polyline对象。
        /// </summary>
        /// <returns></returns>
        public GISPolyline Reversed()
        {
            GISPolyline copied = this.Clone();
            copied.Reverse();
            return copied;
        }
        #endregion

        #region 几何运算
        /// <summary>
        /// 检查给定线段是否与自身相交。
        /// </summary>
        /// <param name="seg"></param>
        /// <returns></returns>
        public bool Intersects(Segment seg)
        {
            if(RectBox.Overlaps(this.GetMBR(), seg.GetMBR()))
            {
                return false;
            }
            else
            //逐一求交
            {
                bool intersects = false;
                for(int index = 0;index < this.pointsList.Count - 1; ++index)
                {
                    if (Segment.Intersects(this.EdgeAt(index), seg))
                    {
                        intersects = true;
                        break;
                    }
                }

                return intersects;
            }
        }

        /// <summary>
        /// 两Polyline求交。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool Intersects(GISPolyline A,GISPolyline B)
        {
            return A.Intersects(B);
        }

        /// <summary>
        /// 检查给定折线是否与自身相交。
        /// </summary>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public bool Intersects(GISPolyline polyline)
        {
            if (!RectBox.Overlaps(this.GetMBR(), polyline.GetMBR()))
            {
                return false;
            }
            else
            //逐一求交
            {
                bool intersects = false;

                int lenA = this.pointsList.Count - 1;
                int lenB = polyline.pointsList.Count - 1;
                for (int indexA = 0; indexA < lenA; ++indexA)
                {
                    for (int indexB = 0; indexB < lenB; ++indexB)
                    {
                        if (Segment.Intersects(this.EdgeAt(indexA), polyline.EdgeAt(indexB)))
                        {
                            intersects = true;
                            break;
                        }
                    }

                    if (intersects)
                    {
                        break;
                    }
                }

                return intersects;
            }
        }
        
        /// <summary>
        /// 求一个点到该Polyline的距离。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public double GetDistanceOfPoint(GeoPoint pt)
        {
            double minDistance = 0;
            for(int index = 0;index <= this.pointsList.Count - 1; ++index)
            {
                Segment seg = this.EdgeAt(index);
                if(index == 0)
                {
                    minDistance = seg.GetPointDistance(pt);
                }
                else
                {
                    minDistance = Math.Min(minDistance, seg.GetPointDistance(pt));
                }
            }

            return minDistance;

        }
        #endregion
    }

    /// <summary>
    /// 用于GIS的，double类型的闭环折线类。主要用于确保多边形的合法构造。
    /// <para>继承自：GISPolyline</para>
    /// </summary>
    public class GISRing: GISPolyline
    {
        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public GISRing() : base() { }

        /// <summary>
        /// 给定ID和点列数组的构造。点列不封闭时，将会自动连接首尾。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pts"></param>
        public GISRing(uint id, GeoPoint[] pts) : base(id,pts)
        {
            if (pts[0] != pts[pts.Length - 1])
            {
                this.pointsList.Add(pts[0]);
            }
            this.UpdateMBR();
        }

        /// <summary>
        /// 给定ID和点列的构造。点列不封闭时，将会自动连接首尾。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="pts">点列</param>
        public GISRing(uint id, List<GeoPoint> pts) : base(id,pts)
        {
            if (pts[0] != pts[pts.Count - 1])
            {
                this.pointsList.Add(pts[0]);
            }
            this.UpdateMBR();
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获得自身的深拷贝
        /// </summary>
        /// <returns></returns>
        public new GISRing Clone()
        {
            GISRing copied = new GISRing();
            copied.pointsList.InsertRange(0, this.pointsList);
            copied.mbr = this.mbr.Clone();
            return copied;
        }

        /// <summary>
        /// 获取自身的环路方向：逆时针为正，顺时针为负，数值代表所绕的圈数。
        /// </summary>
        /// <returns></returns>
        public int GetLoopDirection()
        {
            double loopCount = 0;
            for(int index = 1;index < this.pointsList.Count - 1; ++index)
            {
                loopCount += this.DirectionChangeAt(index);   
            }

            return (int)Math.Floor(loopCount);
        }

        /// <summary>
        /// 规范化该Ring对象。剔除点列中重复的相邻点，并且将环绕方向修改为指定方向。
        /// </summary>
        /// <param name="standardrizeDir">是否要修改环绕方向</param>
        /// <param name="isCounterClockwise">是否将逆时针作为环绕方向。若为false，则将顺时针作为环绕方向。</param>
        public void Standardrize(bool standardrizeDir = false, bool isCounterClockwise = true)
        {
            base.Standardrize();
            if (standardrizeDir && (this.GetLoopDirection() > 0 ^ isCounterClockwise))
            {
                this.Reverse();
            }
        }
        #endregion

        #region 几何关系
        /// <summary>
        /// 检查给定点是否在环的内部。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool Contains(GeoPoint pt)
        {
            //包围盒过滤
            if (!this.GetMBR().Contains(pt))
            {
                return false;
            }
            else
            {
                //作扫描线检查，向x轴负向扫描。
                double scanXEnd = pt.coordX;
                double scanY = pt.coordY;

                int intersectionCount = 0;
                for(int index = 0;index < this.pointsList.Count - 1; ++index)
                {
                    Segment edge = this.EdgeAt(index);
                    RectBox edgeMBR = edge.GetMBR();
                    //包围盒过滤。因为“下端点不计而上端点计”，所以有一处是取等。
                    if(edgeMBR.yMin >= scanY || edgeMBR.yMax < scanY)
                    {
                        //射线不交，跳过
                        continue;
                    }
                    else
                    {
                        GeoPoint scannedPt = edge.GetPointAtY(scanY);
                        if(scannedPt.coordX < scanXEnd)
                        {
                            //存在交点，记录
                            intersectionCount += 1;
                        }
                    }
                }

                //根据交点个数判断是否在内。
                if(intersectionCount % 2 == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查给定线段是否在环的内部。
        /// </summary>
        /// <param name="seg"></param>
        /// <returns></returns>
        public bool Contains(Segment seg)
        {
            //包围盒过滤
            if (!this.GetMBR().Contains(seg.GetMBR()))
            {
                return false;
            }
            //先看两端点是否在内
            else if(!this.Contains(seg.startPoint) || !this.Contains(seg.endPoint))
            {
                return false;
            }
            //再确保线段与环的边界不交（因为存在凹环）
            else
            {
                bool intersect = false;
                for(int index = 0;index < this.pointsList.Count - 1; ++index)
                {
                    if (this.EdgeAt(index).Intersects(seg))
                    {
                        intersect = true;
                        break;
                    }
                }

                return !intersect;
            }
        }

        /// <summary>
        /// 检查给定折线是否在环的内部。
        /// </summary>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public bool Contains(GISPolyline polyline)
        {
            //包围盒过滤
            RectBox a = this.GetMBR();
            RectBox b = polyline.GetMBR();
            if (!this.GetMBR().Contains(polyline.GetMBR()))
            {
                return false;
            }
            else
            {
                //对于折线，先判交比较划算。
                bool intersects = Intersects(this, polyline);
                if (intersects)
                {
                    return false;
                }
                else
                {
                    //在二者不交的前提下，只要目标polyline的任一点在Ring内，polyline就全部在Ring内。
                    return this.Contains(polyline.GetPointAt(0));
                }
            }
        }

        /// <summary>
        /// 检查给定折线是否在环的外部。
        /// </summary>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public bool Seperates(GISPolyline polyline)
        {
            //包围盒过滤
            if (!this.GetMBR().Contains(polyline.GetMBR()))
            {
                return false;
            }
            else
            {
                //对于折线，先判交比较划算。
                bool intersects = Intersects(this, polyline);
                if (intersects)
                {
                    return false;
                }
                else
                {
                    //在二者不交的前提下，只要目标polyline的任一点在Ring外，polyline就全部在Ring外。
                    return !this.Contains(polyline.GetPointAt(0));
                }
            }
        }
        #endregion

    }
}
