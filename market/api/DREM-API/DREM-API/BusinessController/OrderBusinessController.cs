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
        private readonly Mapper mapper;
        /// <summary>
        /// Constructor business Order controller
        /// </summary>
        /// <param name="repository"></param>
        public OrderBusinessController(OrderRepository repository)
        {
            this.repository = repository;
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<OrderBase, OrderWithId>();
            }));
        }

        internal Task<Model.DB.Order> CreateAsync(Model.Comm.OrderBase Order)
        {
            return repository.AddAsync(Order);
        }

        internal Task<Model.DB.Order> UpdateAsync(string id, OrderBase item)
        {
            var withId = mapper.Map<OrderWithId>(item);
            withId.Id = id;
            return repository.UpdateAsync(withId);
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
