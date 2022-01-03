using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using static SAT.MODELS.ViewModels.MonitoramentoViewModel;

namespace SAT.INFRA.Repository
{
    public class MonitoramentoRepository : IMonitoramentoRepository
    {
        private readonly AppDbContext _context;

        public MonitoramentoRepository(AppDbContext context)
        {
            this._context = context;
        }
        public List<MonitoramentoClienteViewModel> ObterListaMonitoramentoClientes()
        {
            DateTime hoje = DateTime.Now;
            List<MonitoramentoClienteViewModel> retorno = new();

            List<Cliente> listaClientes = this._context.Cliente.Where(s => s.IndAtivo == 1).ToList();

            var clientesViaIntegracao =
                this._context.OrdemServico
                .Include(s => s.Equipamento)
                .Where(os => os.DataHoraAberturaOS >= hoje.AddDays(-30) &&
                os.CodCliente != 1 && (os.IndServico == 1 || os.IndIntegracao == 1 || os.CodUsuarioCad == "INTEGRACAO" || os.CodUsuarioCad == "INTEGRACAO-SAT")
                // POS
                && (!os.Equipamento.NomeEquip.Contains("POS") && !os.Equipamento.NomeEquip.Contains("PERTOS")))
                .OrderByDescending(ord => ord.DataHoraAberturaOS)
                .AsEnumerable()
                .GroupBy(gr => gr.CodCliente)
                .ToList();


            var dadosClientes = (from os in clientesViaIntegracao
                                 select new MonitoramentoClienteViewModel()
                                 {
                                     NomeCliente = listaClientes.FirstOrDefault(s => os.Key == s.CodCliente).NomeFantasia,
                                     DataUltimoChamado = os.FirstOrDefault().DataHoraAberturaOS.Value.ToString(),
                                     Ociosidade =
                                     string.Format("{0:00}:{1:00}:{2:00}",
                                      (int)hoje.Subtract(os.FirstOrDefault().DataHoraAberturaOS.Value).TotalHours,
                                     hoje.Subtract(os.FirstOrDefault().DataHoraAberturaOS.Value).Minutes,
                                      hoje.Subtract(os.FirstOrDefault().DataHoraAberturaOS.Value).Seconds)
                                 }).Distinct().ToList();

            retorno.AddRange(dadosClientes);


            var dadosBB = this._context.OrdemServico
                .Include(s => s.EquipamentoContrato)
                .Where(s => s.CodCliente == 1 && s.DataHoraAberturaOS >= hoje.AddDays(-30) && s.EquipamentoContrato.IndAtivo == 1)
                .OrderByDescending(g => g.DataHoraAberturaOS).ToList();

            MonitoramentoClienteViewModel modelBB = new();

            // BB Garantia 
            DateTime ultimoChamadoBB = dadosBB.Where(s => s.EquipamentoContrato.IndGarantia == 1).FirstOrDefault().DataHoraAberturaOS.Value;
            modelBB.NomeCliente = "BB Garantia";
            modelBB.DataUltimoChamado = ultimoChamadoBB.ToString();
            modelBB.Ociosidade =
                 string.Format("{0:00}:{1:00}:{2:00}",
                  (int)hoje.Subtract(ultimoChamadoBB).TotalHours,
                 hoje.Subtract(ultimoChamadoBB).Minutes,
                  hoje.Subtract(ultimoChamadoBB).Seconds);

            retorno.Add(modelBB);

            modelBB = new();

            // BB Cobra 
            ultimoChamadoBB = dadosBB.Where(s => s.EquipamentoContrato.IndGarantia == 0).FirstOrDefault().DataHoraAberturaOS.Value;
            modelBB.NomeCliente = "BB Cobra";
            modelBB.DataUltimoChamado = ultimoChamadoBB.ToString();
            modelBB.Ociosidade =
                 string.Format("{0:00}:{1:00}:{2:00}",
                  (int)hoje.Subtract(ultimoChamadoBB).TotalHours,
                 hoje.Subtract(ultimoChamadoBB).Minutes,
                  hoje.Subtract(ultimoChamadoBB).Seconds);

            retorno.Add(modelBB);

            return retorno.OrderByDescending(s => s.Ociosidade).ToList();
        }

