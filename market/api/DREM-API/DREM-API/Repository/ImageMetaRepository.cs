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
    public class ImageMetaRepository
    {
        private readonly Model.ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public ImageMetaRepository(Model.ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.ImageMetaBase, Model.DB.ImageMeta>();
                cnf.CreateMap<Model.Comm.ImageMetaWithId, Model.DB.ImageMeta>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.ImageMeta>> GetAllAsync()
        {
            return await context.ImageMetas.ToListAsync();
        }

        /// <summary>
        /// List image meta for specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        internal IQueryable<Model.DB.ImageMeta> ListAllForProject(string projectId)
        {
            return context.ImageMetas.Where(p => p.ProjectId == projectId).OrderByDescending(p => p.Created);
        }
        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.ImageMeta> AddAsync(Model.Comm.ImageMetaBase entity)
        {

            var ret = mapper.Map<Model.DB.ImageMeta>(entity);
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
        public async Task<Model.DB.ImageMeta> UpdateAsync(Model.Comm.ImageMetaWithId entity)
        {
            var old = await context.FindAsync<Model.DB.ImageMeta>(entity.Id);
            var updated = mapper.Map<Model.Comm.ImageMetaWithId, Model.DB.ImageMeta>(entity, old);
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
            var toRemove = context.ImageMetas.Where(i => set.Contains(i.Id));
            context.RemoveRange(toRemove);
            return await context.SaveChangesAsync();
        }
        internal int AddRange(List<Model.DB.ImageMeta> data)
        {
            context.ImageMetas.AddRange(data);
            return context.SaveChanges();
        }
    }
}
