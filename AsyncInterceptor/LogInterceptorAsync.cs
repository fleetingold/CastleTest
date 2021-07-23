using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInterceptor
{
    public class LogInterceptorAsync : IAsyncInterceptor
    {
        public LogInterceptorAsync()
        {
            
        }

        /// <summary>
        /// 同步方法拦截时使用
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptSynchronous(IInvocation invocation)
        {
            try
            {
                //调用业务方法
                invocation.Proceed();
                LogExecuteInfo(invocation, invocation.ReturnValue.ToJsonString());//记录日志
            }
            catch (Exception ex)
            {
                LogExecuteError(ex, invocation);
                throw new AopHandledException();
            }
        }

        /// <summary>
        /// 异步方法返回Task时使用
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            #region 第一种处理
            //try
            //{
            //    //调用业务方法
            //    //invocation.Proceed();

            //    if (invocation.ReturnValue is Task task) 
            //    {
            //        LogExecuteInfo(invocation, "");//记录日志
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogExecuteError(ex, invocation);
            //    throw new AopHandledException();
            //}
            #endregion

            #region 第二种处理
            //Castle.Core -> AsyncInterceptor
            //调用业务方法
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
            #endregion
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            try
            {
                var proceed = invocation.CaptureProceedInfo();

                await Task.Delay(10).ConfigureAwait(false);

                proceed.Invoke();

                // Return value is being set in two situations, but this doesn't matter
                // for the above test.
                Task returnValue = (Task)invocation.ReturnValue;

                await returnValue.ConfigureAwait(false);

                LogExecuteInfo(invocation, invocation.ReturnValue.ToString());//记录日志
            }
            catch (Exception ex)
            {
                LogExecuteError(ex, invocation);
                throw new AopHandledException();
            }
        }

        /// <summary>
        /// 异步方法返回Task<T>时使用
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            //调用业务方法
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            try
            {
                //调用业务方法
                invocation.Proceed();
                Task<TResult> task = (Task<TResult>)invocation.ReturnValue;
                TResult result = await task;//获得返回结果
                LogExecuteInfo(invocation, result.ToJsonString());

                return result;
            }
            catch (Exception ex)
            {
                LogExecuteError(ex, invocation);
                throw new AopHandledException();
            }
        }

        #region helpMethod

        /// <summary>
        /// 获取拦截方法信息（类名、方法名、参数）
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private string GetMethodInfo(IInvocation invocation)
        {
            //方法类名
            string className = invocation.Method.DeclaringType.Name;
            //方法名
            string methodName = invocation.Method.Name;
            //参数
            string args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());


            if (string.IsNullOrWhiteSpace(args))
            {
                return $"{className}.{methodName}";
            }
            else
            {
                return $"{className}.{methodName}:{args}";
            }
        }

        private void LogExecuteInfo(IInvocation invocation, string result)
        {
            Trace.TraceInformation("方法{0}，返回值{1}", GetMethodInfo(invocation), result);
        }

        private void LogExecuteError(Exception ex, IInvocation invocation)
        {
            Trace.TraceError("执行{0}时发生错误{1}！", GetMethodInfo(invocation), ex.Message);
        }

        #endregion
    }
}
