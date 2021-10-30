using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// List of daily payouts
    /// </summary>
    public class DailyPayoutBase
    {
        /// <summary>
        /// Project.Id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// Published in the blockchain under tx ids
        /// </summary>
        public List<string> TxId { get; set; } = new List<string>();
        /// <summary>
        /// Amout 
        /// </summary>
        public decimal AggregatedAmount { get; set; }
        /// <summary>
        /// Asset id, eg usdc mainnet algo asset
        /// </summary>
        public ulong Asset { get; set; }
        /// <summary>
        /// Investors / REC / DREM / Liquidity providers
        /// </summary>
        public string ReceiverParty { get; set; }
    }
}
