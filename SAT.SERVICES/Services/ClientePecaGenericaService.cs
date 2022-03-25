using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ClientePecaGenericaService : IClientePecaGenericaService
    {
        private readonly IClientePecaGenericaRepository _clientePecaGenericaRepo;

        public ClientePecaGenericaService(IClientePecaGenericaRepository clientePecaGenericaRepo)
        {
            _clientePecaGenericaRepo = clientePecaGenericaRepo;
        }

        public ListViewModel ObterPorParametros(ClientePecaGenericaParameters parameters)
        {
            var clientePecaGenericas = _clientePecaGenericaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = clientePecaGenericas,
                TotalCount = clientePecaGenericas.TotalCount,
                CurrentPage = clientePecaGenericas.CurrentPage,
                PageSize = clientePecaGenericas.PageSize,
                TotalPages = clientePecaGenericas.TotalPages,
                HasNext = clientePecaGenericas.HasNext,
                HasPrevious = clientePecaGenericas.HasPrevious
            };

            return lista;
        }

        public ClientePecaGenerica Criar(ClientePecaGenerica clientePecaGenerica)
        {
            _clientePecaGenericaRepo.Criar(clientePecaGenerica);
            return clientePecaGenerica;
        }

        public void Deletar(int codigo) =>
            _clientePecaGenericaRepo.Deletar(codigo);

        public void Atualizar(ClientePecaGenerica clientePecaGenerica) =>
            _clientePecaGenericaRepo.Atualizar(clientePecaGenerica);

        public ClientePecaGenerica ObterPorCodigo(int codigo) =>
            _clientePecaGenericaRepo.ObterPorCodigo(codigo);

        public IActionResult ExportToExcel(ClientePecaGenericaParameters parameters)
        {
            var os = _clientePecaGenericaRepo.ObterPorParametros(parameters);
            return null; //new BaseExcelService<ClientePecaGenerica>().CreateWorkbook(os.Cast<ClientePecaGenerica>().ToList());
        }
    }
}