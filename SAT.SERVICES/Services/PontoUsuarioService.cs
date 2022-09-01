using System;
using System.Diagnostics;
using System.Linq;
using NLog;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioService : IPontoUsuarioService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPontoUsuarioRepository _pontoUsuarioRepo;
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ISequenciaRepository _seqRepo;
        private readonly ISatTaskRepository _satTaskRepo;

        public PontoUsuarioService(
            IPontoUsuarioRepository pontoUsuarioRepo,
            IRelatorioAtendimentoRepository relatorioAtendimentoRepo,
            IUsuarioRepository usuarioRepo,
            ISatTaskRepository satTaskRepo,
            ISequenciaRepository seqRepo
        ) {
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _relatorioAtendimentoRepo = relatorioAtendimentoRepo;
            _usuarioRepo = usuarioRepo;
            _seqRepo = seqRepo;
            _satTaskRepo = satTaskRepo;
        }

        public ListViewModel ObterPorParametros(PontoUsuarioParameters parameters)
        {
            var data = _pontoUsuarioRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return lista;
        }

        public PontoUsuario Criar(PontoUsuario pontoUsuario)
        {
            return _pontoUsuarioRepo.Criar(pontoUsuario);
        }

        public void Deletar(int codigo)
        {
            _pontoUsuarioRepo.Deletar(codigo);
        }

        public void Atualizar(PontoUsuario pontoUsuario)
        {
            _pontoUsuarioRepo.Atualizar(pontoUsuario);
        }

        public PontoUsuario ObterPorCodigo(int codigo)
        {
            return _pontoUsuarioRepo.ObterPorCodigo(codigo);
        }

        public void ProcessarTaskAtualizacaoIntervalosPonto()
        {
            try
            {
                var dataProcessamento = DateTime.Now;

                _satTaskRepo.Criar(new SatTask()
                {
                    codSatTaskTipo = (int)SatTaskTipoEnum.CORRECAO_INTERVALOS_RAT,
                    DataHoraProcessamento = dataProcessamento
                });

                var usuarios = _usuarioRepo.ObterPorParametros(new UsuarioParameters {
                    CodPerfil = (int)PerfilEnum.FILIAL_TECNICO_DE_CAMPO,
                    IndAtivo = 1
                });

                foreach (var usuario in usuarios)
                {
                    Debug.WriteLine(usuario.CodUsuario);

                    var pontos = _pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters {
                        CodUsuario = usuario.CodUsuario,
                        DataHoraRegistro = dataProcessamento.Date
                    });

                    if (pontos.Count < 4) continue;

                    var rats = _relatorioAtendimentoRepo
                        .ObterPorParametros(new RelatorioAtendimentoParameters { 
                            DataInicio = dataProcessamento,
                            DataSolucao = dataProcessamento,
                            CodTecnicos = usuario.CodTecnico.ToString()
                        })
                        .Where(r => r.HorarioInicioIntervalo == null || r.HorarioTerminoIntervalo == null)
                        .Where(r => r.CodStatusServico != Constants.CANCELADO)
                        .Where(r => r.CodStatusServico != Constants.CANCELADO_COM_ATENDIMENTO)
                        .ToList();

                    for (int i = 0; i < rats.Count; i++)
                    {
                        rats[i].HorarioInicioIntervalo = pontos[1].DataHoraRegistro.TimeOfDay;
                        rats[i].HorarioTerminoIntervalo = pontos[2].DataHoraRegistro.TimeOfDay;
                        _relatorioAtendimentoRepo.Atualizar(rats[i]);
                    }
                }

                _logger.Info($"Correção de intervalo do RAT executado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Correção de intervalo do RAT executada com erro: {ex.Message}");
            }
        }
    }
}
