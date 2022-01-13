using System;
using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DeslocamentoService : IDeslocamentoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;

        public DeslocamentoService(
            IOrdemServicoRepository ordemServicoRepo
        )
        {
            _ordemServicoRepo = ordemServicoRepo;
        }

        public ListViewModel ObterPorParametros(DeslocamentoParameters parameters)
        {
            var deslocamentos = new List<Deslocamento>();
            var deslocamentoParameters = new OrdemServicoParameters() {
                CodTecnico = parameters.CodTecnico,
                Include = OrdemServicoIncludeEnum.OS_INTENCAO,
                DataHoraInicioInicio = parameters.DataHoraInicioInicio,
                DataHoraInicioFim = parameters.DataHoraInicioFim
            };

            deslocamentos.AddRange(
                _ordemServicoRepo.ObterPorParametros(deslocamentoParameters).Select(os => new Deslocamento {
                    Origem = new DeslocamentoOrigem() {
                        Descricao = "Intenção",
                        Lat = os.Intencoes.FirstOrDefault().Latitude,
                        Lng = os.Intencoes.FirstOrDefault().Longitude,
                    },
                    Destino = new DeslocamentoDestino() {
                        Descricao = os.LocalAtendimento.NomeLocal,
                        Lat = Double.Parse(os.LocalAtendimento.Latitude),
                        Lng = Double.Parse(os.LocalAtendimento.Longitude),
                    },
                    Tipo = DeslocamentoTipoEnum.INTENCAO
                })
            );

            var listaPaginada = PagedList<Deslocamento>.ToPagedList(deslocamentos, parameters.PageNumber, parameters.PageSize);
            var lista = new ListViewModel
            {
                Items = listaPaginada,
                TotalCount = listaPaginada.TotalCount,
                CurrentPage = listaPaginada.CurrentPage,
                PageSize = listaPaginada.PageSize,
                TotalPages = listaPaginada.TotalPages,
                HasNext = listaPaginada.HasNext,
                HasPrevious = listaPaginada.HasPrevious
            };

            return lista;
        }
    }
}
