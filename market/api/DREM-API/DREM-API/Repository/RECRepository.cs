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
                cnf.CreateMap<Model.DB.REC, Model.DB.REC>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.REC>> GetAllAsync()
        {
            return await context.RECs.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.REC> AddAsync(Model.DB.REC entity)
        {

            var ret = mapper.Map<Model.DB.REC>(entity);
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
        public async Task<Model.DB.REC> UpdateAsync(Model.DB.REC entity)
        {
            var old = await context.FindAsync<Model.DB.REC>(entity.Id);
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
        public async Task<bool> DeleteAsync(Model.DB.REC entity)
        {
            _ = context.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        internal int AddRange(List<Model.DB.REC> data)
        {
            context.RECs.AddRange(data);
            return context.SaveChanges();
        }
    }
}
