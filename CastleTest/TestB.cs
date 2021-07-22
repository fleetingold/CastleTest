using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleTest
{
    public class TestB
    {
        public virtual string GetResult()
        {
            string str = DateTime.Now + "TestB-GetResult";
            Trace.WriteLine(str);
            return str;
        }

        public virtual string GetResultWithArgument(DateTime dt)
        {
            string str = DateTime.Now + "TestB-GetResult2";
            Trace.WriteLine(str);
            return str;
        }

        public virtual Task TestActionAsync() 
        {
            return Task.Factory.StartNew(async () => 
            {
                await Task.Delay(5000);

                Trace.TraceError("Hello From TestActionAsync");
            });
        }

        public async virtual Task<string> GetResultAsync() 
        {
            return await Task.Run(async () => 
            {
                await Task.Delay(5000);

                return "Hello From GetResultAsync";
            });
        }
    }
}
