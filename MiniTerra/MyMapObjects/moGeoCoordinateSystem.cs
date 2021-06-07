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
        private string _GeoCSName;
        private string _DatumName;
        private double _X;
        private double _Y;
        private double _Z;
        private string _SpheroidName;
        private double _SemiMajor;
        private double _InverseFlattening;
        private string _PrimeMeridianName;
        private double _PrimeMeridian;
        private string _AnglarUnitName;
        private double _RadiansPerUnit;
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
        #endregion

    }
}
