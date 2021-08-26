using SAT.INFRA.Interfaces;
using SAT.MODELS;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public class IndicadorService : IIndicadorService
    {
        private readonly IOrdemServicoRepository _osRepository;
        
        public IndicadorService(
            IOrdemServicoRepository osRepository
        )
        {
            _osRepository = osRepository;
        }

        public List<Indicador> ObterIndicadoresClientes()
        {
            List<Indicador> Indicadores = new List<Indicador>();
            //DateTime dataInicio = new DateTime(DateTime.Now.Year, 1, 1);

            //var chamados = _osRepository.ObterTodos().AsQueryable().Where(os => os.DataHoraAberturaOS >= dataInicio);
            //List<ClienteService> clientes = chamados.Select(os => os.Cliente).Distinct().ToList();

            //foreach (var cliente in clientes)
            //{
            //    var qtdOS = chamados.Where(os => os.CodCliente == cliente.CodCliente).Count();

            //    Indicadores.Add(new Indicador()
            //    {
            //        Cliente = cliente.NomeFantasia,
            //        QtdOS = qtdOS,
            //    });
            //}

            return (Indicadores);
        }
    }
}
