import { ChangeDetectorRef, Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoDeslocamento } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IEditableFuseCard } from 'app/shared/components/interfaces/ieditable-fuse-card';
import { isEqual } from 'lodash';

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

export class OrcamentoDetalheDeslocamentoComponent implements OnInit, IEditableFuseCard
{
  @Input() deslocamento: OrcamentoDeslocamento;
  oldItem: OrcamentoDeslocamento;
  userSession: UserSession
  isLoading: boolean;
  isEditing: boolean;

  constructor (private _cdRef: ChangeDetectorRef, private _userService: UserService) 
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  editar(): void
  {
    this.isEditing = true;
    this.oldItem = Object.assign({}, this.deslocamento);
  }

  salvar(): void
  {
    this.isEditing = false;
    this.isLoading = true;
    this.isLoading = false;
  }

  cancelar(): void
  {
    this.isEditing = false;
    this.deslocamento = Object.assign({}, this.oldItem);
    this._cdRef.detectChanges();
  }

  isEqual(): boolean
  {
    return isEqual(this.oldItem, this.deslocamento);
  }

  isInvalid(): boolean
  {
    if (this.deslocamento.quantidadeKm <= 0)
      return true;

    return false;
  }

  toNumber(value)
  {
    return +value;
  }

  ngOnInit(): void { }
}