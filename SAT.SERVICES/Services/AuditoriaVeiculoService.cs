using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AuditoriaVeiculoService : IAuditoriaVeiculoService
    {
        private IAuditoriaVeiculoRepository _auditoriaVeiculoRepo { get; }

        public AuditoriaVeiculoService(IAuditoriaVeiculoRepository auditoriaVeiculoRepo)
        {
            _auditoriaVeiculoRepo = auditoriaVeiculoRepo;
        }

        public ListViewModel ObterPorParametros(AuditoriaVeiculoParameters parameters)
        {
            var auditoriaVeiculos = _auditoriaVeiculoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = auditoriaVeiculos,
                TotalCount = auditoriaVeiculos.TotalCount,
                CurrentPage = auditoriaVeiculos.CurrentPage,
                PageSize = auditoriaVeiculos.PageSize,
                TotalPages = auditoriaVeiculos.TotalPages,
                HasNext = auditoriaVeiculos.HasNext,
                HasPrevious = auditoriaVeiculos.HasPrevious
            };

            return lista;
        }

        public AuditoriaVeiculo Criar(AuditoriaVeiculo auditoriaVeiculo)
        {
            _auditoriaVeiculoRepo.Criar(auditoriaVeiculo);

            return auditoriaVeiculo;
        }

        public void Deletar(int codigo)
        {
            _auditoriaVeiculoRepo.Deletar(codigo);
        }

        public void Atualizar(AuditoriaVeiculo auditoriaVeiculo)
        {
            _auditoriaVeiculoRepo.Atualizar(auditoriaVeiculo);
        }

        public AuditoriaVeiculo ObterPorCodigo(int codigo)
        {
            return _auditoriaVeiculoRepo.ObterPorCodigo(codigo);
        }
    }
}
