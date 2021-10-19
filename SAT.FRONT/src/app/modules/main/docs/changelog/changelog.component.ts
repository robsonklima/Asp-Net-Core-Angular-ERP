import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'changelog',
    templateUrl: './changelog.component.html',
    styles: [''],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChangelogComponent
{
    changelog: any[] = [
        {
            version: 'v1.0.0',
            releaseDate: '01 de Março de 2021'
        },
        {
            version: 'v1.0.1',
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
        }
    ];

    constructor () { }
}