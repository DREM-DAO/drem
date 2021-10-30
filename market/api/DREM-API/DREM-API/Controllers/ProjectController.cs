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
    /// This controller manages api methods for projects 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ProjectBusinessController projectBusinessController;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="projectBusinessController"></param>
        public ProjectController(
            IConfiguration configuration,
            ProjectBusinessController projectBusinessController
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.projectBusinessController = projectBusinessController;
        }
        /// <summary>
        /// Create project
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.Project>> Create([FromBody] Model.Comm.ProjectBase project)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await projectBusinessController.CreateAsync(project));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Update project
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.Project>> Update([FromBody] Model.Comm.ProjectWithId project)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await projectBusinessController.UpdateAsync(project));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Delete project
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Delete/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Delete([FromRoute] string id)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await projectBusinessController.DeleteAsync(id));
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
        [HttpGet("ListAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Model.DB.Project>> ListAll()
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(projectBusinessController.ListAll().Where(p => p != null));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// List all publicly visible projects
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Model.Comm.ProjectWithValueSets>> List()
        {
            try
            {
                return Ok(projectBusinessController.ListAllPublic().Where(p=>p != null));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// List all publicly visible projects
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDetail/{urlId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<Model.Comm.ProjectDetail> GetDetail([FromRoute] string urlId)
        {
            try
            {
                return Ok(projectBusinessController.GetDetailByUrlId(urlId));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
