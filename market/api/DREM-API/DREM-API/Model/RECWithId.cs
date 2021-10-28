using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model
{
    /// <summary>
    /// Real estate company db model
    /// </summary>
    public class RECWithId : REC
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
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
