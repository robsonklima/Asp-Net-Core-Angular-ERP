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
            container.PaddingBottom(5).Row(row =>
            {
                row.ConstantItem(280).Column(column =>
                {

                    column.Item().Row(async cr =>
                    {
                        cr.Spacing(20);

                        using (HttpClient webClient = new HttpClient())
                        {
                            byte[] dataArr = await webClient
                                .GetAsync("https://sat.perto.com.br/sat.v2.frontend/assets/images/logo/logo.png")
                                .Result.Content
                                .ReadAsByteArrayAsync();

                            cr.ConstantItem(60).AlignMiddle().Image(dataArr, ImageScaling.FitArea);
                        } 
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
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Padding(10).Column(column =>
            {
                column.Spacing(1);
                column.Item().Element(ComporDadosInstalacaoPleito);
            });
        }

        void ComporDadosInstalacaoPleito(IContainer container)
        {
            container.Padding(10).Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(15);
                grid.AlignCenter();

                grid.Item(4).Text(t =>
                {
                    t.Span($"Cadastro: ").FontSize(8).Bold();
                    t.Span($"{pleito.DataHoraCad}").FontSize(8);
                });
            });
        }
    }
}