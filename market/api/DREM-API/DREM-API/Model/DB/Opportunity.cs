using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.DB
{
    /// <summary>
    /// DB model for opportunity
    /// </summary>
    public class Opportunity : Comm.OpportunityWithId
    {
        /// <summary>
        /// time created
        /// </summary>
        public DateTimeOffset Created { get; set; }
        /// <summary>
        /// time of last update
        /// </summary>
        public DateTimeOffset Updated { get; set; }
    }
}
