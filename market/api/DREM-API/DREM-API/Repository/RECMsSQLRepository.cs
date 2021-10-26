using AutoMapper;
using DREM_API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Repository
{
    public class RECMsSQLRepository
    {
        private readonly ADBContext _context;
        private readonly Mapper mapper;
        public RECMsSQLRepository(ADBContext context)
        {
            this._context = context;

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
            return await _context.RECs.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <typeparam name="T">Model from interest repository.</typeparam>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<RECWithId> AddAsync(Model.REC entity) 
        {

            var ret = mapper.Map<Model.RECWithId>(entity);
            ret.Id = Guid.NewGuid().ToString();
            ret.Created = DateTimeOffset.UtcNow;
            ret.Updated = DateTimeOffset.UtcNow;
            var result = _context.Add(ret);
            await _context.SaveChangesAsync();
            return ret;
        }
        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <typeparam name="T">Model from interest repository.</typeparam>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<RECWithId> UpdateAsync(Model.RECWithId entity)
        {
            var old = await _context.FindAsync<RECWithId>(entity.Id);
            entity.Created = old.Created;
            var result = _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <typeparam name="T">Model from interest repository.</typeparam>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<bool> DeleteAsync(Model.RECWithId entity)
        {
            _ = _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
