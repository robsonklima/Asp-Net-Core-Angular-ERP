using System;
using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoService : IIntegracaoService
    {
        private IIntegracaoRepository _integracaoRepo;
        private IEquipamentoContratoRepository _equipamentoContratoRepo;

        public IntegracaoService(
            IIntegracaoRepository integracaoRepo,
            IEquipamentoContratoRepository equipamentoContratoRepo
        )
        {
            _integracaoRepo = integracaoRepo;
            _equipamentoContratoRepo = equipamentoContratoRepo;
        }

        public Integracao Criar(Integracao ordem)
        {
            return _integracaoRepo.Criar(ordem);
        }

        public IntegracaoViewModel ConsultarEquipamentos(IntegracaoParameters parameters)
        {
            if (!parameters.CodCliente.HasValue)
                throw new Exception("Favor informar o código do cliente");

            var retorno = new IntegracaoViewModel
            {
                Equipamentos = MontaEquipamentosContrato(parameters)    
            };

            return retorno;
        }

        private List<IntegracaoEquipamentoContrato> MontaEquipamentosContrato(IntegracaoParameters parameters) {
            var equipamentos = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters {
                CodClientes = parameters.CodCliente.ToString(),
                NumSerie = parameters.NumSerie,
                NomeLocal = parameters.NomeLocal,
                IndAtivo = 1
            });

            return equipamentos
                .Select(x => new IntegracaoEquipamentoContrato
                { 
                    NumSerie = x.NumSerie, LocalAtendimento = new IntegracaoLocalAtendimento { 
                        NomeLocal = x.LocalAtendimento.NomeLocal,
                        NumAgencia = x.LocalAtendimento.NumAgencia,
                        DCPosto = x.LocalAtendimento.DCPosto,
                        Endereco = x.LocalAtendimento.Endereco,
                        Numero = x.LocalAtendimento.NumeroEnd,
                        Cidade = x.LocalAtendimento.Cidade.NomeCidade,
                        Bairro = x.LocalAtendimento.Bairro,
                        Estado = x.LocalAtendimento.Cidade.UnidadeFederativa.NomeUF
                    }
                })
                .ToList();
        }

        public List<Integracao> ConsultarOrdensServico(IntegracaoParameters parameters)
        {
            if (!parameters.CodCliente.HasValue)
                throw new Exception("Favor informar o código do cliente");

            return _integracaoRepo.ObterPorParametros(parameters);
        }
    }
}
