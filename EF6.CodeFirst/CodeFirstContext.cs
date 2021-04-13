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
        /// ������ʱ�����������ݿ�ʹ���ṹ��ͬ��
        /// ������������ݳ�ʼ��
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////Ĭ�ϣ������ھʹ���
            //new CreateDatabaseIfNotExists<CodeFirstContext>();
            ////ÿ�ζ�ɾ���ؽ�������ʹ�ã����ݻ�ȫ����ʧ�ģ�
            //new DropCreateDatabaseAlways<CodeFirstContext>();
            ////���ݳ�ʼ��
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
