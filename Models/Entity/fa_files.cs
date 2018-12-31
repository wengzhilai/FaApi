using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_files
    {
        public fa_files()
        {
            fa_bulletin_file = new HashSet<fa_bulletin_file>();
            fa_event_files = new HashSet<fa_event_files>();
            fa_task_flow_handle_files = new HashSet<fa_task_flow_handle_files>();
            fa_user_file = new HashSet<fa_user_file>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string PATH { get; set; }
        public int? USER_ID { get; set; }
        public int LENGTH { get; set; }
        public DateTime? UPLOAD_TIME { get; set; }
        public string REMARK { get; set; }
        public string URL { get; set; }
        public string FILE_TYPE { get; set; }

        public virtual ICollection<fa_bulletin_file> fa_bulletin_file { get; set; }
        public virtual ICollection<fa_event_files> fa_event_files { get; set; }
        public virtual ICollection<fa_task_flow_handle_files> fa_task_flow_handle_files { get; set; }
        public virtual ICollection<fa_user_file> fa_user_file { get; set; }
    }
}
