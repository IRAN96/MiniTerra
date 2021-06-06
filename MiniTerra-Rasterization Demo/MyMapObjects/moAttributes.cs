using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 属性集合类
    /// </summary>
    public class moAttributes
    {
        #region 字段
        private List<Object> _Attributes;
        #endregion
        #region 构造函数
        public moAttributes()
        {
            _Attributes = new List<object>();
        }
        #endregion
        #region 方法
        /// <summary>
        /// 获取指定索引号的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetItem(Int32 index)
        {
            return _Attributes[index];
        }
        /// <summary>
        /// 设置指定索引号的值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetItem(Int32 index,object value)
        {
            _Attributes[index] = value;
        }
        /// <summary>
        /// 将所有属性值复制到数组里
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            return _Attributes.ToArray();
        }
        /// <summary>
        /// 从值数组中获取所有的元素
        /// </summary>
        /// <param name="values"></param>
        public void FromArray(object[] values)
        {
            _Attributes.Clear();
            _Attributes.AddRange(values);
        }
        /// <summary>
        /// 追加一个值
        /// </summary>
        /// <param name="value"></param>
        public void Append(object value)
        {
            _Attributes.Add(value);
        }
        /// <summary>
        /// 删除指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(Int32 index)
        {
            _Attributes.RemoveAt(index);
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public moAttributes Clone()
        {
            moAttributes sAttributes = new moAttributes();
            sAttributes._Attributes.AddRange(_Attributes);
            return sAttributes;
        }
        #endregion
    }
}
