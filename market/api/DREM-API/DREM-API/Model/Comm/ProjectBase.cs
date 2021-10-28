using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Project to show on the main screen
    /// </summary>
    public class ProjectBase
    {
        /// <summary>
        /// url friendly name
        /// </summary>
        public string UrlId { get; set; }
        /// <summary>
        /// Underlying algorand standard asset
        /// </summary>
        public ulong? ASA { get; set; }
        /// <summary>
        /// is top offer
        /// </summary>
        public bool Top { get; set; }
        /// <summary>
        /// Lattitude
        /// </summary>
        public decimal? Lat { get; set; }
        /// <summary>
        /// Longtitude
        /// </summary>
        public decimal? Lng { get; set; }
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Interest rate return
        /// </summary>
        public decimal? IRR { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// property type value set
        /// </summary>
        public string PropertyType { get; set; }
        /// <summary>
        /// investment type value set
        /// </summary>
        public string InvestmentType { get; set; }
        /// <summary>
        /// region value set
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// country value set code
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// city value set code
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// When project hit this time, it should get canceled
        /// </summary>
        public DateTimeOffset? Countdown { get; set; }
        /// <summary>
        /// The project is publicly visible
        /// </summary>
        public bool ShowToPublic { get; set; } = false;
    }
}
