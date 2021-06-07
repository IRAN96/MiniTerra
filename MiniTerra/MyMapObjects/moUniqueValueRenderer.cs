using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    public class moUniqueValueRenderer : moRenderer
    {
        #region 字段
        private string _Field = "";//绑定字段
        private string _HeadTitle = "";//在图层显示控件中的标题
        private bool _ShowHead = true;//在图层显示控件中是否显示标题
        private List<string> _Values = new List<string>();//唯一值集合
        private List<moSymbol> _Symbols = new List<moSymbol>();//符号集合
        private moSymbol _DefaultSymbol;//默认符号
        private bool _ShowDefaultSymbol;//是否在图层显示控件中显示默认符号
        #endregion
        #region 构造函数
        public moUniqueValueRenderer()
        {

        }
        #endregion
        #region 属性
        public override moRendererTypeConstant RendererType
        {
            get
            {
                return moRendererTypeConstant.UniqueValue;
            }
        }
        /// <summary>
        /// 获取唯一值数量
        /// </summary>
        public Int32 ValueCount
        {
            get { return _Values.Count; }
        }
        /// <summary>
        /// 获取或设置默认符号
        /// </summary>
        public moSymbol DefaultSymbol
        {
            get { return _DefaultSymbol; }
            set { _DefaultSymbol = value; }
        }
        /// <summary>
        /// 获取或设置绑定字段
        /// </summary>
        public string Field
        {
            get { return _Field; }
            set { _Field = value; }
        }
        //其他属性自行扩充
        #endregion

        #region 方法
        /// <summary>
        /// 获取指定索引号的唯一值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetValue(Int32 index)
        {
            return _Values[index];
        }
        public void SetValue(Int32 index,string value)
        {
            _Values[index] = value;
        }
        public moSymbol GetSymbol(Int32 index)
        {
            return _Symbols[index];
        }
        public void SetSymbol(Int32 index, moSymbol symbol)
        {
            _Symbols[index] = symbol;
        }
        /// <summary>
        /// 增加一个唯一值及对应的符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="symbol"></param>
        public void AddUniqueValue(string value,moSymbol symbol)
        {
            _Values.Add(value);
            _Symbols.Add(symbol);
        }
        /// <summary>
        /// 增加一个唯一值数组及对应的符号数组
        /// </summary>
        /// <param name="values"></param>
        /// <param name="symbols"></param>
        public void AddUniqueValues(string[] values,moSymbol[] symbols)
        {
            if (values.Length != symbols.Length)
            {
                throw new Exception("The Length of the two arrays is not equal!");
            }
            _Values.AddRange(values);
            _Symbols.AddRange(symbols);
        }
        /// <summary>
        /// 根据唯一值找符号，如果唯一值不存在，则返回默认符号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public moSymbol FindSymbol(string value)
        {
            Int32 sValueCount = _Values.Count;
            for(Int32 i = 0; i <= sValueCount -1; i++)
            {
                if (_Values[i] == value)
                {
                    return _Symbols[i];
                }
            }
            return _DefaultSymbol;
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public override moRenderer Clone()
        {
            moUniqueValueRenderer sRenderer = new moUniqueValueRenderer();
            sRenderer._Field = _Field;
            sRenderer._HeadTitle = _HeadTitle;
            sRenderer._ShowHead = _ShowHead;
            Int32 sValueCount = _Values.Count;
            for (Int32 i = 0; i <= sValueCount - 1; i++)
            {
                string sValue = _Values[i];
                moSymbol sSymbol = null;
                if (_Symbols[i] != null)
                    sSymbol = _Symbols[i].Clone();
                sRenderer.AddUniqueValue(sValue, sSymbol);
            }
            if (_DefaultSymbol != null)
                sRenderer.DefaultSymbol = _DefaultSymbol.Clone();
            sRenderer._ShowDefaultSymbol = _ShowDefaultSymbol;
            return sRenderer;
        }
        #endregion
    }
}
