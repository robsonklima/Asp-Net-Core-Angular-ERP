using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;
namespace SAT.UTILS
{
    public class InstalacaoPleitoPdfHelper : IDocument
    {

        public InstalacaoPleito pleito { get; }

        public InstalacaoPleitoPdfHelper(InstalacaoPleito instalacaoPleito)
        {
            pleito = instalacaoPleito;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        static IContainer CellStyle(IContainer container)
        {
            return container.BorderColor(Colors.Grey.Lighten2).PaddingVertical(3);
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
                    page.MarginVertical(15);
                    page.MarginHorizontal(30);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().AlignRight().Text(x =>
                        {
                            x.CurrentPageNumber().FontSize(8);
                            x.Span(" / ").FontSize(8);
                            x.TotalPages().FontSize(8);
                        });
                    });
                    page.Size(PageSizes.A4.Landscape());
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

            container.Row(row =>
            {
                row.Spacing(20);

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"BORDERÔ SEM NOTA FISCAL").Style(titleStyle);
                    column.Item().Text($"TERMO DE ACEITE ORIGINAL");
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

        void ComposeContent(IContainer container)
        {
            container.Padding(10).Column(column =>
            {
                column.Spacing(1);
                //column.Item().Element(ComporDadosInstalacaoPleito);
                column.Item().Element(ComporTabelaPrincipal);
            });
        }

        void ComporDadosInstalacaoPleito(IContainer container)
        {
            container.Padding(10).Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(15);
                grid.AlignCenter();

                grid.Item(1).Text(t =>
                {
                    t.Span($"Cadastro: ").FontSize(8).Bold();
                    t.Span($"{pleito.DataHoraCad}").FontSize(8);
                });
            });
        }

        void ComporTabelaPrincipal(IContainer container)
        {
            container
                .PaddingTop(20)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(40);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(30);
                    });
                    
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Lote").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Contrato").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("NF Venda").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("NF Remessa").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Prefixo").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Posto").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Agência").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Endereço").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Cidade").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("UF").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Filial").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Equipamento").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Num. Série").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Num. Bem Cliente").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Qtd").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Valor Instalação").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Valor Unitário").FontSize(6).Bold();
                    });

                    pleito.InstalacoesPleitoInstal.ForEach(item =>
                    {
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item?.Instalacao?.InstalacaoLote?.NomeLote).FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item?.Instalacao?.EquipamentoContrato?.Contrato?.NomeContrato).FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.InstalacaoNFVenda?.NumNFVenda}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.NFRemessa}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.LocalAtendimentoIns?.NumAgencia}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.LocalAtendimentoIns?.DCPosto}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.LocalAtendimentoIns?.NomeLocal}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.LocalAtendimentoIns?.Endereco}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.LocalAtendimentoIns?.Cidade?.NomeCidade}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.LocalAtendimentoIns?.Cidade?.UnidadeFederativa?.SiglaUF}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.Filial?.NomeFilial}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.Equipamento?.NomeEquip}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.EquipamentoContrato?.NumSerie}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Instalacao?.EquipamentoContrato?.NumSerieCliente}").FontSize(6);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{1}").FontSize(6);
                        });

                        var valorInstalacao = item?.Instalacao?.EquipamentoContrato?.Contrato?.ContratosEquipamento?
                             .Where(ce => ce?.CodEquip == item?.Instalacao?.CodEquip)
                             .Sum(ce => ce.VlrInstalacao);                        

                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(string.Format("{0:C}", valorInstalacao)).FontSize(6);
                        });

                        var valorUnitario = item?.Instalacao?.EquipamentoContrato?.Contrato?.ContratosEquipamento?
                             .Where(ce => ce?.CodEquip == item?.Instalacao?.CodEquip)
                             .Sum(ce => ce.VlrUnitario);

                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(string.Format("{0:C}", valorUnitario)).FontSize(6);
                        });
                    });
                });
        }
    }
}