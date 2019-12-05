
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helper.Query
{
    /// <summary>
    /// 查询
    /// </summary>
    [Table("query")]
    public class QueryEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        [Column("ID")]
        public int id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Column("NAME")]
        public String name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [Display(Name = "代码")]
        [Column("CODE")]
        public String code { get; set; }

        /// <summary>
        /// 自动加载
        /// </summary>
        [Display(Name = "自动加载")]
        [Column("AUTO_LOAD")]
        public Decimal autoLoad { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        [Display(Name = "页面大小")]
        [Column("PAGE_SIZE")]
        public int pageSize { get; set; }

        /// <summary>
        /// 复选框
        /// </summary>
        [Display(Name = "复选框")]
        [Column("SHOW_CHECKBOX")]
        public int showCheckbox { get; set; }

        /// <summary>
        /// 调试
        /// </summary>
        [Display(Name = "调试")]
        [Column("IS_DEBUG")]
        public int isDebug { get; set; }

        /// <summary>
        /// 导出
        /// </summary>
        [Display(Name = "导出")]
        [Column("ALLOW_EXPORT")]
        public int allowExport { get; set; }


        /// <summary>
        /// 选择层级
        /// </summary>
        [Required]
        [Display(Name = "选择层级")]
        [Column("FILTR_LEVEL")]
        public Decimal filtrLevel { get; set; }

        /// <summary>
        /// 查询库
        /// </summary>
        [Required]
        [Display(Name = "查询库")]
        [Column("DB_SERVER_ID")]
        public int dbServerId { get; set; }

        /// <summary>
        /// 查询语句
        /// </summary>
        [Required]
        [Display(Name = "查询语句")]
        [Column("QUERY_CONF")]
        public String queryConf { get; set; }

        /// <summary>
        /// 配置信息
        /// </summary>
        [Required]
        [Display(Name = "配置信息")]
        [Column("QUERY_CFG_JSON")]
        public String queryCfgJson { get; set; }

        /// <summary>
        /// 传入参数
        /// </summary>
        [Required]
        [Display(Name = "传入参数")]
        [Column("IN_PARA_JSON")]
        public String inParaJson { get; set; }

        /// <summary>
        /// JS脚本
        /// </summary>
        [Required]
        [Display(Name = "JS脚本")]
        [Column("JS_STR")]
        public String jsStr { get; set; }

        /// <summary>
        /// 行按钮
        /// </summary>
        [Required]
        [Display(Name = "行按钮")]
        [Column("ROWS_BTN")]
        public String rowsBtn { get; set; }

        /// <summary>
        /// 表头按钮
        /// </summary>
        [Required]
        [Display(Name = "表头按钮")]
        [Column("HEARD_BTN")]
        public String heardBtn { get; set; }

        /// <summary>
        /// 报表脚本
        /// </summary>
        [Required]
        [Display(Name = "报表脚本")]
        [Column("REPORT_SCRIPT")]
        public String reportScript { get; set; }

        /// <summary>
        /// 图表配置
        /// </summary>
        [Required]
        [Display(Name = "图表配置")]
        [Column("CHARTS_CFG")]
        public String chartsCfg { get; set; }

        /// <summary>
        /// 图表类型
        /// </summary>
        [Required]
        [Display(Name = "图表类型")]
        [Column("CHARTS_TYPE")]
        public String chartsType { get; set; }

        /// <summary>
        /// 过虑条件
        /// </summary>
        [Required]
        [Display(Name = "过虑条件")]
        [Column("FILTR_STR")]
        public String filtrStr { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [Display(Name = "备注")]
        [Column("REMARK")]
        public String remark { get; set; }

        /// <summary>
        /// 最新时间
        /// </summary>
        [Required]
        [Display(Name = "最新时间")]
        [Column("NEW_DATA")]
        public String newData { get; set; }


    }
}
