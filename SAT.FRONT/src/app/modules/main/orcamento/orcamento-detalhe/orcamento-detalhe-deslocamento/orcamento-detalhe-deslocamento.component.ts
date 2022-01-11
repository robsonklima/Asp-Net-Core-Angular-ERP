import { ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoDeslocamento } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IEditableFuseCard } from 'app/core/base-components/interfaces/ieditable-fuse-card';
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

export class OrcamentoDetalheDeslocamentoComponent implements IEditableFuseCard
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
    this.deslocamento.quantidadeKm = parseFloat(this.deslocamento.quantidadeKm.toString().replace(',', '.'));

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
    return isEqual(this.oldItem?.quantidadeKm?.toString(), this.deslocamento?.quantidadeKm?.toString());
  }

  isInvalid(): boolean
  {
    if (!this.deslocamento || this.deslocamento?.quantidadeKm < 0 || !this.deslocamento?.quantidadeKm)
      return true;

    return false;
  }

  onkeydown()
  {
    document.getElementById("quantidadeKm").addEventListener("keydown", function (event)
    {
      const isLetter = (event.key >= "a" && event.key <= "z");

      if (isLetter)
        event.preventDefault();
    });
  }
}