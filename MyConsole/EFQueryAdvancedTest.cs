using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class EFQueryAdvancedTest
    {
        public static void Show()
        {
            using (LCDbContext dbContext = new LCDbContext())
            {
                var lgList = dbContext.Set<tb_log>().Where(t => t.id > 1);//1 这句话执行完，没有数据库查询
                foreach (var item in lgList)//2 迭代遍历数据才去数据库查询--在真实需要使用数据时，才去查询的
                {
                    Console.WriteLine(item.info);
                }
                //这就是延迟查询，可以叠加多次查询条件，一次提交给数据库，可以按需获取数据；
                lgList = lgList.Where(t => t.id < 10);
                //...
                lgList = lgList.OrderBy(t => t.id);

                var list = lgList.ToList();//ToList() 迭代器 Count() FirstOrDefalut()都会真实查询数据库
                //延迟查询也要注意：a 迭代使用时，用完了才关闭连接，会占据着连接资源，直到迭代完才关闭连接；b 脱离Context作用域

            }
        }
    }
}
