using System;
using System.Data;
using System.Globalization;
using System.Linq;
using NLog;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public class PlantaoTecnicoService : IPlantaoTecnicoService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ISatTaskService _satTaskService;
        private readonly IPlantaoTecnicoRepository _plantaoTecnicoRepo;
        private readonly IEmailService _emailService;

        public PlantaoTecnicoService(
            ISatTaskService satTaskService,
            IPlantaoTecnicoRepository plantaoTecnicoRepo,
            IEmailService emailService
        )
        {
            _satTaskService = satTaskService;
            _plantaoTecnicoRepo = plantaoTecnicoRepo;
            _emailService = emailService;
        }

        public ListViewModel ObterPorParametros(PlantaoTecnicoParameters parameters)
        {
            var perfis = _plantaoTecnicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = perfis,
                TotalCount = perfis.TotalCount,
                CurrentPage = perfis.CurrentPage,
                PageSize = perfis.PageSize,
                TotalPages = perfis.TotalPages,
                HasNext = perfis.HasNext,
                HasPrevious = perfis.HasPrevious
            };

            return lista;
        }

        public PlantaoTecnico Criar(PlantaoTecnico plantaoTecnico)
        {
            _plantaoTecnicoRepo.Criar(plantaoTecnico);
            return plantaoTecnico;
        }

        public void Deletar(int codigo)
        {
            _plantaoTecnicoRepo.Deletar(codigo);
        }

        public void Atualizar(PlantaoTecnico plantaoTecnico)
        {
            _plantaoTecnicoRepo.Atualizar(plantaoTecnico);
        }

        public PlantaoTecnico ObterPorCodigo(int codigo)
        {
            return _plantaoTecnicoRepo.ObterPorCodigo(codigo);
        }

        /* Envia e-mail com os plantões registrados para feriados e fins de semana */
        public void ProcessarTaskEmailsSobreaviso()
        {
            _satTaskService.Criar(new SatTask()
            {
                codSatTaskTipo = (int)SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL,
                DataHoraProcessamento = DateTime.Now
            });

            SatTask task = (SatTask)_satTaskService.ObterPorParametros(new SatTaskParameters()
            {
                CodSatTaskTipo = (int)SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL,
                SortActive = "DataHoraProcessamento",
                SortDirection = "DESC",
                PageSize = 1
            }).Items.FirstOrDefault();

            var isSexta = DateTime.Now.DayOfWeek == DayOfWeek.Friday;
            var plantoes = _plantaoTecnicoRepo.ObterPorParametros(new PlantaoTecnicoParameters
            {
                DataPlantaoInicio = DateTime.Now,
                DataPlantaoFim = DateTime.Now.AddDays(isSexta ? 4 : 1),
                IndAtivo = 1
            });

            var assunto = "DSS - Técnicos de Sobreaviso";
            string html;

            if (plantoes.Count() == 0)
            {
                html = $"Não foram encontrados plantões na data de: {DateTime.Now.Date}";
                _logger.Info(html);
            }
            else
            {
                var primeiro = plantoes.OrderBy(p => p.DataPlantao).First();
                var ultimo = plantoes.OrderBy(p => p.DataPlantao).Last();

                if (primeiro.DataPlantao == ultimo.DataPlantao)
                    assunto += $" {primeiro.DataPlantao.ToShortDateString()}";
                else
                    assunto += $" entre {primeiro.DataPlantao.ToShortDateString()} e {ultimo.DataPlantao.ToShortDateString()}";

                var tabelaDetalhada = plantoes.Select(p => new PlantaoTecnicoEmailDetalhado
                {
                    Filial = p.Tecnico?.Filial?.NomeFilial,
                    Tecnico = p.Tecnico.Nome,
                    Matricula = p.Tecnico?.Usuario?.NumCracha,
                    Regiao = p.Tecnico?.Regiao?.NomeRegiao,
                    Data = p.DataPlantao.ToShortDateString(),
                    Dia = p.DataPlantao.ToString("ddd", new CultureInfo("pr-BR")).ToUpper()
                }).ToList();

                html = EmailHelper.ConverterParaHtml<PlantaoTecnicoEmailDetalhado>(tabelaDetalhada, "DSS - Técnicos de Sobreaviso",
                    "Segue abaixo os técnicos plantonistas, de sobreaviso para este final de semana/feriado.");

                var tabelaResumida = plantoes.GroupBy(p => p.Tecnico.Filial.NomeFilial)
                    .Select(p => new PlantaoTecnicoEmailResumido { Filial = p.Key, Qtd = p.Count() })
                    .OrderBy(p => p.Filial).ToList();

                html += EmailHelper.ConverterParaHtml<PlantaoTecnicoEmailResumido>(tabelaResumida, "Resumo",
                    "Segue abaixo o resumo da quantidade de plantões por filial.");

                _logger.Info($"Dados de plantão obtidos: {plantoes.Count()}");

            }

            Email email = new()
            {
                Assunto = assunto,
                NomeDestinatario = Constants.EQUIPE_SAT_EMAIL,
                EmailRemetente = Constants.EQUIPE_SAT_EMAIL,
                EmailDestinatario = "andre.figueiredo@perto.com.br;ivan.medina@perto.com.br;cesar.bessa@perto.com.br;claudio.meurer@digicon.com.br;silvana.ribeiro@perto.com.br",
                Corpo = html
            };

            _emailService.Enviar(email);
            _logger.Info($"E-mail Plantão Técnicos enviado às: {DateTime.Now}");
        }
    }
}
