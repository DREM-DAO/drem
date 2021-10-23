using AutoMapper;
using DREM_API.BusinessController;
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
    /// This controller returns version of the current api
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
        /// <param name="visitorRepository"></param>
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
        public async Task<ActionResult<Model.RECWithId>> Register([FromBody] Model.REC rec)
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
        /// Lists all registered RECs
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Model.RECWithId>>> GetAll()
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
