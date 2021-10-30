using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.DB
{
    /// <summary>
    /// Transfer from or to the project money buffer
    /// </summary>
    public class Transfer : Comm.TransferWithId
    {
        /// <summary>
        /// time created
        /// </summary>
        public DateTimeOffset Created { get; set; }
        /// <summary>
        /// time of last update
        /// </summary>
        public DateTimeOffset Updated { get; set; }
    }
}
