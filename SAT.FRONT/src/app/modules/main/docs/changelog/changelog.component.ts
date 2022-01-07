import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'changelog',
    templateUrl: './changelog.component.html',
    styles: [''],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChangelogComponent implements OnInit
{
    changelog: any[] = [];

    constructor () { }

    ngOnInit(): void
    {
        this.changelog = [
            {
                version: '1.1.26',
                releaseDate: '06 de Janeiro de 2022',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Novo monitoramento com espaço em disco e memoria dos servidores',
                            'Corrigido erro de atualização da OS, equipamento nulo',
                            'Adicionada opção de bloqueio de chamado'
                        ],
                    }
                ]
            },
            {
                version: '1.1.25',
                releaseDate: '04 de Janeiro de 2022',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Captura de erros do frontend enviados por e-mail'
                        ],
                    }
                ]
            },
            {
                version: '1.1.24',
                releaseDate: '04 de Janeiro de 2022',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Corrigido erro de inserção de chamado que não consistia o modelo do equipamento'
                        ],
                    }
                ]
            },
            {
                version: '1.1.23',
                releaseDate: '04 de Janeiro de 2022',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Monitoramento das Integrações na tela de boas vindas'
                        ],
                    }
                ]
            },
            {
                version: '1.1.22',
                releaseDate: '31 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajuste no login'
                        ],
                    }
                ]
            },
            {
                version: '1.1.21',
                releaseDate: '29 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajuste na exportação de chamados'
                        ],
                    }
                ]
            },
            {
                version: '1.1.20',
                releaseDate: '29 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Correções Autenticação por Duas Etapas',
                            'Ajuste layout lista de chamados'
                        ],
                    }
                ]
            },
            {
                version: '1.1.19',
                releaseDate: '29 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Performance da Agenda'
                        ],
                    }
                ]
            },
            {
                version: '1.1.18',
                releaseDate: '28 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Tutoriais',
                            'Formulário de Suporte'
                        ],
                    }
                ]
            },
            {
                version: '1.1.17',
                releaseDate: '28 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Novo layout de exibição do relatório de atendimento nos detalhes no chamado',
                            'Ajuste no formulário da RAT'
                        ],
                    }
                ]
            },
            {
                version: '1.1.16',
                releaseDate: '27 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Inclusão de uma hora a mais no inicio da janela da agenda (6:00)',
                            'Inclusão do relato da solução digitado pelo técnico no histórico do chamado, em detalhes',
                            'Inclusão dos detalhes e peças na aba relatórios em detalhes do chamado'
                        ],
                    },
                    {
                        type: 'Correções',
                        list: [
                            'Correção do bug de redirecionamento quando o usuário clicava na notificação',
                            'Ao cancelar transferência do chamado sistema assume último status ao qual o chamado foi atribuído e não último Status do RAT'
                        ],
                    }
                ]
            },
            {
                version: '1.1.15',
                releaseDate: '27 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Otimização na edição da RAT',
                            'Otimização nos detalhes do chamado',
                            'Fotos das RATs'
                        ],
                    }
                ]
            },
            {
                version: '1.1.14',
                releaseDate: '27 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes Agenda Técnico',
                            'Serviço de AutoReload',
                            'Alteração de expiração do token',
                            'Edição das fotos da RAT',
                            'Melhora da performance de carregamento do formulario do RAT',
                            'Melhora performance da Agenda'
                        ],
                    }
                ]
            },
            {
                version: '1.1.13',
                releaseDate: '23 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Autenticação de dois passos',
                            'E-mail da Ordem de Serviço'
                        ],
                    }
                ]
            },
            {
                version: '1.1.12',
                releaseDate: '23 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Novos filtros na OS'
                        ],
                    },
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes Agenda Técnico'
                        ],
                    }
                ]
            },
            {
                version: '1.1.11',
                releaseDate: '21 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes no filtro de OS',
                            'Otimização de desempenho da Agenda Técnico'
                        ],
                    }
                ]
            },
            {
                version: '1.1.9',
                releaseDate: '20 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes no filtro',
                            'Ajustes no layout na tela de detalhes do chamado',
                        ],
                    }
                ]
            },
            {
                version: '1.1.8',
                releaseDate: '17 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes na Agenda Técnico',
                            'Ajustes na tela de detalhes do chamado',
                        ],
                    },
                    {
                        type: 'Adições',
                        list: [
                            'Ordenação de chamados na Agenda Técnico'
                        ],
                    }
                ]
            },
            {
                version: '1.1.7',
                releaseDate: '16 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes na tela de contratos',
                        ],
                    },
                    {
                        type: 'Adições',
                        list: [
                            'Layout da Agenda Técnico'
                        ],
                    }
                ]
            },
            {
                version: '1.1.6',
                releaseDate: '14 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Modificação da Agenda',
                            'Alertas nos chamados',
                            'Sistema de notificações'
                        ],
                    }
                ]
            },
            {
                version: '1.1.5',
                releaseDate: '08 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Modificação do Layout dos chamados',
                            'Ajustes nos filtros'
                        ],
                    }
                ]
            },
            {
                version: '1.1.4',
                releaseDate: '07 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Ponto na Agenda'
                        ],
                    }
                ]
            },
            {
                version: '1.1.3',
                releaseDate: '06 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Histórico Agenda Técnico',
                            'Filtro Tipo Equipamento',
                        ],
                    }
                ]
            },
            {
                version: '1.1.2',
                releaseDate: '04 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes na tela de contratos',
                            'Ajustes na tela de créditos cartão',
                        ],
                    },
                    {
                        type: 'Adições',
                        list: [
                            'Módulo de ponto',
                            'Módulo de instalação'
                        ],
                    }
                ]
            },
            {
                version: '1.1.1',
                releaseDate: '03 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajustes no filtro de chamados da agenda',
                            'Ajustes na tela de contratos',
                        ],
                    },
                    {
                        type: 'Adições',
                        list: [
                            'Módulo de despesas'
                        ],
                    }
                ]
            },
            {
                version: '1.1.0',
                releaseDate: '01 de Dezembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Agendamento da OS',
                        ],
                    },
                    {
                        type: 'Adições',
                        list: [
                            'Filtro de chamados na Agenda Técnico',
                            'Realocação de chamados',
                            'Validação de horário retroativo no agendamento da OS',
                            'Histórico de agendamentos da OS'
                        ],
                    }
                ]
            },
            {
                version: '1.0.9',
                releaseDate: '16 de Novembro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Filtro tela de chamados',
                        ],
                    }
                ]
            },
            {
                version: '1.0.8',
                releaseDate: '10 de Novembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Atualização de Dashboard',
                            'Inclusão de módulos de ponto e despesa',
                        ],
                    }
                ]
            },
            {
                version: '1.0.7',
                releaseDate: '03 de Novembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Carregamento de chamados da filial do perfil de usuário',
                            'Manter filtro de filial após limpar filtros',
                        ],
                    }
                ]
            },
            {
                version: '1.0.6',
                releaseDate: '01 de Novembro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Filtro por filial na Agenda Técnico',
                            'Ajustes na pesquisa de técnicos da RAT',
                        ],
                    }
                ]
            },
            {
                version: '1.0.5',
                releaseDate: '29 de Outubro de 2021',
                changes: [
                    {
                        type: 'Correções',
                        list: [
                            'Ajuste no filtro de técnico da RAT',
                            'Ajustes na impressão de PDF',
                            'Ajustes na Agenda Técnico'
                        ],
                    }
                ]
            },
            {
                version: '1.0.4',
                releaseDate: '26 de Outubro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Impressão da OS',
                            'Filtro na agenda técnicos',
                            'Arrastar para transferir na agenda técnicos'
                        ],

                    },
                    {
                        type: 'Correções',
                        list: [
                            'Melhorias no filtro por equipamento na listagem de OS'
                        ],
                    }
                ]
            },
            {
                version: '1.0.3',
                releaseDate: '20 de Outubro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Mapa de roteirização'
                        ],

                    },
                    {
                        type: 'Correções',
                        list: [
                            'Melhoria performance Agenda Técnicos'
                        ],
                    }
                ]
            },
            {
                version: '1.0.2',
                releaseDate: '19 de Outubro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Técnico padrão na RAT quando o chamado estiver no status transferido'
                        ],

                    },
                    {
                        type: 'Correções',
                        list: [
                            'Filtro de equipamento por texto'
                        ],
                    }
                ]
            },
            {
                version: '1.0.1',
                releaseDate: '19 de Outubro de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Opção de cancelamento de transferência de chamado',
                            'Filtro pelo técnico na tela de chamados',
                            'Informações sobre o técnico ao passar o mouse sobre a coluna',
                            'Status de peças faltantes ao passar o mouse sobre a coluna',
                            'Coluna de status de SLA',
                        ],

                    },
                    {
                        type: 'Correções',
                        list: [
                            'Mantém filtro na tela de chamados',
                            'Nome do técnico apenas para chamados transferidos',
                            'Validação da RAT'
                        ],
                    }
                ]
            },
            {
                version: '1.0.0',
                releaseDate: '01 de Março de 2021',
                changes: [
                    {
                        type: 'Adições',
                        list: [
                            'Criação do sistema',
                        ],

                    },
                ]
            }
        ];
    }
}