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

        internal async Task<ValueSet> SetAsync(string valueSetCode, string itemCode, string itemValue, string language)
        {
            ValueSet ret;
            var first = context.ValueSets.FirstOrDefault(vs => vs.ValueSetCode == valueSetCode && vs.ItemCode == itemCode && vs.Language == language);
            if (first == null)
            {
                await context.ValueSets.AddAsync(ret = new ValueSet() { Id = Guid.NewGuid().ToString(), ItemCode = itemCode, ValueSetCode = valueSetCode, ItemValue = itemValue, Language = language });
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

        internal IQueryable<string> ListCodes()
        {
            return context.ValueSets.Select(i => i.ValueSetCode).Distinct();
        }

        internal IQueryable<ValueSet> Get(string valueSetCode, string language)
        {
            return context.ValueSets.Where(v => v.ValueSetCode == valueSetCode && v.Language == language);
        }
    }
}
