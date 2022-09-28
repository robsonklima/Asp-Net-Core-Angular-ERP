using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;

namespace SAT.UTILS
{
    public class LaudoPdfHelper : IDocument
    {

        public OrdemServico OrdemServico { get; }

        public LaudoPdfHelper(OrdemServico ordemServico)
        {
            OrdemServico = ordemServico;
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

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"RELATÓRIO DE AVALIAÇÃO TÉCNICA").Style(titleStyle);
                    column.Item().Text("DETECÇÃO ANALÍTICA DE FALHAS");
                });

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
                            t.Item().Text($"Perto S.A").SemiBold().FontSize(10);
                            t.Item().Text($"Tecnologia para Bancos e Varejo").FontSize(10);
                            t.Item().Text($"Rua Nissin Castiel, 640 Distrito Industrial").FontSize(10);
                            t.Item().Text($"CEP: 94045-420 | Gravataí | RS | Brasil").FontSize(10);
                            t.Item().Text($"(51) 3489-8700").FontSize(10);
                            t.Item().Text($"www.perto.com.br").FontSize(10);
                        });
                    });
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
                            columns.ConstantColumn(200);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("DADOS DE ENTRADA");
                        });

                        table.Cell().Element(CellStyle).Text("Cliente").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.Cliente?.NomeFantasia).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Contato").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.NomeContato).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Data Atendimento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.DataHoraFechamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("OS Perto").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.CodOS).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Defeito").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.DefeitoRelatado).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Emitente").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.Tecnico?.Nome).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("OS Cliente").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.NumOSCliente).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Cód. Pai do Equipamento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.EquipamentoContrato?.Equipamento?.NomeEquip.Split('-')[1]).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Modelo").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.EquipamentoContrato?.Equipamento?.NomeEquip.Split('-')[0]).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Série").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(OrdemServico.EquipamentoContrato?.NumSerie).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Protocolo STN").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text("---").Style(FontStyle());
                    });
        }

        public void ComporSituacao(IContainer container)
        {
            var laudos = OrdemServico.RelatoriosAtendimento.FirstOrDefault(rel => rel.Laudos.Count() > 0)?.Laudos;
            var laudo = laudos?.FirstOrDefault(l => l.CodLaudoStatus == 2);

            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("SITUAÇÕES").FontSize(10).Bold();

                        });

                        if (laudos is not null)
                        {
                            laudo?.LaudosSituacao.ForEach(sit =>
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

                                            OrdemServico.RelatoriosAtendimento.FirstOrDefault(rel => rel.Laudos.Count() > 0)?.Fotos.OrderByDescending(f => f.DataHoraCad).ToList().ForEach(f =>
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
                                        });
                                    });
                                });
                            });
                        }
                    });
        }

        public void ComporChecagemVisual(IContainer container)
        {
            var laudos = OrdemServico.RelatoriosAtendimento.FirstOrDefault(rel => rel.Laudos.Count() > 0)?.Laudos;
            var laudo = laudos?.FirstOrDefault(l => l.CodLaudoStatus == 2);

            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("CHECAGEM VISUAL").FontSize(10).Bold();

                        });
                        table.Cell().Row(tr =>
                        {
                            tr.RelativeItem().Column(column =>
                            {
                                column.Item().Text($"Relato do cliente durante o atendimento.").SemiBold();
                                column.Item().Element(CellStyle).Text(laudo?.RelatoCliente).Style(FontStyle());

                            });
                        });
                    });
        }

        public void ComporInfraEstruturaSite(IContainer container)
        {
            var laudos = OrdemServico.RelatoriosAtendimento.FirstOrDefault(rel => rel.Laudos.Count() > 0)?.Laudos;
            var laudo = laudos.FirstOrDefault(l => l.CodLaudoStatus == 2);

            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("INFRAESTRUTURA E REDE ELÉTRICA DO SITE").FontSize(10).Bold();

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

                            t2.Cell().Element(CellStyle).AlignMiddle().Text(laudo?.TensaoSemCarga).FontSize(8);
                            t2.Cell().Element(CellStyle).AlignMiddle().Text(laudo?.TensaoComCarga).FontSize(8);
                            t2.Cell().Element(CellStyle).AlignMiddle().Text(laudo?.TensaoTerraENeutro).FontSize(8);
                            t2.Cell().Element(CellStyle).AlignMiddle().Text(laudo?.IndRedeEstabilizada == 1 ? "SIM" : "NÃO").FontSize(8);
                            t2.Cell().Element(CellStyle).AlignMiddle().Text(laudo?.IndPossuiNobreak == 1 ? "SIM" : "NÃO").FontSize(8);
                            t2.Cell().Element(CellStyle).AlignMiddle().Text(laudo?.Temperatura).FontSize(8);
                            t2.Cell().Element(CellStyle).AlignMiddle().Text(laudo?.IndPossuiArCond == 1 ? "SIM" : "NÃO").FontSize(8);
                        });

                    });
        }

        public void ComporConclusao(IContainer container)
        {
            var laudos = OrdemServico.RelatoriosAtendimento.FirstOrDefault(rel => rel.Laudos.Count() > 0)?.Laudos;
            var laudo = laudos.FirstOrDefault(l => l.CodLaudoStatus == 2);

            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("CONCLUSÃO GERAL DO LAUDO").FontSize(10).Bold();

                        });
                        table.Cell().Row(tr =>
                        {
                            tr.RelativeItem().Column(column =>
                            {
                                column.Item().Text($"Conclusão").SemiBold();
                                column.Item().Element(CellStyle).Text(laudo?.Conclusao).Style(FontStyle());

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
                    header.Cell().Element(TitleStyle).Text("ASSINATURA").FontSize(10).Bold();
                });
                table.Cell().Row(tr =>
                {
                    tr.RelativeItem().Column(column =>
                    {
                        column.Item().Grid(grid =>
                        {
                            grid.Spacing(10);
                            grid.Columns(4);

                            OrdemServico.RelatoriosAtendimento.FirstOrDefault(rel => rel.Laudos.Count() > 0)?.Fotos.OrderByDescending(f => f.DataHoraCad).ToList().ForEach(f =>
                            {
                                if (f.NomeFoto.Contains("LAUDO") && f.NomeFoto.Contains("ASSINATURA"))
                                {
                                    using var client = new HttpClient();
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
                        });
                    });
                });
            });
        }
    }
}
