using System.Net;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;

namespace SAT.UTILS
{
    public class OrcamentoPdfHelper : IDocument
    {

        public Orcamento Orcamento { get; }

        public OrcamentoPdfHelper(Orcamento orcamento)
        {
            Orcamento = orcamento;
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
            return TextStyle.Default.FontSize(10).LineHeight(0.8f);
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
                        row.ConstantItem(350).AlignLeft().Text(x =>
                        {
                            x.Span("Os valores deste orçamento são apenas uma previsão. O efetivo faturamento será efetuado de acordo com os serviços executados e comprovados em Relatórios Técnicos, assinados pelo cliente.").FontSize(8);
                            x.EmptyLine();
                            x.Span("*O valor de desconto a base de troca está condicionado ao recolhimento da peça substituída").FontSize(8);
                        });
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
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"ORÇAMENTO DE SERVIÇOS EXTRAS").Style(titleStyle);
                    column.Item().Text(text =>
                    {
                        text.Span($"{Orcamento.Filial.NomeFilial} - ");
                        text.Span($"{Orcamento.Filial.Endereco}");
                    });
                    column.Item().Text($"{Orcamento.Filial.Fone}");
                    column.Item().Text(text =>
                    {
                        text.Span($"{Orcamento.Numero} - ").SemiBold();
                        text.Span($"{DateTime.Now.ToString("dd/MM/yyyy")}");
                    });
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

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(10);

