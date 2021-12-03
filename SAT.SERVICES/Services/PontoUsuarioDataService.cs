using System;
using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioDataService : IPontoUsuarioDataService
    {
        private readonly IPontoUsuarioDataRepository _pontoUsuarioDataRepo;
        private readonly ISequenciaRepository _seqRepo;
        private readonly IPontoUsuarioRepository _pontoUsuarioRepo;
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoRepo;
        private readonly IPontoUsuarioDataDivergenciaRepository _pontoUsuarioDataDivergenciaRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public PontoUsuarioDataService(
            IPontoUsuarioDataRepository pontoUsuarioDataRepo,
            IPontoUsuarioRepository pontoUsuarioRepo,
            ISequenciaRepository seqRepo,
            IRelatorioAtendimentoRepository relatorioAtendimentoRepo,
            IPontoUsuarioDataDivergenciaRepository pontoUsuarioDataDivergenciaRepo,
            IUsuarioRepository usuarioRepo
        )
        {
            _pontoUsuarioDataRepo = pontoUsuarioDataRepo;
            _seqRepo = seqRepo;
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _relatorioAtendimentoRepo = relatorioAtendimentoRepo;
            _pontoUsuarioDataDivergenciaRepo = pontoUsuarioDataDivergenciaRepo;
            _usuarioRepo = usuarioRepo;
        }

        public PontoUsuarioData ObterPorCodigo(int codigo)
        {
            return _pontoUsuarioDataRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(PontoUsuarioDataParameters parameters)
        {
            var datas = _pontoUsuarioDataRepo.ObterPorParametros(parameters);

            datas = ObterRegistrosPontos(datas);
            //InconsisteAutomaticamente(datas);

            var lista = new ListViewModel
            {
                Items = datas,
                TotalCount = datas.TotalCount,
                CurrentPage = datas.CurrentPage,
                PageSize = datas.PageSize,
                TotalPages = datas.TotalPages,
                HasNext = datas.HasNext,
                HasPrevious = datas.HasPrevious
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

        private PagedList<PontoUsuarioData> ObterRegistrosPontos(PagedList<PontoUsuarioData> datas)
        {
            for (int i = 0; i < datas.Count; i++)
            {
                var a = datas[i].CodUsuario;

                if (datas[i].CodUsuario != null)
                {
                    datas[i].PontosUsuario = _pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters()
                    {
                        DataHoraRegistroInicio = DateTime.Parse(datas[i].DataRegistro.ToString("yyyy-MM-dd 00:00:00")),
                        DataHoraRegistroFim = DateTime.Parse(datas[i].DataRegistro.ToString("yyyy-MM-dd 23:59:59")),
                        CodUsuario = datas[i].CodUsuario,
                        CodPontoPeriodo = datas[i].CodPontoPeriodo
                    });
                }
            }

            return datas;
        }

        protected void InconsisteAutomaticamente(List<PontoUsuarioData> pontosData)
        {
            foreach (var pontoData in pontosData)
            {
                if (!PermiteInconsistirAutomaticamente(pontoData))
                {
                    continue;
                }

                var motivoDivergencia = -1;

                var usuario = _usuarioRepo.ObterPorCodigo(pontoData.CodUsuario);

                var relatorios = _relatorioAtendimentoRepo.ObterPorParametros(new RelatorioAtendimentoParameters()
                {
                    DataInicio = pontoData.DataRegistro,
                    DataSolucao = pontoData.DataRegistro.AddHours(23).AddMinutes(59).AddSeconds(59),
                    CodTecnicos = usuario.CodTecnico.ToString()
                });

                if (pontoData.PontosUsuario.Count % 2 == 1)
                {
                    motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.FALTA_MARCACAO;
                }
                else
                {
                    if (pontoData.PontosUsuario.Count >= 2)
                    {
                        TimeSpan intervaloEntrePonto = CalculaIntervalo(pontoData.PontosUsuario.First(), pontoData.PontosUsuario.Last());

                        switch (pontoData.PontosUsuario.Count)
                        {
                            case 2:

                                if (intervaloEntrePonto.Hours > 6)
                                {
                                    motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.AUSENCIA_INTERVALO;
                                }
                                break;

                            case 4:

                                if (intervaloEntrePonto.Hours > 6)
                                {
                                    intervaloEntrePonto = CalculaIntervalo(pontoData.PontosUsuario[1], pontoData.PontosUsuario[2]);

                                    if (intervaloEntrePonto.Hours < 1)
                                    {
                                        motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.INTERVALO_MINIMO_1_HORA_NAO_REALIZADO;
                                    }
                                    else if (intervaloEntrePonto.Hours > 2)
                                    {
                                        motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.INTERVALO_MAXIMO_DE_2_HORAS_EXCEDIDO;
                                    }
                                }
                                break;

                            default:
                                break;
                        }

                    }

                    // Verifica a inconsistência de rats vs ponto.
                    if (relatorios.Count > 0 && pontoData.PontosUsuario.Count == 0 && motivoDivergencia == -1)
                    {
                        motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.RAT_SEM_PONTO;
                    }
                    else if (relatorios.Count > 0 && pontoData.PontosUsuario.Count >= 2 && motivoDivergencia == -1)
                    {
                        TimeSpan horarioPrimeiroPonto = TimeSpan.Parse(pontoData.PontosUsuario.First().DataHoraRegistro.ToString("HH:mm"));
                        TimeSpan horarioPrimeiraRat = TimeSpan.Parse(relatorios.First().DataHoraInicio.ToString("HH:mm"));
                        TimeSpan horarioUltimoPonto = TimeSpan.Parse(pontoData.PontosUsuario.Last().DataHoraRegistro.ToString("HH:mm"));
                        TimeSpan horarioUltimaRat = TimeSpan.Parse(relatorios.Last().DataHoraSolucao.ToString("HH:mm"));

                        if (horarioPrimeiraRat < horarioPrimeiroPonto)
                        {
                            motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.RAT_ANTES_PRIMEIRO_PONTO;
                        }
                        else if (horarioUltimaRat > horarioUltimoPonto)
                        {
                            motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.RAT_APOS_ULTIMO_PONTO;
                        }

                        if (pontoData.PontosUsuario.Count == 4)
                        {
                            TimeSpan horarioInicioIntervalo = TimeSpan.Parse(pontoData.PontosUsuario[1].DataHoraRegistro.ToString("HH:mm"));
                            TimeSpan horarioFimIntervalo = TimeSpan.Parse(pontoData.PontosUsuario[2].DataHoraRegistro.ToString("HH:mm"));

                            foreach (var rat in relatorios)
                            {
                                TimeSpan horarioInicioRat = TimeSpan.Parse(rat.DataHoraInicio.ToString("HH:mm"));
                                TimeSpan horarioSolucaoRat = TimeSpan.Parse(rat.DataHoraSolucao.ToString("HH:mm"));

                                if (!(
                                        (horarioInicioRat < horarioInicioIntervalo && horarioSolucaoRat <= horarioInicioIntervalo)
                                        ||
                                        (horarioInicioRat >= horarioFimIntervalo && horarioSolucaoRat > horarioFimIntervalo)
                                     )
                                   )
                                {
                                    if (rat.HorarioInicioIntervalo == null || rat.HorarioTerminoIntervalo == null)
                                    {
                                        motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.INTERVALO_RAT_DIFERENTE_PONTO;
                                    }
                                    else
                                    {
                                        TimeSpan horarioInicioIntervaloRat = rat.HorarioInicioIntervalo;
                                        TimeSpan horarioFimIntervaloRat = rat.HorarioTerminoIntervalo;
                                        TimeSpan quantidadeIntervalo = horarioFimIntervalo.Subtract(horarioInicioIntervalo);
                                        TimeSpan quantidadeIntervaloRat = horarioFimIntervaloRat.Subtract(horarioInicioIntervaloRat);

                                        if (quantidadeIntervaloRat < quantidadeIntervalo)
                                        {
                                            motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.INTERVALO_RAT_MENOR_PONTO;
                                        }
                                        else
                                        {
                                            if (horarioInicioIntervalo < horarioInicioIntervaloRat || horarioFimIntervalo > horarioFimIntervaloRat)
                                            {
                                                motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.INTERVALO_RAT_DIFERENTE_PONTO;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (motivoDivergencia != -1)
                {
                    PontoUsuarioDataStatus status = new PontoUsuarioDataStatus()
                    {
                        CodPontoUsuarioDataStatus = (int)PontoUsuarioDataStatusEnum.INCONSISTENTE
                    };

                    PontoUsuarioData pontoUsuarioData = new PontoUsuarioData() { };

                    AlteraStatus(pontoData, status, motivoDivergencia, (int)PontoUsuarioDataModoDivergenciaEnum.DIVERGENCIA_AUTOMATICA);

                    pontoData.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus = (int)PontoUsuarioDataStatusEnum.INCONSISTENTE;
                }
            }
        }

        protected bool PermiteInconsistirAutomaticamente(PontoUsuarioData pontoData)
        {
            if (
                    (pontoData.PontoPeriodo.CodPontoPeriodoStatus != (int)PontoPeriodoStatusEnum.CONSOLIDADO &&
                     pontoData.PontoPeriodo.CodPontoPeriodoModoAprovacao == (int)PontoPeriodoModoAprovacaoEnum.DIARIO) ||
                    (pontoData.PontoPeriodo.CodPontoPeriodoStatus == (int)PontoPeriodoStatusEnum.EM_ANALISE)
               )
            {
                if (
                        (pontoData.CodPontoUsuarioDataStatus == (int)PontoUsuarioDataStatusEnum.INCONSISTENTE) ||
                        (pontoData.CodPontoUsuarioDataStatus == (int)PontoUsuarioDataStatusEnum.CONFERIDO) ||
                        (pontoData.DataRegistro.DayOfWeek == DayOfWeek.Saturday || pontoData.DataRegistro.DayOfWeek == DayOfWeek.Sunday) ||
                        (pontoData.DataRegistro.Date >= DateTime.Now.Date)
                    )
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        protected TimeSpan CalculaIntervalo(PontoUsuario horario1, PontoUsuario horario2)
        {
            if (horario1 == null || horario1.DataHoraRegistro == DateTime.MinValue)
            {
                return TimeSpan.Zero;
            }

            if (horario2 == null || horario2.DataHoraRegistro == DateTime.MinValue)
            {
                return TimeSpan.Zero;
            }

            TimeSpan primeiroHorario = TimeSpan.Parse(horario1.DataHoraRegistro.ToString("HH:mm"));
            TimeSpan ultimoHorario = TimeSpan.Parse(horario2.DataHoraRegistro.ToString("HH:mm"));

            if (ultimoHorario > primeiroHorario)
            {
                return ultimoHorario - primeiroHorario;
            }
            else
            {
                return primeiroHorario - ultimoHorario;
            }
        }

        protected void AlteraStatus(PontoUsuarioData pontoData, PontoUsuarioDataStatus status, int motivo, int modoDivergencia)
        {
            try
            {
                PontoUsuarioData dataPontoUsuario = new PontoUsuarioData();

                dataPontoUsuario.CodUsuario = "sat";
                dataPontoUsuario.DataRegistro = pontoData.DataRegistro;

                if (pontoData != null)
                {
                    dataPontoUsuario = new PontoUsuarioData();
                    dataPontoUsuario.CodPontoUsuarioData = pontoData.CodPontoUsuarioData;
                    dataPontoUsuario.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus = status.CodPontoUsuarioDataStatus;
                    dataPontoUsuario.DataHoraManut = DateTime.Now;

                    if (modoDivergencia == (int)PontoUsuarioDataModoDivergenciaEnum.DIVERGENCIA_AUTOMATICA)
                    {
                        dataPontoUsuario.CodUsuarioManut = "sat";
                    }
                    else
                    {
                        dataPontoUsuario.CodUsuarioManut = "sat";
                    }

                    _pontoUsuarioDataRepo.Atualizar(dataPontoUsuario);
                }
                else
                {
                    dataPontoUsuario.CodUsuarioCad = "sat";
                    dataPontoUsuario.DataHoraCad = DateTime.Now;
                    dataPontoUsuario.CodPontoPeriodo = pontoData.PontoPeriodo.CodPontoPeriodo;
                    dataPontoUsuario.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus = status.CodPontoUsuarioDataStatus;

                    _pontoUsuarioDataRepo.Criar(dataPontoUsuario);
                }

                if (status.CodPontoUsuarioDataStatus == (int)PontoUsuarioDataStatusEnum.INCONSISTENTE)
                {
                    PontoUsuarioDataDivergencia divergencia = new PontoUsuarioDataDivergencia();

                    if (motivo > -1)
                    {
                        divergencia.PontoUsuarioDataMotivoDivergencia.CodPontoUsuarioDataMotivoDivergencia = motivo;
                    }

                    if (modoDivergencia > -1)
                    {
                        divergencia.PontoUsuarioDataModoDivergencia.CodPontoUsuarioDataModoDivergencia = modoDivergencia;
                    }

                    divergencia.CodUsuarioCad = "sat";
                    divergencia.DataHoraCad = DateTime.Now;
                    divergencia.CodPontoUsuarioData = dataPontoUsuario.CodPontoUsuarioData;

                    _pontoUsuarioDataDivergenciaRepo.Criar(divergencia);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
