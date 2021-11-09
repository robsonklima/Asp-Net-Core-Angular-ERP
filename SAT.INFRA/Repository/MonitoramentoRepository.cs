using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Collections.Generic;
using System;
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
