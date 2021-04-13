namespace EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_debuglog
    {
        public long id { get; set; }

        public int mqpathid { get; set; }

        public string mqpath { get; set; }

        public string methodname { get; set; }

        public string info { get; set; }

        public DateTime createtime { get; set; }
    }
}
