using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;
namespace SAT.UTILS
{
    public class NFBancadaPdfHelper : IDocument
    {

        public OSBancadaPecas nota { get; }

        public NFBancadaPdfHelper(OSBancadaPecas osBancadaPecas)
        {
            nota = osBancadaPecas;
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
        {}

        void ComporDadosInstalacaoPleito(IContainer container)
        {}

        void ComporTabelaPrincipal(IContainer container)
        {}
    }
}