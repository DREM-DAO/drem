using AutoMapper;
using DREM_API.Model;
using DREM_API.Model.Comm;
using DREM_API.Model.DB;
using DREM_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    /// <summary>
    /// 
    /// </summary>
    public class DailyPayoutBusinessController
    {
        private readonly DailyPayoutRepository repository;
        private readonly Mapper mapper;
        /// <summary>
        /// Constructor business DailyPayout controller
        /// </summary>
        /// <param name="repository"></param>
        public DailyPayoutBusinessController(DailyPayoutRepository repository)
        {
            this.repository = repository;
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<DailyPayoutBase, DailyPayoutWithId>();
            }));
        }

        internal Task<Model.DB.DailyPayout> CreateAsync(Model.Comm.DailyPayoutBase item)
        {
            return repository.AddAsync(item);
        }

        internal Task<Model.DB.DailyPayout> UpdateAsync(string id, DailyPayoutBase item)
        {
            var withId = mapper.Map<DailyPayoutWithId>(item);
            withId.Id = id;
            return repository.UpdateAsync(withId);
        }

        internal Task<int> DeleteAsync(string[] id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.DailyPayout> ListAllForProject(string projectId)
        {
            return repository.ListAllForProject(projectId);
        }
    }
}
