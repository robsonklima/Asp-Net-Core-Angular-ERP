using SAT.INFRA.Interfaces;
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

        public List<Indicador> ObterIndicadoresOrdemServico(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new List<Indicador>();
            DateTime dataInicio = new DateTime(DateTime.Now.Year, 4, 1);

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

            switch (parameters.Agrupador)
            {
                case: "CLIENTE"
                var clientes = chamados
                .GroupBy(os => new { os.CodCliente, os.NomeFantasia })
                .Select(os => new { cliente = os.Key, Count = os.Count() });

                    foreach (var cliente in clientes)
                    {
                        Indicadores.Add(new Indicador()
                        {
                            Nome = cliente.cliente.NomeFantasia,
                            Valor = cliente.Count
                        });
                    }

                default:
                    break;
            }

            

            return Indicadores;
        }
    }
}
