using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Payout with id
    /// </summary>
    public class DailyPayoutWithId : DailyPayoutBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}
