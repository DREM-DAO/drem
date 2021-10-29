using AutoMapper;
using DREM_API.Model;
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
    public class RECRepository
    {
        private readonly ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public RECRepository(ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.REC, Model.RECWithId>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<RECWithId>> GetAllAsync()
        {
            return await context.RECs.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<RECWithId> AddAsync(Model.REC entity)
        {

            var ret = mapper.Map<Model.RECWithId>(entity);
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
        public async Task<RECWithId> UpdateAsync(Model.RECWithId entity)
        {
            var old = await context.FindAsync<RECWithId>(entity.Id);
            entity.Created = old.Created;
            var result = context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<bool> DeleteAsync(Model.RECWithId entity)
        {
            _ = context.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        internal int AddRange(List<RECWithId> data)
        {
            context.RECs.AddRange(data);
            return context.SaveChanges();
        }
    }
}
