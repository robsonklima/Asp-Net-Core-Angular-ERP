using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepo;
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly IEmailService _emailService;

        public OrcamentoService(
            IOrcamentoRepository orcamentoRepo,
            IOrdemServicoRepository ordemServicoRepo,
            IEmailService emailService
        ) {
            _orcamentoRepo = orcamentoRepo;
            _ordemServicoRepo = ordemServicoRepo;
            _emailService = emailService;
        }

        public ListViewModel ObterPorParametros(OrcamentoParameters parameters)
        {
            var orcamentos = _orcamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = orcamentos,
                TotalCount = orcamentos.TotalCount,
                CurrentPage = orcamentos.CurrentPage,
                PageSize = orcamentos.PageSize,
                TotalPages = orcamentos.TotalPages,
                HasNext = orcamentos.HasNext,
                HasPrevious = orcamentos.HasPrevious
            };

            return lista;
        }

        public Orcamento Criar(Orcamento orcamento)
        {
            _orcamentoRepo.Criar(orcamento);
            return orcamento;
        }

        public void Deletar(int codigo)
        {
            _orcamentoRepo.Deletar(codigo);
        }

        public Orcamento Atualizar(Orcamento orcamento)
        {
            _orcamentoRepo.Atualizar(orcamento);
            return orcamento;
        }

        public Orcamento ObterPorCodigo(int codigo)
        {
            return _orcamentoRepo.ObterPorCodigo(codigo);
        }

        public OrcamentoAprovacao Aprovar(OrcamentoAprovacao aprovacao)
        {
            var orcamento = _orcamentoRepo.ObterPorCodigo(aprovacao.CodOrc);
            
            if (aprovacao.IsAprovado == true) {
                orcamento.CodigoStatus = (int)OrcStatusEnum.PENDENTE_APROVACAO_FORNECEDOR;
                orcamento.DataAprovacaoCliente = DateTime.Now;
            }
            else
                orcamento.CodigoStatus = (int)OrcStatusEnum.REPROVADO;

            orcamento.DescricaoOutroMotivo = aprovacao.Motivo;
            _orcamentoRepo.Atualizar(orcamento);

            var os = _ordemServicoRepo.ObterPorCodigo((int)orcamento.CodigoOrdemServico);
            os.ObservacaoCliente = $@"{(aprovacao.IsAprovado == true ? "Aprovado" : "Reprovado")} / { aprovacao.Nome } / { aprovacao.Email } / 
                { aprovacao.Departamento } / { aprovacao.Telefone } / { aprovacao.Ramal } - { os.ObservacaoCliente }";

            _ordemServicoRepo.Atualizar(os);
            
            string[] destinatarios = { "dss.orcamentos@perto.com.br" };

            _emailService.Enviar(new Email {
                EmailDestinatarios = destinatarios,
                Assunto = $"Perto - Orçamento Nro { orcamento.Numero } da OS { os.CodOS }",
                Corpo = $@"Enviado por { aprovacao.Nome }, departamento: { aprovacao.Departamento }, email:  { aprovacao.Email }, 
                    motivo: { aprovacao.Motivo }, fone: { aprovacao.Telefone }, ramal: { aprovacao.Ramal }",
            });

            return aprovacao;
        }
    }
}
