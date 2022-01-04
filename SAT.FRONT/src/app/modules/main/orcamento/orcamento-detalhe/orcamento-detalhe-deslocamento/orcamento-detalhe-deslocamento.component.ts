import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-orcamento-detalhe-deslocamento',
  templateUrl: './orcamento-detalhe-deslocamento.component.html',
  styles: [`
        .list-grid-deslocamento {
            grid-template-columns: 100px 100px auto;
            
            @screen sm {
                grid-template-columns: 100px 100px auto;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheDeslocamentoComponent implements OnInit
{

  isLoading: boolean;
  @Input() orcamento: Orcamento;

  constructor () { }

  ngOnInit(): void { }
}