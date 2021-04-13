using EF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    /// <summary>
    /// SaveChanges是以context为标准的，如果监听到任务数据的变化，会一次性的保存到数据库去，而且会开启事务！
    /// 关注下EntityState
    /// </summary>
    public class EFStateTest
    {
        public static void Show()
        {
            using (LCDbContext dbContext = new LCDbContext())
            {
                    
                {
                    //EntityState状态跟踪
                    tb_log log = new tb_log()
                    {
                        info = "111",
                        methodname = "333",
                        mqpath = "444",
                        mqpathid = 123,
                        updatetime = DateTime.Now,
                        createtime = DateTime.Now
                    };
                    //打印状态
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Detached该实体未由上下文跟踪
                    //new 一个log，跟dbContext完全没有关系
                    dbContext.SaveChanges();
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Detached该实体未由上下文跟踪

                    dbContext.tb_log.Add(log);
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Added
                    dbContext.SaveChanges();//插入数据后自增主键在插入成功后，会自动赋值给实体
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Unchanded(跟踪，但是没有变化)

                    log.info = "yyyy";//修改--内存clone了一份，进行对比
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Modified
                    dbContext.SaveChanges();
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Unchanded(跟踪，但是没有变化)

                    dbContext.tb_log.Remove(log);
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Deleted
                    dbContext.SaveChanges();
                    Console.WriteLine(dbContext.Entry<tb_log>(log).State);//Detached该实体未由上下文跟踪,已经从内存移除了

                }
            }

            {
                //是不是必须的先查询在更新(EF本身是依赖监听变化，然后更新的)
                tb_log lg = null;
                using (LCDbContext dbContext = new LCDbContext())
                {
                    tb_log l = dbContext.tb_log.Find(1);
                    lg = l;
                }
                using (LCDbContext dbContext = new LCDbContext())
                {
                    lg.info = "测试是否会被更新";
                    dbContext.SaveChanges();//不会，未被dbContext监听
                }
                using (LCDbContext dbContext = new LCDbContext())
                {
                    //dbContext.Entry<tb_log>(lg).State = EntityState.Modified;//全部字段更新，不是更新只变化的字段

                    Console.WriteLine(dbContext.Entry<tb_log>(lg).State);//Detached该实体未由上下文跟踪
                    dbContext.tb_log.Attach(lg);//将实体加入监听中后，在修改才有效，如果在加入监听前的修改时无效的
                    dbContext.SaveChanges();//不会更新
                    Console.WriteLine(dbContext.Entry<tb_log>(lg).State);//Unchanded(跟踪，但是没有变化)
                    lg.info = "测试是否会被更新2";//如果值未改变也不会是Modified状态
                    Console.WriteLine(dbContext.Entry<tb_log>(lg).State);//Modified
                    dbContext.SaveChanges();//会更新
                    Console.WriteLine(dbContext.Entry<tb_log>(lg).State);//Unchanded(跟踪，但是没有变化)
                }
            }
        }
    }
}
