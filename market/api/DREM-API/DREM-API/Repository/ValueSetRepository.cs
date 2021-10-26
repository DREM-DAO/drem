using DREM_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Repository
{
    public class ValueSetRepository
    {
        private readonly ADBContext context;
        public ValueSetRepository(ADBContext context)
        {
            this.context = context;
        }

        internal async Task<ValueSet> SetAsync(string valueSetCode, string itemCode, string itemValue)
        {
            ValueSet ret;
            var lang = "en-US";
            var first = context.ValueSets.FirstOrDefault(vs => vs.ValueSetCode == valueSetCode && vs.ItemCode == itemCode && vs.Language == lang);
            if (first == null)
            {
                await context.ValueSets.AddAsync(ret = new ValueSet() { Id = Guid.NewGuid().ToString(), ItemCode = itemCode, ValueSetCode = valueSetCode, ItemValue = itemValue, Language = lang });
                await context.SaveChangesAsync();
            }
            else
            {
                first.ItemValue = itemValue;
                context.Update(ret = first);
                await context.SaveChangesAsync();
            }
            return ret;
        }
    }
}
