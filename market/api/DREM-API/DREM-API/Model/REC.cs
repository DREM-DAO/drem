using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model
{
    public class REC
    {
        public string OrgName { get; set; }
        public string OrgTaxID { get; set; }
        public string OrgPhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
    }
}
