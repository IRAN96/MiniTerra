using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 字段集合类
    /// </summary>
    public class moFields
    {
        #region 字段
        private List<moField> _Fields;
        private string _PrimaryField;//主字段名称
        private bool _ShowAlis = false;//是否显示别名，方便外部程序
        #endregion
        #region 构造函数
        public moFields()
        {
            _Fields = new List<moField>();
        }

        #endregion
        #region 属性
        public Int32 Count
        {
            get { return _Fields.Count; }
        }
        public string PrimaryField
        {
            get { return _PrimaryField; }
            set { _PrimaryField = value; }
        }
        public bool ShowAlias
        {
            get { return _ShowAlis; }
            set { _ShowAlis = value; }
        }

        #endregion
        #region 方法
        /// <summary>
        /// 获取指定索引号的字段
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public moField GetItem(Int32 index)
        {
            return _Fields[index];
        }
        /// <summary>
        /// 获取指定名称的字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public moField GetItem(string name)
        {
            Int32 sIndex = FindField(name);
            if (sIndex >= 0)
            {
                return _Fields[sIndex];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据指定名称查找字段，返回索引号，如无则返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Int32 FindField(string name)
        {
            Int32 sFieldCount = _Fields.Count;
            for (Int32 i = 0; i <= sFieldCount - 1; i++)
            {
                if (_Fields[i].Name.ToLower() == name.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }
        public void Append(moField field)
        {
            if (FindField(field.Name) >= 0)
            {
                throw new Exception("Fields对象中不能存在重名的字段！");
            }
            _Fields.Add(field);
            //触发事件，广播
            if (FieldAppended != null)
            {
                FieldAppended(this, field);
            }
        }
        public void RemoveAt(Int32 index)
        {
            moField sField = _Fields[index];
            _Fields.RemoveAt(index);
            if (FieldRemoved != null)
            {
                FieldRemoved(this, index, sField);
            }
        }
        #endregion
        #region 事件

        internal delegate void FiledAppendedHandle(object sender, moField fieldAppended);
        /// <summary>
        /// 有字段被加入
        /// </summary>
        internal event FiledAppendedHandle FieldAppended;

        internal delegate void FiledRemovedHandle(object sender, Int32 fieldIndex, moField fieldRemoved);
        /// <summary>
        /// 字段被删除
        /// </summary>
        internal event FiledRemovedHandle FieldRemoved;


        #endregion
    }
}
