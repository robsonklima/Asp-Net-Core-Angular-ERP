import { Component, Input, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-ordem-servico-detalhe-orcamento',
  templateUrl: './ordem-servico-detalhe-orcamento.component.html',
  styles: [`
        .list-grid-orcamentos {
            grid-template-columns: 100px auto 100px 100px;
            
            @screen sm {
                grid-template-columns: 100px auto 100px 100px;
            }
        }
    `],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoDetalheOrcamentoComponent implements OnInit
{

  isLoading: boolean = false;
  @Input() orcamentos: Orcamento[] = [];

  constructor () { }

  ngOnInit(): void
  {
  }

}
