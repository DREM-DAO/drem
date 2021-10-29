using DREM_API.Model;
using DREM_API.Model.Comm;
using DREM_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    /// <summary>
    /// REC Business Controller
    /// </summary>
    public class RECBusinessController
    {
        private readonly RECRepository repository;
        private readonly OpportunityRepository opportunityRepository;
        /// <summary>
        /// RECBusinessController constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="opportunityRepository"></param>
        public RECBusinessController(
            RECRepository repository,
            OpportunityRepository opportunityRepository
            )
        {
            this.repository = repository;
            this.opportunityRepository = opportunityRepository;
        }

        internal Task<Model.DB.REC> Register(Model.DB.REC rec)
        {
            return repository.AddAsync(rec);
        }

        internal Task<IEnumerable<Model.DB.REC>> GetAll()
        {
            return repository.GetAllAsync();
        }

        internal Task<Model.DB.Opportunity> CreateOpportunity(OpportunityBase opportunity)
        {
            return opportunityRepository.AddAsync(opportunity);
        }
        internal Task<IEnumerable<Model.DB.Opportunity>> GetAllOpportunitiesAsync()
        {
            return opportunityRepository.GetAllAsync();
        }
    }
}
