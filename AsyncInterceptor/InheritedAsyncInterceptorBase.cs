using Castle.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace AsyncInterceptor
{
    public class InheritedAsyncInterceptorBase : AsyncInterceptorBase
    {
        protected override Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            throw new NotImplementedException();
        }

        protected override Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            throw new NotImplementedException();
        }
    }
}
