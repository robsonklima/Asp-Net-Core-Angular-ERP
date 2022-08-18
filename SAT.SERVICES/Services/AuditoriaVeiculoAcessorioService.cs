using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AuditoriaVeiculoAcessorioService : IAuditoriaVeiculoAcessorioService
    {
        private IAuditoriaVeiculoAcessorioRepository _auditoriaVeiculoAcessorioRepo { get; }

        public AuditoriaVeiculoAcessorioService(IAuditoriaVeiculoAcessorioRepository auditoriaVeiculoAcessorioRepo)
        {
            _auditoriaVeiculoAcessorioRepo = auditoriaVeiculoAcessorioRepo;
        }

        public ListViewModel ObterPorParametros(AuditoriaVeiculoAcessorioParameters parameters)
        {
            var auditoriasVeiculosAcessorios = _auditoriaVeiculoAcessorioRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = auditoriasVeiculosAcessorios,
                TotalCount = auditoriasVeiculosAcessorios.TotalCount,
                CurrentPage = auditoriasVeiculosAcessorios.CurrentPage,
                PageSize = auditoriasVeiculosAcessorios.PageSize,
                TotalPages = auditoriasVeiculosAcessorios.TotalPages,
                HasNext = auditoriasVeiculosAcessorios.HasNext,
                HasPrevious = auditoriasVeiculosAcessorios.HasPrevious
            };

            return lista;
        }

        public AuditoriaVeiculoAcessorio Criar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioRepo.Criar(auditoriaVeiculoAcessorio);

            return auditoriaVeiculoAcessorio;
        }

        public void Deletar(int codigo)
        {
            _auditoriaVeiculoAcessorioRepo.Deletar(codigo);
        }

        public void Atualizar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioRepo.Atualizar(auditoriaVeiculoAcessorio);
        }

        public AuditoriaVeiculoAcessorio ObterPorCodigo(int codigo)
        {
            return _auditoriaVeiculoAcessorioRepo.ObterPorCodigo(codigo);
        }
    }
}
