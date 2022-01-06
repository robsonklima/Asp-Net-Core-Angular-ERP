using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class MonitoramentoService : IMonitoramentoService
    {
        public MonitoramentoCliente[] ObterPorParametros(MonitoramentoClienteParameters parameters)
        {
            List<MonitoramentoCliente> retorno = new();

            List<Cliente> listaClientes = this._clienteRepository.ObterPorQuery(new ClienteParameters
            {
                IndAtivo = 1
            })
            .ToList();

            var clientesViaIntegracao =
                this._osRepository.ObterQuery(new OrdemServicoParameters
                {
                    FilterType = OrdemServicoFilterEnum.FILTER_OS_INTEGRACAO_MONITORAMENTO_OUTROS_CLIENTES,
                    Include = OrdemServicoIncludeEnum.OS_INTEGRACAO_MONITORAMENTO
                })
                .AsEnumerable()
                .GroupBy(gr => gr.CodCliente)
                .ToList();

            retorno.AddRange((from os in clientesViaIntegracao
                              select new MonitoramentoCliente
                              {
                                  Nome = listaClientes.FirstOrDefault(s => os.Key == s.CodCliente).NomeFantasia,
                                  DataProcessamento = (DateTime)os.FirstOrDefault().DataHoraAberturaOS
                              })
                            .Distinct()
                            .ToList());

            var dadosBB = this._osRepository.ObterQuery(new OrdemServicoParameters
            {
                FilterType = OrdemServicoFilterEnum.FILTER_OS_INTEGRACAO_MONITORAMENTO_BB,
                Include = OrdemServicoIncludeEnum.OS_INTEGRACAO_MONITORAMENTO
            })
            .ToList();

            // BB Garantia 
            DateTime ultimoChamadoBBGarantia = dadosBB.Where(s => s.EquipamentoContrato.IndGarantia == 1).FirstOrDefault().DataHoraAberturaOS.Value;
            retorno.Add(new MonitoramentoCliente
            {
                Nome = "BB Garantia",
                DataProcessamento = ultimoChamadoBBGarantia
            });

            // BB Cobra 
            var ultimoChamadoBBCobra = dadosBB.Where(s => s.EquipamentoContrato.IndGarantia == 0).FirstOrDefault().DataHoraAberturaOS.Value;
            retorno.Add(new MonitoramentoCliente
            {
                Nome = "BB Cobra",
                DataProcessamento = ultimoChamadoBBCobra
            });

            return retorno.OrderByDescending(s => s.DataProcessamento).ToArray();
        }
    }
}