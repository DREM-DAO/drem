using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm.Voting
{
    /// <summary>
    /// Vote Coin AMS0001 https://scholtz.github.io/AMS/AMS-0001/AMS-0001.html specs
    /// </summary>
    public class VotingQuestion
    {
        /// <summary>
        /// Tx Id with the question
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("t")]
        public string Title { get; set; }
        /// <summary>
        /// Question
        /// </summary>
        [JsonProperty("q")]
        public string Question { get; set; }
        /// <summary>
        /// Duration
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }
        /// <summary>
        /// Options
        /// </summary>
        [JsonProperty("o")]
        public Dictionary<string,string> Options { get; set; }
    }
}
