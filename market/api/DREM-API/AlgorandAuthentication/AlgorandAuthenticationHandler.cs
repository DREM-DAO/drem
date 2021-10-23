using Algorand;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ISystemClock = Microsoft.AspNetCore.Authentication.ISystemClock;

namespace AlgorandAuthentication
{
    /// <summary>
    /// This class handles internal k8s authentication and allows services to communicate between each other in the simplest possible secure way.
    /// 
    /// This relies on fact that whole K8S network is safe and potential hacker is not capable of doing remote code execution.
    /// </summary>
    public class AlgorandAuthenticationHandler : AuthenticationHandler<AlgorandAuthenticationOptions>, IAuthenticationRequestHandler, IAuthenticationHandler
    {
        public const string ID = "AlgorandAuthentication";
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        public AlgorandAuthenticationHandler(
            IOptionsMonitor<AlgorandAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        public async Task<bool> HandleRequestAsync()
        {
            return false;
        }

        /// <summary>
        /// Add this code to end of configure:
        /// services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, K8SAuthenticationHandler>("BasicAuthentication", null);
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.NoResult();
            //var tx = Convert.FromBase64String(Request.Headers["Authorization"].ToString());
            //var tr = Algorand.Encoder.DecodeFromMsgPack<SignedTransaction>(tx);
            //var user = tr.tx.sender.ToString();
            var user = "1234567890";
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier,user),
                new Claim(ClaimTypes.Name,user.Substring(0,8)),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}