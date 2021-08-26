using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class GrupoEquipamentoService : IGrupoEquipamentoService
    {
        private readonly IGrupoEquipamentoRepository _grupoEquipamentoRepo;

        public GrupoEquipamentoService(IGrupoEquipamentoRepository grupoEquipamentoRepo)
        {
            _grupoEquipamentoRepo = grupoEquipamentoRepo;
        }

        public ListViewModel ObterPorParametros(GrupoEquipamentoParameters parameters)
        {
            var grupoEquipamentos = _grupoEquipamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = grupoEquipamentos,
                TotalCount = grupoEquipamentos.TotalCount,
                CurrentPage = grupoEquipamentos.CurrentPage,
                PageSize = grupoEquipamentos.PageSize,
                TotalPages = grupoEquipamentos.TotalPages,
                HasNext = grupoEquipamentos.HasNext,
                HasPrevious = grupoEquipamentos.HasPrevious
            };

            return lista;
        }

        public GrupoEquipamento Criar(GrupoEquipamento grupoEquipamento)
        {
            _grupoEquipamentoRepo.Criar(grupoEquipamento);
            return grupoEquipamento;
        }

        public void Deletar(int codigo)
        {
            _grupoEquipamentoRepo.Deletar(codigo);
        }

        public void Atualizar(GrupoEquipamento grupoEquipamento)
        {
            _grupoEquipamentoRepo.Atualizar(grupoEquipamento);
        }

        public GrupoEquipamento ObterPorCodigo(int codigo)
        {
            return _grupoEquipamentoRepo.ObterPorCodigo(codigo);
        }
    }
}
