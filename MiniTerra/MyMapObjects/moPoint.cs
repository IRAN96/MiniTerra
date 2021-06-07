using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 点
    /// </summary>
    //三个斜杠自动弹出，可以对外显示注释
    public class moPoint:moGeometry
    {
        #region 字段

        private double _X, _Y;//x、y坐标。准备实现接口的变量推荐使用下划线

        #endregion

        #region 构造函数

        public moPoint()
        {

        }

        public moPoint(double x,double y)//形参，第一个字母建议小写
        {
            _X = x;
            _Y = y;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置x坐标
        /// </summary>
        public double X
        {
            get { return _X; }//x=A.X
            set { _X = value; }//A.X=x
        }
        /// <summary>
        /// 获取或设置y坐标
        /// </summary>
        public double Y
        {
            get { return _Y; }//x=A.X
            set { _Y = value; }//A.X=x
        }

        #endregion

        #region 方法

        public moPoint Clone()
        {
            moPoint sPoint = new moPoint(_X, _Y);
            return sPoint;
        }

        #endregion

    }
}
