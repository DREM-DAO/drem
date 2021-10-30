using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// List of all asa transfers, except of transfers to know escrow accounts meant for trading
    /// </summary>
    public class TransferWithId : Comm.TransferBase
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public string Id { get; set; }
    }
}
