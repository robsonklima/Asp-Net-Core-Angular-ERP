using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class FiltroService : IFiltroService
    {
        private readonly IFiltroRepository _filtroRepo;

        public FiltroService(IFiltroRepository filtroRepo)
        {
            _filtroRepo = filtroRepo;
        }

        public void Atualizar(FiltroUsuario filtroUsuario)
        {
            _filtroRepo.Atualizar(filtroUsuario);
        }

        public FiltroUsuario Criar(FiltroUsuario acao)
        {
            _filtroRepo.Criar(acao);

            return acao;
        }

        public void Deletar(int codigo)
        {
            _filtroRepo.Deletar(codigo);
        }

        public FiltroUsuario ObterPorCodigo(int codigo)
        {
            return _filtroRepo.ObterPorCodigo(codigo);
        }
    }
}
