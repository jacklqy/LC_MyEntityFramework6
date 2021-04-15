# LC_MyEntityFramework6
    /// <summary>
    /// EF6->EntityFramework6：支持多种数据库；支持函数、存储过程等...
    /// 1、ORM对象关系映射
    /// 2、EntityFramework DBFirst
    /// 3、EntityFramework CodeFirst from db --推荐
    /// 4、CodeFirst
    /// 4、EntityFramework modelFirst
    /// 5、EF增删改查基本实现
    /// 
    /// context---映射数据库连接实例
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
    /// EF查看sql有2种方式：方式一：数据库管理工具里面的sql server profiler；方式二：dbContext.Database.Log += sqlStr => Console.WriteLine($"当前执行sql：{sqlStr}");
    /// 
    /// 1 各种复杂查询&直接执行sql
    /// 2 EF状态跟踪，本地增删改查实现
    /// 3 Context生命周期，多种事务
    /// 4 EF延迟查询、导航属性加载&增加&删除
    /// 
    /// 封装类库--上端引用--引用EF--搬迁配置文件数据库连接字符串
    /// 
    /// EF四种事务：
    /// 1 单个SaveChanges本身就是一个事务;。
    /// 2 TransactionScope完成一个context的多次SaveChanges事务。
    /// 3 TransactionScope完成不同context实例的事务。
    /// 4 dbContext.Database.BeginTransaction()实现事务。
    /// 
    /// 1 理解分层架构
    /// 2 EF分层封装数据访问
    /// 3 EF和IOC整合
    /// 4 AOP扩展订制
    /// 
    /// 分层--架构
    /// EF里面 Context已经完成了DAL能做的全部事儿了---没必要在去整一个DAL
    /// EF.Business.Service---业务逻辑层
    /// EF.Business.IService---业务抽象层
    /// </summary>

相关依赖环境：SqlServer2014、EntityFramework6.0、.NET Framework 4.7.2、MVC5


![image](https://user-images.githubusercontent.com/26539681/114819757-855b5800-9df0-11eb-88fa-3d084cdb6ceb.png)
![image](https://user-images.githubusercontent.com/26539681/114819832-a623ad80-9df0-11eb-97d1-b7733d12447a.png)
![image](https://user-images.githubusercontent.com/26539681/114819898-c2bfe580-9df0-11eb-9ceb-9264fa9ec0ea.png)
![image](https://user-images.githubusercontent.com/26539681/114819996-e71bc200-9df0-11eb-9f90-628f9cbaffb4.png)

希望为.net开源社区尽绵薄之力，探lu者###一直在探索前进的路上###（QQ:529987528）
