import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-orcamento-detalhe-mao-de-obra',
  templateUrl: './orcamento-detalhe-mao-de-obra.component.html',
  styles: [`
        .list-grid-mao-de-obra {
            grid-template-columns: 150px auto 150px;
            
            @screen sm {
                grid-template-columns: 150px auto 150px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheMaoDeObraComponent implements OnInit
{

  codOrc: number;
  isLoading: boolean;
  @Input() orcamento: Orcamento;

  constructor () { }

  ngOnInit(): void { }
}