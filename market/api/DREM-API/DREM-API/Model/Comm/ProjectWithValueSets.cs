using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Project with filled in value set names in specific language
    /// </summary>
    public class ProjectWithValueSets : ProjectWithId
    {
        /// <summary>
        /// Currency
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// Property type
        /// </summary>
        public string PropertyTypeName { get; set; }
        /// <summary>
        /// investment type
        /// </summary>
        public string InvestmentTypeName { get; set; }
        /// <summary>
        /// region name
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// country name
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// city name
        /// </summary>
        public string CityName { get; set; }
    }
}
