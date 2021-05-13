using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Security.Cryptography;

using Geometria.Layers;
using Geometria.Enumerates;
using Geometria.Geometry;

namespace MiniTerra
{


    public class SimpleUniqueLayerRenderer
    {
        #region 字段
        /// <summary>
        /// 矢量图层
        /// </summary>
        public GISSimpleUniqueLayer layer;
        /// <summary>
        /// 控制参数：是否可见
        /// </summary>
        public bool isVisible = true;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public SimpleUniqueLayerRenderer()
        {
            this.layer = new GISSimpleUniqueLayer("new layer", GeometryObjectType.Polygon);
        }
        #endregion

        #region 渲染
        /// <summary>
        /// 渲染矢量图层
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cam">追踪相机</param>
        /// <param name="fill">填充</param>
        /// <param name="edge">边框</param>
        public void RenderLayer(Graphics g, Camera cam, Brush fill,Pen edge)
        {
            //图形参数
            GraphicsPath path = new GraphicsPath();
            //变换矩阵
            Matrix trans = cam.GetViewMatrix();

            //绘制每个多边形
            for (int index = 0;index < layer.featureList.Count; ++index)
            {
                List<PointF> ptsList = new List<PointF>();
                Feature f = layer.featureList[index];
                
                //todo 增加对其他类型的支持
                if(f.GeometryType == GeometryObjectType.Polygon)
                {
                    GISPolygon polygon = (GISPolygon)f.Geometry;
                    for(int i = 0; i < polygon.GetOuterRing().Count; ++i)
                    {
                        GeoPoint pt = polygon.GetOuterRing().PointsList[i];
                        ptsList.Add(new PointF((float)pt.coordX, (float)pt.coordY));
                    }
                    path.AddPolygon(ptsList.ToArray());
                }
                path.Transform(trans);

                //g.FillPath(fill, path);
                g.DrawPath(edge, path);
            }
        }
        #endregion

        #region 图层方法
        /// <summary>
        /// 使用随机颜色来栅格化图层
        /// </summary>
        /// <param name="layer">目标图层</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <returns></returns>
        public Bitmap RanColorRasterize(int width)
        {
            int height = (int)Math.Floor(layer.MBR.Height / layer.MBR.Width * width);
            Bitmap outmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(outmap);
            Camera rasterCam = new Camera();
            rasterCam.FocusOnGeometry(layer.MBR, width, height);
            //图形参数
            GraphicsPath path = new GraphicsPath();
            //变换矩阵
            Matrix trans = rasterCam.GetViewMatrix();

            //绘制每个多边形
            for (int index = 0; index < layer.featureList.Count; ++index)
            {
                List<PointF> ptsList = new List<PointF>();
                Feature f = layer.featureList[index];

                //todo 增加对其他类型的支持
                if (f.GeometryType == GeometryObjectType.Polygon)
                {
                    GISPolygon polygon = (GISPolygon)f.Geometry;
                    for (int i = 0; i < polygon.GetOuterRing().Count; ++i)
                    {
                        GeoPoint pt = polygon.GetOuterRing().PointsList[i];
                        ptsList.Add(new PointF((float)pt.coordX, (float)pt.coordY));
                    }
                    path.AddPolygon(ptsList.ToArray());
                    path.Transform(trans);
                    Brush fill = new SolidBrush(CreateRandomColor());
                    g.FillPath(fill, path);
                }
            }

            return outmap;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 随机颜色生成器（from：MapObjectsDemo）
        /// </summary>
        /// <returns></returns>
        private static Color CreateRandomColor()
        {
            //总体思想：每个随机颜色RGB中总有一个为252，其他两个值的取值范围为179-245，这样取值的目的在于让地图颜色偏浅，美观
            //生成4个元素的字节数组，第一个值决定哪个通道取252，另外三个中的两个值决定另外两个通道的值
            byte[] sBytes = new byte[4];
            RNGCryptoServiceProvider sChanelRng = new RNGCryptoServiceProvider();
            sChanelRng.GetBytes(sBytes);
            Int32 sChanelValue = sBytes[0];
            byte A = 255, R, G, B;
            if (sChanelValue <= 85)
            {
                R = 252;
                G = (byte)(179 + 66 * sBytes[2] / 255);
                B = (byte)(179 + 66 * sBytes[3] / 255);
            }
            else if (sChanelValue <= 170)
            {
                G = 252;
                R = (byte)(179 + 66 * sBytes[1] / 255);
                B = (byte)(179 + 66 * sBytes[3] / 255);
            }
            else
            {
                B = 252;
                R = (byte)(179 + 66 * sBytes[1] / 255);
                G = (byte)(179 + 66 * sBytes[2] / 255);
            }
            return Color.FromArgb(A, R, G, B);
        }
        #endregion
    }

    public class SimpleRasterLayerRenderer
    {
        #region 字段
        /// <summary>
        /// 栅格图像
        /// </summary>
        public Bitmap bitmap;
        /// <summary>
        /// 图像在空间坐标中的包围盒
        /// </summary>
        public RectBox originalMBR;
        /// <summary>
        /// 控制参数：是否可见
        /// </summary>
        public bool isVisible = true;
        #endregion

        #region 构造
        /// <summary>
        /// 给定栅格数据大小的空构造
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public SimpleRasterLayerRenderer(int width, int height)
        {
            this.bitmap = new Bitmap(width,height);
        }

        /// <summary>
        /// 给定栅格数据的构造
        /// </summary>
        /// <param name="raster">栅格数据</param>
        public SimpleRasterLayerRenderer(Bitmap raster)
        {
            this.bitmap = raster;
        }
        #endregion

        #region 渲染
        /// <summary>
        /// 渲染栅格图层
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cam">相机</param>
        public void RenderLayer(Graphics g, Camera cam)
        {
            PointF upperLeft = new PointF((float)originalMBR.TopLeftPoint.coordX, (float)originalMBR.TopLeftPoint.coordY);
            PointF upperRight = new PointF((float)originalMBR.TopRightPoint.coordX, (float)originalMBR.TopRightPoint.coordY);
            PointF lowerLeft = new PointF((float)originalMBR.BottomLeftPoint.coordX, (float)originalMBR.BottomLeftPoint.coordY);
            //空间定位
            PointF[] locator =
            {
                upperLeft,upperRight,lowerLeft
            };
            //转到屏幕定位
            cam.GetViewMatrix().TransformPoints(locator);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.DrawImage(this.bitmap, locator);
        }
        #endregion

    }
}
