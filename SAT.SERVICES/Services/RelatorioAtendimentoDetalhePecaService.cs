using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoDetalhePecaService : IRelatorioAtendimentoDetalhePecaService
    {
        private IRelatorioAtendimentoDetalhePecaRepository _ratDetalhePecaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public RelatorioAtendimentoDetalhePecaService(
            IRelatorioAtendimentoDetalhePecaRepository ratDetalhePecaRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _ratDetalhePecaRepo = ratDetalhePecaRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public RelatorioAtendimentoDetalhePeca Criar(RelatorioAtendimentoDetalhePeca ratDetalhePeca)
        {
            ratDetalhePeca.CodRATDetalhePeca = _sequenciaRepo.ObterContador("RATDetalhesPecas");
            ratDetalhePeca.Peca = null;
            _ratDetalhePecaRepo.Criar(ratDetalhePeca);
            return ratDetalhePeca;
        }

        public void Deletar(int codigo)
        {
            _ratDetalhePecaRepo.Deletar(codigo);
        }

        public void Atualizar(RelatorioAtendimentoDetalhePeca ratDetalhePeca)
        {
            //return _ratDetalhePecaRepo.(codigo);
        }

        public RelatorioAtendimentoDetalhePeca ObterPorCodigo(int codigo)
        {
            return _ratDetalhePecaRepo.ObterPorCodigo(codigo);
        }
    }
}
