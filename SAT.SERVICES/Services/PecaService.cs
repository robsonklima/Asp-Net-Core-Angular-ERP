using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PecaService : IPecaService
    {
        private readonly IPecaRepository _pecaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public PecaService(IPecaRepository pecaRepo, ISequenciaRepository sequenciaRepo)
        {
            _pecaRepo = pecaRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(PecaParameters parameters)
        {
            var pecas = _pecaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = pecas,
                TotalCount = pecas.TotalCount,
                CurrentPage = pecas.CurrentPage,
                PageSize = pecas.PageSize,
                TotalPages = pecas.TotalPages,
                HasNext = pecas.HasNext,
                HasPrevious = pecas.HasPrevious
            };

            return lista;
        }

        public Peca Criar(Peca peca)
        {
            peca.CodPeca = _sequenciaRepo.ObterContador("Peca");
            _pecaRepo.Criar(peca);
            return peca;
        }

        public void Deletar(int codigo) =>
            _pecaRepo.Deletar(codigo);

        public void Atualizar(Peca peca) =>
            _pecaRepo.Atualizar(peca);

        public Peca ObterPorCodigo(int codigo) =>
            _pecaRepo.ObterPorCodigo(codigo);

        public IActionResult ExportToExcel(PecaParameters parameters)
        {
            var os = _pecaRepo.ObterPorParametros(parameters);
            return null; //new BaseExcelService<Peca>().CreateWorkbook(os.Cast<Peca>().ToList());
        }
    }
}