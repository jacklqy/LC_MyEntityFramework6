using EF.Business.IService.ICombinationService;
using EF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Business.Service.CombinationService
{
    /// <summary>
    /// 组合服务：tb_mq和tb_log的组合
    /// </summary>
    public class MqLogService : BaseService, IMqLogService
    {
        public MqLogService(DbContext context) : base(context)
        {

        }

        /// <summary>
        /// 传入tb_mq实例，将tb_mq和相关的外键tb_log一起删除
        /// </summary>
        /// <param name="mq"></param>
        /// <returns></returns>
        public bool RemoveMqAndLog(tb_mq mq)
        {
            tb_mq mqDB = this.Find<tb_mq>(mq.id);
            IQueryable<tb_log> logDBList = this.Query<tb_log>(lg => lg.mqpathid == mqDB.id);
            this.Delete<tb_mq>(mqDB);
            this.Delete<tb_log>(logDBList);
            return true;
        }
    }
}
