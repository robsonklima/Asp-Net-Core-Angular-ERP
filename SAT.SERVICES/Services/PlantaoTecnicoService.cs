using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Helpers;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PlantaoTecnicoService : IPlantaoTecnicoService
    {
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

        public void ProcessarTask()
        {
            var task = _satTaskService.ObterPorParametros(new SatTaskParameters() {
                CodSatTaskTipo = (int)SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL,
                SortActive = "DataHoraProcessamento",
                SortDirection = "DESC"
            }).Items.FirstOrDefault();

            var is14Horas = (int)DateTime.Now.Hour == 12;
            var isSexta = (int)DateTime.Now.DayOfWeek == 5;
            var isSabado = (int)DateTime.Now.DayOfWeek == 6;
            var isDomingo = (int)DateTime.Now.DayOfWeek == 0;

            if (isSabado || isDomingo || !is14Horas)
                return;

            var plantoes = _plantaoTecnicoRepo.ObterPorParametros(new PlantaoTecnicoParameters() {
                DataPlantaoInicio = DateTime.Now,
                DataPlantaoFim = DateTime.Now.AddDays(isSexta ? 4 : 1),
                IndAtivo = 1
            });

            if (plantoes.Count() == 0)
                return;

            var primeiro = plantoes.OrderBy(p => p.DataPlantao).First();
            var ultimo = plantoes.OrderBy(p => p.DataPlantao).Last();

            var assunto = "DSS - Técnicos de Sobreaviso";
            if (primeiro.DataPlantao == ultimo.DataPlantao)
                assunto += $" { primeiro.DataPlantao.ToShortDateString() }";
            else 
                assunto += $" entre { primeiro.DataPlantao.ToShortDateString() } e { ultimo.DataPlantao.ToShortDateString() }";

            var tabelaDetalhada = plantoes.Select(p => new PlantaoTecnicoEmailDetalhado
            {
                Filial = p.Tecnico?.Filial?.NomeFilial, 
                Tecnico = p.Tecnico.Nome,
                Matricula = p.Tecnico?.Usuario?.NumCracha,
                Regiao = p.Tecnico?.Regiao?.NomeRegiao,
                Data = p.DataPlantao.ToShortDateString(),
                Dia = p.DataPlantao.ToString("ddd", new CultureInfo("pr-BR")).ToUpper()
            }).ToList();

            string html = EmailHelper.Converter<PlantaoTecnicoEmailDetalhado>(tabelaDetalhada, "DSS - Técnicos de Sobreaviso",
                "Segue abaixo os técnicos plantonistas, de sobreaviso para este final de semana/feriado.");

            var tabelaResumida = plantoes.GroupBy(p => p.Tecnico.Filial.NomeFilial)
                .Select(p => new PlantaoTecnicoEmailResumido
                {
                    Filial = p.Key,
                    Qtd = p.Count()
                })
                .OrderBy(p => p.Filial).ToList();

            html += EmailHelper.Converter<PlantaoTecnicoEmailResumido>(tabelaResumida, "Resumo",
                "Segue abaixo o resumo da quantidade de plantões por filial.");

            Email email = new() {
                Assunto = assunto,
                NomeDestinatario = Constants.SISTEMA_NOME,
                EmailRemetente = Constants.EQUIPE_SAT_EMAIL,
                EmailDestinatario = Constants.EQUIPE_SAT_EMAIL, //andre.figueiredo@perto.com.br;ivan.medina@perto.com.br;cesar.bessa@perto.com.br;claudio.meurer@digicon.com.br;silvana.ribeiro@perto.com.br
                Corpo = html
            };

            _emailService.Enviar(email);
            
            _satTaskService.Criar(new SatTask() {
                codSatTaskTipo = (int)SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL,
                DataHoraProcessamento = DateTime.Now
            });
        }
    }
}
