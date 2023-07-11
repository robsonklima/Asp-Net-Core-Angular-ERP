using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(
        string claimType, string claimValue
    ) : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}

public class ClaimRequirementFilter : IAuthorizationFilter
{
    readonly Claim _claim;

    public ClaimRequirementFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var claim = _claim.Value;
        var type = _claim.Type;
        var codUsuario = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value!;
        var url = context.HttpContext.Request.Path;

        // Aqui vc verifica na sua configuracao se o cara deve ler ou editar

        context.Result = new ForbidResult();
    }
}