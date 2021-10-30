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
    public class OrderRepository
    {
        private readonly Model.ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public OrderRepository(Model.ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.OrderBase, Model.DB.Order>();
                cnf.CreateMap<Model.Comm.OrderWithId, Model.DB.Order>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.Order>> GetAllAsync()
        {
            return await context.Orders.ToListAsync();
        }

        internal IQueryable<Model.DB.Order> ListAllBidsForProject(ulong asaId)
        {
            return context.Orders.Where(o => o.AssetToReceiveId == asaId).OrderByDescending(o => o.Price).OrderBy(o => o.Created);
        }
        internal IQueryable<Model.DB.Order> ListAllOffersForProject(ulong asaId)
        {
            return context.Orders.Where(o => o.AssetToPayId == asaId).OrderBy(o => o.Price).OrderBy(o=>o.Created);
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.Order> AddAsync(Model.Comm.OrderBase entity)
        {

            var ret = mapper.Map<Model.DB.Order>(entity);
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
        public async Task<Model.DB.Order> UpdateAsync(Model.Comm.OrderWithId entity)
        {
            var old = await context.FindAsync<Model.DB.Order>(entity.Id);
            var updated = mapper.Map<Model.Comm.OrderWithId, Model.DB.Order>(entity, old);
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
            var toRemove = context.Orders.Where(i => set.Contains(i.Id)).ToArray();
            _ = context.Remove(toRemove);
            return await context.SaveChangesAsync();
        }
        internal int AddRange(List<Model.DB.Order> data)
        {
            context.Orders.AddRange(data);
            return context.SaveChanges();
        }
    }
}
