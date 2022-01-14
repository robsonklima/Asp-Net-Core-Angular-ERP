using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Linq;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public class ImportacaoService : IImportacaoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly ILocalAtendimentoRepository _localAtendimentoRepo;
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepo;

        public ImportacaoService(
            IOrdemServicoRepository ordemServicoRepo,
            ISequenciaRepository sequenciaRepo,
            ILocalAtendimentoRepository localAtendimentoRepo,
            IEquipamentoContratoRepository equipamentoContratoRepo)
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _localAtendimentoRepo = localAtendimentoRepo;
            _equipamentoContratoRepo = equipamentoContratoRepo;
        }

        public List<int> AberturaChamadosEmMassa(List<ImportacaoAberturaOrdemServico> importacaoOs)
        {
            var osMensagem = new List<string>();

            importacaoOs.Where(o => !string.IsNullOrEmpty(o.NomeFantasia))
                .ToList()
                .ForEach(i =>
                    {
                        var local = _localAtendimentoRepo.ObterPorParametros(new LocalAtendimentoParameters
                        {
                            NumAgencia = i.NumAgenciaBanco.Trim(),
                            DCPosto = i.DcPosto.Trim(),
                            IndAtivo = 1
                        })
                        .Where(o => o.Cliente.NomeFantasia == i.NomeFantasia)
                        .FirstOrDefault();

                        var equipamento = _equipamentoContratoRepo
                            .ObterPorParametros(new EquipamentoContratoParameters
                            {
                                NumSerie = i.NumSerie.Trim()
                            }).FirstOrDefault();


                        if (local is null || equipamento is null) osMensagem.Add($"Erro, informações inválidas - {i.NumSerie}");

                        _ordemServicoRepo.Criar(new OrdemServico
                        {
                            CodCliente = local.CodCliente,
                            CodPosto = local.CodPosto.Value,
                            CodTipoIntervencao = int.Parse(i.TipoIntervencao),
                            DefeitoRelatado = i.DefeitoRelatado,
                            NumOSCliente = i.NumOSCliente,
                            NumOSQuarteirizada = i.NumOSQuarteirizada,
                            CodEquipContrato = equipamento.CodEquipContrato
                        });
                    });

            return new List<int>();
        }
    }
}