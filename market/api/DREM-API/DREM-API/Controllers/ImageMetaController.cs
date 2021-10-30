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
    /// This controller manages api methods for image processor
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ImageMetaController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ImageMetaBusinessController ImageMetaBusinessController;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="ImageMetaBusinessController"></param>
        public ImageMetaController(
            IConfiguration configuration,
            ImageMetaBusinessController ImageMetaBusinessController
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.ImageMetaBusinessController = ImageMetaBusinessController;
        }
        /// <summary>
        /// Create ImageMeta
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.ImageMeta>> Create([FromBody] Model.Comm.ImageMetaBase item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await ImageMetaBusinessController.CreateAsync(item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Create ImageMeta
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Update/{imageId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.DB.ImageMeta>> Update([FromRoute] string imageId, [FromBody] Model.Comm.ImageMetaWithId item)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await ImageMetaBusinessController.UpdateAsync(imageId, item));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
        /// <summary>
        /// Delete ImageMeta
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("Delete/{imageId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Delete([FromRoute] string imageId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(await ImageMetaBusinessController.DeleteAsync(new string[] { imageId }));
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
        public ActionResult<IEnumerable<Model.DB.ImageMeta>> ListAllForProject([FromRoute] string projectId)
        {
            try
            {
                if (!User.IsAdmin(configuration)) throw new Exception("You are not admin");
                return Ok(ImageMetaBusinessController.ListAllForProject(projectId));
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
