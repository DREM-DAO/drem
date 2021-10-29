using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.DB
{
    /// <summary>
    /// Result of the voting
    /// </summary>
    public class VotingResult : Comm.Voting.VotingResultWithId
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
