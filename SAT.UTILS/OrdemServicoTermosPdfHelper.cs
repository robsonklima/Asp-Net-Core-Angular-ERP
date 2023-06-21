using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;
namespace SAT.UTILS
{
    public class OrdemServicoTermosPdfHelper : IDocument
    {

        public OrdemServico OrdemServico { get; }

        public OrdemServicoTermosPdfHelper(OrdemServico ordemServico)
        {
            OrdemServico = ordemServico;
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
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.PaddingBottom(5).Row(row =>
            {
                row.ConstantItem(280).Column(column =>
                {

                    column.Item().Row(cr =>
                    {
                        cr.Spacing(20);

                        cr.RelativeItem().Column(t =>
                        {
                            t.Item().Text($"Perto S.A").SemiBold().FontSize(8);
                            t.Item().Text($"Tecnologia para Bancos e Varejo").FontSize(8);
                            t.Item().Text($"Rua Nissin Castiel, 640 Distrito Industrial").FontSize(8);
                            t.Item().Text($"CEP: 94045-420 | Gravataí | RS | Brasil").FontSize(8);
                            t.Item().Text($"(51) 3489-8700 - www.perto.com.br").FontSize(8);
                        });
                    });
                });

                row.RelativeItem().Column(column =>
                {
                    column.Item().AlignCenter().AlignMiddle().Text(tx =>
                     {
                         tx.Span("Chamado:  ").Style(TextStyle.Default.FontColor(Colors.Grey.Medium).FontSize(15));
                         tx.Span($"{OrdemServico.CodOS}").Style(TextStyle.Default.FontSize(24));
                     });
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Border(1).BorderColor(Colors.Grey.Lighten1).Padding(10).Column(column =>
            {
                column.Spacing(1);
                column.Item().Element(ComporRelatorios);
            });
        }

        void ComporRelatorios(IContainer container)
        {
            container.BorderTop(1).BorderColor(Colors.Grey.Lighten1).Padding(10).Grid(grid =>
            {
                int index = 1;
                OrdemServico?.RelatoriosAtendimento?.ForEach((rel) =>
                {
                    grid.VerticalSpacing(5);
                    grid.HorizontalSpacing(10);
                    grid.AlignCenter();

                    grid.Item(12).AlignLeft().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Text($"Atendimento  #{rel.NumRAT}").Bold();
                    index++;

                    grid.Item(12).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("Foto").FontSize(10).Bold();
                        });
                        table.Cell().Row(tr =>
                        {
                            tr.RelativeItem().Column(column =>
                            {
                                column.Item().Grid(grid =>
                                {
                                    grid.Spacing(7);
                                    grid.Columns(2);

                                    rel.Fotos.ForEach(f =>
                                        {
                                            if ((f.Modalidade.Contains("TERMO") || f.Modalidade.Contains("NOTA")) && f.NumRAT == rel.NumRAT && f.CodOS == rel.CodOS)
                                            {
                                                using var client = new HttpClient();
                                                var result = client.GetAsync($"https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/{f.NomeFoto}");
                                                grid.Item().Row(gr =>
                                                {
                                                    gr.RelativeItem(6).Column(gc =>
                                                    {
                                                        switch (f.Modalidade)
                                                        {
                                                            case "NOTA_FISCAL":
                                                                gc.Item().AlignCenter().Image(result.Result.Content.ReadAsStream());
                                                                gc.Item().AlignCenter().Text("Nota Fiscal").FontSize(8).SemiBold();
                                                                return;
                                                            case "TERMO_ACEITE":
                                                                gc.Item().AlignCenter().Image(result.Result.Content.ReadAsStream());
                                                                gc.Item().AlignCenter().Text("Termo Aceite").FontSize(8).SemiBold();
                                                                return;
                                                            case "TERMO_ENTREGA":
                                                                gc.Item().AlignCenter().Image(result.Result.Content.ReadAsStream());
                                                                gc.Item().AlignCenter().Text("Termo Entrega").FontSize(8).SemiBold();
                                                                return;
                                                            case "TERMO_RECOLHIMENTO":
                                                                gc.Item().AlignCenter().Image(result.Result.Content.ReadAsStream());
                                                                gc.Item().AlignCenter().Text("Termo Recolhimento").FontSize(8).SemiBold();
                                                                return;
                                                        }
                                                    });
                                                });
                                            }
                                        });
                                });
                            });
                        });
                    });
                });
            });
        }
    }
}