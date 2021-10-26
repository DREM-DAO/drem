using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model
{
    public class ValueSet
    {
        public string Id { get; set; }
        public string ValueSetCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemValue { get; set; }
        public string Language { get; set; }
    }
}
