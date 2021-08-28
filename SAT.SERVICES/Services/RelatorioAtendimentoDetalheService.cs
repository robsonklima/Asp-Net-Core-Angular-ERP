using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoDetalheService : IRelatorioAtendimentoDetalheService
    {
        private readonly IRelatorioAtendimentoDetalheRepository _detalheRepo;
        private readonly ISequenciaRepository _seqRepo;

        public RelatorioAtendimentoDetalheService(IRelatorioAtendimentoDetalheRepository detalheRepo, ISequenciaRepository seqRepo)
        {
            _detalheRepo = detalheRepo;
            _seqRepo = seqRepo;
        }

        public RelatorioAtendimentoDetalhe Criar(RelatorioAtendimentoDetalhe detalhe)
        {
            detalhe.CodRATDetalhe = _seqRepo.ObterContador("RATDetalhes");
            detalhe.Acao = null;
            detalhe.Defeito = null;
            detalhe.TipoServico = null;
            detalhe.Causa = null;
            detalhe.TipoCausa = null;
            detalhe.GrupoCausa = null;
            detalhe.RelatorioAtendimentoDetalhePecas = null;
            _detalheRepo.Criar(detalhe);
            return detalhe;
        }

        public void Deletar(int codigo)
        {
            _detalheRepo.Deletar(codigo);
        }

        public void Atualizar(RelatorioAtendimentoDetalhe detalhe)
        {
            _detalheRepo.Atualizar(detalhe);
        }

        public RelatorioAtendimentoDetalhe ObterPorCodigo(int codigo)
        {
            return _detalheRepo.ObterPorCodigo(codigo);
        }
    }
}
