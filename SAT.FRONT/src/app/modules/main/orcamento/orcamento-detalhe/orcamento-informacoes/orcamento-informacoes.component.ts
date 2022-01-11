import { Component, Input, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Filial } from 'app/core/types/filial.types';

@Component({
  selector: 'app-orcamento-informacoes',
  templateUrl: './orcamento-informacoes.component.html',
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoInformacoesComponent implements OnInit
{

  @Input() filial: Filial;

  constructor () { }

  ngOnInit(): void
  {
  }

}
