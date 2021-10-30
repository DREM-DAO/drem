using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Order with id
    /// </summary>
    public class OrderWithId : OrderBase
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public string Id { get; set; }
    }
}
