using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MRPLogixService : IMRPLogixService
    {
        private readonly IMRPLogixRepository _mrpLogixRepo;

        public MRPLogixService(IMRPLogixRepository mrpLogixRepo)
        {
            _mrpLogixRepo = mrpLogixRepo;
        }

        public ListViewModel ObterPorParametros(MRPLogixParameters parameters)
        {
            var mrps = _mrpLogixRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = mrps,
                TotalCount = mrps.TotalCount,
                CurrentPage = mrps.CurrentPage,
                PageSize = mrps.PageSize,
                TotalPages = mrps.TotalPages,
                HasNext = mrps.HasNext,
                HasPrevious = mrps.HasPrevious
            };
        }

        public MRPLogix Criar(MRPLogix mrpLogix)
        {
            _mrpLogixRepo.Criar(mrpLogix);
            return mrpLogix;
        }

        public void Deletar(int codigo)
        {
            _mrpLogixRepo.Deletar(codigo);
        }

        public void Atualizar(MRPLogix mrpLogix)
        {
            _mrpLogixRepo.Atualizar(mrpLogix);
        }

        public MRPLogix ObterPorCodigo(int codigo)
        {
            return _mrpLogixRepo.ObterPorCodigo(codigo);
        }
    }
}
