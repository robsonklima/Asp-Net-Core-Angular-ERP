using NLog;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioService : IPontoUsuarioService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPontoUsuarioRepository _pontoUsuarioRepo;
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ISequenciaRepository _seqRepo;
        private readonly ISatTaskRepository _satTaskRepo;

        public PontoUsuarioService(
            IPontoUsuarioRepository pontoUsuarioRepo,
            IRelatorioAtendimentoRepository relatorioAtendimentoRepo,
            IUsuarioRepository usuarioRepo,
            ISatTaskRepository satTaskRepo,
            ISequenciaRepository seqRepo
        ) {
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _relatorioAtendimentoRepo = relatorioAtendimentoRepo;
            _usuarioRepo = usuarioRepo;
            _seqRepo = seqRepo;
            _satTaskRepo = satTaskRepo;
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
            return _pontoUsuarioRepo.Criar(pontoUsuario);
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
