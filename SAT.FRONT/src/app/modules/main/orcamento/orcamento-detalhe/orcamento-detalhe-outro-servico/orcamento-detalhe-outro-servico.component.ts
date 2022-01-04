import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-orcamento-detalhe-outro-servico',
  templateUrl: './orcamento-detalhe-outro-servico.component.html',
  styles: [`
        .list-grid-servicos {
            grid-template-columns: auto 100px 100px 100px 100px;
            
            @screen sm {
                grid-template-columns: auto 100px 100px 100px 100px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheOutroServicoComponent implements OnInit
{
  codOrc: number;
  isLoading: boolean;
  @Input() orcamento: Orcamento;

  constructor () { }

  ngOnInit(): void { }
}