                column.Item().Element(ComporDadosLocalFaturamento);
                column.Item().Element(ComporDadosLocalEnvioNf);
                column.Item().Element(ComporDadosLocalAtendimento);
                column.Item().Element(ComporMaoDeObra);
                column.Item().Element(ComporDeslocamento);
                column.Item().Element(ComporMaterialUtilizado);
                column.Item().Element(ComporOutrosServicos);
                column.Item().Element(ComporDescontos);
                column.Item().Element(ComporTotal);
                column.Item().Element(ComporCondicoes);

            });
        }

        void ComporDadosLocalFaturamento(IContainer container)
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
                            header.Cell().Element(TitleStyle).Text("DADOS DO LOCAL PARA FATURAMENTO");
                        });

                        table.Cell().Element(CellStyle).Text("Razão Social").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.NomeFantasia).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("CNPJ").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.Cnpj).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("I.E").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.InscricaoEstadual).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Responsável").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ResponsavelFaturamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("E-mail").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EmailFaturamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Fone").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.FoneFaturamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Endereço").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EnderecoFaturamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Número").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.NumeroFaturamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Complemento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ComplementoFaturamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Bairro").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.BairroFaturamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Cidade").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeFaturamento?.NomeCidade).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("UF").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeFaturamento?.UnidadeFederativa.SiglaUF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("CEP").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CepFaturamento).Style(FontStyle());

                    });
        }

        void ComporDadosLocalEnvioNf(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(250);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("DADOS DO LOCAL PARA ENVIO DE NOTA FISCAL");
                        });

                        table.Cell().Element(CellStyle).Text("Razão Social").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.NomeFantasia).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("CNPJ").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.Cnpj).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("I.E").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.InscricaoEstadual).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Responsável").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ResponsavelEnvioNF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("E-mail").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EmailEnvioNF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Fone").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.FoneEnvioNF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Endereço").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EnderecoEnvioNF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Número").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.NumeroEnvioNF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Complemento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ComplementoEnvioNF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Bairro").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.BairroEnvioNF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Cidade").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeEnvioNF?.NomeCidade).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("UF").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeEnvioNF?.UnidadeFederativa.SiglaUF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("CEP").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CepEnvioNF).Style(FontStyle());
                    });
        }

        void ComporDadosLocalAtendimento(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(320);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("DADOS DO LOCAL DE ATENDIMENTO/OCORRÊNCIA");
                        });

                        table.Cell().Element(CellStyle).Text("Nro Contrato").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.EquipamentoContrato?.Contrato?.NroContrato).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Série").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.EquipamentoContrato?.NumSerie).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Modelo").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Equipamento?.NomeEquip).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("OS Cliente").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.NumOSCliente).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Os Perto").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.CodOS).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Agência").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text($"{Orcamento.OrdemServico.LocalAtendimento.NumAgencia}/{Orcamento.OrdemServico.LocalAtendimento.DCPosto}").Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Nome Local").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.NomeLocal).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Bairro").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Bairro).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Endereço").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Endereco).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Número").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.NumeroEnd).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Complemento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.EnderecoComplemento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Cidade").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Cidade?.NomeCidade).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("UF").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Cidade?.UnidadeFederativa?.SiglaUF).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("CEP").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento?.Cep).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Motivo").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoMotivo?.Descricao).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Detalhes").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.Detalhe).Style(FontStyle());
                    });
        }

        void ComporMaoDeObra(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("MÃO DE OBRA");
                        });

                        table.Cell().Element(CellStyle).Text("Hora Técnica").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.MaoDeObra.ValorHoraTecnica).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Previsão de Horas").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.MaoDeObra.PrevisaoHoras).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Total").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.MaoDeObra.ValorTotal).Style(FontStyle().Bold());
                    });
        }

        void ComporDeslocamento(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("DESLOCAMENTO");


                        });

                        table.Cell().Element(CellStyle).Text("Unitário KM Rodado").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorUnitarioKmRodado).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("KM").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.QuantidadeKm).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Percurso").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text("Ida e Volta").Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Total KM Rodado").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorTotalKmRodado).Style(FontStyle().Bold());
                        table.Cell().Element(CellStyle).Text("Hora em Deslocamento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorHoraDeslocamento).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Qtd 1h a cada 65km rodados").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.QuantidadeHoraCadaSessentaKm).Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Total KM Deslocamento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorTotalKmDeslocamento).Style(FontStyle().Bold());
                    });
        }

        void ComporMaterialUtilizado(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("MATERIAL A SER UTILIZADO");

                        });

                        if (Orcamento.Materiais.Any())
                        {
                            table.Cell().Table(t2 =>
                            {
                                t2.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(160);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(180);
                                    columns.RelativeColumn();
                                });
                                t2.Header(h2 =>
                                {
                                    h2.Cell().Element(CellStyle).Text("Código").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Descrição").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Qtd").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Unitário").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Desconto a base de troca*").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Total").Style(FontStyle());
                                });

                                Orcamento.Materiais.ForEach(mat =>
                                {
                                    t2.Cell().Element(CellStyle).Text(mat.CodigoMagnus).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(mat.Descricao).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(mat.Quantidade).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(mat.ValorUnitario).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(mat.ValorDesconto).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(mat.ValorTotal).Style(FontStyle().Bold());
                                });
                            });
                        }
                        else
                        {
                            table.Cell().Text("Nenhum material cadastrado neste orçamento");
                        }
                    });
        }

        void ComporOutrosServicos(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("OUTROS SERVIÇOS");


                        });

                        if (Orcamento.OutrosServicos.Any())
                        {
                            table.Cell().Table(t2 =>
                            {
                                t2.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(200);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                t2.Header(h2 =>
                                {
                                    h2.Cell().Element(CellStyle).Text("Descrição").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Unitário").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Qtd").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Total").Style(FontStyle());
                                });

                                Orcamento.OutrosServicos.ForEach(serv =>
                                {
                                    t2.Cell().Element(CellStyle).Text(serv.Descricao).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(serv.ValorUnitario).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(serv.Quantidade).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(serv.ValorTotal).Style(FontStyle().Bold());
                                });
                            });
                        }
                        else
                        {
                            table.Cell().Text("Nenhum serviço adicional cadastrado neste orçamento");
                        }
                    });
        }

        void ComporDescontos(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("DESCONTOS");


                        });

                        if (Orcamento.Descontos.Any())
                        {
                            table.Cell().Table(t2 =>
                            {
                                t2.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(100);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();

                                });
                                t2.Header(h2 =>
                                {
                                    h2.Cell().Element(CellStyle).Text("Motivo").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Tipo").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Total").Style(FontStyle());

                                });

                                Orcamento.Descontos.ForEach(desc =>
                                {
                                    t2.Cell().Element(CellStyle).Text(desc.Motivo).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(desc.NomeTipo).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(desc.ValorTotal).Style(FontStyle()).Bold();
                                });
                            });
                        }
                        else
                        {
                            table.Cell().Text("Nenhum desconto cadastrado neste orçamento");
                        }
                    });
        }

        void ComporTotal(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("TOTAL");
                        });

                        table.Cell().Element(CellStyle).Text("Descontos").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.ValorTotalDesconto).Style(FontStyle().Bold());
                        table.Cell().Element(CellStyle).Text("ValorTotal").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.ValorTotal).Style(FontStyle().Bold());
                    });
        }

        void ComporCondicoes(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("CONDIÇÕES");
                        });

                        table.Cell().Element(CellStyle).Text("Validade Orçamento").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text("3 dias").Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Impostos Inclusos").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text("Sim").Style(FontStyle());
                    });
        }
    }
}

