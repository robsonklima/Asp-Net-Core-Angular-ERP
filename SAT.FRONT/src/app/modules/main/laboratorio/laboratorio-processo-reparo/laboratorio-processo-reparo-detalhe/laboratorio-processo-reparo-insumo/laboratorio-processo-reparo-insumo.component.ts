import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ORItemInsumoService } from 'app/core/services/or-item-insumo.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORItemInsumo } from 'app/core/types/or-item-insumo.types';
import { ORItem } from 'app/core/types/or-item.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { Peca } from 'app/core/types/peca.types';
import { LaboratorioProcessoReparoInsumoDialogComponent } from './laboratorio-processo-reparo-insumo-dialog/laboratorio-processo-reparo-insumo-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-laboratorio-processo-reparo-insumo',
  templateUrl: './laboratorio-processo-reparo-insumo.component.html'
})
export class LaboratorioProcessoReparoInsumoComponent implements OnInit {
  @Input() codORItem: number;
  usuarioSessao: UsuarioSessao;
  orItem: ORItem;
  orItemInsumos: ORItemInsumo[] = [];
  pecas: Peca[] = [];
  protected _onDestroy = new Subject<void>();
  loading: boolean = true;
  form: FormGroup;
  public pecasFiltro: FormControl = new FormControl();

  constructor(
    private _userService: UserService,
    private _orItemService: ORItemService,
    private _orItemInsumoService: ORItemInsumoService,
    private _dialog: MatDialog,
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.orItem = await this._orItemService.obterPorCodigo(this.codORItem).toPromise();
    await this.obterInsumos();

    this.loading = false;
  }

  private async obterInsumos() {
    this.orItemInsumos = (await this._orItemInsumoService.obterPorParametros({
      sortDirection: 'asc',
      pageSize: 100,
      codORItem: this.codORItem
    }).toPromise()).items;
  }

  adicionarInsumo(){
      const dialogRef = this._dialog.open(LaboratorioProcessoReparoInsumoDialogComponent, {
          data: { orItem: this.orItem }
      });

      dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
          if (confirmacao) {
              this.obterInsumos();
          }
      });
  }

}
