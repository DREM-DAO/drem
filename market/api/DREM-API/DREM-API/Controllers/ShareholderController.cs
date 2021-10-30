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
    /// This controller manages api methods for buffer transfers
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShareholderController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ShareholderBusinessController ShareholderBusinessController;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="ShareholderBusinessController"></param>
        public ShareholderController(
            IConfiguration configuration,
            ShareholderBusinessController ShareholderBusinessController
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.ShareholderBusinessController = ShareholderBusinessController;
        }
        /// <summary>
        /// Create Shareholder
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.Shareholder>> Create([FromBody] Model.Comm.ShareholderBase item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await ShareholderBusinessController.CreateAsync(item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Create Shareholder
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Update/{shareholderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.Shareholder>> Update([FromRoute] string shareholderId, [FromBody] Model.Comm.ShareholderBase item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await ShareholderBusinessController.UpdateAsync(shareholderId, item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Delete Shareholder
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("Delete/{shareholderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Delete([FromRoute] string shareholderId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await ShareholderBusinessController.DeleteAsync(new string[] { shareholderId }));
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
        [HttpGet("ListAllForAsset/{asaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Model.DB.Shareholder>> ListAllForAsset([FromRoute] ulong asaId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(ShareholderBusinessController.ListAllForAsset(asaId));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
