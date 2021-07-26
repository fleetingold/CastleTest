using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInterceptor
{
    public class InheritedAsyncTimingInterceptor : AsyncTimingInterceptor
    {
        protected override void CompletedTiming(IInvocation invocation, Stopwatch stopwatch)
        {
            Trace.WriteLine($"{invocation.Method.Name}:CompletedTiming:{stopwatch.Elapsed:g}");
        }

        protected override void StartingTiming(IInvocation invocation)
        {
            Trace.WriteLine($"{invocation.Method.Name}:StartingTiming");
        }
    }
}
