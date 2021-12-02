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

        public PontoUsuarioDataService(
            IPontoUsuarioDataRepository pontoUsuarioDataRepo,
            IPontoUsuarioRepository pontoUsuarioRepo,
            ISequenciaRepository seqRepo
        )
        {
            _pontoUsuarioDataRepo = pontoUsuarioDataRepo;
            _seqRepo = seqRepo;
            _pontoUsuarioRepo = pontoUsuarioRepo;
        }

        public PontoUsuarioData ObterPorCodigo(int codigo)
        {
            return _pontoUsuarioDataRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(PontoUsuarioDataParameters parameters)
        {
            var datas = _pontoUsuarioDataRepo.ObterPorParametros(parameters);
            
            datas = ObterPontoDatas(datas);

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

        private PagedList<PontoUsuarioData> ObterPontoDatas(PagedList<PontoUsuarioData> datas) {
            for (int i = 0; i < datas.Count; i++)
            {
                var a = datas[i].CodUsuario;

                if (datas[i].CodUsuario != null) {
                    datas[i].PontosUsuario = _pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters() {
                        DataHoraRegistroInicio = DateTime.Parse(datas[i].DataRegistro.ToString("yyyy-MM-dd 00:00:00")),
                        DataHoraRegistroFim = DateTime.Parse(datas[i].DataRegistro.ToString("yyyy-MM-dd 23:59:59")),
                        CodUsuario = datas[i].CodUsuario,
                        CodPontoPeriodo = datas[i].CodPontoPeriodo
                    });
                }
            }

            return datas;
        }
    
        protected bool InconsisteAutomaticamente(PontoUsuarioData pontoData, PontoPeriodo periodo, List<PontoUsuario> pontos, List<RelatorioAtendimento> rats)
        {
            if (!PermiteInconsistirAutomaticamente(pontoData, periodo, pontos))
            {
                return false;
            }
            
            var motivoDivergencia = -1;

            if (pontos.Count % 2 == 1)
            {
                motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.FALTA_MARCACAO;
            }
            else
            {
                if (pontos.Count >= 2)
                {
                    TimeSpan intervaloEntrePonto = CalculaIntervalo(pontos.First(), pontos.Last());

                    switch (pontos.Count)
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
                                intervaloEntrePonto = CalculaIntervalo(pontos[1], pontos[2]);

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
                if (rats.Count > 0 && pontos.Count == 0 && motivoDivergencia == -1)
                {
                    motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.RAT_SEM_PONTO;
                } 
                else if (rats.Count > 0 && pontos.Count >= 2 && motivoDivergencia == -1)
                {
                    TimeSpan horarioPrimeiroPonto = TimeSpan.Parse(pontos.First().DataHoraRegistro.ToString("HH:mm"));
                    TimeSpan horarioPrimeiraRat = TimeSpan.Parse(rats.First().DataHoraInicio.ToString("HH:mm"));
                    TimeSpan horarioUltimoPonto = TimeSpan.Parse(pontos.Last().DataHoraRegistro.ToString("HH:mm"));
                    TimeSpan horarioUltimaRat = TimeSpan.Parse(rats.Last().DataHoraSolucao.ToString("HH:mm"));

                    if (horarioPrimeiraRat < horarioPrimeiroPonto)
                    {
                        motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.RAT_ANTES_PRIMEIRO_PONTO;
                    }
                    else if (horarioUltimaRat > horarioUltimoPonto)
                    {
                        motivoDivergencia = (int)PontoUsuarioDataMotivoDivergenciaEnum.RAT_APOS_ULTIMO_PONTO;
                    }

                    if (pontos.Count == 4)
                    {
                        TimeSpan horarioInicioIntervalo = TimeSpan.Parse(pontos[1].DataHoraRegistro.ToString("HH:mm"));
                        TimeSpan horarioFimIntervalo = TimeSpan.Parse(pontos[2].DataHoraRegistro.ToString("HH:mm"));

                        foreach (var rat in rats)
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

                PontoUsuarioData pontoUsuarioData = new PontoUsuarioData();

                // pontoUsuarioData.AlteraStatus(Usuario, pontoData.DataRegistro,
                //     Periodo, status, motivoDivergencia, (int)PontoUsuarioDataModoDivergenciaEnum.DIVERGENCIA_AUTOMATICA);

                pontoData.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus = (int)PontoUsuarioDataStatusEnum.INCONSISTENTE;
            }
            
            return motivoDivergencia != -1;
        }
    
        protected bool PermiteInconsistirAutomaticamente(PontoUsuarioData pontoData, PontoPeriodo periodo, List<PontoUsuario> pontos)
        {
            if (
                    (periodo.CodPontoPeriodoStatus != (int)PontoPeriodoStatusEnum.CONSOLIDADO && 
                     periodo.PontoPeriodoModoAprovacao.CodPontoPeriodoModoAprovacao == (int)PontoPeriodoModoAprovacaoEnum.DIARIO) ||
                    (periodo.CodPontoPeriodoStatus == (int)PontoPeriodoStatusEnum.EM_ANALISE)
               )
            {
                if (
                        (pontoData.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus == (int)PontoUsuarioDataStatusEnum.INCONSISTENTE) ||
                        (pontoData.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus == (int)PontoUsuarioDataStatusEnum.CONFERIDO) ||
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
    
        protected void AlteraStatus(Usuario usuario, String data, PontoPeriodo periodo, PontoUsuarioDataStatus status, int motivo, int modoDivergencia)
        {
            // try
            // {
            //     PontoUsuarioData dataPontoUsuario = new PontoUsuarioData();

            //     dataPontoUsuario.CodUsuario = usuario.CodUsuario;
            //     dataPontoUsuario.DataRegistro = clsGF.formatDateDB(data, "'");

            //     DataTable listaDatas = dataPontoUsuario.Select(2);

            //     if (listaDatas.Rows.Count > 0)
            //     {
            //         dataPontoUsuario = new clsPontoUsuarioData();
            //         dataPontoUsuario.CodPontoUsuarioData = listaDatas.Rows[0]["CodPontoUsuarioData"].ToString();
            //         dataPontoUsuario.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus = status.CodPontoUsuarioDataStatus;
            //         dataPontoUsuario.DataHoraManut = "getdate()";

            //         if (modoDivergencia == clsPontoUsuarioDataModoDivergencia.DIVERGENCIA_AUTOMATICA)
            //         {
            //             dataPontoUsuario.CodUsuarioManut = "sat";
            //         }
            //         else
            //         {
            //             dataPontoUsuario.CodUsuarioManut = clsGF.getSessionString("CodUsuario");
            //         }

            //         dataPontoUsuario.Update(1);                    
            //     }
            //     else
            //     {
            //         dataPontoUsuario.CodUsuarioCad = clsGF.getSessionString("CodUsuario");
            //         dataPontoUsuario.DataHoraCad = "getdate()";
            //         dataPontoUsuario.PontoPeriodo.CodPontoPeriodo = periodo.CodPontoPeriodo;
            //         dataPontoUsuario.PontoUsuarioDataStatus.CodPontoUsuarioDataStatus = status.CodPontoUsuarioDataStatus;
            //         dataPontoUsuario.CodPontoUsuarioData = dataPontoUsuario.Insert();
            //     }

            //     if (status.CodPontoUsuarioDataStatus == (int)PontoUsuarioDataStatusEnum.INCONSISTENTE)
            //     {
            //         PontoUsuarioDataDivergencia divergencia = new PontoUsuarioDataDivergencia();

            //         if (motivo > -1)
            //         {
            //             divergencia.PontoUsuarioDataMotivoDivergencia.CodPontoUsuarioDataMotivoDivergencia = motivo;
            //         }

            //         if (modoDivergencia > -1)
            //         {
            //             divergencia.PontoUsuarioDataModoDivergencia.CodPontoUsuarioDataModoDivergencia = modoDivergencia;
            //         }

            //         divergencia.CodUsuarioCad = usuario.CodUsuario;
            //         divergencia.DataHoraCad = DateTime.Now;
            //         divergencia.CodPontoUsuarioData = dataPontoUsuario.CodPontoUsuarioData;                    

            //         //divergencia.CodPontoUsuarioDataDivergencia = divergencia.Insert();
            //     }                
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }
        }
    }
}
