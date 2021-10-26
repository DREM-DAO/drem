using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DREM_API.Extensions
{
    /// <summary>
    /// User extensions
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Returns true if user is in admin list
        /// </summary>
        /// <param name="user"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static bool IsAdmin(this ClaimsPrincipal user, IConfiguration configuration)
        {
            if (user == null || configuration == null) return false;
            return string.IsNullOrEmpty(configuration.GetSection("admins").AsEnumerable().FirstOrDefault(i => i.Value == user.Identity.Name).Value);
        }
    }
}
