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
    public class DailyPayoutRepository
    {
        private readonly Model.ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public DailyPayoutRepository(Model.ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.DailyPayoutBase, Model.DB.DailyPayout>();
                cnf.CreateMap<Model.Comm.DailyPayoutWithId, Model.DB.DailyPayout>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.DailyPayout>> GetAllAsync()
        {
            return await context.DailyPayouts.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.DailyPayout> AddAsync(Model.Comm.DailyPayoutBase entity)
        {

            var ret = mapper.Map<Model.DB.DailyPayout>(entity);
            ret.Id = Guid.NewGuid().ToString();
            ret.Created = DateTimeOffset.UtcNow;
            ret.Updated = DateTimeOffset.UtcNow;
            var result = context.Add(ret);
            await context.SaveChangesAsync();
            return ret;
        }
        /// <summary>
        /// List payouts for specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        internal IQueryable<Model.DB.DailyPayout> ListAllForProject(string projectId)
        {
            return context.DailyPayouts.Where(p => p.ProjectId == projectId).OrderByDescending(p => p.Created);
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.DailyPayout> UpdateAsync(Model.Comm.DailyPayoutWithId entity)
        {
            var old = await context.FindAsync<Model.DB.DailyPayout>(entity.Id);
            var updated = mapper.Map<Model.Comm.DailyPayoutWithId, Model.DB.DailyPayout>(entity, old);
            var result = context.Update(updated);
            await context.SaveChangesAsync();
            return updated;
        }
        /// <summary>
        /// Delete items from DB.
        /// </summary>
        /// <param name="ids">Entity, that will be deleted.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<int> DeleteAsync(string[] ids)
        {
            if (ids == null || ids.Length == 0) return 0;
            var set = new HashSet<string>(ids);
            var toRemove = context.DailyPayouts.Where(i => set.Contains(i.Id));
            context.RemoveRange(toRemove);
            return await context.SaveChangesAsync();
        }
        internal int AddRange(List<Model.DB.DailyPayout> data)
        {
            context.DailyPayouts.AddRange(data);
            return context.SaveChanges();
        }
    }
}
