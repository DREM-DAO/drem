﻿using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorandAuthentication
{
    public static class Extensions
    {
        public static AuthenticationBuilder AddAlgorand(this AuthenticationBuilder builder)
            => builder.AddAlgorand(AlgorandAuthenticationHandler.ID, _ => { });
        public static AuthenticationBuilder AddAlgorand(this AuthenticationBuilder builder, string authenticationScheme, Action<AlgorandAuthenticationOptions> configureOptions)
            => builder.AddScheme<AlgorandAuthenticationOptions, AlgorandAuthenticationHandler>(authenticationScheme, authenticationScheme, configureOptions);
    }
}
