using AutoMapper;
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
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="visitorRepository"></param>
        public RECController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        /// <summary>
        /// Returns version of the current api
        /// 
        /// For development purposes it returns version of assembly, for production purposes it returns string build by pipeline which contains project information, pipeline build version, assembly version, and build date
        /// </summary>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Model.RECWithId>> Register([FromBody] Model.REC rec)
        {
            try
            {
                MapperConfiguration _configuration = new MapperConfiguration(cnf =>
                {
                    cnf.CreateMap<Model.REC, Model.RECWithId>();
                });
                var mapper = new Mapper(_configuration);
                var ret = mapper.Map<Model.RECWithId>(rec);
                ret.Id = Guid.NewGuid().ToString();
                return Ok(ret);
            }
            catch (Exception exc)
            {
                return BadRequest(new ProblemDetails() { Detail = exc.Message + (exc.InnerException != null ? $";\n{exc.InnerException.Message}" : "") + "\n" + exc.StackTrace, Title = exc.Message, Type = exc.GetType().ToString() });
            }
        }
    }
}
