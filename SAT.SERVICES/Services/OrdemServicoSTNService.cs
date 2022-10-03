using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoSTNService : IOrdemServicoSTNService
    {
        private readonly IOrdemServicoSTNRepository _ordemServicoSTNRepo;
        private readonly IEquipamentoContratoService _equipamentoContratoService;

        public OrdemServicoSTNService(
            IOrdemServicoSTNRepository ordemServicoSTNRepo,
            IEquipamentoContratoService equipamentoContratoService
        )
        {
            _ordemServicoSTNRepo = ordemServicoSTNRepo;
            _equipamentoContratoService = equipamentoContratoService;
        }

        public ListViewModel ObterPorParametros(OrdemServicoSTNParameters parameters)
        {
            var ordens = _ordemServicoSTNRepo.ObterPorParametros(parameters);

            for (int i = 0; i < ordens.Count; i++)
            {
                try {
                    ordens[i].OrdemServico.EquipamentoContrato.Mtbf = _equipamentoContratoService.CalcularMTBF((int)ordens[i]?.OrdemServico?.CodEquipContrato,
                        (DateTime)ordens[i]?.OrdemServico?.EquipamentoContrato?.DataAtivacao, DateTime.Now);
                }
                catch {

                }
            }

            var lista = new ListViewModel
            {
                Items = ordens,
                TotalCount = ordens.TotalCount,
                CurrentPage = ordens.CurrentPage,
                PageSize = ordens.PageSize,
                TotalPages = ordens.TotalPages,
                HasNext = ordens.HasNext,
                HasPrevious = ordens.HasPrevious
            };

            return lista;
        }

        public OrdemServicoSTN Criar(OrdemServicoSTN ordem)
        {
            return _ordemServicoSTNRepo.Criar(ordem);
        }

        public void Deletar(int codigo)
        {
            _ordemServicoSTNRepo.Deletar(codigo);
        }

        public OrdemServicoSTN Atualizar(OrdemServicoSTN ordem)
        {
            _ordemServicoSTNRepo.Atualizar(ordem);

            return ordem;
        }

        public OrdemServicoSTN ObterPorCodigo(int codigo)
        {
            return _ordemServicoSTNRepo.ObterPorCodigo(codigo);
        }
    }
}
