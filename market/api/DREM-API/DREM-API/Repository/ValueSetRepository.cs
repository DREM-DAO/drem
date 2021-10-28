using DREM_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Repository
{
    /// <summary>
    /// Value set EF repository
    /// </summary>
    public class ValueSetRepository
    {
        private readonly ADBContext context;
        /// <summary>
        /// value set repo constructor
        /// </summary>
        /// <param name="context"></param>
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

        internal Dictionary<string, Dictionary<string, string>> ListAll(string language)
        {
            var ret = new Dictionary<string, Dictionary<string, string>>();
            foreach (var item in context.ValueSets.Where(vs => vs.Language == language))
            {
                if (!ret.ContainsKey(item.ValueSetCode)) ret[item.ValueSetCode] = new Dictionary<string, string>();
                ret[item.ValueSetCode][item.ItemCode] = item.ItemValue;
            }
            return ret;
        }

        internal async Task<int> DeleteSetAsync(string valueSetCode)
        {
            var toRemove = context.ValueSets.Where(i => i.ValueSetCode == valueSetCode).ToArray();
            context.ValueSets.RemoveRange(toRemove);
            return await context.SaveChangesAsync();
        }

        internal async Task<int> DeleteItemAsync(string valueSetCode, string itemCode)
        {
            var toRemove = context.ValueSets.Where(i => i.ValueSetCode == valueSetCode && i.ItemCode == itemCode).ToArray();
            context.ValueSets.RemoveRange(toRemove);
            return await context.SaveChangesAsync();
        }

        internal IQueryable<string> ListCodes()
        {
            return context.ValueSets.Select(i => i.ValueSetCode).Distinct();
        }

        internal IQueryable<ValueSet> Get(string valueSetCode, string language)
        {
            return context.ValueSets.Where(v => v.ValueSetCode == valueSetCode && v.Language == language);
        }

        internal int AddRange(List<ValueSet> data)
        {
            context.ValueSets.AddRange(data);
            return context.SaveChanges();
        }
    }
}
