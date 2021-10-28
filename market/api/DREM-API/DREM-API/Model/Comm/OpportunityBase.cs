using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Opportunty from REC to DREM
    /// </summary>
    public class OpportunityBase
    {
        public string RecName { get; set; }
        public string RecID { get; set; }
        public string AssetType { get; set; }
        public int? BedRooms { get; set; }
        public int? BathRooms { get; set; }
        public decimal? LivingAreaSqft { get; set; }
        public decimal? NonLivingAreaSqft { get; set; }
        public decimal? LandSqft { get; set; }
        public int? YearBuilt { get; set; }
        public ulong AsaId { get; set; }
        public decimal? AssetValue { get; set; }
        public string AssetValueCurrency { get; set; }
        public decimal? EstimatedIRR { get; set; }
        public string Comments { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerMiddleName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string OwnerEmail { get; set; }
    }
}
