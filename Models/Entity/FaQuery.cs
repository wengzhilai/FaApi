using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaQuery
    {
        public FaQuery()
        {
            FaRoleQueryAuthority = new HashSet<FaRoleQueryAuthority>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal AutoLoad { get; set; }
        public int PageSize { get; set; }
        public decimal ShowCheckbox { get; set; }
        public decimal IsDebug { get; set; }
        public decimal? FiltrLevel { get; set; }
        public int? DbServerId { get; set; }
        public string QueryConf { get; set; }
        public string QueryCfgJson { get; set; }
        public string InParaJson { get; set; }
        public string JsStr { get; set; }
        public string RowsBtn { get; set; }
        public string HeardBtn { get; set; }
        public string ReportScript { get; set; }
        public string ChartsCfg { get; set; }
        public string ChartsType { get; set; }
        public string FiltrStr { get; set; }
        public string Remark { get; set; }
        public string NewData { get; set; }

        public virtual ICollection<FaRoleQueryAuthority> FaRoleQueryAuthority { get; set; }
    }
}
