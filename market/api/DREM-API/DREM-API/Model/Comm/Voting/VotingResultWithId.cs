using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm.Voting
{
    /// <summary>
    /// voting result 
    /// </summary>
    public class VotingResultWithId : VotingResultBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}