        private List<IntegracaoServidorModel> ObterListaGeralMonitoramento()
        {
            string[] notServidores = new string[] { "GD3234", "SAT_INT1", "SAT_APL1" };
            DateTime hoje = DateTime.Now;
            return (from monitoramento in this._context.Monitoramento.Where(m =>
                                                       !m.Servidor.Equals("GD3233") && !m.Item.Equals("Ticket_Log") &&
                                                       !notServidores.Contains(m.Servidor) &&
                                                       (!m.Disco.Equals(@"O:\") || string.IsNullOrWhiteSpace(m.Disco)))
                    group monitoramento
                    by new { monitoramento.Servidor, monitoramento.Item, monitoramento.Mensagem, monitoramento.Disco, monitoramento.Tipo, monitoramento.TamanhoEmGb }
                             into gruposMonitoramento
                    select new IntegracaoServidorModel()
                    {
                        Tipo = gruposMonitoramento.Key.Tipo,
                        Disco = gruposMonitoramento.Key.Disco,
                        TamanhoEmGB = gruposMonitoramento.Key.TamanhoEmGb,
                        Item = gruposMonitoramento.Key.Item,
                        Servidor = gruposMonitoramento.Key.Servidor.Equals("SATAPLPROD") ? "SAT_APL1" : "SAT_INT1",
                        Mensagem = gruposMonitoramento.Max(e => e.EspacoEmGb) == null ? gruposMonitoramento.Key.Mensagem :
                                   $"{gruposMonitoramento.Key.Mensagem} {gruposMonitoramento.Key.Disco} {this._context.Monitoramento.Where(f => f.Disco == gruposMonitoramento.Key.Disco && f.Servidor == gruposMonitoramento.Key.Servidor).OrderByDescending(ord => ord.DataHoraProcessamento).FirstOrDefault().EspacoEmGb} GB",
                        EspacoEmGb = gruposMonitoramento.Max(e => e.EspacoEmGb),
                        DataHoraProcessamento = gruposMonitoramento.Max(e => e.DataHoraProcessamento ?? e.DataHoraCad),
                        DataHoraCad = gruposMonitoramento.Max(e => e.DataHoraCad)
                    }
            ).ToList();
        }

        public MonitoramentoViewModel ObterListaMonitoramento()
        {
            MonitoramentoViewModel monitoramentoView = new();
            string[] tipoServidores = new string[] { "INTEGRACAO", "SERVICO" };

            monitoramentoView.IntegracaoServidor.AddRange(this.ObterListaGeralMonitoramento());

            foreach (IntegracaoServidorModel monitoramento in monitoramentoView.IntegracaoServidor.Where(w => w.Tipo == "STORAGE").ToList())
            {
                decimal valor = 100 - (((decimal)monitoramento.EspacoEmGb.Value / (decimal)monitoramento.TamanhoEmGB.Value) * 100);
                string unidade = monitoramento.Disco.Replace("\\", "");

                if (monitoramento.Servidor.Equals("SAT_APL1"))
                {
                    monitoramentoView.StorageAPL1.Add(new StorageModel() { Unidade = unidade, Valor = valor });
                }
                else if (monitoramento.Servidor.Equals("SAT_INT1"))
                {
                    monitoramentoView.StorageINT1.Add(new StorageModel() { Unidade = unidade, Valor = valor });
                }
            }

            monitoramentoView.StorageAPL1 = monitoramentoView.StorageAPL1.OrderBy(o => o.Unidade).ToList();
            monitoramentoView.StorageINT1 = monitoramentoView.StorageINT1.OrderBy(o => o.Unidade).ToList();
            monitoramentoView.IntegracaoServidor = monitoramentoView.IntegracaoServidor
                .Where(t => tipoServidores.Contains(t.Tipo))
                .OrderBy(o => o.Tipo)
                .ThenBy(i => i.Item)
                .ToList();

            return monitoramentoView;
        }
    }
}
