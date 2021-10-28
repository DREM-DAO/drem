using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model
{
    /// <summary>
    /// value set db model
    /// </summary>
    public class ValueSet
    {
        /// <summary>
        /// id 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// value set code
        /// </summary>
        public string ValueSetCode { get; set; }
        /// <summary>
        /// value set item code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// localization text in specific language
        /// </summary>
        public string ItemValue { get; set; }
        /// <summary>
        /// language
        /// </summary>
        public string Language { get; set; }
    }
}
