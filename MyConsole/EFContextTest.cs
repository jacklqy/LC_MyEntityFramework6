using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    /// <summary>
    /// 能不能整个进程就一个context实例呀？当然不可以
    /// 多线程能不能是一个context实例呢？一般来说不行(除非自己很明确状况)
    /// 那每个数据操作都去来一个context实例？也不好，内存消耗大，没法缓存
    /// 
    /// context使用建议：
    /// DBcontext是一个上下文环境，里面内置对象跟踪，会开启一个连接(就等同于一个数据库连接)
    /// 一次请求，最好是一个context实例；
    /// 多个请求/多线程最好是多个context实例；
    /// 用完尽快释放！！！
    /// </summary>
    public class EFContextTest
    {
        public static void Show()
        {
            #region 多个数据修改，一次SaveChanges，开启事务提交
            //using (LCDbContext dbContext = new LCDbContext())
            //{
            //    tb_log log = new tb_log()
            //    {
            //        info = "嗯嗯嗯",
            //        methodname = "发发发",
            //        mqpath = "滚滚滚",
            //        updatetime = DateTime.Now,
            //        mqpathid = 123,
            //        createtime = DateTime.Now
            //    };
            //    dbContext.tb_log.Add(log);

            //    dbContext.tb_log.Remove(log);

            //    tb_log lg0 = dbContext.tb_log.FirstOrDefault(t => t.id == 1);
            //    lg0.info += "哈哈哈";

            //    tb_log lg1 = dbContext.tb_log.Find(2);
            //    lg1.info += "啪啪啪";

            //    dbContext.SaveChanges();
            //}
            #endregion

            #region 多个数据修改，一次SaveChanges，任何一个失败直接全部失败
            //using (LCDbContext dbContext = new LCDbContext())
            //{
            //    tb_log log = new tb_log()
            //    {
            //        info = "嗯嗯嗯111",
            //        methodname = "发发发111",
            //        mqpath = "滚滚滚111",
            //        updatetime = DateTime.Now,
            //        mqpathid = 123,
            //        createtime = DateTime.Now
            //    };
            //    dbContext.tb_log.Add(log);

            //    tb_log lg0 = dbContext.tb_log.FirstOrDefault(t => t.id == 1);
            //    lg0.info += "哈哈哈";

            //    tb_log lg1 = dbContext.tb_log.Find(2);
            //    lg1.info += "啪啪啪";

            //    tb_log lg3 = dbContext.tb_log.First(t => t.id == 1000);
            //    dbContext.tb_log.Remove(log);

            //    dbContext.SaveChanges();
            //}
            #endregion


            
            //1 多context实例join行吗？不行，因为上下文环境不一样；除非把数据查到内存，在去linq
            //2 多context的事务也麻烦
            using (LCDbContext dbContext1 = new LCDbContext())
            using (LCDbContext dbContext2 = new LCDbContext())
            {
                //inner join
                //这里会抛异常，因为context上下文环境不一样！
                var list = (from t in dbContext1.tb_log
                            join c in dbContext2.tb_error on t.id equals c.id
                            where new long[] { 1, 2, 3, 4 }.Contains(t.id)
                            orderby t.id
                            select new
                            {
                                id = t.id,
                                info = t.info,
                                mqpath = t.mqpath
                            }).Skip(3).Take(5);
                foreach (var item in list)
                {
                    Console.WriteLine(item.info);
                }
            }

            

        }
    }
}
