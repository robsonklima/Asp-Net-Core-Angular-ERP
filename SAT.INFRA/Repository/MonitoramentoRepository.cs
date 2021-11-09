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
            return null;
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
