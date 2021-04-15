using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF.Common.UnitOfWork
{
    public class UnitOfWorkHelper
    {
        public static void Invoke(Action action)
        {
            //需要在程序集-》添加‘System.Transactions’引用
            TransactionScope trans = null;
            try
            {
                trans = new TransactionScope();//分布式事务(同一局域网内)
                action.Invoke();
                trans.Complete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
