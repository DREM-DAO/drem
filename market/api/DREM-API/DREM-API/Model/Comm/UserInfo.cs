using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Show user information
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Crypto address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Is admin
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// is KYC verified
        /// </summary>
        public bool KYCVerified { get; set; }
    }
}
