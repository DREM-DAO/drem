using AutoMapper;
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
    /// <summary>
    /// 
    /// </summary>
    public class ImageMetaBusinessController
    {
        private readonly ImageMetaRepository repository;
        /// <summary>
        /// Constructor business ImageMeta controller
        /// </summary>
        /// <param name="repository"></param>
        public ImageMetaBusinessController(ImageMetaRepository repository)
        {
            this.repository = repository;
        }

        internal Task<Model.DB.ImageMeta> CreateAsync(Model.Comm.ImageMetaBase ImageMeta)
        {
            return repository.AddAsync(ImageMeta);
        }

        internal Task<Model.DB.ImageMeta> UpdateAsync(ImageMetaWithId ImageMeta)
        {
            return repository.UpdateAsync(ImageMeta);
        }

        internal Task<int> DeleteAsync(string[] id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.ImageMeta> ListAllForProject(string projectId)
        {
            return repository.ListAllForProject(projectId);
        }
    }
}
