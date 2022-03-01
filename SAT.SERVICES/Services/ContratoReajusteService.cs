using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ContratoReajusteService : IContratoReajusteService
    {
        private readonly IContratoReajusteRepository _contratoReajusteRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ContratoReajusteService(IContratoReajusteRepository contratoReajusteRepo, ISequenciaRepository sequenciaRepo)
        {
            _contratoReajusteRepo = contratoReajusteRepo;
            _sequenciaRepo = sequenciaRepo;

        }

        public ListViewModel ObterPorParametros(ContratoReajusteParameters parameters)
        {
            var tiposCausa = _contratoReajusteRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tiposCausa,
                TotalCount = tiposCausa.TotalCount,
                CurrentPage = tiposCausa.CurrentPage,
                PageSize = tiposCausa.PageSize,
                TotalPages = tiposCausa.TotalPages,
                HasNext = tiposCausa.HasNext,
                HasPrevious = tiposCausa.HasPrevious
            };

            return lista;
        }

        public ContratoReajuste Criar(ContratoReajuste contratoReajuste)
        {
            contratoReajuste.CodContratoReajuste = _sequenciaRepo.ObterContador("ContratoReajuste");

            _contratoReajusteRepo.Criar(contratoReajuste);
            return contratoReajuste;
        }

        public void Deletar(int codigo)
        {
            _contratoReajusteRepo.Deletar(codigo);
        }

        public void Atualizar(ContratoReajuste tipoContrato)
        {
            _contratoReajusteRepo.Atualizar(tipoContrato);
        }

        public ContratoReajuste ObterPorCodigo(int codigo)
        {
            return _contratoReajusteRepo.ObterPorCodigo(codigo);
        }

    }
}
