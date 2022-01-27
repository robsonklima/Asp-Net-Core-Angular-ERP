import { Component, OnInit } from '@angular/core';
import { VersaoService } from 'app/core/services/versao.service';
import { Versao } from 'app/core/types/versao.types';

@Component({
    selector: 'changelog',
    templateUrl: './changelog.component.html'
})
export class ChangelogComponent implements OnInit
{
    versoes: Versao[] = [];

    constructor (
        private _versaoService: VersaoService
    ) { }

    async ngOnInit()
    {
        const data = await this._versaoService
            .obterPorParametros({ 
                sortDirection: 'desc',
                sortActive: 'codSatVersao',
                pageSize: 10
            })
            .toPromise();

        this.versoes = data.items;
    }
}