import { AfterViewInit, Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ORItem } from 'app/core/types/or-item.types';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORService } from 'app/core/services/or.service';
import { OR } from 'app/core/types/OR.types';
import { Filial } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';

@Component({
  selector: 'app-laboratorio-processo-reparo-historico',
  templateUrl: './laboratorio-processo-reparo-historico.component.html'
})
export class LaboratorioProcessoReparoHistoricoComponent implements AfterViewInit {
  isLoading: boolean = false;
  userSession: UsuarioSessao;
  codORItem: number;
  or: OR;
  orItem: ORItem;
  filial: Filial;
  form: FormGroup;
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _snack: CustomSnackbarService,
    private _orItemService: ORItemService,
    private _orService: ORService,
    private _filialService: FilialService,
    private _route: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _router: Router,
    private dialogRef: MatDialogRef<LaboratorioProcessoReparoHistoricoComponent>) {
      if (data)
      {
          this.orItem = data.item;
      }
  
      this.userSession = JSON.parse(this._userService.userSession);
    }

  async ngAfterViewInit() {
    this.or = await this._orService.obterPorCodigo(this.orItem.codOR).toPromise();
    this.filial = await this._filialService.obterPorCodigo(this.or.codOrigem).toPromise();
    console.log(this.or);
    console.log(this.orItem);
    
   }

  private registrarEmitters() { }
}