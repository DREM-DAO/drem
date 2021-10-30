using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// List of all asa transfers, except of transfers to know escrow accounts meant for trading
    /// </summary>
    public class TransferBase
    {
        /// <summary>
        /// From account
        /// </summary>
        public string FromAccount { get; set; }
        /// <summary>
        /// To account
        /// </summary>
        public string ToAccount { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Price if can be detected from the escrow smart contract
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// If price uknown always null
        /// If price known and underlying trading asset is algo, asset is null and price filled in, if stable coin then ASA id
        /// </summary>
        public ulong PriceAsset { get; set; }
        /// <summary>
        /// Project asa id
        /// </summary>
        public ulong ProjectAsset { get; set; }
    }
}
