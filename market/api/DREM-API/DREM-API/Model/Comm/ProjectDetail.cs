using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Project details
    /// </summary>
    public class ProjectDetail
    {
        /// <summary>
        /// Project info
        /// </summary>
        public ProjectWithValueSets Project { get; set; }
        /// <summary>
        /// List of daily payments to owners of asa
        /// </summary>
        public DailyPayoutBase[] DailyPayouts { get; set; }
        /// <summary>
        /// List of images of the estate
        /// </summary>
        public ImageMeta[] Images { get; set; }
        /// <summary>
        /// List of images of the estate
        /// </summary>
        public BufferTransferBase[] BufferTxs { get; set; }
        /// <summary>
        /// List of all shareholders
        /// </summary>
        public Shareholder[] Shareholders { get; set; }
        /// <summary>
        /// List of all asset transfers. If the trading in place through known escrow smart contracts, than it shows also underlying price
        /// </summary>
        public Transfer[] Transfers { get; set; }
        /// <summary>
        /// List of all current bids on the escrow accounts
        /// </summary>
        public Order[] Bids { get; set; }
        /// <summary>
        /// List of all current offers on the escrow accounts
        /// </summary> 
        public Order[] Offers { get; set; }
        /// <summary>
        /// Votings - List of questions with public auditable results
        /// </summary>
        public Voting.Voting[] Votings { get; set; }
    }
}
