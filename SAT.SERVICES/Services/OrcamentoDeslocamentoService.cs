using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoDeslocamentoService : IOrcamentoDeslocamentoService
    {
        private readonly IOrcamentoDeslocamentoRepository _orcDeslocamentoRepo;

        public OrcamentoDeslocamentoService(IOrcamentoDeslocamentoRepository orcDeslocamentoRepo)
        {
            _orcDeslocamentoRepo = orcDeslocamentoRepo;
        }

        public OrcamentoDeslocamento Criar(OrcamentoDeslocamento deslocamento)
        {
            _orcDeslocamentoRepo.Criar(deslocamento);
            return deslocamento;
        }
    }
}