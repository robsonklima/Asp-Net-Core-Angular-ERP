import { Component } from '@angular/core';
import versoes from './data/versions.data.json';


@Component({
    selector: 'versoes',
    templateUrl: './versoes.component.html'
})
export class VersoesComponent {
    versoes: any[] = [];
    
    constructor () {
        this.versoes = versoes;
    }
}