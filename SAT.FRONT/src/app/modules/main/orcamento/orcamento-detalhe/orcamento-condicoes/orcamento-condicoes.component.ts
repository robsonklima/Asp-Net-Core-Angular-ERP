import { Component, Input, LOCALE_ID, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-orcamento-condicoes',
  templateUrl: './orcamento-condicoes.component.html',
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoCondicoesComponent
{

  @Input() orcamento: Orcamento;

  constructor () { }
}