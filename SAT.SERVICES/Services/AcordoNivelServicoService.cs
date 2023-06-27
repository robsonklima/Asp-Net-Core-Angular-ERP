using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;

namespace SAT.SERVICES.Services
{
    public partial class AcordoNivelServicoService : IAcordoNivelServicoService
    {
        private readonly IAcordoNivelServicoRepository _ansRepo;
        private readonly ISequenciaRepository _sequenciaRepository;

        public AcordoNivelServicoService(IAcordoNivelServicoRepository ansRepo, ISequenciaRepository sequenciaRepository)
        {
            _ansRepo = ansRepo;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(AcordoNivelServico ans)
        {
            _ansRepo.Atualizar(ans);
            _ansRepo.AtualizarLegado(this.GeraModeloSLALegado(ans));
        }

        public DateTime CalcularSLA(OrdemServico os)
        {
            if (os.EquipamentoContrato is null || !os.CodEquipContrato.HasValue)
                throw new Exception(MsgConst.SLA_NAO_ENCONTRADO_INF_EC);

            var ans = new AcordoNivelServico();

            if (os.EquipamentoContrato.AcordoNivelServico is not null)
                ans = os.EquipamentoContrato.AcordoNivelServico;
            else 
                ans = _ansRepo.ObterPorCodigo(os.EquipamentoContrato.CodSLA);

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

        public AcordoNivelServico Criar(AcordoNivelServico ans)
        {
            ans.CodSLA = this._sequenciaRepository.ObterContador("SLA");

            _ansRepo.Criar(ans);
            _ansRepo.CriarLegado(this.GeraModeloSLALegado(ans));
            return ans;
        }

        public void Deletar(int codigo)
        {
            _ansRepo.Deletar(codigo);
            _ansRepo.DeletarLegado(codigo);
        }

        public AcordoNivelServico ObterPorCodigo(int codigo)
        {
            return _ansRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(AcordoNivelServicoParameters parameters)
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

        private AcordoNivelServicoLegado GeraModeloSLALegado(AcordoNivelServico sla)
        {
            return new AcordoNivelServicoLegado
            {
                CodSla = sla.CodSLA,
                NomeSla = sla.NomeSLA,
                DescSla = sla.DescSLA,
                TempoInicio = sla.TempoInicio,
                TempoReparo = sla.TempoReparo,
                TempoSolucao = sla.TempoSolucao,
                HorarioInicio = sla.HorarioInicio,
                HorarioFim = sla.HorarioFim,
                DataCadastro = sla.DataCadastro,
                CodUsuarioCadastro = sla.CodUsuarioCad,
                DataManutencao = sla.DataManutencao,
                CodUsuarioManutencao = sla.CodUsuarioManutencao,
                IndAgendamento = Convert.ToByte(sla.IndAgendamento),
                IndSabado = Convert.ToByte(sla.IndSabado),
                IndDomingo = Convert.ToByte(sla.IndDomingo),
                IndFeriado = Convert.ToByte(sla.IndFeriado)
            };
        }
    }
}
