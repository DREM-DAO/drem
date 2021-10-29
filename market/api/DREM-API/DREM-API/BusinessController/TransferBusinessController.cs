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
    public class TransferBusinessController
    {
        private readonly TransferRepository repository;
        /// <summary>
        /// Constructor business Transfer controller
        /// </summary>
        /// <param name="repository"></param>
        public TransferBusinessController(TransferRepository repository)
        {
            this.repository = repository;
        }

        internal Task<Model.DB.Transfer> CreateAsync(Model.Comm.TransferBase Transfer)
        {
            return repository.AddAsync(Transfer);
        }

        internal Task<Model.DB.Transfer> UpdateAsync(TransferWithId Transfer)
        {
            return repository.UpdateAsync(Transfer);
        }

        internal Task<int> DeleteAsync(string[] id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.Transfer> ListAllForAsset(ulong asaId)
        {
            return repository.ListAllForAsset(asaId);
        }
    }
}
