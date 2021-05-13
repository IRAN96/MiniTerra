using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Drawing2D;

using Geometria.Geometry;

namespace MiniTerra
{
    /// <summary>
    /// 渲染管线用，根据相机参数生成相机变换矩阵的相机类。
    /// </summary>
    public class Camera
    {
        #region 字段
        /// <summary>
        /// (地理坐标) 中心位置
        /// </summary>
        public PointF center;
        /// <summary>
        /// 放大率
        /// </summary>
        public float zoom;
        /// <summary>
        /// 旋转角度。弧度制。
        /// </summary>
        public float rotation;

        /// <summary>
        /// (屏幕坐标) x方向偏移
        /// </summary>
        public float fixedScreenOffsetX;
        /// <summary>
        /// (屏幕坐标) y方向偏移
        /// </summary>
        public float fixedScreenOffsetY;

        public float dynamicScreenOffsetX;
        public float dynamicScreenOffsetY;

        //响应鼠标拖动事件用
        private bool mouseDown = false;
        private int mouseStartLocationX = 0;
        private int mouseStartLocationY = 0;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public Camera()
        {
            this.center = new PointF(0, 0);
            this.zoom = 1.0f;
            this.rotation = 0;
        }

        /// <summary>
        /// 仅规定中心点的构造。中心点由两座标值给出。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Camera(double x, double y)
        {
            this.center = new PointF((float)x, (float)y);
            this.zoom = 1.0f;
            this.rotation = 0;
            this.fixedScreenOffsetX = 0;
            this.fixedScreenOffsetY = 0;
            this.dynamicScreenOffsetX = 0;
            this.dynamicScreenOffsetY = 0;
        }

        /// <summary>
        /// 规定所有世界坐标相机参数的构造。
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="zoom">放大率</param>
        /// <param name="rotation">旋转角度</param>
        public Camera(PointF center, float zoom = 1.0f, float rotation = 0.0f)
        {
            this.center = center;
            this.zoom = zoom;
            this.rotation = rotation;
            this.fixedScreenOffsetX = 0;
            this.fixedScreenOffsetY = 0;
            this.dynamicScreenOffsetX = 0;
            this.dynamicScreenOffsetY = 0;
        }
        #endregion

        #region 派生属性
        /// <summary>
        /// 获取相机变换矩阵
        /// </summary>
        public Matrix ViewMatrix
        {
            get
            {
                return this.GetViewMatrix();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取相机变换矩阵
        /// </summary>
        /// <returns></returns>
        public Matrix GetViewMatrix()
        {
            Matrix view = new Matrix();
            //顺序很重要。世界平移→缩放→屏幕变换→屏幕平移→旋转
            view.Translate(-center.X, -center.Y);
            view.Scale(zoom, zoom, MatrixOrder.Append);
            view.Scale(1, -1, MatrixOrder.Append);
            view.Translate(fixedScreenOffsetX + dynamicScreenOffsetX, 
                fixedScreenOffsetY + dynamicScreenOffsetY, MatrixOrder.Append);
            //view.Rotate(rotation, MatrixOrder.Append);

            return view;
        }

        /// <summary>
        /// 使自身对准指定的几何对象，并缩放至合适的大小。
        /// </summary>
        /// <param name="mbr">几何对象的包围盒</param>
        /// <param name="screenWidth">视窗宽</param>
        /// <param name="screenHeight">视窗高</param>
        public void FocusOnGeometry(RectBox mbr, int screenWidth, int screenHeight)
        {
            this.center = new PointF((float)mbr.Center.coordX, (float)mbr.Center.coordY);

            float xZoom = screenWidth / (float)mbr.Width;
            float yZoom = screenHeight / (float)mbr.Height;

            this.zoom = Math.Min(xZoom, yZoom);
            this.rotation = 0;

            this.fixedScreenOffsetX = screenWidth / 2;
            this.fixedScreenOffsetY = screenHeight / 2;
        }

        /// <summary>
        /// 鼠标滚动事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseScrollResponse(object sender, MouseEventArgs e)
        {
            float formerZoom = this.zoom;

            if (this.zoom >= 0.01f && e.Delta > 0)
            {
                this.zoom = 0.01f;
            }
            else if (this.zoom <= 1e-7f && e.Delta < 0)
            {
                this.zoom = 1e-7f;
            }
            else
            {
                this.zoom *= (1 + (float)e.Delta / 1000);
            }

            this.dynamicScreenOffsetX *= zoom / formerZoom;
            this.dynamicScreenOffsetY *= zoom / formerZoom;

        }

        /// <summary>
        /// 鼠标按下事件响应。拖动窗口时改变显示。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseDownResponse(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseStartLocationX = e.Location.X;
            mouseStartLocationY = e.Location.Y;
        }

        /// <summary>
        /// 鼠标抬起事件响应。拖动窗口时改变显示。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUpResponse(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        /// <summary>
        /// 鼠标拖动事件响应。拖动窗口时改变显示。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMoveResponse(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.dynamicScreenOffsetX += e.Location.X - mouseStartLocationX;
                this.dynamicScreenOffsetY += e.Location.Y - mouseStartLocationY;

                mouseStartLocationX = e.Location.X;
                mouseStartLocationY = e.Location.Y;
            }
        }

        #endregion
    }
}
