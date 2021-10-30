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
    public class BufferTransferController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly BufferTransferBusinessController bufferTransferBusinessController;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="bufferTransferBusinessController"></param>
        public BufferTransferController(
            IConfiguration configuration,
            BufferTransferBusinessController bufferTransferBusinessController
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.bufferTransferBusinessController = bufferTransferBusinessController;
        }
        /// <summary>
        /// Create BufferTransfer
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.BufferTransfer>> Create([FromBody] Model.Comm.BufferTransferBase item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await bufferTransferBusinessController.CreateAsync(item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Create BufferTransfer
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Update/{bufferTransferId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.BufferTransfer>> Update([FromRoute] string bufferTransferId, [FromBody] Model.Comm.BufferTransferBase item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await bufferTransferBusinessController.UpdateAsync(bufferTransferId, item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Delete BufferTransfer
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("Delete/{bufferTransferId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Delete([FromRoute] string bufferTransferId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await bufferTransferBusinessController.DeleteAsync(new string[] { bufferTransferId }));
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
        [HttpGet("ListAllForProject/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Model.DB.BufferTransfer>> ListAllForProject([FromRoute] string projectId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(bufferTransferBusinessController.ListAllForProject(projectId));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
