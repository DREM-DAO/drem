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
    /// This controller returns version of the current api
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public UserController(
            IConfiguration configuration
            )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        /// <summary>
        /// Returns authenticated user account
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Me")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<Model.Comm.UserInfo> Me()
        {
            try
            {
                return Ok(new Model.Comm.UserInfo { Address = User.Identity.Name, IsAdmin = User.IsAdmin(configuration), KYCVerified = false });
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
