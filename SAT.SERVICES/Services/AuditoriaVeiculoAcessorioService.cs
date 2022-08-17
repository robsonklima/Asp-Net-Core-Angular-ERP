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

        // public ListViewModel ObterPorParametros(AuditoriaVeiculoParameters parameters)
        // {
        //     var auditoriaVeiculos = _auditoriaVeiculoRepo.ObterPorParametros(parameters);

        //     var lista = new ListViewModel
        //     {
        //         Items = causas,
        //         TotalCount = causas.TotalCount,
        //         CurrentPage = causas.CurrentPage,
        //         PageSize = causas.PageSize,
        //         TotalPages = causas.TotalPages,
        //         HasNext = causas.HasNext,
        //         HasPrevious = causas.HasPrevious
        //     };

        //     return lista;
        // }

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
