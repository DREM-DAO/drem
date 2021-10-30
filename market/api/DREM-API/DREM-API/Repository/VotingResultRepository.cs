using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Repository
{
    /// <summary>
    /// Real estate companies EF repository
    /// </summary>
    public class VotingResultRepository
    {
        private readonly Model.ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public VotingResultRepository(Model.ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.Voting.VotingResultBase, Model.DB.VotingResult>();
                cnf.CreateMap<Model.Comm.Voting.VotingResultWithId, Model.DB.VotingResult>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.VotingResult>> GetAllAsync()
        {
            return await context.VotingResults.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.VotingResult> AddAsync(Model.Comm.Voting.VotingResultBase entity)
        {

            var ret = mapper.Map<Model.DB.VotingResult>(entity);
            ret.Id = Guid.NewGuid().ToString();
            ret.Created = DateTimeOffset.UtcNow;
            ret.Updated = DateTimeOffset.UtcNow;
            var result = context.Add(ret);
            await context.SaveChangesAsync();
            return ret;
        }
        /// <summary>
        /// List all transactions for asset
        /// </summary>
        /// <param name="questionTxId">Tx id where question has been asked</param>
        /// <returns></returns>
        internal Model.DB.VotingResult  GetResultForQuestion(string questionTxId)
        {
            return context.VotingResults.Where(p => p.QuestionTxId == questionTxId).OrderByDescending(p => p.Created).FirstOrDefault();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.VotingResult> UpdateAsync(Model.Comm.Voting.VotingResultWithId entity)
        {
            var old = await context.FindAsync<Model.DB.VotingResult>(entity.Id);
            var updated = mapper.Map<Model.Comm.Voting.VotingResultWithId, Model.DB.VotingResult>(entity, old);
            var result = context.Update(updated);
            await context.SaveChangesAsync();
            return updated;
        }
        /// <summary>
        /// Delete items from DB.
        /// </summary>
        /// <param name="ids">Entity, that will be deleted.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<int> DeleteAsync(string[] ids)
        {
            if (ids == null || ids.Length == 0) return 0;
            var set = new HashSet<string>(ids);
            var toRemove = context.VotingResults.Where(i => set.Contains(i.Id));
            context.RemoveRange(toRemove);
            return await context.SaveChangesAsync();
        }
        internal int AddRange(List<Model.DB.VotingResult> data)
        {
            context.VotingResults.AddRange(data);
            return context.SaveChanges();
        }
    }
}
