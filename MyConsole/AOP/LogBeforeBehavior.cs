using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace MyConsole.AOP
{
    /// <summary>
    /// 不需要特性
    /// </summary>
    public class LogBeforeBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //Session 
            Console.WriteLine($"{input.MethodBase.Name} LogBeforeBehavior..Start.");
            //foreach (ParameterInfo item in input.Inputs)
            //{
            //    Console.WriteLine($"{item.Name}：{Newtonsoft.Json.JsonConvert.SerializeObject(item)}" );
            //}
            Console.WriteLine($"{input.MethodBase.Name} LogBeforeBehavior..  End.");
            return getNext().Invoke(input, getNext);
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
