using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 属性字段类
    /// </summary>
    public class moField
    {
        #region 字段
        private string _Name = "";
        private string _AliasName = "";//别名
        private moValueTypeConstant _ValueType = moValueTypeConstant.dInt32;
        private Int32 _Length;//长度

        #endregion
        #region 构造函数
        public moField(string name)//必须输入字段名称
        {
            _Name = name;
            _AliasName = name;
        }
        public moField(string name,moValueTypeConstant valueType)
        {
            _Name = name;
            _AliasName = name;
            _ValueType = valueType;
        }
        #endregion
        #region 属性
        public string Name
        {
            get { return _Name; }
        }
        public string AliasName
        {
            get { return _AliasName; }
            set { _AliasName = value; }
        }
        public moValueTypeConstant ValueType
        {
            get { return _ValueType; }
        }

        public Int32 Length
        {
            get { return _Length; }
        }
        #endregion
        #region 方法
        public moField Clone()
        {
            moField sField = new moField(_Name, _ValueType);
            sField._AliasName = _AliasName;
            sField._Length = _Length;
            return sField;
        }
        #endregion
    }
}
