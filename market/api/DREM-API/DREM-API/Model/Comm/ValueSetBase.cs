using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// value set
    /// </summary>
    public class ValueSetBase
    {
        /// <summary>
        /// value set code
        /// </summary>
        public string ValueSetCode { get; set; }
        /// <summary>
        /// item code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// item value
        /// </summary>
        public string ItemValue { get; set; }
    }
}
