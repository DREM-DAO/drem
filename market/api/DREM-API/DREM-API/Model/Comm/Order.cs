using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Hittable Bid or Offer in the escrow account
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Amount of asset to be recieved
        /// </summary>
        public ulong AssetReceiveAmount { get; set; }
        /// <summary>
        /// Amount of asset to be paid
        /// </summary>
        public ulong AssetPayAmount { get; set; }
        /// <summary>
        /// Asset id to be received. If algo, than null
        /// </summary>
        public ulong? AssetReceiveId { get; set; }
        /// <summary>
        /// Asset id to be paid. If algo, than null
        /// </summary>
        public ulong? AssetPayId { get; set; }
        /// <summary>
        /// Receive asset decimals
        /// </summary>
        public int AssetReceiveDecimals { get; set; }
        /// <summary>
        /// Pay asset decimals
        /// </summary>
        public int AssetPayDecimals { get; set; }
        /// <summary>
        /// Address of the escrow account
        /// </summary>
        public string EscrowAddress { get; set; }
        /// <summary>
        /// Address of the market maker account
        /// </summary>
        public string OwnerAddress { get; set; }
        /// <summary>
        /// Round when the order has been placed
        /// </summary>
        public ulong Round { get; set; }
        /// <summary>
        /// Time when order has been placed
        /// </summary>
        public DateTimeOffset Time { get; set; }
        /// <summary>
        /// App id
        /// </summary>
        public ulong AppId { get; set; }
        /// <summary>
        /// Version of the approval application
        /// </summary>
        public string Version { get; set; }
    }
}
