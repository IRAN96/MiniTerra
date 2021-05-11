using System;
using System.Collections.Generic;
using System.Text;

namespace Geometria
{
    /// <summary>
    /// [抽象]几何对象。在二维直角坐标系下具有有限的范围。
    /// </summary>
    public abstract class GeometryObject
    {
        /// <summary>
        /// 对象的全局ID
        /// </summary>
        public uint id;

        /// <summary>
        /// 获取自身的包围盒
        /// </summary>
        public abstract RectBox MBR
        {
            get;
        }

        /// <summary>
        /// 获取自身的包围盒
        /// </summary>
        /// <returns></returns>
        public abstract RectBox GetMBR();
    }

    /// <summary>
    /// [抽象]几何对象有限集合。在二维直角坐标系下具有有限的范围。
    /// <para>继承自：GeometryObject</para>
    /// </summary>
    public abstract class GeometryMultiObject: GeometryObject
    {
        /// <summary>
        /// 获取自身内部单体对象的数量
        /// </summary>
        public abstract int Count
        {
            get;
        }
    }

    /// <summary>
    /// 用于GIS的，double类型的二维点。包含点的基础处理方法集。
    /// <para>继承自：GeometryObject</para>
    /// </summary>
    public class GeoPoint: GeometryObject
    {
        #region 字段
        //点的二维坐标
        public double coordX, coordY;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造。
        /// </summary>
        public GeoPoint()
        {
            this.coordX = 0;
            this.coordY = 0;
        }

        /// <summary>
        /// 代有坐标参数的构造。参数为double类型。
        /// </summary>
        /// <param name="x">x坐标值</param>
        /// <param name="y">y坐标值</param>
        public GeoPoint(double x, double y)
        {
            this.coordX = x;
            this.coordY = y;
        }

        /// <summary>
        /// 自Vector2D的转换构造。
        /// </summary>
        /// <param name="vec"></param>
        public GeoPoint(Vector2D vec)
        {
            this.coordX = vec.x;
            this.coordY = vec.y;
        }
        #endregion

