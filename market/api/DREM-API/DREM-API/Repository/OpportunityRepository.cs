using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Repository
{
    /// <summary>
    /// Real estate companies EF repository
    /// </summary>
    public class OpportunityRepository
    {
        private readonly Model.ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public OpportunityRepository(Model.ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.OpportunityBase, Model.DB.Opportunity>();
                cnf.CreateMap<Model.Comm.OpportunityWithId, Model.DB.Opportunity>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.Opportunity>> GetAllAsync()
        {
            return await context.Opportunities.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.Opportunity> AddAsync(Model.Comm.OpportunityBase entity)
        {

            var ret = mapper.Map<Model.DB.Opportunity>(entity);
            ret.Id = Guid.NewGuid().ToString();
            ret.Created = DateTimeOffset.UtcNow;
            ret.Updated = DateTimeOffset.UtcNow;
            var result = context.Add(ret);
            await context.SaveChangesAsync();
            return ret;
        }
        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.Opportunity> UpdateAsync(Model.Comm.OpportunityWithId entity)
        {
            var old = await context.FindAsync<Model.DB.Opportunity>(entity.Id);
            var updated = mapper.Map<Model.Comm.OpportunityWithId, Model.DB.Opportunity>(entity, old);
            var result = context.Update(updated);
            await context.SaveChangesAsync();
            return updated;
        }
        /// <summary>
        /// Delete items from DB.
        /// </summary>
        /// <param name="ids">Entity, that will be deleted.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<bool> DeleteAsync(string[] ids)
        {
            if (ids == null || ids.Length == 0) return true;
            var set = new HashSet<string>(ids);
            var toRemove = context.Opportunities.Where(i => set.Contains(i.Id)).ToArray();
            _ = context.Remove(toRemove);
            await context.SaveChangesAsync();
            return true;
        }
        internal int AddRange(List<Model.DB.Opportunity> data)
        {
            context.Opportunities.AddRange(data);
            return context.SaveChanges();
        }
    }
}
