using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    /// <summary>
    /// 延迟查询
    /// </summary>
    public class EFQueryAdvancedTest
    {
        public static void Show()
        {
            {
                using (LCDbContext dbContext = new LCDbContext())
                {
                    var lgList = dbContext.Set<tb_log>().Where(t => t.id > 1);//1 这句话执行完，没有数据库查询
                    foreach (var item in lgList)//2 迭代遍历数据才去数据库查询--在真实需要使用数据时，才去查询的
                    {
                        Console.WriteLine(item.info);
                    }
                    //这就是延迟查询，可以叠加多次查询条件(表达式目录树)，一次提交给数据库，可以按需获取数据；
                    lgList = lgList.Where(t => t.id < 10);//***表达式目录树 --》linq to sql***
                    //...
                    lgList = lgList.OrderBy(t => t.id);

                    var list = lgList.ToList();//ToList() 迭代器 Count() FirstOrDefalut()都会真实查询数据库
                                               //延迟查询也要注意：a 迭代使用时，用完了才关闭连接，会占据着连接资源，直到迭代完才关闭连接；b 脱离Context作用域
                    //其实lgList只是一个包装对象，里面有表达式目录树，有结果类型，有解析工具，还有上下文，真实需要数据时才会到数据库查询
                }
                //对比：
                //linq to sql：楼上是IQueryable类型，数据在数据库里面，这个list里面有表达式目录树(所以可以一直拼接查询条件)---返回值类型--IQueryProvider(查询的支持工具，SqlServer语句的生成)
                //linq to object：楼下是IEnumerable类型，数据其实已经在内存里，有个迭代器的实现，用的是委托
                {
                    List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    var list = intList.Where(t => t > 3);//***委托 --》linq to object***
                    foreach (var i in list)
                    {
                        Console.WriteLine(i);
                    }
                    //这里是延迟的，利用的是迭代器的方式，每次去迭代访问时，才去筛选一次。委托+迭代器
                }
            }
            
            
        }
    }
}
