using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoDetalheService : IRelatorioAtendimentoDetalheService
    {
        private readonly IRelatorioAtendimentoDetalheRepository _detalheRepo;

        public RelatorioAtendimentoDetalheService(IRelatorioAtendimentoDetalheRepository detalheRepo)
        {
            _detalheRepo = detalheRepo;
        }

        public RelatorioAtendimentoDetalhe Criar(RelatorioAtendimentoDetalhe detalhe)
        {
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
