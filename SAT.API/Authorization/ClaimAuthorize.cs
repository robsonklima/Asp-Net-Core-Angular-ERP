using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAT.SERVICES.Interfaces;

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}

public class ClaimRequirementFilter : IAuthorizationFilter
{
    private readonly Claim _claim;
    private readonly IUsuarioService _usuarioService;

    public ClaimRequirementFilter(Claim claim, IUsuarioService usuarioService)
    {
        _claim = claim;
        _usuarioService = usuarioService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var claim = _claim.Value;
        var type = _claim.Type;
        var codUsuario = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value!;

        if (string.IsNullOrWhiteSpace(codUsuario))
            context.Result = new ForbidResult();

        var usuario = _usuarioService.ObterPorCodigo(codUsuario);
        var url = context.HttpContext.Request.Path;

        switch (claim)
        {
            case "CanReadResource":
                context.Result = new ForbidResult();

                break;
            case "CanAddResource":
                context.Result = new ForbidResult();

                break;
            case "CanEditResource":
                context.Result = new ForbidResult();

                break;
            case "CanDeleteResource":

                context.Result = new ForbidResult();
                break;
            default:
                context.Result = new ForbidResult();

                break;
        }
    }
}