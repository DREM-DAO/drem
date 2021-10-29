using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Opportunity to be updated
    /// </summary>
    public class OpportunityWithId : OpportunityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}
