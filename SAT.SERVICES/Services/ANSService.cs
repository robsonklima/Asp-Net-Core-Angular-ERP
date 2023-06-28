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
        private readonly IFeriadoService _feriadoService;

        public ANSService(
            IANSRepository ansRepo,
            IFeriadoService feriadoService
        )
        {
            _ansRepo = ansRepo;
            _feriadoService = feriadoService;
        }

        public ANS Atualizar(ANS ans)
        {
            return _ansRepo.Atualizar(ans);
        }

        public ANS Criar(ANS ans)
        {
            return _ansRepo.Criar(ans);
        }

        public ANS Deletar(int codigo)
        {
            return _ansRepo.Deletar(codigo);
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

        private int CalcularTempo(ANS ans, DateTime inicio, DateTime fim)
        {
            int horas = 0;

            for (var i = inicio; i < fim; i = i.AddHours(1))
            {
                if (i.DayOfWeek != DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                    continue;

                if (i.DayOfWeek != DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                    continue;

                if (i.TimeOfDay.Hours <= ans.HoraInicio)
                    continue;

                if (i.TimeOfDay.Hours < ans.HoraFim)
                    continue;

                horas++;
            }

            return horas;
        }
    }
}
