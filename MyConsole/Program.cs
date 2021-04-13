using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    /// <summary>
    /// EF6->EntityFramework6：支持多种数据库；支持函数、存储过程等...
    /// 1、ORM对象关系映射
    /// 2、EntityFramework DBFirst
    /// 3、EntityFramework CodeFirst from db 和 CodeFirst
    /// 4、EntityFramework modelFirst
    /// 5、EF增删改查基本实现
    /// 
    /// context---映射数据库实例
    /// 实体类---映射表
    /// 
    /// 
    /// 各种方式创建步骤：
    /// DBFirst：数据库优先，传统的开发模式，有个很重的edmx。
    ///          DBFirst创建流程：项目右键-》添加-》新建项-》数据-》ADO.NET实体数据模型-》来自数据库的EF设计器-》...
    /// CodeFirstFromDB：***推荐使用这种方式***
    ///          CodeFirst From DB创建流程：项目右键-》添加-》新建项-》数据-》ADO.NET实体数据模型-》来自数据库的CodeFirst-》...
    /// CodeFirst：代码先行，不关心数据库，从业务出发，然后能自动生成数据库
    ///          无
    /// ModelFirst：自己设计器里面设计表，然后在更新同步到数据库中
    ///           ModelFirst创建流程：项目右键-》添加-》新建项-》数据-》ADO.NET实体数据模型-》空EF设计器模型-》...
    ///        
    /// 
    /// EF查看sql有2种方式：方式一：数据库管理工具里面的sql server profiler；方式二：dbContext.Database.Log += sqlStr => Console.WriteLine($"当前执行sql：{sqlStr}");
    /// 
    /// 
    /// 
    /// 1 各种复杂查询&直接执行sql
    /// 2 EF状态跟踪，本地增删改查实现
    /// 3 Context生命周期，多种事务
    /// 4 EF延迟查询、导航属性加载&增加&删除
    /// 
    /// 
    /// 封装类库--上端引用--引用EF--搬迁配置文件数据库连接字符串
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //using(LCDbContext context=new LCDbContext())
                //{
                //    tb_log log = context.tb_log.Find(1);
                //}

                {
                    ////基础查询
                    //EFQueryTest.Show();
                }
                {
                    ////状态跟踪
                    //EFStateTest.Show();
                }
                {
                    ////DBContext使用
                    //EFContextTest.Show();
                }
                {
                    //查询进阶(延迟查询)
                    EFContextTest.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
