using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    public class moRectangle:moShape
    {
        #region 字段
        private double _MinX, _MinY, _MaxX, _MaxY;
        #endregion
        #region 构造函数
        public moRectangle()
        {

        }
        public moRectangle(double minX,double maxX,double minY,double maxY)
        {
            _MinX = minX;
            _MinY = minY;
            _MaxX = maxX;
            _MaxY = maxY;
        }

        #endregion
        #region 属性
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
        /// <summary>
        /// 获取矩形的宽度
        /// </summary>
        public double Width
        {
            get { return _MaxX - _MinX; }
        }
        /// <summary>
        /// 获取矩形的高度
        /// </summary>
        public double Height
        {
            get { return _MaxY - _MinY; }
        }
        /// <summary>
        /// 提示是否为空矩形
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (_MaxX <= _MinX)
                {
                    return true;
                }
                else if (_MaxY <= _MinY)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
