using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Views;

namespace SAT.UTILS
{
    public class DespesaPeriodoTecnicoPdfHelper : IDocument
    {
        private readonly DespesaPeriodoTecnico _despesa;

        private readonly List<ViewDespesaImpressaoItem> _itens;
        private readonly List<DespesaAdiantamentoPeriodo> _adiantamentos;

        public DespesaPeriodoTecnicoPdfHelper(DespesaPeriodoTecnico despesa, List<ViewDespesaImpressaoItem> itens, List<DespesaAdiantamentoPeriodo> adiantamentos)
        {
            _despesa = despesa;
            _itens = itens;
            _adiantamentos = adiantamentos;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        static IContainer CellStyle(IContainer container)
        {
            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
        }

        static IContainer TitleStyle(IContainer container)
        {
            return container.PaddingVertical(5);
        }

        static TextStyle FontStyle()
        {
            return TextStyle.Default.FontSize(8).LineHeight(0.8f);
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(15);
                    page.Header().Element(ComporHeader);
                    page.Content().Element(ComporContent);
                    page.Footer().Element(ComporFooter);
                    page.Size(PageSizes.A4.Landscape());

                });
        }

        public void ComporContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Element(ComporInformacoesTecnico);
                column.Item().Element(ComporDespesaItens);
            });
        }

        void ComporHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

            container.Row(row =>
            {
                row.Spacing(20);

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Relatório de Despesas").Style(titleStyle);
                    column.Item().Text($"De {_despesa?.DespesaPeriodo?.DataInicio.ToString("dd/MM/yyyy")} até {_despesa?.DespesaPeriodo?.DataFim.ToString("dd/MM/yyyy")}");
                });

                row.ConstantItem(280).Column(column =>
                {
                    column.Item().Row(async cr =>
                    {
                        cr.Spacing(20);

                        cr.RelativeItem().Column(t =>
                        {
                            t.Item().AlignRight().Text($"Perto S.A").SemiBold().FontSize(10);
                            t.Item().AlignRight().Text($"Tecnologia para Bancos e Varejo").FontSize(10);
                            t.Item().AlignRight().Text($"www.perto.com.br").FontSize(10);
                        });

                        using (HttpClient webClient = new HttpClient())
                        {
                            byte[] dataArr = await webClient.GetAsync("https://sat.perto.com.br/sat.v2.frontend/assets/images/logo/logo.png")
                                .Result.Content.ReadAsByteArrayAsync();

                            cr.ConstantItem(35).AlignRight().Image(dataArr, ImageScaling.FitArea);
                        }
                    });
                });
            });
        }

        void ComporInformacoesTecnico(IContainer container)
        {
            container
                .PaddingTop(20)
                .Background(Colors.Grey.Lighten5)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(140);
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(100);
                    });

                    var tecnico = _despesa.Tecnico;

                    var veiculo = _despesa.Tecnico?.Veiculos?
                        .OrderByDescending(v => v.CodVeiculoCombustivel).FirstOrDefault();

                    var cartao = _despesa.Tecnico?.DespesaCartaoCombustivelTecnico?
                        .OrderByDescending(c => c.CodDespesaCartaoCombustivelTecnico).FirstOrDefault()?.DespesaCartaoCombustivel;
                    
                    var conta = _despesa.Tecnico?.TecnicoContas?
                        .OrderByDescending(c => c.CodTecnicoConta).FirstOrDefault();

                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Nome: ").FontSize(6).Bold();
                        t.Span(tecnico?.Nome).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("CPF: ").FontSize(6).Bold();
                        t.Span(tecnico?.Cpf).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Endereço: ").FontSize(6).Bold();
                        t.Span(tecnico?.Endereco).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Veículo: ").FontSize(6).Bold();
                        t.Span(veiculo?.Modelo).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Placa: ").FontSize(6).Bold();
                        t.Span(veiculo?.Placa).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Ano: ").FontSize(6).Bold();
                        t.Span($"{veiculo?.Ano}").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Cartão Combustível: ").FontSize(6).Bold();
                        t.Span(cartao?.Numero).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Filial: ").FontSize(6).Bold();
                        t.Span(tecnico?.Filial?.NomeFilial).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Centro de Custo: ").FontSize(6).Bold();
                        t.Span("5001").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Banco: ").FontSize(6).Bold();
                        t.Span($"{conta?.NumBanco}").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Agência: ").FontSize(6).Bold();
                        t.Span($"{conta?.NumAgencia}").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Conta: ").FontSize(6).Bold();
                        t.Span($"{conta?.NumConta}").FontSize(6);
                    });
                });
        }

        void ComporDespesaItens(IContainer container)
        {
            container
                .PaddingTop(20)
                .Background(Colors.Grey.Lighten5)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(35);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(40);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                    });
                    
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Data").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Dia").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("OS").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("RAT").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Cliente").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Local Origem").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Local Destino").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("H.I.").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("H.F.").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Km Previsto").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Km Percorrido").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("NF").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Tipo").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Valor").FontSize(6).Bold();
                    });

                    _itens.ForEach(item =>
                    {
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.DataHoraSolucao).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.DiaSemana).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item.CodOS}").FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.NumRAT).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.NomeCliente).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.LocalOrigem).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.LocalDestino).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.HoraInicio).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.HoraFim).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item.KmPrevisto}").FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item.KmPercorrido}").FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.NumNF).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item.NomeTipo).FontSize(6).Bold();
                        });
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item.DespesaValor}").FontSize(6).Bold();
                        });
                    });
                });
        }

        void ComporFooter(IContainer container)
        {
            decimal valorAluguelCarro = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.ALUGUEL_CARRO).Sum(i => i.DespesaValor);
            decimal valorCorreio = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.CORREIO).Sum(i => i.DespesaValor);
            decimal valorFrete = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.FRETE).Sum(i => i.DespesaValor);
            decimal valorOutros = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.OUTROS).Sum(i => i.DespesaValor);
            decimal valorPedagio = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.PEDAGIO).Sum(i => i.DespesaValor);
            decimal valorTaxi = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.TAXI).Sum(i => i.DespesaValor);
            decimal valorCartaoTelefonico = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.CARTAO_TEL).Sum(i => i.DespesaValor);
            decimal valorEstacionamento = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.ESTACIONAMENTO).Sum(i => i.DespesaValor);
            decimal valorHotel = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.HOTEL).Sum(i => i.DespesaValor);
            decimal valorPassagemAerea = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.PA).Sum(i => i.DespesaValor);
            decimal valorCartaoCombustivel = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.KM).Sum(i => i.DespesaValor);
            decimal valorTelefone = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.TELEFONE).Sum(i => i.DespesaValor);
            decimal valorCombustivel = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.COMBUSTIVEL).Sum(i => i.DespesaValor);
            decimal valorFerramentas = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.FERRAMENTAS).Sum(i => i.DespesaValor);
            decimal valorOnibus = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.ONIBUS).Sum(i => i.DespesaValor);
            decimal valorPecasComponentes = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.PECAS).Sum(i => i.DespesaValor);
            decimal valorRefeicao = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.REFEICAO).Sum(i => i.DespesaValor);
            decimal valorInternet = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.INTERNET).Sum(i => i.DespesaValor);

            container
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                    });

                    table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(8).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(100);
                        });

                        table.Cell().ColumnSpan(6).BorderBottom(1).PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Totalização de Despesas").FontSize(8).Bold();
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Aluguel de Carro: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorAluguelCarro)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Outros: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorOutros)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Cartão Telefônico: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorCartaoTelefonico)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Passagem Aérea: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorPassagemAerea)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Combustível: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorCombustivel)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Peça/Componente: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorPecasComponentes)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Correio: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorCorreio)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Pedágio: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorPedagio)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Estacionamento: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorEstacionamento)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Cartão Combustível: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorCartaoCombustivel)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Ferramentas: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorFerramentas)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Refeição: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorRefeicao)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Frete: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorFrete)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Táxi: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorTaxi)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Hotel: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorHotel)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Telefone: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorTelefone)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Ônibus: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorOnibus)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Internet: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorInternet)).FontSize(6);
                        });
                    });

                    decimal valorDespesaKM = _itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.KM).Sum(i => i.DespesaValor);
                    decimal valorDespesaOutros = _itens.Where(i => i.CodDespesaTipo != (int)DespesaTipoEnum.KM).Sum(i => i.DespesaValor);
                    decimal valorTotalDespesa = valorDespesaOutros + valorDespesaKM;
                    decimal valorAdiantamentoRecebido = _adiantamentos.Sum(i => i.DespesaAdiantamento.ValorAdiantamento);
                    decimal valorAdiantamentoUtilizado = _adiantamentos.Sum(i => i.ValorAdiantamentoUtilizado);
                    decimal valorAReceberViaDeposito = valorDespesaOutros - valorAdiantamentoUtilizado < 0 ? 0 : valorDespesaOutros - valorAdiantamentoUtilizado;
                    decimal saldoAdiantamento = valorTotalDespesa - valorAdiantamentoUtilizado;
                    decimal percentualOutros = Math.Round((valorDespesaOutros / valorTotalDespesa * 100), 2);;
                    decimal percentualDespesaCB = Math.Round((valorDespesaKM / valorTotalDespesa * 100), 2);

                    if (saldoAdiantamento > valorAdiantamentoUtilizado)
                        saldoAdiantamento = valorAdiantamentoUtilizado;

                    table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(8).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(100);
                        });

                        table.Cell().ColumnSpan(2).BorderBottom(1).PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Totais").FontSize(8).Bold();
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Despesas: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorDespesaOutros)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Percentual Outros: ").FontSize(6).Bold();
                            t.Span($"{percentualOutros}%").FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Adiantamentos: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", saldoAdiantamento)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Percentual CB: ").FontSize(6).Bold();
                            t.Span($"{percentualDespesaCB}%").FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Receber via Depósito: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorAReceberViaDeposito)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t => {
                            t.Span("Crédito via CB: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", valorDespesaKM)).FontSize(6);
                        });
                        table.Cell().ColumnSpan(2).PaddingTop(8).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Revisei as despesas deste relatório e estou de pleno acordo com as mesmas.").FontSize(6);
                        });
                    });

                    table.Cell().PaddingTop(4).AlignCenter().PaddingBottom(1).Table(table => 
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("____________________").FontSize(6).Bold();
                        });

                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Data").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Funcionário").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Data").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Administrativo").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Data").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Líder").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Coordenador").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Gerência").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t => {
                            t.Span("Controladoria").FontSize(6);
                        });
                    });
                });
        }
    }
}
