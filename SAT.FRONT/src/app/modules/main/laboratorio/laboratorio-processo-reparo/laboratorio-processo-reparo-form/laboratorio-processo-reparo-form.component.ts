import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ORService } from 'app/core/services/or.service';
import { ORItem } from 'app/core/types/or-item.types';
import { OR } from 'app/core/types/OR.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { LaboratorioProcessoReparoListaComponent } from '../laboratorio-processo-reparo-lista/laboratorio-processo-reparo-lista.component';

@Component({
  selector: 'app-laboratorio-processo-reparo-form',
  templateUrl: './laboratorio-processo-reparo-form.component.html'
})
export class LaboratorioProcessoReparoFormComponent implements OnInit {
  item: ORItem;
  or: OR;
  userSession: UserSession;
  form: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    public _dialogRef: MatDialogRef<LaboratorioProcessoReparoListaComponent>,
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _orService: ORService
  ) {
    this.item = data?.item;
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.criarForm();
  }

  criarForm() {
    this.form = this._formBuilder.group({
      
    });
  }

  salvar() {
    
  }

  fechar() {
    this._dialogRef.close();
  }
}
