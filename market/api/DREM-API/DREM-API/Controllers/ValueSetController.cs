using AutoMapper;
using DREM_API.BusinessController;
using DREM_API.Extensions;
using DREM_API.Model;
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
    /// This controller returns version of the current api
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ValueSetController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ValueSetBusinessController valueSetBusinessController;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="visitorRepository"></param>
        public ValueSetController(
            IConfiguration configuration,
            ValueSetBusinessController valueSetBusinessController
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.valueSetBusinessController = valueSetBusinessController;
        }
        /// <summary>
        /// Register real estate company
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Set")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ValueSet>> Set([FromBody] Model.Comm.ValueSetBase valueSet)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await valueSetBusinessController.SetAsync(valueSet.ValueSetCode, valueSet.ItemCode, valueSet.ItemValue));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Removes value set
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteSet/{valueSetCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> DeleteSet([FromRoute] string valueSetCode)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await valueSetBusinessController.DeleteSetAsync(valueSetCode));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Removes value set item
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteItem/{valueSetCode}/{itemCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> DeleteItem([FromRoute] string valueSetCode, [FromRoute] string itemCode)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await valueSetBusinessController.DeleteItemAsync(valueSetCode, itemCode));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Get specific value set in dictionary form key->Text
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get/{valueSetCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Dictionary<string, string>>> GetByValueSetCode([FromRoute] string valueSetCode)
        {
            try
            {
                return Ok(valueSetBusinessController.Get(valueSetCode));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Get specific value set in dictionary form key->Text
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Dictionary<string, string>>> List()
        {
            try
            {
                return Ok(valueSetBusinessController.List());
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
