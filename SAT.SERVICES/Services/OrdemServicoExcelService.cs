using System.Collections.Generic;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoExcelService : BaseExcelService<OrdemServico>
    {
        public OrdemServicoExcelService()
        {
            IgnoredProperties = new List<string>
            {
                { "DataHoraManut" },
                { "DataHoraCad" },
                { "DataAcertoParque" },
                { "DataManutencao" },
                { "DataHoraIntegracaoRevisao" },
                { "Fotos" },
                { "CodFilial" },
                { "IndCienciaTerceirizada" },
                { "IndServico" },
                { "ServicoEmail" },
                { "Alertas" },
                { "IndCienciaAtendente" },
                { "DataHoraIntegracaoRevisaoAgendamento" },
                { "DataHoraEnderecoVerificado" },
                { "CodUsuarioAcertoParque" },
                { "IndAcertoParque" },
                { "IndOSIntervencaoEquipamento" },
                { "DataExclusaoBanrisul" },
                { "DataHoraTransf" },
                { "DataHoraFechamento" },
                { "DescNumSerieNi" },
                { "DataHoraSolicitacao" },
                { "DescMotivoMarcaEspecial" },
                { "IndMarcaEspecial" },
                { "CodUsuarioCancelamento" },
                { "IndRevisaoReincidencia" },
                { "TempoSlaSolucao" },
                { "TempoSlaReparo" },
                { "TempoSlaInicio" },
                { "CodTipoMarcacao" },
                { "CodUsuarioManut" },
                { "NumAgenciaNI" },
                { "CodUsuarioCadastro" },
                { "TempoEfetInicio" },
                { "TempoEfetReparo" },
                { "TempoEfetSolucao" },
                { "CodUsuarioOSMobileLida" },
                { "CodUsuarioOsmobileRecebida" },
                { "DataHoraIntegracaoRevisaoV2" },
                { "DataHoraIntegracaoRevisaoAgendamentoV2" },
                { "CodUsuarioMarcaEspecial" },
                { "CodUsuarioFechamento" },
                { "IndNumRATObrigatorio" },
                { "OrdensServicoRelatorioInstalacao" },
                { "CodDefeitoPOS" },
                { "CodOperadoraTelefonia" },
                { "CodMotivoCancelamento" },
                { "SugestaoOS" },
                { "CodContrato" },
                { "CodSeveridade" },
                { "NumEstabelecimentoCliente" },
                { "ObsFechamentoBanrisul" },
                { "DataFechamentoBanrisul" },
                { "IndFechamentoBanrisul" },
                { "IndStatusEnvioReincidencia" },
                { "CodDefeitoPOS" },
                { "IndVandalismo" },
                { "IndBloqueioReincidencia" },
                { "IndExclusaoBanrisul" },
                { "IndLiberacaoFechaduraCofre" },
                { "NomeArquivoIntegracaoBanrisul" },
                { "IndReaberturaIntegracaoBanrisul" },
                { "CodMotivoCancelamentoBanrisul" },
                { "IndNotificacaoOrcamentoEnviado" },
                { "IndAgendamentoUnico" },
                { "DataHoraEnvioAgendamentoSemat" },
                { "IndIntegracao" },
                { "IndServicoVerificado" },
                { "DataHoraOsmobileLida" },
                { "DataHoraOsmobileRecebida" },
                { "IndAgendamentoReenviado" },
                { "IndEnderecoRevisado" },
                { "IndOrcamentoEnd" },
                { "IndRevOk" },
                { "IndEndossado" },
                { "IndAtrasoSLA" },
                { "CodUsuarioManutencao" },
                { "CodStatusServico" },
                { "CodTipoIntervencao" },
                { "CodTecnico" },
                { "AgendaTecnico" }
            };

            ComplexProperties = new List<string>
            {
                { "RelatoriosAtendimento" },
                { "Agendamentos" },
                { "PrazosAtendimento" }
            };

            SimpleProperties = new Dictionary<string, string>
            {
                { "StatusServico", "NomeStatusServico" },
                { "TipoIntervencao", "NomTipoIntervencao" },
                { "LocalAtendimento", "NomeLocal" },
                { "EquipamentoContrato", "NumSerie" },
                { "Filial", "NomeFilial" },
                { "RegiaoAutorizada", "CodRegiao" },
                { "Equipamento", "NomeEquip" },
                { "Cliente", "RazaoSocial" },
                { "Tecnico", "Nome" }
            };
        }
    }
}