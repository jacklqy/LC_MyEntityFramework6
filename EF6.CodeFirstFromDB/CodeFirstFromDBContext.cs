using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EF6.CodeFirstFromDB
{
    public partial class CodeFirstFromDBContext : DbContext
    {
        public CodeFirstFromDBContext()
            : base("name=CodeFirstFromDBContext")
        {
        }

        public virtual DbSet<tb_debuglog> tb_debuglog { get; set; }
        public virtual DbSet<tberror> tb_error { get; set; }
        public virtual DbSet<tb_log> tb_log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //2 �������ݿ�����ƺ��ֶ����������ƺ��ֶε�ӳ��
            modelBuilder.Entity<tberror>()
                .ToTable("tb_error")
                .Property(p=>p.path)
                .HasColumnName("mqpath");

            ////3 �������ݿ�����ƺ��ֶ����������ƺ��ֶε�ӳ��
            //modelBuilder.Configurations.Add(new TbErrorMapping());


            modelBuilder.Entity<tb_debuglog>()
                .Property(e => e.mqpath)
                .IsUnicode(false);

            modelBuilder.Entity<tb_debuglog>()
                .Property(e => e.methodname)
                .IsUnicode(false);

            modelBuilder.Entity<tb_debuglog>()
                .Property(e => e.info)
                .IsUnicode(false);

            modelBuilder.Entity<tberror>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<tberror>()
                .Property(e => e.methodname)
                .IsUnicode(false);

            modelBuilder.Entity<tberror>()
                .Property(e => e.info)
                .IsUnicode(false);

            modelBuilder.Entity<tb_log>()
                .Property(e => e.mqpath)
                .IsUnicode(false);

            modelBuilder.Entity<tb_log>()
                .Property(e => e.methodname)
                .IsUnicode(false);

            modelBuilder.Entity<tb_log>()
                .Property(e => e.info)
                .IsUnicode(false);
        }
    }
}
