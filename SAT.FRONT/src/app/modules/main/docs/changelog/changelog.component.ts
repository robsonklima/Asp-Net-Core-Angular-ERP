import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector       : 'changelog',
    templateUrl    : './changelog.component.html',
    styles         : [''],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChangelogComponent
{
    changelog: any[] = [
        {
            version    : 'v1.0.0',
            releaseDate: '01 de Março de 2021',
            changes    : [
                {
                    type: 'Adições',
                    list: [
                        'Exemplo de texto que descreve a funcionalidade citada'
                    ]
                }
            ]
        }
    ];

    constructor() {}
}
