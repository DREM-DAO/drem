using AutoMapper;
using DREM_API.Model;
using DREM_API.Model.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Repository
{
    public class ProjectRepository
    {
        private readonly ADBContext _context;
        private readonly Mapper mapper;
        public ProjectRepository(ADBContext context)
        {
            this._context = context;
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.ProjectBase, Model.DB.Project>();
            }));
        }

        internal async Task<Model.DB.Project> Create(Model.Comm.ProjectBase project)
        {
            var ret = mapper.Map<Model.DB.Project>(project);
            ret.Id = Guid.NewGuid().ToString();
            ret.Created = DateTimeOffset.UtcNow;
            ret.Updated = DateTimeOffset.UtcNow;
            var result = _context.Add(ret);
            await _context.SaveChangesAsync();
            return ret;
        }

        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

    }
}
