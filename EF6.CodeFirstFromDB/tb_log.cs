namespace EF6.CodeFirstFromDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �������ݿ�����
    /// 1 ����ӳ�䣺������������ʵ�������ƺ����ݿ����Ʋ�һ�¿���ͨ�����ַ�ʽʵ��
    /// 2 OnModelCreating������ɳ��󻯵�ӳ��
    /// 3 ӳ�����ļ�
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
