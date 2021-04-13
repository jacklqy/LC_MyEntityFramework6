using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EF6.CodeFirst
{
    public partial class CodeFirstContext : DbContext
    {
        public CodeFirstContext()
            : base("name=CodeFirstContext")
        {
        }

        public virtual DbSet<tb_debuglog> tb_debuglog { get; set; }
        public virtual DbSet<tberror> tb_error { get; set; }
        public virtual DbSet<tb_log> tb_log { get; set; }

        /// <summary>
        /// 启动的时候可以完成数据库和代码结构的同步
        /// 还可以完成数据初始化
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////默认，不存在就创建
            //new CreateDatabaseIfNotExists<CodeFirstContext>();
            ////每次都删除重建（谨慎使用，数据会全部丢失的）
            //new DropCreateDatabaseAlways<CodeFirstContext>();
            ////数据初始化
            //Database.SetInitializer<CodeFirstContext>(new DropCreateDatabaseIfModelChanges<CodeFirstContext>());
            
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
                .Property(e => e.mqpath)
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
