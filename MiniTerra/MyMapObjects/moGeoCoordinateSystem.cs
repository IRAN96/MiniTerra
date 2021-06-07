using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moGeoCoordinateSystem:moCoordinateSystem
    {
        #region 字段
        private string _GeoCSName;//坐标系名称
        private string _DatumName;//大地基准面名称
        private double _X;//椭球体定位参数
        private double _Y;
        private double _Z;
        private string _SpheroidName;//椭球体名称
        private double _SemiMajor;//椭球体长半轴
        private double _InverseFlattening;//椭球体扁率倒数
        private string _PrimeMeridianName;//初始经线名称
        private double _PrimeMeridian;//初始经线（主经线）
        private string _AnglarUnitName;//角度单位名称
        private AngleUnitType _AnglarUnit;//角度单位
        private double _RadiansPerUnit;//每单位的弧度
        #endregion

        #region 属性
        public override CoordinateSystemTypeConstant CoordinateSystemType
        {
            get { return CoordinateSystemTypeConstant.GeographicCoordinate; }
        }
        public string GeoCSName
        {
            get { return _GeoCSName; }
            set { _GeoCSName = value; }
        }
        public string DatumName
        {
            get { return _DatumName; }
            set { _DatumName = value; }
        }
        public double X
        {
            get { return _X; }
            set { _X = value; }
        }
        public double Y
        {
            get { return _Y; }
            set { _Y = value; }
        }
        public double Z
        {
            get { return _Z; }
            set { _Z = value; }
        }
        public string SpheroidName
        {
            get { return _SpheroidName; }
            set { _SpheroidName = value; }
        }
        public double SemiMajor
        {
            get { return _SemiMajor; }
            set { _SemiMajor = value; }
        }
        public double InverseFlattening
        {
            get { return _InverseFlattening; }
            set { _InverseFlattening = value; }
        }
        public string PrimeMeridianName
        {
            get { return _PrimeMeridianName; }
            set { _PrimeMeridianName = value; }
        }
        public double PrimeMeridian
        {
            get { return _PrimeMeridian; }
            set { _PrimeMeridian = value; }
        }
        public string AnglarUnitName
        {
            get { return _AnglarUnitName; }
            set { _AnglarUnitName = value; }
        }
        public AngleUnitType AnglarUnit
        {
            get { return _AnglarUnit; }
            set { _AnglarUnit = value; }
        }
        public double RadiansPerUnit
        {
            get { return _RadiansPerUnit; }
            set { _RadiansPerUnit = value; }
        }
        #endregion
    }

}
