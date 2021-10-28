using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Shareholder
    /// </summary>
    public class Shareholder
    {
        /// <summary>
        /// Account address
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Share
        /// </summary>
        public decimal Share { get; set; }
    }
}
