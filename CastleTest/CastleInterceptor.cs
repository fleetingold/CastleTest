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
            Trace.WriteLine(invocation.Method.Name + "执行中");
            try
            {
                base.PerformProceed(invocation);
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
