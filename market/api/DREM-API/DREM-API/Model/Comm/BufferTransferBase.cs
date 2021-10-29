using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Buffer transfer transaction s
    /// </summary>
    public class BufferTransferBase
    {
        /// <summary>
        /// Transfer reason
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Tx amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Tx currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        public DateTimeOffset Time { get; set; }
    }
}
