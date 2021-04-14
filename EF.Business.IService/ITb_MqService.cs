using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Business.IService
{
    public interface ITb_MqService : IBaseService
    {
        void UpdateLastData(tb_mq mq);
    }
}
