using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MyConsole
{
    public class UnitOfWork
    {
        public static void Invoke(Action action)
        {
            TransactionScope trans = null;
            try
            {
                trans = new TransactionScope();//分布式事务
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
