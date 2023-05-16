using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EquipamentoPOSService : IEquipamentoPOSService
    {
        private readonly IEquipamentoPOSRepository _motivoRepo;

        public EquipamentoPOSService(IEquipamentoPOSRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(EquipamentoPOSParameters parameters)
        {
            var regioes = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public EquipamentoPOS Criar(EquipamentoPOS op)
        {
            _motivoRepo.Criar(op);

            return op;
        }

        public EquipamentoPOS Deletar(int codigo)
        {
            return _motivoRepo.Deletar(codigo);
        }

        public EquipamentoPOS Atualizar(EquipamentoPOS op)
        {
            return _motivoRepo.Atualizar(op);
        }

        public EquipamentoPOS ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
