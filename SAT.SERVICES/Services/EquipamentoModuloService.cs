using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EquipamentoModuloService : IEquipamentoModuloService
    {
        private readonly IEquipamentoModuloRepository _equipamentoModuloRepo;

        public EquipamentoModuloService(IEquipamentoModuloRepository equipamentoModuloRepo)
        {
            _equipamentoModuloRepo = equipamentoModuloRepo;
        }

        /// <summary>
        /// Acao componente é sempre vai atualizar o registro, raramente adiciona o que não existe
        /// </summary>
        /// <param name="acao"></param>
        public void Atualizar(EquipamentoModulo acao)
        {
            if (_equipamentoModuloRepo.ExisteEquipamentoModulo(acao))
            {
                _equipamentoModuloRepo.Atualizar(acao);
            }
            else
            {
                acao.CodUsuarioCad = acao.CodUsuarioManut;
                acao.DataHoraCad = acao.DataHoraManut;
                _equipamentoModuloRepo.Criar(acao);
            }
        }

        public EquipamentoModulo Criar(EquipamentoModulo acao)
        {
            _equipamentoModuloRepo.Criar(acao);
            return acao;
        }

        public void Deletar(int codigo)
        {
            _equipamentoModuloRepo.Deletar(codigo);
        }

        public EquipamentoModulo ObterPorCodigo(int codigo)
        {
            return _equipamentoModuloRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(EquipamentoModuloParameters parameters)
        {
            var acoes = _equipamentoModuloRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = acoes,
                TotalCount = acoes.TotalCount,
                CurrentPage = acoes.CurrentPage,
                PageSize = acoes.PageSize,
                TotalPages = acoes.TotalPages,
                HasNext = acoes.HasNext,
                HasPrevious = acoes.HasPrevious
            };

            return lista;
        }
    }
}
