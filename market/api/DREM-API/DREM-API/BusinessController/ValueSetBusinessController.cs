using AutoMapper;
using DREM_API.Model;
using DREM_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    /// <summary>
    /// ValueSet Business Controller
    /// </summary>
    public class ValueSetBusinessController
    {
        private readonly ValueSetRepository repository;
        /// <summary>
        /// ValueSet Business Controller constructor
        /// </summary>
        /// <param name="repository"></param>
        public ValueSetBusinessController(ValueSetRepository repository)
        {
            this.repository = repository;
        }

        internal Task<ValueSet> SetAsync(string valueSetCode, string itemCode, string itemValue)
        {
            return repository.SetAsync(valueSetCode, itemCode, itemValue, "en-US");
        }

        internal Dictionary<string,string> Get(string valueSetCode)
        {
            var list = repository.Get(valueSetCode, "en-US");
            return list.ToDictionary(k=>k.ItemCode, k=>k.ItemValue);
        }

        internal IQueryable<string> List()
        {
            return repository.ListCodes();
        }

        internal Task<int> DeleteSetAsync(string valueSetCode)
        {
            return repository.DeleteSetAsync(valueSetCode);
        }

        internal Task<int> DeleteItemAsync(string valueSetCode, string itemCode)
        {
            return repository.DeleteItemAsync(valueSetCode, itemCode);
        }
    }
}
