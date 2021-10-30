using AutoMapper;
using DREM_API.BusinessController;
using DREM_API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Controllers
{
    /// <summary>
    /// This controller manages api methods for buffer Votings
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class VotingController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly VotingBusinessController VotingBusinessController;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="VotingBusinessController"></param>
        public VotingController(
            IConfiguration configuration,
            VotingBusinessController VotingBusinessController
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.VotingBusinessController = VotingBusinessController;
        }
        /// <summary>
        /// Create Voting Question
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("CreateVotingQuestion")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.VotingQuestion>> CreateVotingQuestion([FromBody] Model.Comm.Voting.VotingQuestionBase item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await VotingBusinessController.CreateVotingQuestionAsync(item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Create Voting Result
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("CreateVotingResult")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.VotingResult>> CreateVotingQuestion([FromBody] Model.Comm.Voting.VotingResultBase item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await VotingBusinessController.CreateVotingResultAsync(item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Update VotingQuestion
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateVotingQuestion/{votingQuestionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.VotingQuestion>> UpdateVotingQuestion([FromRoute] string votingQuestionId, [FromBody] Model.Comm.Voting.VotingQuestionWithId item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await VotingBusinessController.UpdateVotingQuestionAsync(votingQuestionId, item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Update VotingQuestion
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateVotingResult/{votingResultId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.VotingResult>> UpdateVotingResult([FromRoute] string votingResultId, [FromBody] Model.Comm.Voting.VotingResultWithId item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await VotingBusinessController.UpdateVotingResultAsync(votingResultId, item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }

        /// <summary>
        /// Delete Voting Question
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("DeleteVotingQuestion/{votingQuestionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> DeleteVotingQuestion([FromRoute] string votingQuestionId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await VotingBusinessController.DeleteVotingQuestionAsync(new string[] { votingQuestionId }));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }

        /// <summary>
        /// Delete Voting Result
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("DeleteVotingResult/{votingResultId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> DeleteVotingResult([FromRoute] string votingResultId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await VotingBusinessController.DeleteVotingResultAsync(new string[] { votingResultId }));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }

        /// <summary>
        /// List all projects for administration purposes
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ListAllForAsset/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Model.Comm.Voting.VotingBase>> ListAllForAsset([FromRoute] string projectId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(VotingBusinessController.ListAllForProject(projectId));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
