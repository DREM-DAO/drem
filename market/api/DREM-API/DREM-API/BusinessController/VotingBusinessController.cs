using AutoMapper;
using DREM_API.Model;
using DREM_API.Model.Comm;
using DREM_API.Model.DB;
using DREM_API.Repository;
using System.Linq;

namespace DREM_API.BusinessController
{
    /// <summary>
    /// 
    /// </summary>
    public class VotingBusinessController
    {
        private readonly VotingQuestionRepository votingQuestionRepository;
        private readonly VotingResultRepository votingResultRepository;
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
        }

        internal IQueryable<Model.DB.VotingQuestion> ListAllForProject(string questionerAccount)
        {
            return votingQuestionRepository.ListAllForQuestioner(questionerAccount);
        }
        internal Model.DB.VotingResult Get(string questionTxId)
        {
            return votingResultRepository.GetResultForQuestion(questionTxId);
        }
    }
}
