using System.Linq;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities;

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
    private readonly IRecursoBloqueadoService _recursoBloqueadoService;

    public ClaimRequirementFilter(
        Claim claim,
        IUsuarioService usuarioService,
        IRecursoBloqueadoService recursoBloqueadoService
    )
    {
        _claim = claim;
        _usuarioService = usuarioService;
        _recursoBloqueadoService = recursoBloqueadoService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var claim = _claim.Value;
        var type = _claim.Type;
        var codUsuario = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value!;
        var usuario = _usuarioService.ObterPorCodigo(codUsuario);
        var url = context.HttpContext.Request.Path.ToString().Replace("api/", "");
        var recursoBloqueado = (RecursoBloqueado)_recursoBloqueadoService
            .ObterPorParametros(new RecursoBloqueadoParameters
            {
                CodPerfil = usuario.CodPerfil,
                CodSetor = usuario.CodSetor,
                Url = url,
                Claims = claim
            })
            .Items
            .FirstOrDefault()!;

        if (recursoBloqueado is not null)
            context.Result = new ForbidResult();
    }
}