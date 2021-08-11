using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioAtendimentoDetalheController : ControllerBase
    {
        private readonly IRelatorioAtendimentoDetalheRepository _relatorioAtendimentoDetalheInterface;
        private readonly IRelatorioAtendimentoDetalhePecaRepository _relatorioAtendimentoDetalhePecaRepository;

        public RelatorioAtendimentoDetalheController(
            IRelatorioAtendimentoDetalheRepository relatorioAtendimentoDetalheInterface,
            IRelatorioAtendimentoDetalhePecaRepository relatorioAtendimentoDetalhePecaRepository
        )
        {
            _relatorioAtendimentoDetalheInterface = relatorioAtendimentoDetalheInterface;
            _relatorioAtendimentoDetalhePecaRepository = relatorioAtendimentoDetalhePecaRepository;
        }

        [HttpDelete("{codRATDetalhe}")]
        public void Delete(int codRATDetalhe)
        {
            var detalhe = _relatorioAtendimentoDetalheInterface.ObterPorCodigo(codRATDetalhe);

            if (detalhe.RelatorioAtendimentoDetalhePecas != null)
            {
                foreach (var detalhePeca in detalhe.RelatorioAtendimentoDetalhePecas)
                {
                    _relatorioAtendimentoDetalhePecaRepository.Deletar(detalhePeca.CodRATDetalhePeca);
                }
            }

            _relatorioAtendimentoDetalheInterface.Deletar(codRATDetalhe);
        }
    }
}
