using DREM_API.Model;
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
        public ProjectRepository(ADBContext context)
        {
            this._context = context;
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
