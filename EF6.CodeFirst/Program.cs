using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6.CodeFirst
{
    /// <summary>
    /// 步骤：
    /// 1、项目需要提前在NuGet安装EntityFramework对应的版本
    /// 2、创建实体类（数据库表对应的实体类），同时需要添加一个集成DBContext的类，并完成相应的代码配置
    /// 3、在App.config文件里面添加数据库连接字符串(注意设置数据库名称)
    /// 4、运行代码时，会自己生成相应的数据库
    /// 5、数据库里面就会生成一个多余的表__MigrationHistory，这个就是更新版本记录，后期可以借助相关的Migration工具进行配合使用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (CodeFirstContext context = new CodeFirstContext())
                {
                    tb_log t = context.tb_log.Find(1);
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
