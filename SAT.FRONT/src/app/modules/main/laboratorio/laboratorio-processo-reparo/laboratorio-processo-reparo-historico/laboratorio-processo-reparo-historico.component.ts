import { AfterViewInit, Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { first } from 'rxjs/operators';
import { ORItem } from 'app/core/types/or-item.types';
import { ORItemService } from 'app/core/services/or-item.service';

@Component({
  selector: 'app-laboratorio-processo-reparo-historico',
  templateUrl: './laboratorio-processo-reparo-historico.component.html'
})
export class LaboratorioProcessoReparoHistoricoComponent implements AfterViewInit {
  isLoading: boolean = false;
  userSession: UsuarioSessao;
  codORItem: number;
  orItem: ORItem;
  form: FormGroup;
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _snack: CustomSnackbarService,
    private _orItemService: ORItemService,
    private _route: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _router: Router,
    private dialogRef: MatDialogRef<LaboratorioProcessoReparoHistoricoComponent>) {
      if (data)
      {
          this.orItem = data.item;
          console.log(this.orItem);
          console.log(data.item);
          
          
      }
  
      this.userSession = JSON.parse(this._userService.userSession);
    }

  async ngAfterViewInit() {
    console.log(this.orItem.codOR);
    
   }

  private registrarEmitters() { }
}