        #region 派生属性
        public override RectBox MBR
        {
            get
            {
                return new RectBox(this, this);
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获得自身的拷贝。
        /// </summary>
        /// <returns></returns>
        public GeoPoint Clone()
        {
            return new GeoPoint(this.coordX, this.coordY);
        }

        /// <summary>
        /// 获得点的包围盒。点的包围盒即为位于该点的0大小矩形。
        /// </summary>
        /// <returns></returns>
        public override RectBox GetMBR()
        {
            return new RectBox(this, this);
        }
        #endregion

        #region 数学计算
        /// <summary>
        /// 计算两点之间的欧氏距离。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double Distance(GeoPoint A, GeoPoint B)
        {
            return Math.Sqrt((B.coordX - A.coordX) * (B.coordX - A.coordX) + (B.coordY - A.coordY) * (B.coordY - A.coordY));
        }

        /// <summary>
        /// 计算两点之间欧氏距离的平方。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double DistanceSquared(GeoPoint A, GeoPoint B)
        {
            return (B.coordX - A.coordX) * (B.coordX - A.coordX) + (B.coordY - A.coordY) * (B.coordY - A.coordY);
        }

        /// <summary>
        /// 检查两点在给定误差范围内是否可认为是重合的（考虑到浮点数的精度偏差）。
        /// </summary>
        /// <param name="other">被检查重合的另一点</param>
        /// <param name="errorRange">误差范围</param>
        /// <returns></returns>
        public bool Overlaps(GeoPoint other, double errorRange = 1e-14)
        {
            if(DistanceSquared(this,other) <= errorRange * errorRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查两点是否相同。使用double类型的==运算符判定。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        //重载Equals的静态、动态方法
        public static bool Equals(GeoPoint A, GeoPoint B)
        {
            if (A.coordX == B.coordX && A.coordY == B.coordY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查两点是否相同。使用double类型的==运算符判定。
        /// </summary>
        /// <param name="other">被检查的另一点</param>
        /// <returns></returns>
        public bool Equals(GeoPoint other)
        {
            if (this.coordX == other.coordX && this.coordY == other.coordY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //重载==和!=运算符
        public static bool operator==(GeoPoint A,GeoPoint B)
        {
            return Equals(A, B);
        }
        public static bool operator !=(GeoPoint A, GeoPoint B)
        {
            return !Equals(A, B);
        }

        //重载-运算符。两点之差定义为从后一点到前一点的向量。
        public static Vector2D operator-(GeoPoint end,GeoPoint start)
        {
            return new Vector2D(end.coordX - start.coordX, end.coordY - start.coordY);
        }
        #endregion

        #region 坐标变换
        /// <summary>
        /// 平移指定距离。距离以两个double坐标输入。直接修改自身的属性。
        /// </summary>
        /// <param name="dx">x方向距离</param>
        /// <param name="dy">y方向距离</param>
        public void MoveBy(double dx, double dy)
        {
            this.coordX += dx;
            this.coordY += dy;
        }

        /// <summary>
        /// 平移指定距离。距离以一个Vector2D输入。直接修改自身的属性。
        /// </summary>
        /// <param name="vec"></param>
        public void MoveBy(Vector2D vec)
        {
            this.coordX += vec.x;
            this.coordY += vec.y;
        }

        /// <summary>
        /// 平移指定距离。距离以两个double坐标输入。给出平移后的结果。
        /// </summary>
        /// <param name="dx">x方向距离</param>
        /// <param name="dy">y方向距离</param>
        public GeoPoint MovedBy(double dx, double dy)
        {
            return new GeoPoint(this.coordX + dx, this.coordY + dy);
        }

        /// <summary>
        /// 平移指定距离。距离以一个Vector2D输入。给出平移后的结果。
        /// </summary>
        /// <param name="vec"></param>
        public GeoPoint MovedBy(Vector2D vec)
        {
            return new GeoPoint(this.coordX + vec.x, this.coordY + vec.y);
        }

        /// <summary>
        /// 以原点为中心，将点进行缩放移动。缩放比例为double类型。
        /// </summary>
        /// <param name="scaleFactor">缩放比例</param>
        public void ScaleBy(double scaleFactor)
        {
            this.coordX *= scaleFactor;
            this.coordY *= scaleFactor;
        }

        /// <summary>
        /// 以原点为中心，将点进行旋转移动。旋转角度为弧度制，逆时针方向为正。
        /// </summary>
        /// <param name="radians">旋转角度（弧度制）</param>
        public void RotateBy(double radians)
        {
            //存储原坐标信息
            double tempX = this.coordX;
            double tempY = this.coordY;
            //计算旋转变换参数
            double sinVal = Math.Sin(radians);
            double cosVal = Math.Cos(radians);
            //旋转变换
            this.coordX = tempX * cosVal - tempY * sinVal;
            this.coordY = tempX * sinVal + tempY * cosVal;
        }
        #endregion
    }

    /// <summary>
    /// 用于GIS的，double类型的二维向量。包含向量的基础运算。
    /// </summary>
    public class Vector2D
    {
        #region 字段
        //二维坐标
        public double x, y;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造。
        /// </summary>
        public Vector2D()
        {
            this.x = 0;
            this.y = 0;
        }

        /// <summary>
        /// 带有坐标参数的构造。参数为double类型。
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public Vector2D(double x,double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 自GeoPoint类型的转换构造。
        /// </summary>
        /// <param name="pt"></param>
        public Vector2D(GeoPoint pt)
        {
            this.x = pt.coordX;
            this.y = pt.coordY;
        }

        /// <summary>
        /// 自Segment类型的转换构造。
        /// </summary>
        /// <param name="seg"></param>
        public Vector2D(Segment seg)
        {
            this.x = seg.endPoint.coordX - seg.startPoint.coordX;
            this.y = seg.endPoint.coordY - seg.startPoint.coordY;
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获得自身的拷贝。
        /// </summary>
        /// <returns></returns>
        public Vector2D Clone()
        {
            return new Vector2D(this.x, this.y);
        }
        #endregion

        #region 数学计算
        /// <summary>
        /// 计算向量的长度。
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y);
        }

        /// <summary>
        /// 计算向量长度的平方。
        /// </summary>
        /// <returns></returns>
        public double LengthSquared()
        {
            return this.x * this.x + this.y * this.y;
        }

        /// <summary>
        /// 计算向量的方向。以x轴正方向为0，逆时针为正，弧度制。范围是-pi到pi。
        /// </summary>
        /// <returns></returns>
        public double Direction()
        {
            return Math.Atan2(this.y, this.x);
        }

        /// <summary>
        /// 判断两向量是否相等。使用double类型的==运算符判定。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        //重载Equals的静态、动态方法
        public static bool Equals(Vector2D A, Vector2D B)
        {
            if (A.x == B.x && A.y == B.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断两向量是否相等。使用double类型的==运算符判定。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector2D other)
        {
            if (this.x == other.x && this.y == other.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //重载==和!=运算符
        public static bool operator ==(Vector2D A, Vector2D B)
        {
            return Equals(A, B);
        }

        public static bool operator !=(Vector2D A, Vector2D B)
        {
            return !Equals(A, B);
        }

        //重载取负运算符
        public static Vector2D operator -(Vector2D A)
        {
            return new Vector2D(-A.x, -A.y);
        }

        //重载 + 和 - 运算符
        public static Vector2D operator +(Vector2D A,Vector2D B)
        {
            return new Vector2D(A.x + B.x, A.y + B.y);
        }
        public static Vector2D operator -(Vector2D A,Vector2D B)
        {
            return new Vector2D(A.x - B.x, A.y - B.y);
        }

        //重载 * 运算符：数乘
        public static Vector2D operator *(double k,Vector2D vec)
        {
            return new Vector2D(k * vec.x, k * vec.y);
        }

        //重载 * 运算符：直积
        public static Vector2D operator *(Vector2D A,Vector2D B)
        {
            return new Vector2D(A.x * B.x, A.y * B.y);
        }

        /// <summary>
        /// 计算两向量的内积。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double DotProduct(Vector2D A,Vector2D B)
        {
            return A.x * B.x + A.y * B.y;
        }

        /// <summary>
        /// 计算两向量外积的值大小。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns>两向量外积的值大小。其正负号表示方向。</returns>
        public static double CrossProductVal(Vector2D A,Vector2D B)
        {
            return A.x * B.y - A.y * B.x;
        }

        /// <summary>
        /// 获得单位向量。直接修改自身属性。
        /// </summary>
        public void Unitize()
        {
            double len = this.Length();
            this.x /= len;
            this.y /= len;
        }

        /// <summary>
        /// 获得单位向量。返回单位化的结果。
        /// </summary>
        /// <returns></returns>
        public Vector2D Unitized()
        {
            double len = this.Length();
            return new Vector2D(this.x / len, this.y / len);
        }
        #endregion

    }

    /// <summary>
    /// 用于GIS的，double类型的矩形类。主要作为外包矩形使用。
    /// </summary>
    public class RectBox
    {
        #region 字段
        //自身的x轴和y轴范围
        public double xMin, xMax, yMin, yMax;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造。
        /// </summary>
        public RectBox()
        {
            this.xMin = 0;
            this.xMax = 0;
            this.yMin = 0;
            this.yMax = 0;
        }

        /// <summary>
        /// 带参数的构造。参数为double类型。
        /// </summary>
        /// <param name="xmin">x最小值</param>
        /// <param name="xmax">x最大值</param>
        /// <param name="ymin">y最小值</param>
        /// <param name="ymax">y最大值</param>
        public RectBox(double xmin, double xmax, double ymin, double ymax)
        {
            //自动判断输入值的大小并且赋给正确的属性，使得xMax >= xMin成立.
            if(xmax >= xmin)
            {
                this.xMin = xmin;
                this.xMax = xmax;
            }
            else
            {
                this.xMin = xmax;
                this.xMax = xmin;
            }
            //对y方向同理
            if (ymax >= ymin)
            {
                this.yMin = ymin;
                this.yMax = ymax;
            }
            else
            {
                this.yMin = ymax;
                this.yMax = ymin;
            }
        }

        /// <summary>
        /// 使用两点构造。这两点将作为矩形的一组对角点。
        /// </summary>
        /// <param name="diagPt1"></param>
        /// <param name="diagPt2"></param>
        public RectBox(GeoPoint diagPt1, GeoPoint diagPt2)
        {
            double xmin = diagPt1.coordX;
            double xmax = diagPt2.coordX;
            double ymin = diagPt1.coordY;
            double ymax = diagPt2.coordY;

            if (xmax >= xmin)
            {
                this.xMin = xmin;
                this.xMax = xmax;
            }
            else
            {
                this.xMin = xmax;
                this.xMax = xmin;
            }

            if (ymax >= ymin)
            {
                this.yMin = ymin;
                this.yMax = ymax;
            }
            else
            {
                this.yMin = ymax;
                this.yMax = ymin;
            }
        }
        #endregion       

        #region 派生属性
        /// <summary>
        /// 获得左下角点。
        /// </summary>
        /// <returns></returns>
        //这里假设平面直角坐标系是一个最常见的右手系
        public GeoPoint BottomLeftPoint
        {
            get
            {
                return new GeoPoint(this.xMin, this.yMin);
            }     
        }

        /// <summary>
        /// 获得左上角点。
        /// </summary>
        /// <returns></returns>
        public GeoPoint TopLeftPoint
        {
            get
            {
                return new GeoPoint(this.xMin, this.yMax);
            }
        }

        /// <summary>
        /// 获得右下角点。
        /// </summary>
        /// <returns></returns>
        public GeoPoint BottomRightPoint
        {
            get
            {
                return new GeoPoint(this.xMax, this.yMin);
            }
        }

        /// <summary>
        /// 获得右上角点。
        /// </summary>
        /// <returns></returns>
        public GeoPoint TopRightPoint
        {
            get
            {
                return new GeoPoint(this.xMax, this.yMax);
            }
        }

        /// <summary>
        /// 获取矩形的宽度。
        /// </summary>
        public double Width
        {
            get
            {
                return this.xMax - this.xMin;
            }
        }

        /// <summary>
        /// 获取矩形的高度。
        /// </summary>
        public double Height
        {
            get
            {
                return this.yMax - this.yMin;
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获得自身的拷贝。
        /// </summary>
        /// <returns></returns>
        public RectBox Clone()
        {
            return new RectBox(this.xMin, this.xMax, this.yMin, this.yMax);
        }

        /// <summary>
        /// 矩形是否是空矩形（即区域面积为0的矩形）
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this.yMax <= this.yMin || this.xMax <= this.xMin;
        }

        /// <summary>
        /// 获得矩形的大小。大小以从左下角指向右上角的二维向量表出。
        /// </summary>
        /// <returns>矩形左下角指向右上角的向量。</returns>
        public Vector2D GetSize()
        {
            return new Vector2D(this.xMax - this.xMin, this.yMax - this.yMin);
        }
        #endregion

        #region 拓扑关系
        //判断点和矩形的关系
        /// <summary>
        /// 判断点是否在矩形内部（包括边界）。
        /// </summary>
        /// <param name="point">需要检查的点</param>
        /// <returns></returns>
        public bool Contains(GeoPoint point)
        {
            if(point.coordX >= this.xMin &&
                point.coordX <= this.xMax &&
                point.coordY >= this.yMin &&
                point.coordY <= this.yMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断矩形之间的关系
        /// <summary>
        /// 判断另一个矩形是否在该矩形内部。公共边重合的包含情况算在内。
        /// </summary>
        /// <param name="target">需要判断的矩形</param>
        /// <returns></returns>
        public bool Contains(RectBox target)
        {
            if(this.xMin <= target.xMin &&
                this.xMax >= target.xMax &&
                this.yMin <= target.yMin &&
                this.yMax >= target.yMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断两矩形是否相交。任何二者有公共点的情况都算在内。
        /// </summary>
        /// <param name="target">需要判断的另一矩形</param>
        /// <returns></returns>
        public bool Overlaps(RectBox target)
        {
            if(this.xMin > target.xMax ||
                this.xMax < target.xMin ||
                this.yMin > target.yMax ||
                this.yMax < target.yMin)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断两矩形是否相交。任何二者有公共点的情况都算在内。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool Overlaps(RectBox A, RectBox B)
        {
            if (A.xMin > B.xMax ||
                A.xMax < B.xMin ||
                A.yMin > B.yMax ||
                A.yMax < B.yMin)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 同类型计算
        /// <summary>
        /// 拼合两个矩形，给出能够包含这两个矩形的最小外包矩形。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns>同时包含A和B的最小外包矩形。</returns>
        public static RectBox Merge(RectBox A, RectBox B)
        {
            return new RectBox(Math.Min(A.xMin, B.xMin), Math.Max(A.xMax, B.xMax),
                Math.Min(A.yMin, B.yMin), Math.Max(A.yMax, B.yMax));
        }

        //重载 + 运算符为拼合
        public static RectBox operator +(RectBox A, RectBox B)
        {
            return Merge(A, B);
        }
        #endregion
    }

    /// <summary>
    /// 点相对于直线的方向
    /// </summary>
    public enum SegmentSide
    {
        /// <summary>
        /// 左侧
        /// </summary>
        Left,
        /// <summary>
        /// 右侧
        /// </summary>
        Right,
        /// <summary>
        /// 异常值
        /// </summary>
        Error
    }

    /// <summary>
    /// 用于GIS的，double类型的线段类。不参与Polyline的存储，主要用于线段相关的集合运算及拓扑运算。
    /// 线段默认的方向从起始点指向终点（在变量名上体现）。
    /// </summary>
    public class Segment
    {
        #region 字段
        public GeoPoint startPoint, endPoint;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造。
        /// </summary>
        public Segment()
        {
            this.startPoint = new GeoPoint();
            this.endPoint = new GeoPoint();
        }

        /// <summary>
        /// 使用两点坐标的构造。坐标以四个double类型给出。
        /// </summary>
        /// <param name="startX">起点x坐标</param>
        /// <param name="startY">起点y坐标</param>
        /// <param name="endX">终点x坐标</param>
        /// <param name="endY">终点y坐标</param>
        public Segment(double startX,double startY,double endX,double endY)
        {
            this.startPoint = new GeoPoint(startX, startY);
            this.endPoint = new GeoPoint(endX, endY);
        }

        /// <summary>
        /// 使用两GeoPoint对象作为首尾点的构造。
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        public Segment(GeoPoint start,GeoPoint end)
        {
            this.startPoint = start;
            this.endPoint = end;
        }

        /// <summary>
        /// 使用一GeoPoint对象作为起点，Vector2D作为线段走向的构造。
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="vec">线段走向。终点将由起点沿该向量平移得到。</param>
        public Segment(GeoPoint start, Vector2D vec)
        {
            this.startPoint = start;
            this.endPoint = start.MovedBy(vec);
        }
        #endregion

        #region 派生属性
        /// <summary>
        /// 获取自身的外包矩形/包围盒
        /// </summary>
        public RectBox MBR
        {
            get
            {
                return new RectBox(this.startPoint, this.endPoint);
            }
        }

        /// <summary>
        /// 获取自身的长度
        /// </summary>
        public double Length
        {
            get
            {
                double dx = this.endPoint.coordX - this.startPoint.coordX;
                double dy = this.endPoint.coordY - this.startPoint.coordY;

                return Math.Sqrt(dx * dx + dy * dy);
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获得自身的拷贝
        /// </summary>
        /// <returns></returns>
        public Segment Clone()
        {
            return new Segment(this.startPoint.Clone(), this.endPoint.Clone());
        }

        /// <summary>
        /// 获取自身的外包矩形/包围盒
        /// </summary>
        /// <returns></returns>
        public RectBox GetMBR()
        {
            return new RectBox(this.startPoint, this.endPoint);
        }

        /// <summary>
        /// 检查线段自身是否退化为一个点。
        /// </summary>
        /// <returns></returns>
        public bool isCollapsed()
        {
            return this.startPoint == this.endPoint;
        }
        #endregion

        #region 数学计算
        /// <summary>
        /// 获得自身的长度。
        /// </summary>
        /// <returns></returns>
        public double GetLength()
        {
            double dx = this.endPoint.coordX - this.startPoint.coordX;
            double dy = this.endPoint.coordY - this.startPoint.coordY;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 获得自身长度的平方。
        /// </summary>
        /// <returns></returns>
        public double GetLengthSquared()
        {
            double dx = this.endPoint.coordX - this.startPoint.coordX;
            double dy = this.endPoint.coordY - this.startPoint.coordY;

            return dx * dx + dy * dy;
        }

        /// <summary>
        /// 获得线段的方向。方向的定义等同于向量方向的定义。
        /// </summary>
        /// <returns></returns>
        public double GetDirection()
        {
            return (this.endPoint - this.startPoint).Direction();
        }

        /// <summary>
        /// 获得自身的单位方向向量。
        /// </summary>
        /// <returns></returns>
        public Vector2D GetDirectionalVector()
        {
            //退化情况，返回空向量
            if (this.isCollapsed())
            {
                return new Vector2D(0, 0);
            }
            else {
                Vector2D dir = new Vector2D(this.endPoint.coordX - this.startPoint.coordX,
                    this.endPoint.coordY - this.startPoint.coordY);
                return dir.Unitized();
            }
        }

        /// <summary>
        /// 获得自身的单位左侧法向向量。左侧相对于自身方向正向而言。
        /// </summary>
        /// <returns></returns>
        public Vector2D GetLeftNormalVector()
        {
            //退化情况，返回空向量
            if (this.isCollapsed())
            {
                return new Vector2D(0, 0);
            }
            else
            {
                Vector2D dir = this.GetDirectionalVector();
                return new Vector2D(dir.y, -dir.x);
            }
        }

        /// <summary>
        /// 获得两线段的相对方向，以其中一条线段作为参考方向，逆时针为正，弧度制。范围是-pi到pi。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static double GetRelativeDirection(Segment target,Segment reference)
        {
            Vector2D refVec = reference.GetDirectionalVector();
            Vector2D tarVec = new Vector2D(target);
            double dir = Vector2D.DotProduct(refVec, tarVec);
            double norm = Vector2D.CrossProductVal(refVec, tarVec);

            return Math.Atan2(norm, dir);
        }

        /// <summary>
        /// 获取给定x位置的，线段所在直线上的点。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public GeoPoint GetPointAtX(double x)
        {
            //退化情况，返回原点
            if (this.isCollapsed())
            {
                return new GeoPoint(0, 0);
            }
            else
            {
                double dx = x - this.startPoint.coordX;
                double dy = (this.endPoint.coordY - this.startPoint.coordY) / (this.endPoint.coordX - this.startPoint.coordX) * dx;

                return new GeoPoint(x, this.startPoint.coordY + dy);
            }
        }

        /// <summary>
        /// 获取给定y位置的，线段所在直线上的点。
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public GeoPoint GetPointAtY(double y)
        {
            //退化情况，返回原点
            if (this.isCollapsed())
            {
                return new GeoPoint(0, 0);
            }
            else
            {
                double dy = y - this.startPoint.coordY;
                double dx = (this.endPoint.coordX - this.startPoint.coordX) / (this.endPoint.coordY - this.startPoint.coordY) * dy;

                return new GeoPoint(this.startPoint.coordX + dx, y);
            }
        }

        /// <summary>
        /// 计算指定点到该线段的投影距离。距离考虑线段的延长线。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public double GetPointOffset(GeoPoint pt)
        {
            //退化情况，按照点的距离处理
            if (this.isCollapsed())
            {
                return GeoPoint.Distance(this.startPoint, pt);
            }
            else
            {
                Vector2D norm = this.GetLeftNormalVector();
                Vector2D vecPt = new Vector2D(pt.coordX - this.startPoint.coordX, pt.coordY - this.startPoint.coordY);
                return Math.Abs(Vector2D.DotProduct(vecPt, norm));
            }
        }

        /// <summary>
        /// 计算指定点到该线段的距离。距离不考虑线段的延长线。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public double GetPointDistance(GeoPoint pt)
        {
            //退化情况，按照点距离处理
            if (this.isCollapsed())
            {
                return GeoPoint.Distance(this.startPoint, pt);
            }
            else
            {
                Vector2D dir = new Vector2D(this.endPoint.coordX - this.startPoint.coordX,
                    this.endPoint.coordY - this.startPoint.coordY);
                Vector2D vecPt = new Vector2D(pt.coordX - this.startPoint.coordX, pt.coordY - this.startPoint.coordY);
                double lenSqr = this.GetLengthSquared();

                //判断点的投影在线段的什么位置，并且分类计算
                double dirProduct = Vector2D.DotProduct(dir, vecPt);
                double distance = 0;
                if (dirProduct >= lenSqr)
                //投影在终点一侧的延长线上
                {
                    distance = GeoPoint.Distance(this.endPoint, pt);
                }
                else if (dirProduct <= 0)
                //投影在起点一侧的延长线上
                {
                    distance = GeoPoint.Distance(this.startPoint, pt);
                }
                else
                //投影在线段上
                {
                    dir.Unitize();
                    Vector2D norm = new Vector2D(dir.y, -dir.x);
                    distance = Math.Abs(Vector2D.DotProduct(vecPt, norm));
                }

                return distance;
            }
        }
        #endregion

        #region 几何关系
        /// <summary>
        /// 判断给定点是否在线段上（不包括延长线）
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="errorRange">误差范围</param>
        /// <returns></returns>
        public bool Includes(GeoPoint pt, double errorRange = 1e-14)
        {
            //退化情况，检查点重合
            if (this.isCollapsed())
            {
                return this.startPoint == pt;
            }
            else if (this.GetMBR().Contains(pt))
            //先进行包围盒检测
            {
                Vector2D norm = this.GetLeftNormalVector();
                Vector2D vecPt = new Vector2D(pt.coordX - this.startPoint.coordX, pt.coordY - this.startPoint.coordY);
                if(Vector2D.DotProduct(norm,vecPt) <= errorRange)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断给定点在该线段的左侧（包括线段所在直线上）或者右侧（不包括线段所在直线上）。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>枚举量SegmentSide.Left或SegmentSide.Right</returns>
        public SegmentSide GetSideOfPoint(GeoPoint pt)
        {
            if (this.isCollapsed())
            {
                return SegmentSide.Error;
            }
            Vector2D leftNorm = this.GetLeftNormalVector();
            Vector2D vecPt = new Vector2D(pt.coordX - this.startPoint.coordX, pt.coordY - this.startPoint.coordY);

            double product = Vector2D.DotProduct(leftNorm, vecPt);
            return product >= 0 ? SegmentSide.Left : SegmentSide.Right;
        }

        /// <summary>
        /// 判断给定点在该线段的左侧（包括线段所在直线上）或者右侧。左侧为True，右侧为False。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>该点是否在线段左侧或者线段所在直线上。</returns>
        public bool GetSideOfPoint_Bool(GeoPoint pt)
        {
            SegmentSide side = this.GetSideOfPoint(pt);
            if(side == SegmentSide.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断两线段是否相交。不考虑延长线相交的情况。
        /// </summary>
        /// <param name="lineA"></param>
        /// <param name="lineB"></param>
        /// <returns></returns>
        public static bool Intersects(Segment lineA, Segment lineB)
        {
            //有线段发生退化，不予处理直接返回不交
            if(lineA.isCollapsed() || lineB.isCollapsed())
            {
                return false;
            }
            else if (!lineA.GetMBR().Overlaps(lineB.GetMBR()))
            //包围盒排除
            {
                return false;
            }
            else if(lineB.Includes(lineA.startPoint) || lineB.Includes(lineA.endPoint) ||
                lineA.Includes(lineB.startPoint) || lineA.Includes(lineB.endPoint))
            //当一条线的端点在另一条线上时，一定相交
            {
                return true;
            }
            else
            //两线段求交测试：互相跨越对方
            {
                bool lineACrossesLineB = lineB.GetSideOfPoint_Bool(lineA.startPoint) ^ lineB.GetSideOfPoint_Bool(lineA.endPoint);
                bool lineBCrossesLineA = lineA.GetSideOfPoint_Bool(lineB.startPoint) ^ lineA.GetSideOfPoint_Bool(lineB.endPoint);

                if (lineACrossesLineB && lineBCrossesLineA)
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
        /// 判断当前线段与目标线段是否相交。不考虑延长线相交的情况。
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool Intersects(Segment line)
        {
            return Intersects(this, line);
        }

        /// <summary>
        /// 判断当前线段与包围盒是否相交（有公共点）。线段在包围盒内也记为相交。
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public bool Overlaps(RectBox box)
        {
            //退化，按点处理
            if (this.isCollapsed())
            {
                return box.Contains(this.startPoint);
            }
            if (!this.GetMBR().Overlaps(box))
            //包围盒排除
            {
                return false;
            }
            else if(box.Contains(this.startPoint) || box.Contains(this.endPoint))
            //端点在矩形内，必定相交
            {
                return true;
            }
            else if(box.Contains(this.GetPointAtX(box.xMin)) ||
                box.Contains(this.GetPointAtX(box.xMax)) ||
                box.Contains(this.GetPointAtY(box.yMin)) ||
                box.Contains(this.GetPointAtY(box.yMax)))
            //与矩形边界求交
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
