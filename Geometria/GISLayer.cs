using System;
using System.Collections.Generic;
using System.Text;

using Geometria.Geometry;
using Geometria.Attributes;
using Geometria.Enumerates;

namespace Geometria.Layers
{
    /// <summary>
    /// GIS的图层。功能最简单的系统唯一图层。
    /// </summary>
    public class GISSimpleUniqueLayer
    {
        #region 字段
        //元数据
        public string name;
        public FieldList attributeFieldList;
        private GeometryObjectType featureGeometryType;
        //地理实体
        public List<Feature> featureList;
        private RectBox mbr;
        private bool mbrUpdateRequired = false;
        #endregion

        #region 构造
        /// <summary>
        /// 仅给出名称和几何类型的构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public GISSimpleUniqueLayer(string name = "new layer", GeometryObjectType type = GeometryObjectType.Point)
        {
            this.name = name;
            this.attributeFieldList = new FieldList();
            this.featureGeometryType = type;

            this.featureList = new List<Feature>();
            this.mbr = new RectBox();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取自身的几何类型
        /// </summary>
        public GeometryObjectType FeatureGeometryType
        {
            get
            {
                return this.featureGeometryType;
            }
        }

        /// <summary>
        /// 获取自身的包围盒
        /// </summary>
        public RectBox MBR
        {
            get
            {
                if (this.mbrUpdateRequired)
                {
                    this.UpdateMBR();
                    this.mbrUpdateRequired = false;
                }
                return this.mbr;
            }
        }

        #endregion

        #region 基本方法
        /// <summary>
        /// 更新该对象的包围盒
        /// </summary>
        protected void UpdateMBR()
        {
            if (this.featureList.Count > 0)
            {
                this.mbr = this.featureList[0].GetMBR().Clone();
                foreach (Feature f in this.featureList)
                {
                    this.mbr += f.GetMBR();
                }
            }
            else
            {
                this.mbr = new RectBox();
            }

        }

        /// <summary>
        /// 获取自身的包围盒
        /// </summary>
        /// <returns></returns>
        public RectBox GetMBR()
        {
            if (this.mbrUpdateRequired)
            {
                this.UpdateMBR();
                this.mbrUpdateRequired = false;
            }
            return this.mbr;
        }

        /// <summary>
        /// 添加一个地理对象
        /// </summary>
        /// <param name="f"></param>
        public void AddFeature(Feature f)
        {
            //类型检查
            if(f.GeometryType == this.featureGeometryType)
            {
                this.featureList.Add(f.Clone());
                this.mbrUpdateRequired = true;
            }
            else
            {
                throw new Exception("添加的地理对象类型不符合");
            }
        }

        /// <summary>
        /// 删除指定位置的地理对象
        /// </summary>
        /// <param name="index"></param>
        public void DeleteFeature(int index)
        {
            this.featureList.RemoveAt(index);
            this.mbrUpdateRequired = true;
        }

        /// <summary>
        /// 删除指定的地理对象
        /// </summary>
        /// <param name="f"></param>
        public void DeleteFeature(Feature f)
        {
            this.featureList.Remove(f);
        }

        /// <summary>
        /// 获取给定下标的地理对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Feature GetFeature(int index)
        {
            return this.featureList[index];
        }

        /// <summary>
        /// 设置给定下标的地理对象
        /// </summary>
        /// <param name="index"></param>
        /// <param name="f"></param>
        public void SetFeature(int index, Feature f)
        {
            //类型检查
            if (f.GeometryType == this.featureGeometryType)
            {
                this.featureList[index] = f.Clone();
                this.mbrUpdateRequired = true;
            }
            else
            {
                throw new Exception("添加的地理对象类型不符合");
            }
        }

        //todo 对字段的增删操作
        #endregion
    }
}
