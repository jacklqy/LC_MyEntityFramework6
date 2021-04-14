namespace EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_debuglog
    {
        [Key]
        public long id { get; set; }

        //[ForeignKey("tb_mq")]//标记对应表的外键
        public int mqpathid;

        public string mqpath { get; set; }

        public string methodname { get; set; }

        public string info { get; set; }

        public DateTime createtime { get; set; }

        //导航属性
        //public virtual tb_mq tb_mq { get; set; }
    }
}
