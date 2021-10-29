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
    /// ShareholderBusinessController
    /// </summary>
    public class ShareholderBusinessController
    {
        private readonly ShareholderRepository repository;
        /// <summary>
        /// Constructor business Shareholder controller
        /// </summary>
        /// <param name="repository"></param>
        public ShareholderBusinessController(ShareholderRepository repository)
        {
            this.repository = repository;
        }

        internal Task<Model.DB.Shareholder> CreateAsync(Model.Comm.ShareholderBase Shareholder)
        {
            return repository.AddAsync(Shareholder);
        }

        internal Task<Model.DB.Shareholder> UpdateAsync(ShareholderWithId Shareholder)
        {
            return repository.UpdateAsync(Shareholder);
        }

        internal Task<int> DeleteAsync(string[] id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.Shareholder> ListAllForAsset(ulong asaId)
        {
            return repository.ListAllForAsset(asaId);
        }
    }
}
