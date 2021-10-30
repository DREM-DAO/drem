using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Shareholder information
    /// </summary>
    public class ShareholderWithId : ShareholderBase
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public string Id { get; set; }
    }
}
