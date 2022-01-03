import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-orcamento-detalhe-material',
  templateUrl: './orcamento-detalhe-material.component.html',
  styles: [`
        .list-grid-material {
            grid-template-columns: 75px 75px  auto 75px 75px 75px 75px 75px;
            
            @screen sm {
                grid-template-columns: 75px 75px auto 75px 75px 75px 75px 75px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoDetalheMaterialComponent implements OnInit
{

  codOrc: number;
  isLoading: boolean;
  @Input() orcamento: Orcamento;

  constructor () { }

  ngOnInit(): void { }

  excluirMaterial() { }
}