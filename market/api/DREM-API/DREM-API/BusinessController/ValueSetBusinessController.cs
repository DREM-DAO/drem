using DREM_API.Model;
using DREM_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    public class ValueSetBusinessController
    {
        private readonly ValueSetRepository repository;
        public ValueSetBusinessController(ValueSetRepository repository)
        {
            this.repository = repository;
        }

        internal Task<ValueSet> SetAsync(string valueSetCode, string itemCode, string itemValue)
        {
            return repository.SetAsync(valueSetCode, itemCode, itemValue);
        }
    }
}
