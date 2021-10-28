using AutoMapper;
using DREM_API.Model;
using DREM_API.Model.Comm;
using DREM_API.Model.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Repository
{
    /// <summary>
    /// project EF repository 
    /// </summary>
    public class ProjectRepository
    {
        private readonly ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// constructor for project repository
        /// </summary>
        /// <param name="context"></param>
        public ProjectRepository(ADBContext context)
        {
            this.context = context;
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.ProjectBase, Model.DB.Project>();
                cnf.CreateMap<Model.Comm.ProjectWithId, Model.DB.Project>();
            }));
        }

        internal async Task<Model.DB.Project> CreateAsync(Model.Comm.ProjectBase project)
        {
            var ret = mapper.Map<Model.DB.Project>(project);
            ret.Id = Guid.NewGuid().ToString();
            ret.Created = DateTimeOffset.UtcNow;
            ret.Updated = DateTimeOffset.UtcNow;
            var result = context.Add(ret);
            await context.SaveChangesAsync();
            return ret;
        }

        internal IQueryable<Model.DB.Project> ListAll()
        {
            return context.Projects;
        }

        internal IQueryable<Model.DB.Project> ListAllPublic()
        {
            return context.Projects.Where(p => p.ShowToPublic == true);
        }

        internal async Task<int> DeleteAsync(string id)
        {
            var toDelete = context.Projects.FirstOrDefault(vs => vs.Id == id);
            if (toDelete == null) throw new Exception("Project not found");
            var result = context.Remove(toDelete);
            return await context.SaveChangesAsync();
        }

        internal async Task<Model.DB.Project> UpdateAsync(ProjectWithId project)
        {
            var toUpdate = context.Projects.FirstOrDefault(vs => vs.Id == project.Id);
            if (toUpdate == null) throw new Exception("Project not found");
            var updated = mapper.Map<Model.Comm.ProjectWithId, Model.DB.Project>(project, toUpdate);
            updated.Updated = DateTimeOffset.UtcNow;
            var result = context.Update(updated);
            await context.SaveChangesAsync();
            return updated;
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.Project>> GetAllAsync()
        {
            return await context.Projects.ToListAsync();
        }

        internal int AddRange(List<Project> data)
        {
            context.Projects.AddRange(data);
            return context.SaveChanges();
        }
    }
}
