import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IEditableFuseCard } from 'app/core/base-components/interfaces/ieditable-fuse-card';
import { isEqual } from 'lodash';
import { OrcamentoMaoDeObraService } from 'app/core/services/orcamento-mao-de-obra.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrcamentoMaoDeObra } from 'app/core/types/orcamento-mao-de-obra.types';

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

  constructor (private _cdRef: ChangeDetectorRef,
    private _userService: UserService,
    private _maoDeObraService: OrcamentoMaoDeObraService,
    private _orcService: OrcamentoService) 
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
    this.calcularMaoDeObra();

    this._maoDeObraService.atualizar(this.maoDeObra).subscribe(m =>
    {
      this.maoDeObra = m;
      this._orcService.atualizarTotalizacao(m.codOrc);
      this.oldItem = Object.assign({}, this.maoDeObra);
    });

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

  calcularMaoDeObra()
  {
    this.maoDeObra.previsaoHoras = parseFloat(this.maoDeObra.previsaoHoras.toString().replace(',', '.'));
    this.maoDeObra.valorTotal = this.maoDeObra.previsaoHoras * this.maoDeObra.valorHoraTecnica;
  }

  isEqual(): boolean
  {
    return isEqual(this.oldItem?.previsaoHoras?.toString(), this.maoDeObra?.previsaoHoras?.toString());
  }

  isInvalid(): boolean
  {
    if (!this.maoDeObra || this.maoDeObra?.previsaoHoras < 0 || !this.maoDeObra?.previsaoHoras)
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