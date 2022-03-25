using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ClientePecaService : IClientePecaService
    {
        private readonly IClientePecaRepository _clientePecaRepo;

        public ClientePecaService(IClientePecaRepository clientePecaRepo)
        {
            _clientePecaRepo = clientePecaRepo;
        }

        public ListViewModel ObterPorParametros(ClientePecaParameters parameters)
        {
            var clientePecas = _clientePecaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = clientePecas,
                TotalCount = clientePecas.TotalCount,
                CurrentPage = clientePecas.CurrentPage,
                PageSize = clientePecas.PageSize,
                TotalPages = clientePecas.TotalPages,
                HasNext = clientePecas.HasNext,
                HasPrevious = clientePecas.HasPrevious
            };

            return lista;
        }

        public ClientePeca Criar(ClientePeca clientePeca)
        {
            _clientePecaRepo.Criar(clientePeca);
            return clientePeca;
        }

        public void Deletar(int codigo) =>
            _clientePecaRepo.Deletar(codigo);

        public void Atualizar(ClientePeca clientePeca) =>
            _clientePecaRepo.Atualizar(clientePeca);

        public ClientePeca ObterPorCodigo(int codigo) =>
            _clientePecaRepo.ObterPorCodigo(codigo);

        public IActionResult ExportToExcel(ClientePecaParameters parameters)
        {
            var os = _clientePecaRepo.ObterPorParametros(parameters);
            return null; //new BaseExcelService<ClientePeca>().CreateWorkbook(os.Cast<ClientePeca>().ToList());
        }
    }
}