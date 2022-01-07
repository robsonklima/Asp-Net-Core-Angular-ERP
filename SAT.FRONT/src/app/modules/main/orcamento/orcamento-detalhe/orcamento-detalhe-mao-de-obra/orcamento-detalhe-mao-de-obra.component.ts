import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoMaoDeObra } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IEditableFuseCard } from 'app/shared/components/interfaces/ieditable-fuse-card';
import { isEqual } from 'lodash';

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

export class OrcamentoDetalheMaoDeObraComponent implements AfterViewInit, IEditableFuseCard
{

  @Input() maoDeObra: OrcamentoMaoDeObra;
  oldItem: OrcamentoMaoDeObra;
  userSession: UserSession
  isLoading: boolean;
  isEditing: boolean;

  constructor (private _cdRef: ChangeDetectorRef, private _userService: UserService) 
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }
  ngAfterViewInit(): void
  {
    this.onkeydown();
  }

  editar(): void
  {
    this.isEditing = true;
    this.oldItem = Object.assign({}, this.maoDeObra);
  }

  salvar(): void
  {
    this.maoDeObra.previsaoHoras = parseFloat(this.maoDeObra.previsaoHoras.toString().replace(',', '.'));
    this.isEditing = false;
    this.isLoading = true;
    this.isLoading = false;
  }

  cancelar(): void
  {
    this.isEditing = false;
    this.maoDeObra = Object.assign({}, this.oldItem);
    this._cdRef.detectChanges();
  }

  isEqual(): boolean
  {
    return isEqual(this.oldItem.previsaoHoras.toString(), this.maoDeObra.previsaoHoras.toString());
  }

  isInvalid(): boolean
  {
    if (this.maoDeObra.previsaoHoras < 0 || this.maoDeObra.previsaoHoras == null)
      return true;

    return false;
  }


  onkeydown()
  {
    document.getElementById("previsaoHoras").addEventListener("keydown", function (event)
    {
      const isLetter = (event.key >= "a" && event.key <= "z");

      if (isLetter)
        event.preventDefault();
    });
  }
}