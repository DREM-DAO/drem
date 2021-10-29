using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm.Voting
{
    /// <summary>
    /// Public voting
    /// </summary>
    public class VotingBase
    {
        /// <summary>
        /// Question
        /// </summary>
        public VotingQuestionBase Question { get; set; }
        /// <summary>
        /// Result
        /// </summary>
        public VotingResultBase Result { get; set; }
    }
}
