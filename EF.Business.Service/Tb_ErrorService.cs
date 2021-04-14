using EF.Business.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Business.Service
{
    public class Tb_ErrorService : BaseService,ITb_ErrorService
    {
        public Tb_ErrorService(DbContext context) : base(context)
        {

        }
    }
}
