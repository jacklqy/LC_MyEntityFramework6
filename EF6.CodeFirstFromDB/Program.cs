﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6.CodeFirstFromDB
{
    /// <summary>
    /// 推荐使用CodeFirstFromDB
    /// 这中方式比较推荐：
    /// CodeFirst From DB创建流程：项目右键-》添加-》新建项-》数据-》ADO.NET实体数据模型-》来自数据库的CodeFirst-》...
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //实现了IDisposable接口的，记得都要using释放
                using (CodeFirstFromDBContext context = new CodeFirstFromDBContext())
                {
                    //DBContext里面才可以
                    context.Database.Log += sqlStr => Console.WriteLine($"当前执行sql：{sqlStr}");

                    //查询
                    tb_log log1 = context.tb_log.Find(1);
                    tb_log log2 = context.tb_log.FirstOrDefault(t => t.id == 2);
                    List<tb_log> list = context.tb_log.Where(t => t.id > 3).ToList();

                    //新增
                    tb_log logNew = new tb_log()
                    {
                        mqpathid = 12345,
                        info = "aaa",
                        methodname = "bbb",
                        mqpath = "ccc",
                        createtime = DateTime.Now,
                        updatetime = DateTime.Now
                    };
                    context.tb_log.Add(logNew);
                    context.SaveChanges();//表示保存

                    //修改
                    logNew.info = "哈哈";
                    context.SaveChanges();//表示保存

                    //删除
                    context.tb_log.Remove(logNew);
                    context.SaveChanges();//表示保存

                    tberror errorNew = new tberror()
                    {
                        mqpathid = 12345,
                        info = "aaa",
                        methodname = "bbb",
                        createtime = DateTime.Now,
                         path="12312312"
                    };
                    context.tb_error.Add(errorNew);
                    context.SaveChanges();//表示保存
                    tberror m = context.tb_error.Find(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.Read();
        }
    }
}
