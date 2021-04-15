using EF.Business.IService;
using EF.Business.IService.ICombinationService;
using EF.Common.UnitOfWork;
using EF.Common.Unity;
//using EF.Business.Service;
//using EF.Business.Service.CombinationService;
using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MyConsole
{
    public class IOCTest
    {
        public static void Show()
        {
            //using (LCDbContext dbContext = new LCDbContext())
            //{
            //    //既然分层封装，就不再跨层访问
            //    tb_log lg = dbContext.tb_log.Find(1);
            //}

            //{
            //    //1 通过Service来对数据库的访问
            //    ITb_MqService iTb_MqService = new Tb_MqService();
            //    iTb_MqService.Add();
            //}

            {
                ////2 访问很多张表
                ////每个service都提供增删改查基本方法？来一个父类继承一下；//
                ////接口也来一个父类继承一下
                ////把常规API放入接口和实现
                //ITb_MqService iTb_MqService = new Tb_MqService();
                //iTb_MqService.Add();
            }

            {
                //{
                //    using (ITb_LogService iTb_LogService = new Tb_LogService(new LCDbContext()))
                //    {
                //        tb_log lg = iTb_LogService.Find<tb_log>(1);
                //        //...
                //    }
                //    using (ITb_MqService iTb_MqService = new Tb_MqService(new LCDbContext()))
                //    {
                //        tb_mq mq = iTb_MqService.Find<tb_mq>(1);
                //        //...
                //    }
                //}

                ////1 每张表一个server？
                ////需要join怎么办呢？还有其它复杂操作
                ////这是业务逻辑，不应该写在上端，应该在server
                //using (ITb_LogService iTb_LogService = new Tb_LogService(new LCDbContext()))
                //using (ITb_MqService iTb_MqService = new Tb_MqService(new LCDbContext()))
                //{
                //    var result = from m in iTb_MqService.Set<tb_mq>()
                //                 join c in iTb_LogService.Set<tb_log>()
                //                 on m.id equals c.mqpathid
                //                 where m.id > 1
                //                 select m;//这种方式是错的，不同的上下文！！！
                //}
                ////2 单表单Service很多时候无法满足业务的需求，业务经常是跨表的
                ////所以在封装Service时，需要划分边界，完成组合，一个Service完成一个有机整体的全部操作
                ////难就难在到底要怎么组合呢？根据项目的实际情况！
                ////  a 比如主外键关系的一般放一个Service
                ////  b Mapping式一般也可以放在一起
                ////  c 单表单Service不算错比如日志
                ////  d 频繁互动的类可以考虑合并一下
                //using (IMqLogService iMqLogService = new MqLogService(new LCDbContext()))
                //{
                //    tb_mq mq = iMqLogService.Find<tb_mq>(1);
                //    iMqLogService.RemoveMqAndLog(mq);
                //}

                ////3 上端难免还是要多个Service(多个Context)共同操作，不要Join！事务(UnitOfWork)怎么办呢？TransactionScope
                //UnitOfWorkHelper.Invoke(() =>
                //{
                //    using (IMqLogService iMqLogService = new MqLogService(new LCDbContext()))
                //    {
                //        //增删改
                //    }
                //});
            }

            {
                //IOC整合：去掉细节依赖，降低耦合，增强扩展性
                //1 NuGet包引用：Unity/Unity.Interception/Unity.Configuration/Unity.Interception.Configuration
                //2 配置Unity.Config文件
                //3 配置AOP
                IUnityContainer container = ContainerFactory.GetContainer();
                using (IMqLogService iMqLogService = container.Resolve<IMqLogService>())
                {
                    tb_log lg = iMqLogService.Find<tb_log>(1);
                }
                using (ITb_MqService iTb_MqService = container.Resolve<ITb_MqService>())
                {
                    tb_mq mq = iTb_MqService.Find<tb_mq>(1);
                }

                //多个Service(多个Context)共同操作，事务处理
                UnitOfWorkHelper.Invoke(() =>
                {
                    using (IMqLogService iMqLogService = container.Resolve<IMqLogService>())
                    {
                        tb_log lg = iMqLogService.Find<tb_log>(1);
                    }
                    using (ITb_MqService iTb_MqService = container.Resolve<ITb_MqService>())
                    {
                        tb_mq mq = iTb_MqService.Find<tb_mq>(1);
                    }
                });

            }
        }
    }
}
