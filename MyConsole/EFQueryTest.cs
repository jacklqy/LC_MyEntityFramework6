using EF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class EFQueryTest
    {
        public static void Show()
        {
            //基础查询
            using (LCDbContext dbContext = new LCDbContext())
            {
                ////打印执行sql
                //dbContext.Database.Log += sqlStr => Console.WriteLine($"sql：{sqlStr}");

                {
                    //in查询1
                    var list = dbContext.tb_log.Where(t => new long[] { 1, 2, 3, 4 }.Contains(t.id));
                    list = list.Where(t => t.id < 3);
                    list = list.OrderBy(t => t.id);
                    foreach (var item in list)
                    {
                        Console.WriteLine(item.info);
                    }
                }
                {
                    //in查询2
                    var list = from t in dbContext.tb_log
                               where new long[] { 1, 2, 3, 4 }.Contains(t.id)
                               orderby t.id
                               select t;
                    foreach (var item in list)
                    {
                        Console.WriteLine(item.info);
                    }
                }
                {
                    //分页1
                    var list = dbContext.tb_log.Where(t => new long[] { 1, 2, 3, 4 }
                    .Contains(t.id)).OrderBy(t => t.id).Select(t => new
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
                {
                    //分页2
                    var list = (from t in dbContext.tb_log
                               where new long[] { 1, 2, 3, 4 }.Contains(t.id)
                               orderby t.id
                               select new {
                                   id = t.id,
                                   info = t.info,
                                   mqpath = t.mqpath
                               }).Skip(3).Take(5);
                    foreach (var item in list)
                    {
                        Console.WriteLine(item.info);
                    }
                }
                {
                    //多where
                    var list = dbContext.tb_log.Where(t => t.info.StartsWith("小") && t.info.EndsWith("新"))
                        .Where(t => t.info.EndsWith("11"))
                        .Where(t => t.info.Contains("22"))
                        .Where(t => t.info.Length < 5)
                        .OrderBy(t => t.id);
                    foreach (var item in list)
                    {
                        Console.WriteLine(item.info);
                    }
                }
                {
                    //inner join
                    var list = (from t in dbContext.tb_log
                                join c in dbContext.tb_error on t.id equals c.id
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
                {
                    //left join (没有right join和full join，如果要右连接，执行将查询实体顺序换一下就可以)
                    var list = (from t in dbContext.tb_log
                                join c in dbContext.tb_error on t.id equals c.id
                                into tList //*
                                from tl in tList.DefaultIfEmpty() //*
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

            //事务
            using (LCDbContext dbContext = new LCDbContext())
            {
                {
                    //事务--sql修改
                    DbContextTransaction trans = null;
                    try
                    {
                        trans = dbContext.Database.BeginTransaction();
                        string sql = "Update [tb_log] Set info='test' where id=@id";
                        SqlParameter parameter = new SqlParameter("@id", 1);
                        dbContext.Database.ExecuteSqlCommand(sql, parameter);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (trans != null)
                            trans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        if (trans != null)
                            trans.Dispose();
                    }
                }
                {
                    //事务--sql查询
                    //DbContextTransaction trans = null;
                    try
                    {
                        //trans = dbContext.Database.BeginTransaction();
                        string sql = "Select * from [tb_log] where id=@id";
                        SqlParameter parameter = new SqlParameter("@id", 1);
                        List<tb_log> list = dbContext.Database.SqlQuery<tb_log>(sql, parameter).ToList<tb_log>();
                        //trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //if (trans != null)
                        //    trans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        //if (trans != null)
                        //    trans.Dispose();
                    }
                }
            }

            //小知识点
            using (LCDbContext dbContext = new LCDbContext())
            {
                //var lgList = dbContext.tb_log.Where(t => t.id > 5).ToList();
                var lgList = dbContext.tb_log.Where(t => t.id > 5).AsNoTracking().ToList();//AsNoTracking如果数据不做更新，加一个可以提升性能。
                Console.WriteLine(dbContext.Entry<tb_log>(lgList[0]).State);
                Console.WriteLine("*************************************");
                var lg1 = dbContext.tb_log.Find(11);//优先内存查，没有在走数据库
                Console.WriteLine("*************************************");
                var lg2 = dbContext.tb_log.FirstOrDefault(t=>t.id==11);//但是linq时不会使用缓存，比如FirstOrDefault每次都会走数据库查
                Console.WriteLine("*************************************");
                var lg3 = dbContext.tb_log.Find(11);
                Console.WriteLine("*************************************");
                var lg4 = dbContext.tb_log.FirstOrDefault(t => t.id == 11);

                //Find优先内存查(限于当前context)，没有在走数据库
                //但是linq时不会使用缓存，比如FirstOrDefault每次都会走数据库查
                //AsNoTracking如果数据不做更新，加一个可以提升性能。
            }

            //按需更新
            using (LCDbContext dbContext = new LCDbContext())
            {
                //var lg = dbContext.tb_log.Find(5);
                //lg.info = "这是按需更新";
                //dbContext.SaveChanges();


                //tb_log lg = new tb_log()
                //{
                //    id = 1,
                //    info = ""
                //};//不能实体传递
                //dbContext.tb_log.Attach(lg);
                //dbContext.SaveChanges();//这种方式实现按需更新不行的


                //传递json--包含主键：值，更改属性：值---根据json来解读赋值，数据库查询后在匹配，在赋值更新

                var lg = dbContext.tb_log.Find(5);
                dbContext.Entry<tb_log>(lg).Property("info").IsModified = true;//指定某字段被修改过
                dbContext.SaveChanges();
            }
        }
    }
}
