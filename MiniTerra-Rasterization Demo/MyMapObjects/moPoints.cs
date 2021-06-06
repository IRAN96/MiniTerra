using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 点集合类
    /// </summary>
    public class moPoints:moShape
    {
        #region 字段
        private List<moPoint> _Points;
        private double _MinX = double.MaxValue, _MaxX = double.MinValue;//赋初值的字段
        private double _MinY = double.MaxValue, _MaxY = double.MaxValue;
        #endregion
        #region 构造函数

        public moPoints()
        {
            _Points = new List<moPoint>();
        }
        public moPoints(moPoint[] points)
        {
            _Points = new List<moPoint>();
            _Points.AddRange(points);
        }

        #endregion
        #region 属性
        public Int32 Count
        {
            get { return _Points.Count; }
        }
        /// <summary>
        /// 获取最小x坐标
        /// </summary>
        public double MinX
        {
            get { return _MinX; }
        }
        /// <summary>
        /// 获取最大x坐标
        /// </summary>
        public double MaxX
        {
            get { return _MaxX; }
        }
        /// <summary>
        /// 获取最小y坐标
        /// </summary>
        public double MinY
        {
            get { return _MinY; }
        }
        /// <summary>
        /// 获取最大y坐标
        /// </summary>
        public double MaxY
        {
            get { return _MaxY; }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 获取指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public moPoint GetItem(Int32 index)
        {
            return _Points[index];
        }
        /// <summary>
        /// 增加一个点
        /// </summary>
        /// <param name="point">拟增加的点</param>
        public void Add(moPoint point)
        {
            _Points.Add(point);
        }
        /// <summary>
        /// 将指定数组的元素添加到末尾
        /// </summary>
        /// <param name="points">点的数组</param>
        public void AddRange(moPoint[] points)
        {
            _Points.AddRange(points);
        }
        /// <summary>
        /// 将指定数组中的元素插入到指定索引出
        /// </summary>
        /// <param name="index"></param>
        /// <param name="points"></param>
        public void InsertRange(Int32 index,moPoint[] points)
        {
            _Points.InsertRange(index, points);
        }
        /// <summary>
        /// 将指定元素插入到指定索引处
        /// </summary>
        /// <param name="index"></param>
        /// <param name="point"></param>
        public void Insert(Int32 index,moPoint point)
        {
            _Points.Insert(index, point);
        }
        /// <summary>
        /// 删除指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(Int32 index)
        {
            _Points.RemoveAt(index);
        }
        /// <summary>
        /// 将所有点复制到一个数组中，并返回该数组
        /// </summary>
        /// <returns></returns>
        public moPoint[] ToArray()
        {
            return _Points.ToArray();
        }
        /// <summary>
        /// 删除所有元素
        /// </summary>
        public void Clear()
        {
            _Points.Clear();
        }
        /// <summary>
        /// 获得外包矩形
        /// </summary>
        /// <returns></returns>

        public moRectangle GetEnvelope()
        {
            moRectangle sRect = new moRectangle(_MinX, _MaxX, _MinY, _MaxY);
            return sRect;
        }
        /// <summary>
        /// 重新计算坐标范围
        /// </summary>
        public void UpdateExtent()
        {
            CalExtent();
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public moPoints Clone()
        {
            moPoints sPoints = new moPoints();
            Int32 sPointCount = _Points.Count;
            for (Int32 i = 0; i <= sPointCount - 1; i++)
            {
                moPoint sPoint = new moPoint(_Points[i].X, _Points[i].Y);
                sPoints.Add(sPoint);
            }
            sPoints._MinX = _MinX;
            sPoints._MaxX = _MaxX;
            sPoints._MinY = _MinY;
            sPoints._MaxY = _MaxY;
            return sPoints;
        }
        #endregion
        #region 私有函数
        private void CalExtent()
        {
            double sMinX = double.MaxValue;
            double sMaxX = double.MinValue;
            double sMinY = double.MaxValue;
            double sMaxY = double.MinValue;
            Int32 sPointCount = _Points.Count;
            for (Int32 i = 0; i <= sPointCount - 1; i++)
            {
                if (_Points[i].X < sMinX)
                    sMinX = _Points[i].X;
                if (_Points[i].X > sMaxX)
                    sMaxX = _Points[i].X;
                if (_Points[i].Y < sMinY)
                    sMinY = _Points[i].Y;
                if (_Points[i].Y > sMaxY)
                    sMaxY = _Points[i].Y;
            }
            _MinX = sMinX;
            _MaxX = sMaxX;
            _MinY = sMinY;
            _MaxY = sMaxY;
        }
        #endregion
    }
}
