using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class ANSService : IANSService
    {
        private readonly IANSRepository _ansRepo;

        public ANSService(
            IANSRepository ansRepo
        )
        {
            _ansRepo = ansRepo;
        }

        public void Atualizar(ANS ans)
        {
            _ansRepo.Atualizar(ans);
        }

        public void Criar(ANS ans)
        {
            _ansRepo.Criar(ans);
        }

        public void Deletar(int codigo)
        {
            _ansRepo.Deletar(codigo);
        }

        public ANS ObterPorCodigo(int codigo)
        {
            return _ansRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(ANSParameters parameters)
        {
            var anss = _ansRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = anss,
                TotalCount = anss.TotalCount,
                CurrentPage = anss.CurrentPage,
                PageSize = anss.PageSize,
                TotalPages = anss.TotalPages,
                HasNext = anss.HasNext,
                HasPrevious = anss.HasPrevious
            };

            return lista;
        }

        public DateTime CalcularSLA(OrdemServico os)
        {
            if (os.EquipamentoContrato is null || !os.CodEquipContrato.HasValue)
                throw new Exception(MsgConst.SLA_NAO_ENCONTRADO_INF_EC);

            var ans = new ANS();

            if (os.CodCliente == Constants.CLIENTE_BB)
            {
                return CalcularSLABB(os);
            }

            switch (os.CodCliente)
            {
                case Constants.CLIENTE_BB:
                    return CalcularSLABB(os);
                case Constants.CLIENTE_BANRISUL:
                    return CalcularSLABanrisul(os);
                default:
                    return CalcularSLADefault();
            }
        }
    }
}
