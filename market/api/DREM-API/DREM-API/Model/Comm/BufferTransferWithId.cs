using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Buffer transfer - money sent to the buffer or sent out from the buffer
    /// </summary>
    public class BufferTransferWithId : Comm.BufferTransferBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}
