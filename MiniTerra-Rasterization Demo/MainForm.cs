using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Geometria.FileIO;
using Geometria.Layers;
using Geometria.Enumerates;
using Geometria.Geometry;

namespace MiniTerra
{
    public partial class RasterizeDemo : Form
    {
        #region 窗体全局变量
        public GISSimpleUniqueLayer layer;
        public Camera viewCamera;

        public SimpleUniqueLayerRenderer vectorRenderer;
        public SimpleRasterLayerRenderer rasterRenderer;
        #endregion

        #region 构造
        public RasterizeDemo()
        {
            InitializeComponent();

            // 变量初始化
            layer = new GISSimpleUniqueLayer("new layer",GeometryObjectType.Polygon);
            viewCamera = new Camera(0, 0);
            vectorRenderer = new SimpleUniqueLayerRenderer();
            rasterRenderer = new SimpleRasterLayerRenderer(1,1);

            // 勾选初始化
            this.VectorVisibleBox.Checked = true;
            this.RasterVisibleBox.Checked = true;
        }
        #endregion

        #region 渲染
        //渲染MapDisplay控件
        private void MapDisplay_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush fill = new SolidBrush(Color.Transparent);
            Pen edge = new Pen(Color.Black, 1.0f);

            //渲染栅格
            if (rasterRenderer.bitmap.Width > 1 && rasterRenderer.isVisible)
            {
                rasterRenderer.RenderLayer(g, viewCamera);
            }

            //渲染矢量
            if (layer.featureList.Count > 0 && vectorRenderer.isVisible)
            {
                vectorRenderer.RenderLayer(g, viewCamera, fill, edge);
            }
        }
        #endregion

        #region 漫游
        //缩放控制
        private void MapDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            viewCamera.MouseScrollResponse(sender, e);
            MapDisplay.Refresh();
        }
        
        //漫游控制三部曲：鼠标按下-拖动-抬起
        private void MapDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            viewCamera.MouseDownResponse(sender, e);
            MapDisplay.Cursor = Cursors.Hand;
        }

        private void MapDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            viewCamera.MouseMoveResponse(sender, e);
            MapDisplay.Refresh();
        }

        private void MapDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            viewCamera.MouseUpResponse(sender, e);
            MapDisplay.Cursor = Cursors.Default;
        }
        #endregion

        #region 菜单栏功能
        //加载矢量图层
        private void LoadVectorMenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                //发起文件读取对话
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Filter = "E00文件(*.e00)|*.e00|所有文件(*.*)|*.*";
                openfile.RestoreDirectory = true;
                openfile.Multiselect = false;

                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    //读取图层
                    layer = FileReader.LoadE00FileToSimpleUniqueLayer(openfile.FileName);
                    this.vectorRenderer.layer = layer;
                    //调整显示
                    viewCamera.FocusOnGeometry(layer.MBR, MapDisplay.Width, MapDisplay.Height);
                    MapDisplay.Refresh();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        //加载栅格图层
        private void LoadRasterMenuItem_Clicked(object sender, EventArgs e)
        {
            //因为加载图片并不带有空间信息，默认直接借用矢量图层的信息
            if (this.layer.MBR.IsEmpty())
            {
                MessageBox.Show("请先加载矢量文件，获得空间信息！");
            }
            else
            {
                try
                {
                    //发起文件读取对话
                    OpenFileDialog openfile = new OpenFileDialog();
                    openfile.Filter = "PNG文件(*.png)|*.png|所有文件(*.*)|*.*";
                    openfile.RestoreDirectory = true;
                    openfile.Multiselect = false;

                    if (openfile.ShowDialog() == DialogResult.OK)
                    {
                        //读取图层
                        rasterRenderer.bitmap = new Bitmap(openfile.FileName);
                        rasterRenderer.originalMBR = this.layer.MBR;
                        //调整显示
                        viewCamera.FocusOnGeometry(layer.MBR, MapDisplay.Width, MapDisplay.Height);
                        MapDisplay.Refresh();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        //保存栅格图层
        private void SaveRasterMenuItem_Clicked(object sender, EventArgs e)
        {
            if (this.rasterRenderer.bitmap.Width > 1)
            {
                try
                {
                    //发起存储对话
                    SaveFileDialog safefile = new SaveFileDialog();
                    safefile.OverwritePrompt = true;
                    safefile.RestoreDirectory = true;
                    safefile.Filter = "PNG文件(*.png)|*.png";
                    if (safefile.ShowDialog() == DialogResult.OK)
                    {
                        string filename = safefile.FileName;
                        rasterRenderer.bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MessageBox.Show("当前没有栅格图层！");
            }

        }

        //栅格化
        private void RasterizeMenuItem_Clicked(object sender, EventArgs e)
        {
            if (this.layer.featureList.Count > 0)
            {
                //栅格化
                Bitmap raster = this.vectorRenderer.RanColorRasterize(2000);
                rasterRenderer = new SimpleRasterLayerRenderer(raster);
                rasterRenderer.originalMBR = this.layer.MBR;

                MapDisplay.Refresh();
            }
        }
        #endregion

        #region 图层显示控制
        //矢量图层是否显示
        private void VectorVisibleBox_CheckedStateChanged(object sender, EventArgs e)
        {
            vectorRenderer.isVisible = VectorVisibleBox.Checked;
            MapDisplay.Refresh();
        }

        //栅格图层是否显示
        private void RasterVisibleBox_CheckStateChanged(object sender, EventArgs e)
        {
            rasterRenderer.isVisible = RasterVisibleBox.Checked;
            MapDisplay.Refresh();
        }
        #endregion

    }
}
