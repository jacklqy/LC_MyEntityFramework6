namespace EF6.CodeFirstFromDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 设置数据库名称
    /// 1 特性映射：如果代码里面的实体类名称和数据库名称不一致可以通过这种方式实现
    /// 2 OnModelCreating里面完成初审化的映射
    /// 3 映射类文件
    /// </summary>
    [Table("tb_log")]
    public partial class tb_log
    {
        public long id { get; set; }

        public int mqpathid { get; set; }

        [Required]
        [StringLength(300)]
        public string mqpath { get; set; }

        [Required]
        [StringLength(500)]
        public string methodname { get; set; }

        [Required]
        public string info { get; set; }

        public DateTime createtime { get; set; }

        public DateTime? updatetime { get; set; }
    }
}
