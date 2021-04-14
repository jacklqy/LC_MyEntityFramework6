using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EF.Model
{
    public partial class LCDbContext : DbContext
    {
        public LCDbContext()
            : base("name=LCDbContext")
        {
            //´òÓ¡Ö´ÐÐsql
            this.Database.Log += sqlStr => Console.WriteLine($"sql£º{sqlStr}");
        }

        public virtual DbSet<tb_debuglog> tb_debuglog { get; set; }
        public virtual DbSet<tb_error> tb_error { get; set; }
        public virtual DbSet<tb_log> tb_log { get; set; }
        public virtual DbSet<tb_mq> tb_mq { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_debuglog>()
                .Property(e => e.mqpath)
                .IsUnicode(false);

            modelBuilder.Entity<tb_debuglog>()
                .Property(e => e.methodname)
                .IsUnicode(false);

            modelBuilder.Entity<tb_debuglog>()
                .Property(e => e.info)
                .IsUnicode(false);

            modelBuilder.Entity<tb_error>()
                .Property(e => e.mqpath)
                .IsUnicode(false);

            modelBuilder.Entity<tb_error>()
                .Property(e => e.methodname)
                .IsUnicode(false);

            modelBuilder.Entity<tb_error>()
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

            modelBuilder.Entity<tb_mq>()
                .Property(e => e.mqname)
                .IsUnicode(false);
        }
    }
}
