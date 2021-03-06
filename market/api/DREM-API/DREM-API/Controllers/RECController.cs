using AutoMapper;
using DREM_API.BusinessController;
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
    /// This controller manages api methods for recs
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RECController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly RECBusinessController recBusinessController;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="recBusinessController"></param>
        public RECController(
            IConfiguration configuration,
            RECBusinessController recBusinessController
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.recBusinessController = recBusinessController;
        }
        /// <summary>
        /// Register real estate company
        /// </summary>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.REC>> Register([FromBody] Model.DB.REC rec)
        {
            try
            {
                return Ok(await recBusinessController.Register(rec));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Register real estate company
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateOpportunity")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.Opportunity>> CreateOpportunity([FromBody] Model.Comm.OpportunityBase opportunity)
        {
            try
            {
                return Ok(await recBusinessController.CreateOpportunity(opportunity));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// List All Opportunities registered from rec
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListAllOpportunities")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.Opportunity>> ListAllOpportunities()
        {
            try
            {
                return Ok(await recBusinessController.GetAllOpportunitiesAsync());
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Lists all registered RECs
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Model.DB.REC>>> GetAll()
        {
            try
            {
                return Ok(await recBusinessController.GetAll());
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Lists all registered RECs
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAllAuth")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Model.DB.REC>>> GetAll2()
        {
            try
            {
                return Ok(await recBusinessController.GetAll());
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
