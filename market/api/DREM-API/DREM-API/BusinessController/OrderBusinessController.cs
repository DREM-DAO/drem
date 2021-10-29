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
    /// OrderBusinessController
    /// </summary>
    public class OrderBusinessController
    {
        private readonly OrderRepository repository;
        /// <summary>
        /// Constructor business Order controller
        /// </summary>
        /// <param name="repository"></param>
        public OrderBusinessController(OrderRepository repository)
        {
            this.repository = repository;
        }

        internal Task<Model.DB.Order> CreateAsync(Model.Comm.OrderBase Order)
        {
            return repository.AddAsync(Order);
        }

        internal Task<Model.DB.Order> UpdateAsync(OrderWithId Order)
        {
            return repository.UpdateAsync(Order);
        }

        internal Task<int> DeleteAsync(string[] id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.Order> ListAllBidsForProject(ulong asaId)
        {
            return repository.ListAllBidsForProject(asaId);
        }
        internal IQueryable<Model.DB.Order> ListAllOffersForProject(ulong asaId)
        {
            return repository.ListAllOffersForProject(asaId);
        }
    }
}
