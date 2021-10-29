using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm.Voting
{
    /// <summary>
    /// Public voting
    /// </summary>
    public class Voting
    {
        /// <summary>
        /// Question
        /// </summary>
        public VotingQuestion Question { get; set; }
        /// <summary>
        /// Result
        /// </summary>
        public VotingResult Result { get; set; }
    }
}
