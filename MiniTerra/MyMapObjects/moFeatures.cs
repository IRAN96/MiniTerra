using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 要素集合类型
    /// </summary>
    public class moFeatures
    {
        #region 字段
        private List<moFeature> _Features;

        #endregion
        #region 构造函数
        public moFeatures()
        {
            _Features = new List<moFeature>();
        }

        #endregion
        #region 属性
        public Int32 Count
        {
            get { return _Features.Count; }
        }
        #endregion
        #region 方法
        public moFeature GetItem(Int32 index)
        {
            return _Features[index];
        }
        public void SetItem(Int32 index,moFeature feature)
        {
            _Features[index] = feature;
        }
        public void Add(moFeature feature)
        {
            _Features.Add(feature);
        }
        public void RemoveAt(Int32 index)
        {
            _Features.RemoveAt(index);
        }
        /// <summary>
        /// 删除指定元素
        /// </summary>
        /// <param name="feature"></param>
        public void Remove(moFeature feature)
        {
            _Features.Remove(feature);
        }
        public void Clear()
        {
            _Features.Clear();
        }
        #endregion
    }
}
