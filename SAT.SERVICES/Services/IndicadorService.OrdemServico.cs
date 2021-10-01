using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private List<Indicador> ObterIndicadorOrdemServico(IndicadorParameters parameters)
        {
            var chamados = ObterOrdensServico(parameters);

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    return ObterIndicadorOrdemServicoCliente(chamados);
                case IndicadorAgrupadorEnum.FILIAL:
                    return ObterIndicadorOrdemServicoFilial(chamados);
                case IndicadorAgrupadorEnum.STATUS_SERVICO:
                    return ObterIndicadorOrdemServicoStatusServico(chamados);
                case IndicadorAgrupadorEnum.TIPO_INTERVENCAO:
                    return ObterIndicadorOrdemServicoTipoIntervencao(chamados);
                case IndicadorAgrupadorEnum.DATA:
                    return ObterIndicadorOrdemServicoData(chamados);
                default:
                    throw new NotImplementedException("Não Implementado");
            }
        }

        private List<Indicador> ObterIndicadorOrdemServicoFilial(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var filiais = chamados
                        .Where(os => os.Filial != null)
                        .GroupBy(os => new { os.CodFilial, os.Filial.NomeFilial })
                        .Select(os => new { filial = os.Key, Count = os.Count() });

            foreach (var filial in filiais)
            {
                Indicadores.Add(new Indicador()
                {
                    Label = filial.filial.NomeFilial,
                    Valor = filial.Count
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoCliente(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var clientes = chamados
                .Where(os => os.Cliente != null)
                .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                .Select(os => new { cliente = os.Key, Count = os.Count() });

            foreach (var cliente in clientes)
            {
                Indicadores.Add(new Indicador()
                {
                    Label = cliente.cliente.NomeFantasia,
                    Valor = cliente.Count
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoData(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var datas = chamados
                        .Where(os => os.DataHoraAberturaOS != null)
                        .GroupBy(os => new { os.DataHoraAberturaOS.Value.Date })
                        .Select(os => new { datas = os.Key, Count = os.Count() });

            foreach (var data in datas)
            {
                Indicadores.Add(new Indicador()
                {
                    Label = data.datas.Date.ToString(),
                    Valor = data.Count
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoStatusServico(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var status = chamados
                         .Where(os => os.Filial != null)
                         .GroupBy(os => new { os.CodStatusServico, os.StatusServico.NomeStatusServico })
                         .Select(os => new { status = os.Key, Count = os.Count() });

            foreach (var st in status)
            {
                Indicadores.Add(new Indicador()
                {
                    Label = st.status.NomeStatusServico,
                    Valor = st.Count
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoTipoIntervencao(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var tipos = chamados
                        .GroupBy(os => new { os.CodTipoIntervencao, os.TipoIntervencao.NomTipoIntervencao })
                        .Select(os => new { tipos = os.Key, Count = os.Count() });

            foreach (var tipo in tipos)
            {
                Indicadores.Add(new Indicador()
                {
                    Label = tipo.tipos.NomTipoIntervencao,
                    Valor = tipo.Count
                });
            }

            return Indicadores;
        }
    }
}
