using DREM_API.Model;
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
        /// <summary>
        /// RECBusinessController constructor
        /// </summary>
        /// <param name="repository"></param>
        public RECBusinessController(RECRepository repository)
        {
            this.repository = repository;
        }

        internal Task<RECWithId> Register(REC rec)
        {
            return repository.AddAsync(rec);
        }

        internal Task<IEnumerable<RECWithId>> GetAll()
        {
            
            return repository.GetAllAsync();
        }
    }
}
