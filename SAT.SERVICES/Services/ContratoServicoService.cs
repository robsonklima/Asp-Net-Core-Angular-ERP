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

        public ContratoServicoService(IContratoServicoRepository contratoServicoRepo)
        {
            _contratoServicoRepo = contratoServicoRepo;
        }

        public void Atualizar(ContratoServico contratoServico)
        {
            _contratoServicoRepo.Atualizar(contratoServico);
        }

        public void Criar(ContratoServico contratoServico)
        {
            _contratoServicoRepo.Criar(contratoServico);
        }

        public void Deletar(int codContrato, int codContratoServico)
        {
            _contratoServicoRepo.Deletar(codContrato,codContratoServico);
        }

        public ContratoServico ObterPorCodigo(int codContrato, int codContratoServico)
        {
            return _contratoServicoRepo.ObterPorCodigo(codContrato, codContratoServico);
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
