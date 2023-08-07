using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.UTILS
{
    public class LaudoPdfHelper : IDocument
    {
        public OrdemServico _ordemServico { get; }
        private readonly Laudo _laudo;

        public LaudoPdfHelper(OrdemServico ordemServico, Laudo laudo)
        {
            _ordemServico = ordemServico;
            _laudo = laudo;
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
            container.Page(page =>
            {
                page.Margin(30);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Row(row =>
                {
                    row.RelativeItem().AlignRight().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
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

                row.ConstantItem(280).Column(column =>
                {
                    column.Item().Row(async cr =>
                    {
                        cr.Spacing(20);

                        using (HttpClient webClient = new HttpClient())
                        {
                            byte[] dataArr = await webClient.GetAsync("https://sat.perto.com.br/sat.v2.frontend/assets/images/logo/logo.png").Result.Content.ReadAsByteArrayAsync();

                            cr.ConstantItem(65).AlignMiddle().Image(dataArr, ImageScaling.FitArea);
                        }
                        cr.RelativeItem().Column(t =>
                        {
                            t.Item().Text(Constants.EMP_NOME).SemiBold().FontSize(10);
                            t.Item().Text(Constants.EMP_SLOGAN).FontSize(10);
                            t.Item().Text(Constants.EMP_ENDERECO).FontSize(10);
                            t.Item().Text(Constants.EMP_COMPLEMENTO).FontSize(10);
                            t.Item().Text(Constants.EMP_TELEFONE).FontSize(10);
                            t.Item().Text(Constants.EMP_SITE).FontSize(10);
                        });
                    });
                });

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"RELATÓRIO DE AVALIAÇÃO TÉCNICA").Style(titleStyle);
                    column.Item().Text("DETECÇÃO ANALÍTICA DE FALHAS");
                });
            });
        }

        public void ComposeContent(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                column.Spacing(10);
                column.Item().AlignCenter().Text($"DIRETORIA COMERCIAL - DCM, GERÊNCIA DE SUPORTE E SERVIÇO, {DateTime.Now.ToString("dd/MM/yyyy")}");
                column.Item().Element(ComporDadosEntrada);
                column.Item().Element(ComporSituacao);
                column.Item().Element(ComporChecagemVisual);
                column.Item().Element(ComporInfraEstruturaSite);
                column.Item().Element(ComporConclusao);
                column.Item().Element(ComporAssinatura);

            });
        }

        void ComporDadosEntrada(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                table.Header(header =>
                {
                    header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DADOS DE ENTRADA").FontSize(10).Bold();
                });

                table.Cell().Element(CellStyle).Grid(grid =>
                {
                    grid.VerticalSpacing(1);
                    grid.HorizontalSpacing(15);
                    grid.AlignCenter();

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Cliente: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.Cliente?.NomeFantasia}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Contato: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.NomeContato ?? "Não Informado"}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Data Atendimento: ").FontSize(8).Bold();
                        t.Span($"{_laudo.DataHoraCad}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Chamado Perto: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.CodOS}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Emitente: ").FontSize(8).Bold();
                        t.Span($"{_laudo.Tecnico.Nome}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Chamado Cliente: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.NumOSCliente}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Defeito: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.DefeitoRelatado}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Cód. Pai do Equipamento: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.EquipamentoContrato?.Equipamento?.CodEEquip}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Modelo: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.EquipamentoContrato?.Equipamento?.NomeEquip}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Série: ").FontSize(8).Bold();
                        t.Span($"{_ordemServico.EquipamentoContrato?.NumSerie}").FontSize(8);
                    });

                    grid.Item(6).Text(t =>
                    {
                        t.Span($"Protocolo STN: ").FontSize(8).Bold();
                        t.Span($"---").FontSize(8);
                    });

                    grid.Item(6).Text(t => { });

                });
            });
        }

        public void ComporSituacao(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("SITUAÇÕES").FontSize(10).Bold();
                });

                if (_laudo is not null)
                {
                    _laudo?.LaudosSituacao.ForEach(sit =>
                    {
                        table.Cell().Row(tr =>
                        {
                            tr.RelativeItem().Column(column =>
                            {
                                column.Item().Text($"Causa").FontSize(9).SemiBold();
                                column.Item().Element(CellStyle).Text(sit.Causa).Style(FontStyle());

                                column.Item().Text($"Ação").FontSize(9).SemiBold();
                                column.Item().Element(CellStyle).Text(sit.Acao).Style(FontStyle());

                                column.Item().Text($"Fotos").FontSize(9).SemiBold();

                                column.Item().Grid(grid =>
                                {
                                    grid.Spacing(5);
                                    grid.Columns(6);
                                    using var client = new HttpClient();

                                    foreach (var item in _laudo.Or.RelatoriosAtendimento)
                                    {
                                        item?.Fotos.OrderByDescending(f => f.DataHoraCad).ToList().ForEach(f =>
                                        {
                                            if (f.NomeFoto.Contains("LAUDO") && !f.NomeFoto.Contains("ASSINATURA"))
                                            {
                                                var result = client.GetAsync($"https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/{f.NomeFoto}");
                                                grid.Item().Row(gr =>
                                                {
                                                    gr.RelativeItem().Column(gc =>
                                                    {
                                                        gc.Item().Image(result.Result.Content.ReadAsStream(), ImageScaling.FitWidth);
                                                        gc.Item().Text(f.Modalidade).FontSize(6).SemiBold();
                                                    });
                                                });
                                            }
                                        });
                                    }
                                });
                            });
                        });

                    });
                }
            });
        }

        public void ComporChecagemVisual(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("CHECAGEM VISUAL").FontSize(10).Bold();

                });
                table.Cell().Row(tr =>
                {
                    tr.RelativeItem().Column(column =>
                    {
                        column.Item().Text($"Relato do cliente durante o atendimento.").FontSize(10).SemiBold();
                        column.Item().Element(CellStyle).Text(_laudo?.RelatoCliente).Style(FontStyle());

                    });
                });
            });
        }

        public void ComporInfraEstruturaSite(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("INFRAESTRUTURA E REDE ELÉTRICA DO SITE").FontSize(10).Bold();
                }); table.Cell().Table(t2 =>
                {
                    t2.ColumnsDefinition(col2 =>
                    {
                        col2.RelativeColumn();
                        col2.RelativeColumn();
                        col2.RelativeColumn();
                        col2.RelativeColumn();
                        col2.RelativeColumn();
                        col2.RelativeColumn();
                        col2.RelativeColumn();
                    });

                    t2.Header(h2 =>
                    {
                        h2.Cell().Element(CellStyle).AlignMiddle().Text("Tensão sem carga").Bold().FontSize(8);
                        h2.Cell().Element(CellStyle).AlignMiddle().Text("Tensão com carga").Bold().FontSize(8);
                        h2.Cell().Element(CellStyle).AlignMiddle().Text("Tensão entre terra e neutro").Bold().FontSize(8);
                        h2.Cell().Element(CellStyle).AlignMiddle().Text("Rede estabilizada").Bold().FontSize(8);
                        h2.Cell().Element(CellStyle).AlignMiddle().Text("Possui Nobreak").Bold().FontSize(8);
                        h2.Cell().Element(CellStyle).AlignMiddle().Text("Temperatura").Bold().FontSize(8);
                        h2.Cell().Element(CellStyle).AlignMiddle().Text("Possui ar condicionado").Bold().FontSize(8);
                    });

                    t2.Cell().Element(CellStyle).AlignMiddle().Text(_laudo?.TensaoSemCarga).FontSize(8);
                    t2.Cell().Element(CellStyle).AlignMiddle().Text(_laudo?.TensaoComCarga).FontSize(8);
                    t2.Cell().Element(CellStyle).AlignMiddle().Text(_laudo?.TensaoTerraENeutro).FontSize(8);
                    t2.Cell().Element(CellStyle).AlignMiddle().Text(_laudo?.IndRedeEstabilizada == 1 ? "SIM" : "NÃO").FontSize(8);
                    t2.Cell().Element(CellStyle).AlignMiddle().Text(_laudo?.IndPossuiNobreak == 1 ? "SIM" : "NÃO").FontSize(8);
                    t2.Cell().Element(CellStyle).AlignMiddle().Text(_laudo?.Temperatura).FontSize(8);
                    t2.Cell().Element(CellStyle).AlignMiddle().Text(_laudo?.IndPossuiArCond == 1 ? "SIM" : "NÃO").FontSize(8);
                });

            });
        }

        public void ComporConclusao(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("CONCLUSÃO GERAL DO LAUDO").FontSize(10).Bold();
                });
                table.Cell().Row(tr =>
                {
                    tr.RelativeItem().Column(column =>
                    {
                        column.Item().Text($"Conclusão").FontSize(10).SemiBold();
                        column.Item().Element(CellStyle).Text(_laudo?.Conclusao).Style(FontStyle());

                    });
                });
            });
        }

        public void ComporAssinatura(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("ASSINATURA").FontSize(10).Bold();
                });
                table.Cell().Row(tr =>
                {
                    tr.RelativeItem().Column(column =>
                    {
                        column.Item().Grid(grid =>
                        {
                            grid.Spacing(9);
                            grid.Columns(2);

                            foreach (var item in _laudo.Or.RelatoriosAtendimento)
                            {
                                if(item.CodRAT == _laudo.CodRAT)
                                {
                                    item?.Fotos.OrderByDescending(f => f.DataHoraCad).ToList().ForEach(f =>
                                    {
                                        if (f.NomeFoto.Contains("LAUDO") && f.NomeFoto.Contains("ASSINATURA"))
                                        {
                                            using var client = new HttpClient();
                                            var result = client.GetAsync($"https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/{f.NomeFoto}");
                                            grid.Item().Row(gr =>
                                            {
                                                gr.RelativeItem(6).Column(gc =>
                                                {
                                                    switch (f.Modalidade)
                                                    {
                                                        case "ASSINATURATECNICOLAUDO":
                                                            gc.Item().Border(0.5f).MaxHeight(150).MaxWidth(150).AlignLeft().Image(result.Result.Content.ReadAsStream(), ImageScaling.FitWidth);
                                                            gc.Item().AlignCenter().Text("Assinatura Técnico\n" + item.Tecnico.Nome).FontSize(8).SemiBold();
                                                            return;

                                                        case "ASSINATURACLIENTELAUDO":
                                                            gc.Item().Border(0.5f).MaxHeight(150).MaxWidth(150).AlignLeft().Image(result.Result.Content.ReadAsStream(), ImageScaling.FitWidth);
                                                            gc.Item().AlignCenter().Text("Assinatura Cliente").FontSize(8).SemiBold();
                                                            return;
                                                    }
                                                });
                                            });
                                        }
                                    });
                                }
                            }
                        });
                    });
                });
            });
        }
    }
}
