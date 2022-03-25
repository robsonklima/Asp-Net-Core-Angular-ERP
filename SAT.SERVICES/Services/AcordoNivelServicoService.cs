using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;

namespace SAT.SERVICES.Services
{
    public class AcordoNivelServicoService : IAcordoNivelServicoService
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

        /// <summary>
        /// Gera o modelo SLA Legado
        /// </summary>
        /// <param name="modelo">SLA_NEW</param>
        /// <returns></returns>
        private AcordoNivelServicoLegado GeraModeloSLALegado(AcordoNivelServico modelo)
        {
            return new AcordoNivelServicoLegado
            {
                CodSla = modelo.CodSLA,
                NomeSla = modelo.NomeSLA,
                DescSla = modelo.DescSLA,
                TempoInicio = modelo.TempoInicio,
                TempoReparo = modelo.TempoReparo,
                TempoSolucao = modelo.TempoSolucao,
                HorarioInicio = modelo.HorarioInicio,
                HorarioFim = modelo.HorarioFim,
                DataCadastro = modelo.DataCadastro,
                CodUsuarioCadastro = modelo.CodUsuarioCad,
                DataManutencao = modelo.DataManutencao,
                CodUsuarioManutencao = modelo.CodUsuarioManutencao,
                IndAgendamento = Convert.ToByte(modelo.IndAgendamento),
                IndSabado = Convert.ToByte(modelo.IndSabado),
                IndDomingo = Convert.ToByte(modelo.IndDomingo),
                IndFeriado = Convert.ToByte(modelo.IndFeriado)
            };
        }
    }
}
