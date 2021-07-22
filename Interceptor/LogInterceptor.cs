using AsyncInterceptor;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interceptor
{
    public class LogInterceptor : IInterceptor
    {
        private readonly LogInterceptorAsync _logInterceptorAsync;

        public LogInterceptor(LogInterceptorAsync logInterceptorAsync)
        {
            _logInterceptorAsync = logInterceptorAsync;
        }

        public void Intercept(IInvocation invocation)
        {
            _logInterceptorAsync.ToInterceptor().Intercept(invocation);
        }
    }
}
