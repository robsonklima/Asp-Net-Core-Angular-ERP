import { Component, OnInit } from '@angular/core';
import { VersaoService } from 'app/core/services/versao.service';
import { Versao } from 'app/core/types/versao.types';

@Component({
    selector: 'versoes',
    templateUrl: './versoes.component.html'
})
export class VersoesComponent implements OnInit
{
    versoes: Versao[] = [];
    isLoading: boolean;

    constructor (
        private _versaoService: VersaoService
    ) { }

    async ngOnInit()
    {
        this.isLoading = true;
        const data = await this._versaoService
            .obterPorParametros({ 
                sortDirection: 'desc',
                sortActive: 'codSatVersao',
                pageSize: 10
            })
            .toPromise();

        this.versoes = data.items;
        this.isLoading = false;
    }
}