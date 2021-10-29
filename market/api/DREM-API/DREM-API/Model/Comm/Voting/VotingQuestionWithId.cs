using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm.Voting
{
    /// <summary>
    /// question in voting
    /// </summary>
    public class VotingQuestionWithId : VotingQuestionBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}
