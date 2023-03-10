using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;
namespace SAT.UTILS
{
    public class OrcBancadaPdfHelper : IDocument
    {

        public OsBancadaPecasOrcamento orcamento { get; }
        public List<OrcamentoPecasEspec> pecas { get; }

        public OrcBancadaPdfHelper(OsBancadaPecasOrcamento orcBancada, List<OrcamentoPecasEspec> orcamentoPecasEspec)
        {
            orcamento = orcBancada;
            pecas = orcamentoPecasEspec;
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
                        text.Span($"Filial: ").SemiBold().FontSize(8);
                        text.Span($" FRS").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Endereço: ").SemiBold().FontSize(8);
                        text.Span($" Rua Nissin Castiel, 785 - Distrito Industrial - Gravataí/RS").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Fone: ").SemiBold().FontSize(8);
                        text.Span($" (51) 3489-8824 / 3489-8939").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Cnpj: ").SemiBold().FontSize(8);
                        text.Span($" 92.080.035/0008-72").FontSize(8);
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
            container.Padding(10).Column(column =>
            {
                column.Spacing(1);
                column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("PROPOSTA").FontSize(10).Bold();
                column.Item().Element(ComporDadosProposta);
                column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DADOS DO LOCAL PARA FATURAMENTO").FontSize(10).Bold();
                column.Item().Element(ComporDadosLocal);
                column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("ITENS FORA DE GARANTIA").FontSize(10).Bold();
                column.Item().Element(ComporDadosPeca);
                column.Item().Element(ComporTabelaPrincipal);
                column.Item().AlignRight().Text(text =>
                    {
                        text.Span($"Valor Total: ").Bold().FontSize(10);
                        text.Span(string.Format("{0:C}", orcamento.ValorTotal)).Bold().FontSize(10);
                    });
                column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DEMAIS CONDIÇÕES").FontSize(10).Bold();
                column.Item().Element(ComporCondicoes);
            });
        }

        void ComporDadosPeca(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

            container.Row(row =>
            {
                row.Spacing(20);

                row.ConstantItem(60).Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"RE5114").SemiBold().FontSize(10);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{orcamento.OSBancadaPecas.PecaRE5114.NumRe5114}").FontSize(10);
                    });
                });
                row.ConstantItem(60).Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"Código").SemiBold().FontSize(10);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{orcamento.OSBancadaPecas.PecaRE5114.Peca.CodMagnus}").FontSize(10);
                    });
                });
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"Descrição").SemiBold().FontSize(10);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{orcamento.OSBancadaPecas.PecaRE5114.Peca.NomePeca}").FontSize(10);
                    });
                });
                row.ConstantItem(60).Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"N° Série").SemiBold().FontSize(10);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{orcamento.OSBancadaPecas.PecaRE5114.NumSerie}").FontSize(10);
                    });
                });
            });
        }

        void ComporDadosProposta(IContainer container)
        {
            {
                var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

                container.Row(row =>
                {
                    row.Spacing(20);

                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span($"Número da Proposta: ").SemiBold().FontSize(10);
                            text.Span($" {orcamento.CodOrcamento}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Data Proposta: ").SemiBold().FontSize(10);
                            text.Span($"{DateTime.Now.ToString("dd/MM/yyyy")}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Responsável pelo Proposta: ").SemiBold().FontSize(10);
                            text.Span($" {orcamento.Usuario.NomeUsuario}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"E-mail: ").SemiBold().FontSize(10);
                            text.Span($" reparobalcao@perto.com.br").FontSize(10);
                        });
                    });
                });
            }
        }

        void ComporDadosLocal(IContainer container)
        {
            {
                var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

                container.Row(row =>
                {
                    row.Spacing(20);


                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span($"Razão Social: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.NomeCliente}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Fone: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Telefone}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Endereço: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Endereco}, {orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Numero}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Bairro: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Bairro}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Cidade: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Cidade.NomeCidade}").FontSize(10);
                        });
                    });

                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span($"CNPJ: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.CNPJ_CGC}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"E-mail: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Email}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"Complemento: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Complem}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"CEP: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Cep}").FontSize(10);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span($"UF: ").FontSize(10).Bold();
                            text.Span($"{orcamento.OSBancadaPecas.OSBancada.ClienteBancada.Cidade.UnidadeFederativa.SiglaUF}").FontSize(10);
                        });
                    });
                });
            }
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
                        t.Span("Preço Unit.").FontSize(10).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("%IPI (a Incluir)").FontSize(10).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                        t.Span("Preço Total c/IPI").FontSize(10).Bold();
                    });

                    pecas.ForEach(item =>
                    {
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item?.Peca?.CodMagnus).FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(item?.Peca?.NomePeca).FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span($"{item?.Quantidade}").FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(string.Format("{0:C}", item?.ValorPecaDesconto)).FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(string.Format("{0:C}", item?.PercIpi)).FontSize(10);
                        });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t => {
                            t.Span(string.Format("{0:C}", item?.ValorTotal)).FontSize(10);
                        });
                    });
                });
        }

        void ComporCondicoes(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

            container.Row(row =>
            {
                row.Spacing(20);

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"Validade da Proposta: ").SemiBold().FontSize(8);
                        text.Span($" 30 dias").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Impostos Inclusos: ").SemiBold().FontSize(8);
                        text.Span($" IPI,ISS, Cofins e PIS").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Tipo de Pagamento: ").SemiBold().FontSize(8);
                        text.Span($" Á vista, mediante aprovação").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Prazo de Entrega: ").SemiBold().FontSize(8);
                        text.Span($" 15 dias úteis").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Frete: ").SemiBold().FontSize(8);
                        text.Span($" FOB Gravataí - RS").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Garantia balcão em Gravatai/RS: ").SemiBold().FontSize(8);
                        text.Span($" 90 dias para defeito do serviço e das partes e peças substituídas").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"Observação: ").SemiBold().FontSize(8);
                        text.Span($" Os preços poderão sofrer alterações caso ocorra modificação tributarária.").FontSize(8);
                        text.Span($" Maiores informações contatar o responsável por esta proposta indicada no cabeçalho deste formulário.").FontSize(8);
                        text.Span($" Obrigado por nos contatar e aguardamos seu retorno.").FontSize(8);
                    });
                });
            });
        }
    }
}