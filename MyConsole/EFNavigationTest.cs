using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MyConsole
{
    /// <summary>
    /// 导航属性：一般是外键生成的
    /// 主外键表，主表有个子表的集合，导航属性
    /// 子表里面有个主表的实例，引用属性
    /// 
    /// 二班来说，不是主外键也可以这么玩
    /// </summary>
    public class EFNavigationTest
    {
        /// <summary>
        /// 导航属性查询
        /// </summary>
        public static void ShowQuery()
        {
            //默认情况下，导航属性是延迟查询的
            //条件是virtaul属性+默认配置
            using (LCDbContext dbContext = new LCDbContext())
            {
                //默认为true开启了延迟查询的
                //dbContext.Configuration.LazyLoadingEnabled = true;
                var mqList = dbContext.tb_mq.Where(t => t.id >= 1);//
                foreach (var mq in mqList)//只查询tb_mq
                {
                    Console.WriteLine(mq.mqname);
                    foreach (var lg in mq.Tb_Logs)//再去查Tb_Logs
                    {
                        Console.WriteLine(lg.info);
                    }
                }
            }

            {
                //关闭延迟查询后，子表数据就没有了
                using (LCDbContext dbContext = new LCDbContext())
                {
                    //默认为true开启了延迟查询的
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    var mqList = dbContext.tb_mq.Where(t => t.id >= 1);
                    foreach (var mq in mqList)
                    {
                        Console.WriteLine(mq.mqname);
                        foreach (var lg in mq.Tb_Logs)//这里没有数据
                        {
                            Console.WriteLine(lg.info);
                        }
                    }
                }
                //关闭延迟查询后，如果需要子表数据，可以自己显示加载
                using (LCDbContext dbContext = new LCDbContext())
                {
                    //默认为true开启了延迟查询的
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    var mqList = dbContext.tb_mq.Where(t => t.id >= 1);//
                    foreach (var mq in mqList)
                    {
                        Console.WriteLine(mq.mqname);
                        dbContext.Entry<tb_mq>(mq).Collection(c => c.Tb_Logs).Load();//方式一：显示加载
                        //dbContext.Entry<tb_mq>(mq).Reference(c => c.Tb_Logs).Load();//方式二：显示加载
                        foreach (var lg in mq.Tb_Logs)//这里没有数据
                        {
                            Console.WriteLine(lg.info);
                        }
                    }
                }
            }

            //Include预先加载(贪婪加载)，在查询主表数据时，就一次性把子表数据查询出来
            using (LCDbContext dbContext = new LCDbContext())
            {
                var mqList = dbContext.tb_mq.Include("Tb_Logs").Where(t => t.id >= 1);//
                foreach (var mq in mqList)
                {
                    Console.WriteLine(mq.mqname);
                    foreach (var lg in mq.Tb_Logs)//这里没有数据
                    {
                        Console.WriteLine(lg.info);
                    }
                }
            }

            //我觉得，其实导航属性的东西，自己join也是可以搞定的，只不过框架给我们提供了


            //非主外键

        }

        /// <summary>
        /// 主外键关系表数据插入：A表---B表(包含A的ID)---ID是自增的
        /// </summary>
        public static void ShowInsert()
        {
            {
                //常规做法：能成功，但是事务问题就无法保证---不能保证事务(多个SaveChanges，A成功了，B没有成功，就无法回退A)
                using (LCDbContext dbContext = new LCDbContext())
                {
                    tb_mq mq = new tb_mq()//A表
                    {
                        mqname = "队列1",
                        createtime = DateTime.Now
                    };
                    dbContext.tb_mq.Add(mq);
                    dbContext.SaveChanges();//1

                    tb_log log1 = new tb_log()
                    {
                        info = "xxxx",
                        methodname = "vvvv",
                        mqpath = "bbbb",
                        mqpathid = mq.id,//----------A表ID
                        updatetime = DateTime.Now,
                        createtime = DateTime.Now
                    };
                    tb_log log2 = new tb_log()
                    {
                        info = "8888",
                        methodname = "9999",
                        mqpath = "123456",
                        mqpathid = mq.id,//----------A表ID
                        updatetime = DateTime.Now,
                        createtime = DateTime.Now,
                    };
                    dbContext.tb_log.Add(log1);
                    dbContext.tb_log.Add(log2);
                    dbContext.SaveChanges();//2
                }

                //TransactionScope 这个就可以保证事务，A成功了，B没有成功，就回退A
                //它是利用操作系统来实现的
                using (LCDbContext dbContext = new LCDbContext())
                {
                    using (TransactionScope trans = new TransactionScope())//分布式事务
                    {
                        tb_mq mq = new tb_mq()//A表
                        {
                            mqname = "队列1",
                            createtime = DateTime.Now
                        };
                        dbContext.tb_mq.Add(mq);
                        dbContext.SaveChanges();//1

                        tb_log log1 = new tb_log()
                        {
                            info = "xxxx",
                            methodname = "vvvv",
                            mqpath = "bbbb",
                            mqpathid = mq.id,//----------A表ID
                            updatetime = DateTime.Now,
                            createtime = DateTime.Now
                        };
                        tb_log log2 = new tb_log()
                        {
                            info = "8888",
                            methodname = "9999",
                            mqpath = "123456",
                            mqpathid = mq.id,//----------A表ID
                            updatetime = DateTime.Now,
                            createtime = DateTime.Now,
                        };
                        dbContext.tb_log.Add(log1);
                        dbContext.tb_log.Add(log2);
                        dbContext.SaveChanges();//2
                        trans.Complete();//能执行这个就表示成功
                    }
                }

                //TransactionScope多个context也可以保证事务，A成功了，B没有成功，就回退A
                using (LCDbContext dbContext1 = new LCDbContext())
                using (LCDbContext dbContext2 = new LCDbContext())
                {
                    using (TransactionScope trans = new TransactionScope())//分布式事务
                    {
                        tb_mq mq = new tb_mq()//A表
                        {
                            mqname = "队列1",
                            createtime = DateTime.Now
                        };
                        dbContext1.tb_mq.Add(mq);
                        dbContext1.SaveChanges();//1

                        tb_log log1 = new tb_log()
                        {
                            info = "xxxx",
                            methodname = "vvvv",
                            mqpath = "bbbb",
                            mqpathid = mq.id,//----------A表ID
                            updatetime = DateTime.Now,
                            createtime = DateTime.Now
                        };
                        tb_log log2 = new tb_log()
                        {
                            info = "8888",
                            methodname = "9999",
                            mqpath = "123456",
                            mqpathid = mq.id,//----------A表ID
                            updatetime = DateTime.Now,
                            createtime = DateTime.Now,
                        };
                        dbContext2.tb_log.Add(log1);
                        dbContext2.tb_log.Add(log2);
                        dbContext2.SaveChanges();//2
                        trans.Complete();//能执行这个就表示成功
                    }
                }
            }

            {
                //一次SaveChanges，如果表是主外键关系，那会在插入数据库时会自动给外键表字段添加主表的ID，反之则不会自动设置，而是已你设置的值未准。---能保证事务
                using (LCDbContext dbContext = new LCDbContext())
                {
                    tb_mq mq = new tb_mq()//A表
                    {
                        mqname = "队列1",
                        createtime = DateTime.Now
                    };
                    tb_log log1 = new tb_log()
                    {
                        info = "xxxx",
                        methodname = "vvvv",
                        mqpath = "bbbb",
                        mqpathid = 343,//--------------A表ID
                        updatetime = DateTime.Now,
                        createtime = DateTime.Now
                    };
                    tb_log log2 = new tb_log()
                    {
                        info = "8888",
                        methodname = "9999",
                        mqpath = "123456",
                        mqpathid = 123,//-------------A表ID
                        updatetime = DateTime.Now,
                        createtime = DateTime.Now,
                    };
                    dbContext.tb_mq.Add(mq);
                    dbContext.tb_log.Add(log1);
                    dbContext.tb_log.Add(log2);
                    dbContext.SaveChanges();
                }
            }

            {
                //一次SaveChanges，如果没有主外键关系，那会在插入数据库时，需要像下面这样方式设置 ---能保证事务
                using (LCDbContext dbContext = new LCDbContext())
                {
                    tb_mq mq = new tb_mq()//A表
                    {
                        mqname = "队列1",
                        createtime = DateTime.Now
                    };
                    tb_log log1 = new tb_log()
                    {
                        info = "xxxx",
                        methodname = "vvvv",
                        mqpath = "bbbb",
                        mqpathid = mq.id,//----------A表ID---------推荐做法
                        updatetime = DateTime.Now,
                        createtime = DateTime.Now
                    };
                    tb_log log2 = new tb_log()
                    {
                        info = "8888",
                        methodname = "9999",
                        mqpath = "123456",
                        mqpathid = mq.id,//----------A表ID---------推荐做法
                        updatetime = DateTime.Now,
                        createtime = DateTime.Now,
                    };

                    //这三个放置顺序无关
                    dbContext.tb_mq.Add(mq);
                    dbContext.tb_log.Add(log1);
                    dbContext.tb_log.Add(log2);
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 针对主外键---前提是数据库有主外键关系
        /// 级联删除：主表数据删除后，子表是删除/不变/改成默认值null/ ------(数据库表外键处可以配置)
        /// 级联更新：应该用不上
        /// </summary>
        public static void ShowDelete()
        {
            using (LCDbContext dbContext = new LCDbContext())
            {
                tb_mq mq = dbContext.Set<tb_mq>().Find(1);
                dbContext.tb_mq.Remove(mq);
                dbContext.SaveChanges();//数据库设置级联删除，就只需要删除主表，相应的子表也会删除。
            }
        }
    }
}
