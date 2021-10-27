using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    public class ProjectBase
    {
        public bool Top { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Name { get; set; }
        public decimal IRR { get; set; }
        public string Image { get; set; }
        public string Currency { get; set; }
        public string PropertyType { get; set; }
        public string InvestmentType { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
