using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SAT.MODELS.Enums;

namespace SAT.API.Authorization
{
    public static class ClaimsPrincipalExtensions
    {
        public static RoleEnum GetRole(this IEnumerable<Claim> principal)
        {
            var role = principal.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .FirstOrDefault();

            return (RoleEnum)Enum.Parse(typeof(RoleEnum), role);
        }
    }
}