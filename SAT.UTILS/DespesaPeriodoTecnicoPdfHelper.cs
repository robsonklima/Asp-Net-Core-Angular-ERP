using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.ViewModels;

namespace SAT.UTILS
{
    public class DespesaPeriodoTecnicoPdfHelper : IDocument
    {
        private readonly DespesaPeriodoTecnicoImpressaoModel _impressao;

        public DespesaPeriodoTecnicoPdfHelper(
            DespesaPeriodoTecnicoImpressaoModel impressao
        )
        {
            _impressao = impressao;
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
            var totalKM = _impressao.Itens.Sum(p => (p.KmPercorrido));
            var totalValor = _impressao.Itens.Sum(p => (p.DespesaValor));

            container.Column(column =>
            {
                column.Item().Element(ComporInformacoesTecnico);
                column.Item().Element(ComporDespesaItens);
                column.Item().AlignRight().Text(text =>
                {
                    text.Span($"Total KM Percorrido: ").Bold().FontSize(6);
                    text.Span($"{totalKM}").Bold().FontSize(6);
                    text.Span("     ");
                    text.Span($"Valor Total: ").Bold().FontSize(6);
                    text.Span(string.Format("{0:C}", totalValor)).Bold().FontSize(6);
                });
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
                    column.Item().Text($"N° {_impressao.Despesa?.CodDespesaPeriodoTecnico} ").Style(titleStyle);
                    column.Item().Text($"De {_impressao.Despesa?.DespesaPeriodo?.DataInicio.ToString("dd/MM/yyyy")} até {_impressao.Despesa?.DespesaPeriodo?.DataFim.ToString("dd/MM/yyyy")}");
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

                    var tecnico = _impressao.Despesa.Tecnico;

                    var veiculo = _impressao.Despesa.Tecnico?.Veiculos?
                        .OrderByDescending(v => v.CodVeiculoCombustivel).FirstOrDefault();

                    var cartao = _impressao.Despesa.Tecnico?.DespesaCartaoCombustivelTecnico?
                        .OrderByDescending(c => c.CodDespesaCartaoCombustivelTecnico).FirstOrDefault()?.DespesaCartaoCombustivel;

                    var conta = _impressao.Despesa.Tecnico?.TecnicoContas?
                        .OrderByDescending(c => c.CodTecnicoConta).FirstOrDefault();

                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Nome: ").FontSize(6).Bold();
                        t.Span(tecnico?.Nome).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("CPF: ").FontSize(6).Bold();
                        t.Span(tecnico?.Cpf).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Endereço: ").FontSize(6).Bold();
                        t.Span(tecnico?.Endereco).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Veículo: ").FontSize(6).Bold();
                        t.Span(veiculo?.Modelo).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Placa: ").FontSize(6).Bold();
                        t.Span(veiculo?.Placa).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Ano: ").FontSize(6).Bold();
                        t.Span($"{veiculo?.Ano}").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Cartão Combustível: ").FontSize(6).Bold();
                        t.Span(cartao?.Numero).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Filial: ").FontSize(6).Bold();
                        t.Span(tecnico?.Filial?.NomeFilial).FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Centro de Custo: ").FontSize(6).Bold();
                        t.Span("5001").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Banco: ").FontSize(6).Bold();
                        t.Span($"{conta?.NumBanco}").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Agência: ").FontSize(6).Bold();
                        t.Span($"{conta?.NumAgencia}").FontSize(6);
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Conta: ").FontSize(6).Bold();
                        t.Span($"{conta?.NumConta}").FontSize(6);
                    });
                });
        }

        void ComporDespesaItens(IContainer container)
        {
            container
                .PaddingTop(20)
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

                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Data").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Dia").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("OS").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("RAT").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Cliente").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Local Origem").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Local Destino").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("H.I.").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("H.F.").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Km Previsto").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Km Percorrido").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("NF").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Tipo").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Valor").FontSize(6).Bold();
                    });

                    _impressao.Itens.ForEach(item =>
                    {
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.DataHoraSolucao).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.DiaSemana).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item.CodOS}").FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.NumRAT).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.NomeCliente).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.LocalOrigem).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.LocalDestino).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.HoraInicio).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.HoraFim).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item.KmPrevisto}").FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item.KmPercorrido}").FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.NumNF).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item.NomeTipo).FontSize(6);
                            });
                        table.Cell().Background(item.CodDespesaItemAlerta > 0 ? Colors.Grey.Lighten2 : Colors.Grey.Lighten5)
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item.DespesaValor}").FontSize(6);
                            });
                    });
                });
        }

        void ComporFooter(IContainer container)
        {
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

                        table.Cell().ColumnSpan(6).BorderBottom(1).PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Totalização de Despesas").FontSize(8).Bold();
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Aluguel de Carro: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.AluguelCarro)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Outros: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Outros)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Cartão Telefônico: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.CartaoTelefonico)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Passagem Aérea: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.PassagemAerea)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Combustível: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Combustivel)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Peça/Componente: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.PecasComponentes)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Correio: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Correio)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Pedágio: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Pedagio)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Estacionamento: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Estacionamento)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Cartão Combustível: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.CartaoCombustivel)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Ferramentas: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Ferramentas)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Refeição: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Refeicao)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Frete: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Frete)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Táxi: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Taxi)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Hotel: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Hotel)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Telefone: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Telefone)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Ônibus: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Onibus)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Internet: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.Internet)).FontSize(6);
                        });
                    });

                    table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(8).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(100);
                        });

                        table.Cell().ColumnSpan(2).BorderBottom(1).PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Totais").FontSize(8).Bold();
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Despesas: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.DespesaOutros)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Percentual Outros: ").FontSize(6).Bold();
                            t.Span($"{_impressao.PercentualOutros}%").FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Adiantamentos: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.AdiantamentoUtilizado)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Percentual CB: ").FontSize(6).Bold();
                            t.Span($"{_impressao.PercentualDespesaCB}%").FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Receber via Depósito: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.AReceberViaDeposito)).FontSize(6);
                        });
                        table.Cell().BorderBottom(1).PaddingTop(1).AlignLeft().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Crédito via CB: ").FontSize(6).Bold();
                            t.Span(string.Format("{0:C}", _impressao.DespesaKM)).FontSize(6);
                        });
                        table.Cell().ColumnSpan(2).PaddingTop(8).AlignCenter().PaddingBottom(1).Text(t =>
                        {
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

                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("____________________").FontSize(6).Bold();
                        });

                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Data").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Funcionário").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Data").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Administrativo").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Data").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Líder").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Coordenador").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Gerência").FontSize(6);
                        });
                        table.Cell().PaddingTop(1).AlignCenter().PaddingBottom(1).Text(t =>
                        {
                            t.Span("Controladoria").FontSize(6);
                        });
                    });
                });
        }
    }
}
