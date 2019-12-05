using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Query.Dto
{
    public class FieldCfgDto
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool hide { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 标题说明
        /// </summary>
        public string tooltip { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public string width { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool editable { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 编辑配置
        /// </summary>
        public FieldCfgDditor editor { get; set; }

        /// <summary>
        /// 是否可以筛选
        /// </summary>
        public bool filter { get; set; }

    }

    public class FieldCfgDditor
    {
        /// <summary>
        /// 编辑框类型
        /// 'text' | 'textarea' | 'completer' | 'list' | 'checkbox'|'datetime'|'number'
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 是否可编辑,否则只显示
        /// </summary>
        public bool editable { get; set; }

        /// <summary>
        /// 编辑框架的配置
        /// </summary>
        public Dictionary<string, object> config { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<KeyValuePair<string,string>> list { get; set; }
    }
}
