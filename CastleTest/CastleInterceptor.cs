using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleTest
{
    public class CastleInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 创建代理类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateProxy<T>() where T : class
        {
            ProxyGenerator generator = new ProxyGenerator();
            var testa = generator.CreateClassProxy<T>(new CastleInterceptor());
            return testa;
        }

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Trace.WriteLine(invocation.Method.Name + "执行前, 入参: " + string.Join(",", invocation.Arguments));
        }

        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            // Step 1. Do something prior to invocation.
            Trace.WriteLine(invocation.Method.Name + "执行中");

            try
            {
                base.PerformProceed(invocation);

                //How to intercept asynchronous methods without AsyncInterceptor ?
                //a.Methods that return Task
                Type type = invocation.ReturnValue?.GetType();
                if (type != null && type == typeof(Task)) 
                {
                    // Given the method returns a Task, wait for it to complete before performing Step 2
                    Func<Task> continuation = async () =>
                    {
                        await (Task)invocation.ReturnValue;

                        // Step 2. Do something after invocation
                        // 这一步怎么没有执行呢?
                        Trace.WriteLine(invocation.Method.Name + "await执行后");
                    };

                    invocation.ReturnValue = continuation();
                    return;
                }

                //b.Methods that return Task<TResult>

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Trace.WriteLine(invocation.Method.Name + "执行后, 返回值: " + invocation.ReturnValue);
        }
    }
}
