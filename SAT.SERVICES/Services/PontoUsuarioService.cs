using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioService : IPontoUsuarioService
    {
        private readonly IPontoUsuarioRepository _pontoUsuarioRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PontoUsuarioService(IPontoUsuarioRepository pontoUsuarioRepo, ISequenciaRepository seqRepo)
        {
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(PontoUsuarioParameters parameters)
        {
            var data = _pontoUsuarioRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return lista;
        }

        public PontoUsuario Criar(PontoUsuario pontoUsuario)
        {
            pontoUsuario.CodPontoUsuario = _seqRepo.ObterContador("PontoUsuario");
            _pontoUsuarioRepo.Criar(pontoUsuario);
            return pontoUsuario;
        }

        public void Deletar(int codigo)
        {
            _pontoUsuarioRepo.Deletar(codigo);
        }

        public void Atualizar(PontoUsuario pontoUsuario)
        {
            _pontoUsuarioRepo.Atualizar(pontoUsuario);
        }

        public PontoUsuario ObterPorCodigo(int codigo)
        {
            return _pontoUsuarioRepo.ObterPorCodigo(codigo);
        }
    }
}
