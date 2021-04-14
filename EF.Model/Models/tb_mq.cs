namespace EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_mq
    {
        [Key]
        public int id { get; set; }

        public string mqname { get; set; }

        public DateTime createtime { get; set; }

        //µº∫Ω Ù–‘
        public virtual ICollection<tb_log> Tb_Logs { get; set; }
        //public virtual ICollection<tb_error> Tb_Errors { get; set; }
        //public virtual ICollection<tb_debuglog> Tb_Debuglogs { get; set; }
    }
}
