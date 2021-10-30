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
    public class VotingQuestionRepository
    {
        private readonly Model.ADBContext context;
        private readonly Mapper mapper;
        /// <summary>
        /// rec repository constructor
        /// </summary>
        /// <param name="context"></param>
        public VotingQuestionRepository(Model.ADBContext context)
        {
            this.context = context;

            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.Voting.VotingQuestionBase, Model.DB.VotingQuestion>();
                cnf.CreateMap<Model.Comm.Voting.VotingQuestionWithId, Model.DB.VotingQuestion>();
            }));
        }
        /// <summary>
        /// Return all real estate companies
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Model.DB.VotingQuestion>> GetAllAsync()
        {
            return await context.VotingQuestions.ToListAsync();
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.VotingQuestion> AddAsync(Model.Comm.Voting.VotingQuestionBase entity)
        {

            var ret = mapper.Map<Model.DB.VotingQuestion>(entity);
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
        /// <param name="questionerAccount">Account of asa creator is allowed to ask questions</param>
        /// <returns></returns>
        internal IQueryable<Model.DB.VotingQuestion> ListAllForQuestioner(string questionerAccount)
        {
            return context.VotingQuestions.Where(p => p.QuestionerAccount == questionerAccount).OrderByDescending(p => p.Created);
        }

        /// <summary>
        /// Adds record to DB.
        /// </summary>
        /// <param name="entity">Entity, that will be added.</param>
        /// <returns>Id of newly created record.</returns>
        public async Task<Model.DB.VotingQuestion> UpdateAsync(Model.Comm.Voting.VotingQuestionWithId entity)
        {
            var old = await context.FindAsync<Model.DB.VotingQuestion>(entity.Id);
            var updated = mapper.Map<Model.Comm.Voting.VotingQuestionWithId, Model.DB.VotingQuestion>(entity, old);
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
            var toRemove = context.VotingQuestions.Where(i => set.Contains(i.Id)).ToArray();
            _ = context.Remove(toRemove);
            return await context.SaveChangesAsync();
        }
        internal int AddRange(List<Model.DB.VotingQuestion> data)
        {
            context.VotingQuestions.AddRange(data);
            return context.SaveChanges();
        }
    }
}
