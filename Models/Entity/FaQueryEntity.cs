
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Models.Entity
{
    /// <summary>
    /// 查询
    /// </summary>
    [Table("fa_query")]
    public class FaQueryEntity
    {
        public string _DictStr;

        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Key]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "名称")]
        [Column]
        public string NAME { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "代码")]
        [Column]
        public string CODE { get; set; }
        /// <summary>
        /// 自动加载
        /// </summary>
        [Required]
        [Display(Name = "自动加载")]
        [Column]
        public bool AUTO_LOAD { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        [Required]
        [Display(Name = "页面大小")]
        [Column]
        public int PAGE_SIZE { get; set; }
        /// <summary>
        /// 复选框
        /// </summary>
        [Required]
        [Display(Name = "复选框")]
        [Column]
        public bool SHOW_CHECKBOX { get; set; }

        /// <summary>
        /// 调试
        /// </summary>
        [Required]
        [Display(Name = "调试")]
        [Column]
        public bool IS_DEBUG { get; set; }

        /// <summary>
        /// 查询语句
        /// </summary>
        [Display(Name = "查询语句")]
        [Column]
       
        public string QUERY_CONF { get; set; }
        /// <summary>
        /// 配置信息
        /// </summary>
        [Display(Name = "配置信息")]
        [Column]
        public string QUERY_CFG_JSON { get; set; }
        /// <summary>
        /// 传入参数
        /// </summary>
        [Display(Name = "传入参数")]
        [Column]
        public string IN_PARA_JSON { get; set; }
        /// <summary>
        /// JS脚本
        /// </summary>
        [Display(Name = "JS脚本")]
        [Column]
        public string JS_STR { get; set; }
        /// <summary>
        /// 行按钮
        /// </summary>
        [Display(Name = "行按钮")]

        [Column]
        public string ROWS_BTN { get; set; }
        /// <summary>
        /// 表头按钮
        /// </summary>
        [Display(Name = "表头按钮")]
        [Column]
        public string HEARD_BTN { get; set; }
        /// <summary>
        /// 报表脚本
        /// </summary>
        [Display(Name = "报表脚本")]
        [Column]
        public string REPORT_SCRIPT { get; set; }

        /// <summary>
        /// 图表配置
        /// </summary>
        [Display(Name = "图表配置")]
        [Column]
        public string CHARTS_CFG { get; set; }


        /// <summary>
        /// 图表类型
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "图表类型")]
        [Column]
        public string CHARTS_TYPE { get; set; }


        /// <summary>
        /// 统计范围
        /// </summary>
        [Required]
        [Display(Name = "统计范围")]
        [Column]
        public short FILTR_LEVEL { get; set; }

        /// <summary>
        /// 过滤配置
        /// </summary>
        [Display(Name = "过滤配置")]
        [Column]
        public string FILTR_STR { get; set; }

        /// <summary>
        /// 查询库
        /// </summary>
        [Display(Name = "查询库")]
        [Column]
        public int? DB_SERVER_ID { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        [Column]
        public string REMARK { get; set; }

        /// <summary>
        /// 最新时间
        /// </summary>
        [StringLength(50)]
        [Display(Name = "最新时间")]
        [Column]
        public string NEW_DATA { get; set; }

        public string _DictQueryCfgStr { get; set; }
    }

    public class QuerySearchModel
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 排序类型 asc|desc
        /// </summary>
        public string order { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 每页显示数
        /// </summary>
        public int rows { get; set; }
        private string _OrderStr;
        public string OrderStr
        {
            get
            {
                if (string.IsNullOrEmpty(_OrderStr))
                {
                    _OrderStr = string.Format("{0} {1}", sort, order);
                }
                return _OrderStr;
            }
            set {
                _OrderStr = value;
            }
        }
        public List<QueryPara> ParaList { get; set; }
        public IList<QueryRowBtnShowCondition> WhereList { get; set; }
        public string WhereListStr { get; set; }
        public string ParaListStr { get; set; }

    }

    /// <summary>
    /// 参数
    /// </summary>
    public class QueryPara
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }
    }
    /// <summary>
    /// 配置
    /// </summary>
    public class QueryCfg
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        [Display(Name = "字段名称")]
        public string FieldName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        [Display(Name = "别名")]
        public string Alias { get; set; }
        /// <summary>
        /// 是否可搜索
        /// </summary>
        [Display(Name = "是否可搜索")]
        public bool CanSearch { get; set; }

        /// <summary>
        /// 过滤控件类型
        /// </summary>
        [Display(Name = "过滤控件类型")]
        public string SearchType { get; set; }

        /// <summary>
        /// 过滤控件脚本
        /// </summary>
        [Display(Name = "过滤控件脚本")]
        public string SearchScript { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [Display(Name = "是否显示")]
        public bool Show { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        [Display(Name = "宽度")]
        public string Width { get; set; }
        /// <summary>
        /// 可排序
        /// </summary>
        [Display(Name = "可排序")]
        public bool Sortable { get; set; }

        /// <summary>
        /// 变量
        /// </summary>
        [Display(Name = "变量")]
        public string IsVariable { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        [Display(Name = "字段类型")]
        public string FieldType { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        [Display(Name = "格式化")]
        public string Format { get; set; }
    }


    public class QueryRowBtn
    {
        /// <summary>
        /// 按钮名
        /// </summary>
        [Display(Name = "按钮名")]
        public string Name { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        [Display(Name = "样式")]
        public string IconCls { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        public string Url { get; set; }

        /// <summary>
        /// 对话框模式
        /// </summary>
        [Display(Name = "对话框模式")]
        public string DialogMode { get; set; }

        /// <summary>
        /// 对话框宽
        /// </summary>
        [Display(Name = "对话框宽")]
        public string DialogWidth { get; set; }

        /// <summary>
        /// 对话框宽
        /// </summary>
        [Display(Name = "对话框高")]
        public string DialogHeigth { get; set; }

        /// <summary>
        /// 显示条件
        /// </summary>
        [Display(Name = "显示条件")]
        public IList<QueryRowBtnShowCondition> ShowCondition { get; set; }

        /// <summary>
        /// 传值参数
        /// </summary>
        [Display(Name = "参数")]
        public IList<QueryRowBtnParameter> Parameter { get; set; }
    }
    /// <summary>
    /// 行按键显示条件
    /// </summary>
    public class QueryRowBtnShowCondition
    {
        /// <summary>
        /// 对象字段
        /// </summary>
        [Display(Name = "对象字段")]
        public string ObjFiled { get; set; }
        /// <summary>
        /// 操作符 如：between、in、《、》、=、like
        /// </summary>
        [Display(Name = "操作符")]
        public string OpType { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [Display(Name = "值")]
        public string Value { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        [Display(Name = "字段类型")]
        public string FieldType { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        [Display(Name = "字段名称")]
        public string FieldName { get; set; }
    }

    /// <summary>
    /// 行按键参数
    /// </summary>
    public class QueryRowBtnParameter
    {
        /// <summary>
        /// 参数
        /// </summary>
        [Display(Name = "参数")]
        public string Para { get; set; }
        /// <summary>
        /// 对象值
        /// </summary>
        [Display(Name = "对象值")]
        public string ObjValue { get; set; }
    }

    /// <summary>
    /// 字段类型
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        字符器 = 0,
        /// <summary>
        /// 数字
        /// </summary>
        数字 = 1,
        /// <summary>
        /// 日期
        /// </summary>
        日期 = 2
    }

    public class DataGridDataJson
    {
        public int total { get; set; }
        public DataTable rows { get; set; }
        public string errMsg { get; set; }
    }
}
