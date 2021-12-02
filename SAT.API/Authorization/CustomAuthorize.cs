using System.Linq;
using Microsoft.AspNetCore.Authorization;
using SAT.MODELS.Enums;

namespace SAT.API.Authorization
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public CustomAuthorize(params RoleEnum[] allowedProfiles)
        {
            AddAdmin(allowedProfiles);
            Roles = JoinEnum(allowedProfiles);
        }

        public CustomAuthorize(string allowedProfiles)
        {
            Roles = allowedProfiles;
        }

        public CustomAuthorize(string allowedProfiles, params RoleEnum[] allowedProfilesEnum)
        {
            AddAdmin(allowedProfilesEnum);
            Roles = string.Join(",", allowedProfiles, JoinEnum(allowedProfilesEnum));
        }

        private string JoinEnum(params RoleEnum[] allowedProfiles) =>
            string.Join(",", allowedProfiles.Select(r => ((int)r).ToString()));

        private RoleEnum[] AddAdmin(RoleEnum[] allowedProfiles) =>
            allowedProfiles.Append(RoleEnum.ADMIN).ToArray();
    }
}