using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    public class ProjectBase
    {
        public ulong? ASA { get; set; }
        public bool Top { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public string Name { get; set; }
        public decimal? IRR { get; set; }
        public string Image { get; set; }
        public string Currency { get; set; }
        public string PropertyType { get; set; }
        public string InvestmentType { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Project { get; set; }
        /// <summary>
        /// When project hit this time, it should get canceled
        /// </summary>
        public DateTimeOffset Countdown { get; set; }
        /// <summary>
        /// The project is publicly visible
        /// </summary>
        public bool ShowToPublic { get; set; } = false;
    }
}
