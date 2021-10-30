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
    public class ShareholderRepository
    {
        private readonly Model.ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public ShareholderRepository(Model.ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.ShareholderBase, Model.DB.Shareholder>();
                cnf.CreateMap<Model.Comm.ShareholderWithId, Model.DB.Shareholder>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.Shareholder>> GetAllAsync()
        {
            return await context.Shareholders.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.Shareholder> AddAsync(Model.Comm.ShareholderBase entity)
        {

            var ret = mapper.Map<Model.DB.Shareholder>(entity);
            ret.Id = Guid.NewGuid().ToString();
            ret.Created = DateTimeOffset.UtcNow;
            ret.Updated = DateTimeOffset.UtcNow;
            var result = context.Add(ret);
            await context.SaveChangesAsync();
            return ret;
        }
        /// <summary>
        /// List all transactions for asset
        /// </summary>
        /// <param name="asaId"></param>
        /// <returns></returns>
        internal IQueryable<Model.DB.Shareholder> ListAllForAsset(ulong asaId)
        {
            return context.Shareholders.Where(p => p.ProjectAsset == asaId).OrderByDescending(p => p.Created);
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.Shareholder> UpdateAsync(Model.Comm.ShareholderWithId entity)
        {
            var old = await context.FindAsync<Model.DB.Shareholder>(entity.Id);
            var updated = mapper.Map<Model.Comm.ShareholderWithId, Model.DB.Shareholder>(entity, old);
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
            var toRemove = context.Shareholders.Where(i => set.Contains(i.Id)).ToArray();
            _ = context.Remove(toRemove);
            return await context.SaveChangesAsync();
        }
        internal int AddRange(List<Model.DB.Shareholder> data)
        {
            context.Shareholders.AddRange(data);
            return context.SaveChanges();
        }
    }
}
