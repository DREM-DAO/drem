using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm.Voting
{
    /// <summary>
    /// Result
    /// </summary>
    public class VotingResult
    {
        /// <summary>
        /// Tx Id with the results
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Results calculated with rule 1 share = 1 vote
        /// </summary>
        [JsonProperty("o")]
        public Dictionary<string, decimal> Options { get; set; }
    }
}
