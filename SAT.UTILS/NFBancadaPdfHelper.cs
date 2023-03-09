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
        public List<OSBancadaPecas> pecas { get; }

        public Usuario login {get;}

        public NFBancadaPdfHelper(OSBancadaPecas osBancadaPecas,List<OSBancadaPecas> bancadaPecas, Usuario usuario)
        {
            nota = osBancadaPecas;
            pecas = bancadaPecas;
            login = usuario;
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
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

            container.Row(row =>
            {
                row.Spacing(20);

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"Data: ").SemiBold().FontSize(12);
                        text.Span($"{DateTime.Now.ToString("dd/MM/yyyy")}").FontSize(12);
                    });
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
            var total = pecas.Sum(p => (p.NumItemNf * p.PecaRE5114.Peca.ValPeca));

            container.Padding(10).Column(column =>
            {
                column.Spacing(1);
                column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("SOLICITAÇÃO DE NOTA FISCAL").FontSize(10).Bold();
                column.Item().Element(ComporDadosCliente);
                column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("RETORNO DO MATERIAL DE CONSERTO").FontSize(10).Bold();
                column.Item().Element(ComporDadosNota);
                column.Item().Element(ComporTabelaPrincipal);
                column.Item().AlignRight().Text(text =>
                    {
                        text.Span($"Valor Total: ").Bold().FontSize(10);
                        text.Span(string.Format("{0:C}", total)).FontSize(10);
                    });
                column.Item().Element(ComporDadosRequisicao);

            });
        }

        void ComporDadosCliente(IContainer container)
        {
             var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

                container.Row(row =>
                {
                    row.Spacing(20);

                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span($"Nome: ").SemiBold().FontSize(10);
                            text.Span($" {nota.OSBancada.ClienteBancada.NomeCliente}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Endereço: ").SemiBold().FontSize(10);
                            text.Span($"{nota.OSBancada.ClienteBancada.Endereco}, {nota.OSBancada.ClienteBancada.Numero}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Bairro: ").SemiBold().FontSize(10);
                            text.Span($" {nota.OSBancada.ClienteBancada.Bairro}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Cidade: ").SemiBold().FontSize(10);
                            text.Span($"{nota.OSBancada.ClienteBancada.Cidade.NomeCidade}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Cnpj: ").SemiBold().FontSize(10);
                            text.Span($"{nota.OSBancada.ClienteBancada.CNPJ_CGC}").FontSize(10);
                        });
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
                        columns.ConstantColumn(60);
                        columns.RelativeColumn();
                        columns.ConstantColumn(50);
                        columns.ConstantColumn(60);
                        columns.ConstantColumn(60);
                    });

                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Código").FontSize(10).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Descrição").FontSize(10).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Qtd").FontSize(10).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Valor Unit.").FontSize(10).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Valor Total").FontSize(10).Bold();
                    });

                    pecas.ForEach(item =>
                    {
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item?.PecaRE5114?.Peca?.CodMagnus).FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item?.PecaRE5114?.Peca?.NomePeca).FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.NumItemNf}").FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(string.Format("{0:C}", item?.PecaRE5114.Peca.ValPeca)).FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(string.Format("{0:C}", item?.PecaRE5114.Peca.ValPeca * item?.NumItemNf)).FontSize(10);
                        });
                    });
                });
        }
        void ComporDadosRequisicao(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

                container.Row(row =>
                {
                    row.Spacing(20);

                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span($"Requisitante: ").SemiBold().FontSize(10);
                            text.Span($"{login.NomeUsuario}").FontSize(10);
                        });
                    });
                });
        }

        void ComporDadosNota(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

                container.Row(row =>
                {
                    row.Spacing(20);

                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span($"Nota Fiscal de Entrada: N° ").SemiBold().FontSize(10);
                            text.Span($"{nota.OSBancada.Nfentrada}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Data da NF Entrada: ").SemiBold().FontSize(10);
                            text.Span(nota.OSBancada.DataNf != null ? nota.OSBancada?.DataNf.Value.ToString("dd/MM/yyyy") : "").FontSize(10);
                        });
                    });
                });
        }
    }
}