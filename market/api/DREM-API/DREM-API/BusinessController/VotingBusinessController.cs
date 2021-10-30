using AutoMapper;
using DREM_API.Model;
using DREM_API.Model.Comm;
using DREM_API.Model.DB;
using DREM_API.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    /// <summary>
    /// 
    /// </summary>
    public class VotingBusinessController
    {
        private readonly VotingQuestionRepository votingQuestionRepository;
        private readonly VotingResultRepository votingResultRepository;
        private readonly Mapper mapper;
        /// <summary>
        /// Constructor business Voting controller
        /// </summary>
        /// <param name="votingQuestionRepository"></param>
        /// <param name="votingResultRepository"></param>
        public VotingBusinessController(
            VotingQuestionRepository votingQuestionRepository,
            VotingResultRepository votingResultRepository
            )
        {
            this.votingQuestionRepository = votingQuestionRepository;
            this.votingResultRepository = votingResultRepository;
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.Comm.Voting.VotingQuestionBase, Model.Comm.Voting.VotingQuestionWithId>();
                cnf.CreateMap<Model.Comm.Voting.VotingResultBase, Model.Comm.Voting.VotingResultWithId>();
            }));
        }

        internal IQueryable<Model.DB.VotingQuestion> ListAllForProject(string questionerAccount)
        {
            return votingQuestionRepository.ListAllForQuestioner(questionerAccount);
        }
        internal Model.DB.VotingResult Get(string questionTxId)
        {
            return votingResultRepository.GetResultForQuestion(questionTxId);
        }


        internal Task<Model.DB.VotingQuestion> CreateVotingQuestionAsync(Model.Comm.Voting.VotingQuestionBase item)
        {
            return votingQuestionRepository.AddAsync(item);
        }

        internal Task<Model.DB.VotingQuestion> UpdateVotingQuestionAsync(string id, Model.Comm.Voting.VotingQuestionBase item)
        {
            var withId = mapper.Map<Model.Comm.Voting.VotingQuestionWithId>(item);
            withId.Id = id;
            return votingQuestionRepository.UpdateAsync(withId);
        }

        internal Task<int> DeleteVotingQuestionAsync(string[] id)
        {
            return votingQuestionRepository.DeleteAsync(id);
        }
        internal Task<Model.DB.VotingResult> CreateVotingResultAsync(Model.Comm.Voting.VotingResultBase item)
        {
            return votingResultRepository.AddAsync(item);
        }

        internal Task<Model.DB.VotingResult> UpdateVotingResultAsync(string id, Model.Comm.Voting.VotingResultBase item)
        {
            var withId = mapper.Map<Model.Comm.Voting.VotingResultWithId>(item);
            withId.Id = id;
            return votingResultRepository.UpdateAsync(withId);
        }

        internal Task<int> DeleteVotingResultAsync(string[] id)
        {
            return votingResultRepository.DeleteAsync(id);
        }
    }
}
