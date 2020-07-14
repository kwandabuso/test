using System;
using System.Collections.Generic;
using System.Text;

namespace test
{
    class Class1
    {
        public List<Class1> racelist { get; set; }
    }
    public class ProviderOperator
    {
        public string first_name { get; set; }
    }
    public class ProviderInfo
    {
        public int provider_id { get; set; }
        public string provider_name { get; set; }
        public string provider_code { get; set; }
        public int service_id { get; set; }
        public string service { get; set; }
        public string provider_image { get; set; }
        public string status { get; set; }
    }
}
