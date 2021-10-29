using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model
{
    /// <summary>
    /// Real estate company base model
    /// </summary>
    public class RECBase
    {
        /// <summary>
        /// Org name
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// Tax id
        /// </summary>
        public string OrgTaxID { get; set; }
        /// <summary>
        /// phone
        /// </summary>
        public string OrgPhoneNumber { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public string AddressLine1 { get; set; }
        /// <summary>
        /// Address line 2
        /// </summary>
        public string AddressLine2 { get; set; }
        /// <summary>
        /// city 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// postal code
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// contact first name
        /// </summary>
        public string ContactFirstName { get; set; }
        /// <summary>
        /// contact last name
        /// </summary>
        public string ContactLastName { get; set; }
        /// <summary>
        /// middle name
        /// </summary>
        public string ContactMiddleName { get; set; }
        /// <summary>
        /// phone
        /// </summary>
        public string ContactPhoneNumber { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string ContactEmail { get; set; }
    }
}
