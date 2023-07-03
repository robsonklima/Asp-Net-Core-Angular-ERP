using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ContratoServicoService : IContratoServicoService
    {
        private readonly IContratoServicoRepository _contratoServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ContratoServicoService(
            IContratoServicoRepository contratoServicoRepo,
            ISequenciaRepository sequenciaRepo
        ) 
        {
            _contratoServicoRepo = contratoServicoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(ContratoServico contratoServico)
        {
            _contratoServicoRepo.Atualizar(contratoServico);
        }

        public ContratoServico Criar(ContratoServico contratoServico)
        {
            contratoServico.CodContratoServico = _sequenciaRepo.ObterContador("ContratoServico");
            _contratoServicoRepo.Criar(contratoServico);
            return contratoServico;
        }

        public void Deletar(int codContratoServico)
        {
            _contratoServicoRepo.Deletar(codContratoServico);
        }

        public ContratoServico ObterPorCodigo(int codContratoServico)
        {
            return _contratoServicoRepo.ObterPorCodigo(codContratoServico);
        }

        public ListViewModel ObterPorParametros(ContratoServicoParameters parameters)
        {
            var contratoServicos = _contratoServicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = contratoServicos,
                TotalCount = contratoServicos.TotalCount,
                CurrentPage = contratoServicos.CurrentPage,
                PageSize = contratoServicos.PageSize,
                TotalPages = contratoServicos.TotalPages,
                HasNext = contratoServicos.HasNext,
                HasPrevious = contratoServicos.HasPrevious
            };

            return lista;
        }
    }
}
