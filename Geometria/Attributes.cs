using System;
using System.Collections.Generic;
using System.Text;

using Geometria.Enumerates;

namespace Geometria.Attributes
{
    /// <summary>
    /// GIS的属性数据用。单个字段。
    /// </summary>
    public class Field
    {
        #region 字段
        private string name;
        public string alias = "";
        private AttributeDataType dataType;
        private int length; //这个是干嘛用的
        #endregion

        #region 构造
        /// <summary>
        /// 给定字段名、别名、类型[可选]和长度[可选]的构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <param name="val"></param>
        /// <param name="length"></param>
        public Field(string name, string alias = null,
            AttributeDataType type = AttributeDataType.Int32, int length = 0)
        {
            this.name = name;
            this.alias = alias;
            this.dataType = type;
            this.length = length;

        }

        /// <summary>
        /// 给定字段名、类型[可选]和长度[可选]的构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        public Field(string name, AttributeDataType type = AttributeDataType.Int32, int length = 0)
        {
            this.name = name;
            this.dataType = type;
            this.length = length;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取自身的名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// 获取自身的数据类型
        /// </summary>
        public AttributeDataType DataType
        {
            get
            {
                return this.dataType;
            }
        }

        /// <summary>
        /// 获取自身的长度
        /// </summary>
        public int Length
        {
            get
            {
                return this.length;
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获取自身的拷贝
        /// </summary>
        /// <returns></returns>
        public Field Clone()
        {
            return new Field(name, alias, dataType, length);

        }
        #endregion
    }

    /// <summary>
    /// GIS的属性数据用。一个图层中所有地理实体的字段列表。
    /// <para>包含有主码的信息。</para>
    /// </summary>
    public class FieldList
    {
        #region 字段
        private List<Field> fields;
        //主码
        public string primaryKey = "";
        //在显示的时候是否显示别名
        public bool showAlias = false;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public FieldList()
        {
            this.fields = new List<Field>();
        }

        /// <summary>
        /// 给定字段类型数组的构造
        /// </summary>
        /// <param name="fieldarr"></param>
        /// <param name="pmKey"></param>
        /// <param name="showAlias"></param>
        public FieldList(Field[] fieldArr, string pmKey = "", bool showAlias = false)
        {
            this.fields = new List<Field>();
            foreach(Field f in fieldArr)
            {
                if (IndexOfField(f.Name) == -1)
                {
                    this.fields.Add(f.Clone());
                }
                else
                {
                    throw new Exception("不可以添加名称相同的字段。");
                }
            }
            this.primaryKey = pmKey;
            this.showAlias = showAlias;
        }

        /// <summary>
        /// 给定字段类型列表的构造
        /// </summary>
        /// <param name="fieldarr"></param>
        /// <param name="pmKey"></param>
        /// <param name="showAlias"></param>
        public FieldList(List<Field> fieldList, string pmKey = "", bool showAlias = false)
        {
            this.fields = new List<Field>();
            foreach (Field f in fieldList)
            {
                if (IndexOfField(f.Name) == -1)
                {
                    this.fields.Add(f.Clone());
                }
                else
                {
                    throw new Exception("不可以添加名称相同的字段。");
                }
            }
            this.primaryKey = pmKey;
            this.showAlias = showAlias;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取字段列表的长度
        /// </summary>
        public int Count
        {
            get
            {
                return this.fields.Count;
            }
        }

        /// <summary>
        /// 获取指定下标的字段
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Field this[int index]
        {
            get
            {
                return this.fields[index];
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 根据字段名获取下标
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public int IndexOfField(string fieldName)
        {
            int outIndex = -1;
            for(int index = 0;index < this.fields.Count; ++index)
            {
                if(this[index].Name == fieldName)
                {
                    outIndex = index;
                    break;
                }
            }
            return outIndex;
        }
        
        /// <summary>
        /// 根据下标获取字段
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Field GetField(int index)
        {
            return this.fields[index];
        }

        /// <summary>
        /// 根据名称获取字段
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public Field GetField(string fieldName)
        {
            for (int index = 0; index < this.fields.Count; ++index)
            {
                if (this[index].Name == fieldName)
                {
                    return this[index];
                }
            }

            return null;
        }

        /// <summary>
        /// 添加一个新字段
        /// <para>新的字段的名称不得与已有字段重复。</para>
        /// </summary>
        /// <param name="f"></param>
        public void AddField(Field f)
        {
            //新字段的名称不与已有的重复
            if(IndexOfField(f.Name) == -1)
            {
                this.fields.Add(f.Clone());
                //发个事件
                if (FieldAppended != null)
                {
                    FieldAppended(this, f);
                }
            }
            else
            {
                throw new Exception("不可以添加名称相同的字段。");
            }
        }

        /// <summary>
        /// 删除一个字段
        /// </summary>
        /// <param name="index"></param>
        public void DeleteField(int index)
        {
            Field toDel = this.fields[index];
            this.fields.RemoveAt(index);
            //把被删除的字段信息送出去，供其他类做删除维护/撤销功能维护
            //发个事件出去
            if (FieldRemoved != null)
            {
                FieldRemoved(this, index, toDel);
            }
        }
        #endregion

        #region 事件

        internal delegate void FieldAppendedHandle(object sender, Field fieldAppended);
        internal event FieldAppendedHandle FieldAppended;

        internal delegate void FieldRemovedHandle(object sender, int fieldIndex, Field fieldDeleted);
        internal event FieldRemovedHandle FieldRemoved;
        #endregion
    }

    /// <summary>
    /// GIS的属性数据用。一个地理实体的属性值列表。
    /// <para>其顺序默认与整个图层的字段列表一致。</para>
    /// </summary>
    public class AttributeList
    {
        #region 字段
        private List<object> attributesList;
        #endregion

        #region 构造
        /// <summary>
        /// 空构造
        /// </summary>
        public AttributeList()
        {
            this.attributesList = new List<object>();
        }

        /// <summary>
        /// 使用数组的构造
        /// </summary>
        /// <param name="values"></param>
        public AttributeList(object[] values)
        {
            this.attributesList = new List<object>();
            this.attributesList.AddRange(values);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取属性列表的长度
        /// </summary>
        public int Count
        {
            get
            {
                return this.attributesList.Count;
            }
        }

        /// <summary>
        /// 获取或设置指定位置的属性值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[int index]
        {
            set
            {
                this.attributesList[index] = value;
            }
            get
            {
                return this.attributesList[index];
            }
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获取指定位置的属性值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetItem(int index)
        {
            return this.attributesList[index];
        }

        /// <summary>
        /// 设置指定位置的属性值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetItem(int index, object value)
        {
            this.attributesList[index] = value;
        }

        /// <summary>
        /// 添加一个属性值
        /// </summary>
        /// <param name="value"></param>
        public void AddItem(object value)
        {
            this.attributesList.Add(value);
        }

        /// <summary>
        /// 给定位置，删除一个属性值
        /// </summary>
        /// <param name="index"></param>
        public void DeleteItem(int index)
        {
            this.attributesList.RemoveAt(index);
        }

        /// <summary>
        /// 把自身转换为数组
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            return this.attributesList.ToArray();
        }

        /// <summary>
        /// 导入一个数组内的数据。会覆盖原数据。
        /// </summary>
        /// <param name="values"></param>
        public void FromArray(object[] values)
        {
            this.attributesList.Clear();
            this.attributesList.AddRange(values);
        }

        /// <summary>
        /// 获取自身的深拷贝
        /// </summary>
        /// <returns></returns>
        public AttributeList Clone()
        {
            return new AttributeList(this.ToArray());
        }
        #endregion
    }
}
