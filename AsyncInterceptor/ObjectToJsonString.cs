using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInterceptor
{
    public static class ObjectToJsonString
    {
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj).Replace("\r\n", "");
        }
    }
}
