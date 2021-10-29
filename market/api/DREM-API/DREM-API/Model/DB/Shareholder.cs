using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.DB
{
    /// <summary>
    /// Shareholder db model
    /// </summary>
    public class Shareholder : Comm.ShareholderWithId
    {
        /// <summary>
        /// time created
        /// </summary>
        public DateTimeOffset Created { get; set; }
        /// <summary>
        /// time updated
        /// </summary>
        public DateTimeOffset Updated { get; set; }
    }
}
