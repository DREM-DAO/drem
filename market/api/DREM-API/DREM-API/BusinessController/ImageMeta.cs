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
        private readonly Mapper mapper;
        /// <summary>
        /// Constructor business ImageMeta controller
        /// </summary>
        /// <param name="repository"></param>
        public ImageMetaBusinessController(ImageMetaRepository repository)
        {
            this.repository = repository;
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<ImageMetaBase, ImageMetaWithId>();
            }));
        }

        internal Task<Model.DB.ImageMeta> CreateAsync(Model.Comm.ImageMetaBase ImageMeta)
        {
            return repository.AddAsync(ImageMeta);
        }

        internal Task<Model.DB.ImageMeta> UpdateAsync(string id, ImageMetaBase item)
        {
            var withId = mapper.Map<ImageMetaWithId>(item);
            withId.Id = id;
            return repository.UpdateAsync(withId);
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
