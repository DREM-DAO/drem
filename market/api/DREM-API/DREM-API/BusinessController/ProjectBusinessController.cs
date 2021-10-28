using DREM_API.Model;
using DREM_API.Model.Comm;
using DREM_API.Model.DB;
using DREM_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    public class ProjectBusinessController
    {
        private readonly ProjectRepository repository;
        public ProjectBusinessController(ProjectRepository repository)
        {
            this.repository = repository;
        }

        internal Task<Model.DB.Project> CreateAsync(Model.Comm.ProjectBase project)
        {
            return repository.CreateAsync(project);
        }

        internal Task<Model.DB.Project> UpdateAsync(ProjectWithId project)
        {
            return repository.UpdateAsync(project);
        }

        internal Task<int> DeleteAsync(string id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.Project> ListAll()
        {
            return repository.ListAll();
        }
        internal IQueryable<Model.DB.Project> ListAllPublic()
        {
            return repository.ListAllPublic();
        }
    }
}
