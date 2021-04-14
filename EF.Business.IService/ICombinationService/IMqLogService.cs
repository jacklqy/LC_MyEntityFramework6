using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Business.IService.ICombinationService
{
    /// <summary>
    /// 组合接口：tb_mq和tb_log的组合
    /// </summary>
    public interface IMqLogService : IBaseService
    {
        /// <summary>
        /// 传入tb_mq实例，将tb_mq和相关的外键tb_log一起删除
        /// </summary>
        /// <param name="mq"></param>
        /// <returns></returns>
        bool RemoveMqAndLog(tb_mq mq);
    }
}
