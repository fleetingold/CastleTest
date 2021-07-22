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

        public virtual string GetResult2()
        {
            string str = DateTime.Now + "TestB-GetResult2";
            Trace.WriteLine(str);
            return str;
        }
    }
}
