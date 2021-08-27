using SAT.INFRA.Interfaces;
using SAT.MODELS;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public class IndicadorService : IIndicadorService
    {
        private readonly IOrdemServicoRepository _osRepository;

        public IndicadorService(IOrdemServicoRepository osRepository)
        {
            _osRepository = osRepository;
        }

        public List<Indicador> ObterIndicadores()
        {
            List<Indicador> Indicadores = new List<Indicador>();
            DateTime dataInicio = new DateTime(DateTime.Now.Year, 3, 1);

            var chamados = _osRepository
                .ObterTodos()
                .AsQueryable()
                .Where(os => os.DataHoraAberturaOS >= dataInicio && os.CodStatusServico == 3)
                .Select(os => new {
                    os.CodOS,
                    os.CodCliente,
                    os.Cliente.NomeFantasia,
                    os.CodFilial,
                    os.Filial.NomeFilial,
                    os.CodTipoIntervencao,
                    os.TipoIntervencao,
                    os.CodStatusServico,
                    os.StatusServico.NomeStatusServico,
                    os.RelatoriosAtendimento
                 })
                .ToList();

            var clientes = chamados
                .GroupBy(os => new { os.CodCliente, os.NomeFantasia })
                .Select(os => new { cliente = os.Key, Count = os.Count() });

            var tiposInterv = chamados
                .GroupBy(os => new { os.CodTipoIntervencao, os.TipoIntervencao.NomTipoIntervencao })
                .Select(os => new { tipoInterv = os.Key });

            var statusServicos = chamados
                .GroupBy(os => new { os.CodStatusServico, os.NomeStatusServico })
                .Select(os => new { statusServ = os.Key });

            var filiais = chamados
                .GroupBy(os => new { os.CodFilial, os.NomeFilial })
                .Select(os => new { filial = os.Key });

            //var relatorios = chamados
            //    .Select(os => os.RelatoriosAtendimento);

            //var tecnicos = relatorios.Select(r => r.Distinct().Select(t => t.Tecnico));

            List<Indicador> IndicadoresClientes = new List<Indicador>();

            foreach (var cliente in clientes)
            {
                List<Indicador> IndicadoresTiposInterv = new List<Indicador>();

                foreach (var tipo in tiposInterv)
                {
                    List<Indicador> IndicadoresStatusServ = new List<Indicador>();

                    var qtdTiposServ = chamados.Where(c => c.CodCliente.ToString() == cliente.cliente.CodCliente.ToString() &&
                              c.CodTipoIntervencao.ToString() == tipo.tipoInterv.CodTipoIntervencao.ToString()).Count();

                    foreach (var status in statusServicos)
                    {
                        List<Indicador> IndicadoresFiliais = new List<Indicador>();

                        var qtdStatusServ = chamados.Where(
                              c => c.CodStatusServico.ToString() == status.statusServ.CodStatusServico.ToString() &&
                              c.CodTipoIntervencao.ToString() == tipo.tipoInterv.CodTipoIntervencao.ToString() &&
                              c.CodCliente.ToString() == cliente.cliente.CodCliente.ToString()
                        ).Count();

                        foreach (var filial in filiais)
                        {
                            var qtdFiliais = chamados.Where(
                              c => c.CodStatusServico.ToString() == status.statusServ.CodStatusServico.ToString() &&
                              c.CodTipoIntervencao.ToString() == tipo.tipoInterv.CodTipoIntervencao.ToString() &&
                              c.CodCliente.ToString() == cliente.cliente.CodCliente.ToString() &&
                              c.CodFilial.ToString() == filial.filial.CodFilial.ToString()
                            ).Count();

                            IndicadoresFiliais.Add(new Indicador()
                            {
                                Nome = filial.filial.NomeFilial,
                                Valor = qtdFiliais
                            });
                        }

                        IndicadoresStatusServ.Add(new Indicador()
                        {
                            Nome = status.statusServ.NomeStatusServico,
                            Valor = qtdStatusServ,
                            Filho = IndicadoresFiliais
                        });
                    }

                    IndicadoresTiposInterv.Add(new Indicador()
                    {
                        Nome = tipo.tipoInterv.NomTipoIntervencao,
                        Valor = qtdTiposServ,
                        Filho = IndicadoresStatusServ
                    });
                }

                IndicadoresClientes.Add(new Indicador()
                {
                    Nome = cliente.cliente.NomeFantasia,
                    Valor = cliente.Count,
                    Filho = IndicadoresTiposInterv
                });
            }

            Indicadores.Add(new Indicador()
            {
                Nome = "OS",
                Valor = chamados.Count(),
                Filho = IndicadoresClientes
            });

            return Indicadores;
        }
    }
}
