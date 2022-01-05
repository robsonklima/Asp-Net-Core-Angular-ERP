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
                && (!os.Equipamento.NomeEquip.Contains("POS") && !os.Equipamento.NomeEquip.Contains("PERTOS")))
                .OrderByDescending(ord => ord.DataHoraAberturaOS)
                .AsEnumerable()
                .GroupBy(gr => gr.CodCliente)
                .ToList();


            var dadosClientes = (from os in clientesViaIntegracao
                                 select new MonitoramentoClienteViewModel()
                                 {
                                     NomeCliente = listaClientes.FirstOrDefault(s => os.Key == s.CodCliente).NomeFantasia,
                                     DataUltimoChamado = (DateTime)os.FirstOrDefault().DataHoraAberturaOS
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
            modelBB.DataUltimoChamado = ultimoChamadoBB;

            retorno.Add(modelBB);

            modelBB = new();

            // BB Cobra 
            ultimoChamadoBB = dadosBB.Where(s => s.EquipamentoContrato.IndGarantia == 0).FirstOrDefault().DataHoraAberturaOS.Value;
            modelBB.NomeCliente = "BB Cobra";
            modelBB.DataUltimoChamado = ultimoChamadoBB;

            retorno.Add(modelBB);

            return retorno.OrderByDescending(s => s.DataUltimoChamado).ToList();
        }
    }
}
