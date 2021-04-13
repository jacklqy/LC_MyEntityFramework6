namespace EF6.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Spatial;

    //[Table("tb_error")]
    public partial class tberror
    {
        public long id { get; set; }

        public int mqpathid { get; set; }

        [Required]
        [StringLength(300)]
        //[Column("mqpath")]//设置数据库表的属性名称
        public string mqpath { get; set; }

        [Required]
        [StringLength(500)]
        public string methodname { get; set; }

        [Required]
        public string info { get; set; }

        public DateTime createtime { get; set; }
    }

}
