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
    public class BufferTransferBusinessController
    {
        private readonly BufferTransferRepository repository;
        private readonly Mapper mapper;
        /// <summary>
        /// Constructor business BufferTransfer controller
        /// </summary>
        /// <param name="repository"></param>
        public BufferTransferBusinessController(BufferTransferRepository repository)
        {
            this.repository = repository;
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<BufferTransferBase, BufferTransferWithId>();
            }));
        }

        internal Task<Model.DB.BufferTransfer> CreateAsync(Model.Comm.BufferTransferBase BufferTransfer)
        {
            return repository.AddAsync(BufferTransfer);
        }

        internal Task<Model.DB.BufferTransfer> UpdateAsync(string id, BufferTransferBase BufferTransfer)
        {
            var withId = mapper.Map<BufferTransferWithId>(BufferTransfer);
            withId.Id = id;
            return repository.UpdateAsync(withId);
        }

        internal Task<int> DeleteAsync(string[] id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.BufferTransfer> ListAllForProject(string projectId)
        {
            return repository.ListAllForProject(projectId);
        }
    }
}
