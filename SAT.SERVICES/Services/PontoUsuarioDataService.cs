using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioDataService : IPontoUsuarioDataService
    {
        private readonly IPontoUsuarioDataRepository _pontoUsuarioDataRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PontoUsuarioDataService(IPontoUsuarioDataRepository pontoUsuarioDataRepo, ISequenciaRepository seqRepo)
        {
            _pontoUsuarioDataRepo = pontoUsuarioDataRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(PontoUsuarioDataParameters parameters)
        {
            var data = _pontoUsuarioDataRepo.ObterPorParametros(parameters);

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

        public PontoUsuarioData Criar(PontoUsuarioData pontoUsuarioData)
        {
            pontoUsuarioData.CodPontoUsuarioData = _seqRepo.ObterContador("PontoUsuarioData");
            _pontoUsuarioDataRepo.Criar(pontoUsuarioData);
            return pontoUsuarioData;
        }

        public void Deletar(int codigo)
        {
            _pontoUsuarioDataRepo.Deletar(codigo);
        }

        public void Atualizar(PontoUsuarioData pontoUsuarioData)
        {
            _pontoUsuarioDataRepo.Atualizar(pontoUsuarioData);
        }

        public PontoUsuarioData ObterPorCodigo(int codigo)
        {
            return _pontoUsuarioDataRepo.ObterPorCodigo(codigo);
        }
    }
}
