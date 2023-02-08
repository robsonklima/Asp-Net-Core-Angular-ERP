using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OSBancadaService : IOSBancadaService
    {
        private readonly IOSBancadaRepository _osBancadaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public OSBancadaService(IOSBancadaRepository osBancadaRepo, ISequenciaRepository sequenciaRepo)
        {
            _osBancadaRepo = osBancadaRepo;
            this._sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(OSBancada osBancada)
        {
            this._osBancadaRepo.Atualizar(osBancada);
        }

        public OSBancada Criar(OSBancada osBancada)
        {
            osBancada.CodOsbancada = this._sequenciaRepo.ObterContador("OSBancada");
            this._osBancadaRepo.Criar(osBancada);
            return osBancada;
        }

        public void Deletar(int codigo)
        {
            this._osBancadaRepo.Deletar(codigo);
        }

        public OSBancada ObterPorCodigo(int codigo)
        {
            return this._osBancadaRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(OSBancadaParameters parameters)
        {
            var osBancadas = _osBancadaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = osBancadas,
                TotalCount = osBancadas.TotalCount,
                CurrentPage = osBancadas.CurrentPage,
                PageSize = osBancadas.PageSize,
                TotalPages = osBancadas.TotalPages,
                HasNext = osBancadas.HasNext,
                HasPrevious = osBancadas.HasPrevious
            };

            return lista;
        }
    }
}
