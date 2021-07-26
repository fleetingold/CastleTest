using Castle.DynamicProxy;
using System;
using System.Diagnostics;

namespace AsyncInterceptor
{
    public class ExtendProcessingAsyncInterceptor : ProcessingAsyncInterceptor<string>
    {
        protected override string StartingInvocation(IInvocation invocation)
        {
            return $"{invocation.Method.Name}:StartingInvocation:{DateTime.UtcNow:O}";
        }

        protected override void CompletedInvocation(IInvocation invocation, string state)
        {
            Trace.WriteLine(state);
            Trace.WriteLine($"{invocation.Method.Name}:CompletedInvocation:{DateTime.UtcNow:O}");
        }
    }
}
