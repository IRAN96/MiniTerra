using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moProjectedCoordinateSystem : moCoordinateSystem
    {
        #region 字段
        string _ProjCSName;//投影坐标系名称
        moGeoCoordinateSystem _GeoCS;//地理坐标系
        string _ProjName;//投影名称
        ProjectionType _ProjType;//投影类型
        double _OriginLatitude;//原点纬度
        double _CentralMeridian;//中央经线
        double _FalseEasting;//东伪偏移
        double _FalseNorthing;//北伪偏移
        double _StandardParallelOne;//标准纬线一
        double _StandardParallelTwo;//标准纬线二
        double _ScaleFactor;//比例因子
        LinearUnitType _LinearUnit;//线性单位

        #endregion

        #region 属性
        public override CoordinateSystemTypeConstant CoordinateSystemType
        {
            get { return CoordinateSystemTypeConstant.ProjectedCoordinate; }
        }
        public string ProjCSName
        {
            get { return _ProjCSName; }
            set { _ProjCSName = value; }
        }
        public moGeoCoordinateSystem GeoCS
        {
            get { return _GeoCS; }
            set { _GeoCS = value; }
        }
        public string ProjName
        {
            get { return _ProjName; }
            set { _ProjName = value; }
        }
        public ProjectionType ProjType
        {
            get { return _ProjType; }
            set { _ProjType = value; }
        }
        public double OriginLatitude
        {
            get { return _OriginLatitude; }
            set { _OriginLatitude = value; }
        }
        public double CentralMeridian
        {
            get { return _CentralMeridian; }
            set { _CentralMeridian = value; }
        }
        public double FalseEasting
        {
            get { return _FalseEasting; }
            set { _FalseEasting = value; }
        }
        public double FalseNorthing
        {
            get { return _FalseNorthing; }
            set { _FalseNorthing = value; }
        }
        public double StandardParallelOne
        {
            get { return _StandardParallelOne; }
            set { _StandardParallelOne = value; }
        }
        public double StandardParallelTwo
        {
            get { return _StandardParallelTwo; }
            set { _StandardParallelTwo = value; }
        }
        public double ScaleFactor
        {
            get { return _ScaleFactor; }
            set { _ScaleFactor = value; }
        }
        public LinearUnitType LinearUnit
        {
            get { return _LinearUnit; }
            set { _LinearUnit = value; }
        }
        #endregion
    }
}
